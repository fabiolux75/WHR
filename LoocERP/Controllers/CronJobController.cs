using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LoocERP.Models;
using LoocERP.ViewModels;
using LoocERP.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using System.Globalization;


namespace LoocERP.Controllers
{
    public class CronJobController : Controller
    {
        /// <summary>
        /// WorkShifts <c>ajaxIndex</c> 
        /// Lista turni di lavoro
        /// </summary>
        /// <param>active</param> Filtra le aziende per livello   
        [AllowAnonymous]      
        public String checkAutoChiusura()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "10.60.1.12";
                builder.UserID = "sa";
                builder.Password = "4PCgKYB3yyj5hE78";
                builder.InitialCatalog = "CDI_TEST";
                string json = ""; //json da ritornare

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    String sql = @"select

                                        cts.Id,		
	                                    cts.CodiceOperatore,
	                                    crtu.WorkDate,
	                                    cturni.OraInizio,
	                                    cturni.OraFine,
	                                    cts.OraLogin,
	                                    cts.OraLogout,
	                                    cts.DataLogout,
	                                    cturni.oraAutoChiusura,
	                                    OreGalleria = CONVERT(VARCHAR, DATEADD(HOUR, SUM(cts.OreGalleria), 0), 108),
	                                    CAST(CONVERT(varchar(12),
		                                    DATEADD(minute,
			                                    SUM(DATEDIFF(minute,
				                                    DATEADD(millisecond ,DATEDIFF(millisecond, 0, OraLogin) ,DataLogin),
				                                    DATEADD(millisecond ,DATEDIFF(millisecond, 0, OraLogout) ,DataLogout)
			                                    ))
		                                    , 0), 114) as time(7)) as OreEffettive, 		--OreEffettive De Magistris
	                                    cts.idDevice,cts.Stato,cts.Evento,cts.MultiTenantId,crtu.TurnoId

                                    from C_TimeSheets cts
                                        join C_Rel_TurniUsers crtu
                                            on crtu.UserId = cts.CodiceOperatore and crtu.WorkDate = cts.DataLogin
                                        join C_Turni cturni
                                            on cturni.Id = crtu.TurnoId

                                    where
                                        cts.Evento = 'Login' and
                                        cts.DataLogin = crtu.WorkDate and
                                        cts.MultiTenantId = crtu.MultiTenantId and
                                        cturni.MultiTenantId = crtu.MultiTenantId and
                                        (DATEADD(millisecond, (DATEDIFF(millisecond, 0, cturni.OraFine) + (cturni.oraAutoChiusura * 3600000)), crtu.WorkDate) <= GETDATE())

                                    group by
                                        cts.Id,		
	                                    cts.CodiceOperatore,
	                                    crtu.WorkDate,
	                                    cturni.OraInizio,
	                                    cturni.OraFine,
	                                    cts.OraLogin,
	                                    cts.OraLogout,
                                    	cts.DataLogout,
	                                    cturni.oraAutoChiusura,
	                                    OreGalleria,
	                                    OreEffettive,
	                                    crtu.TurnoId,cts.DataLogin,cts.OraLogin,cts.idDevice,cts.Stato,cts.Evento,cts.MultiTenantId

                                    order by
                                        cts.Id"; // OreGalleria da sistemare, dal valore decimale vengono convertite solo le ore e non i minuti.

                    List<Dictionary<string, string>> values = new List<Dictionary<string, string>>();

                    using (SqlCommand command = new SqlCommand(sql, connection)) //sequenza per aggiornare DataLogout ed OraLogout dove sono NULL
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            do
                            {
                                while (reader.Read())
                                {
                                    var fieldValues = new Dictionary<string, string>();
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        fieldValues.Add(reader.GetName(i), reader[i].ToString());
                                    }
                                    values.Add(fieldValues);
                                }
                            } while (reader.NextResult()); //popolo la lista di dizionari

                            reader.Close();

                            foreach (var obj in values) //ciclo la lista di dizionari
                            {
                                string today = DateTime.Today.ToString();
                                string now = DateTime.Now.ToString("HH:mm:ss");

                                string currentDataLogout = "";
                                string currentId = "";
                                string currentCodiceOperatore = "";
                                string currentWorkDate = "";
                                string currentTurnoId = "";
                                string currentEvento = "";

                                obj.TryGetValue("DataLogout", out currentDataLogout);
                                obj.TryGetValue("Id", out currentId);
                                obj.TryGetValue("CodiceOperatore", out currentCodiceOperatore);
                                obj.TryGetValue("WorkDate", out currentWorkDate);
                                obj.TryGetValue("TurnoId", out currentTurnoId);
                                obj.TryGetValue("Evento", out currentEvento);

                                if (currentDataLogout == "") // Se DataLogout è NULL, aggiorno a NOW filtrando per Id
                                {
                                    //Console.WriteLine("Date NULL");
                                    //Console.WriteLine("Id: " + currentId);
                                    //Console.WriteLine("Today: " + DateTime.Today); //ok 17/03/2021 00:00:00
                                    //Console.WriteLine("Now: " + DateTime.Now.ToString("HH:mm:ss")); //ok oralogout 17/03/2021 12:01:13 -> 12:01:13

                                    String sqlUpdateDataLogout = @"UPDATE C_TimeSheets "
                                                                    + "SET DataLogout='" + today + "', OraLogout='" + now + "', Evento='Logout' "
                                                                    + "WHERE Id='" + currentId + "'";

                                    SqlCommand commandUpdate = new SqlCommand(sqlUpdateDataLogout, connection);
                                    commandUpdate.ExecuteScalar();
                                }

                            }//fine ciclo dizionari

                        }//fine reader

                 

                        using (SqlDataReader reader = command.ExecuteReader()) //sequenza per Inserire o Aggiornare le righe di TimeSheetDailyReport
                        {
                            do
                            {
                                while (reader.Read())
                                {
                                    var fieldValues = new Dictionary<string, string>();
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        fieldValues.Add(reader.GetName(i), reader[i].ToString());
                                    }
                                    values.Add(fieldValues);
                                }
                            } while (reader.NextResult()); //popolo la lista di dizionari

                            reader.Close();

                            json = JsonConvert.SerializeObject(values);

                            foreach (var obj in values) //ciclo la lista di dizionari
                            {
                                string today = DateTime.Today.ToString();
                                string now = DateTime.Now.ToString("HH:mm:ss");

                                string currentCodiceOperatore = "";
                                string currentWorkDate = "";
                                string currentTurnoId = "";
                                string OreEffettive = "";
                                string OreGalleria = "";
                                string MultiTenantId = "";
                                string currentId = "";

                                obj.TryGetValue("CodiceOperatore", out currentCodiceOperatore);
                                obj.TryGetValue("WorkDate", out currentWorkDate);
                                obj.TryGetValue("TurnoId", out currentTurnoId);
                                obj.TryGetValue("OreEffettive", out OreEffettive);
                                obj.TryGetValue("OreGalleria", out OreGalleria);
                                obj.TryGetValue("MultiTenantId", out MultiTenantId);
                                obj.TryGetValue("Id", out currentId);

                                decimal decOreEffettive = Convert.ToDecimal(TimeSpan.Parse(OreEffettive).TotalHours);
                                if (OreGalleria == "")
                                {
                                    OreGalleria = "00:00:00";
                                }
                                decimal decOreGalleria = Convert.ToDecimal(TimeSpan.Parse(OreGalleria).TotalHours);

                                //Console.WriteLine("OreEffettive: " + OreEffettive);
                                //Console.WriteLine("OreGalleria: " + OreGalleria);
                                //Console.WriteLine("decOreEffettive: " + decOreEffettive);
                                //Console.WriteLine("decOreGalleria: " + decOreGalleria);

                                string decOreEffettiveWithDot = decOreEffettive.ToString().Replace(",", ".");
                                string decOreGalleriaWithDot = decOreGalleria.ToString().Replace(",", ".");

                                //Console.WriteLine("decOreEffettiveOk: " + decOreEffettiveWithDot);
                                //Console.WriteLine("decOreGalleriaOk: " + decOreGalleriaWithDot);

                                //Controllo l'esistenza dell'elemento corrente in TimeSheetDailyReport
                                //----------------currentWorkDate DA SOSTITUIRE con today, se si vuole selezionare sulla base del giorno attuale e non del giorno di lavoro già presente nel TimeSheet-----------
                                String sqlCheckExistTSDR = @"select *
                                                                from C_TimeSheetsDailyReport
                                                                where WorkDate = '" + currentWorkDate + "'and UserId = '" + currentCodiceOperatore + "' and TurnoId = '" + currentTurnoId +
                                                                "' order by WorkDate desc";

                                SqlCommand commandCheckExistTSDR = new SqlCommand(sqlCheckExistTSDR, connection);
                                dynamic resultId = commandCheckExistTSDR.ExecuteScalar(); //ritorna l'ID della riga di TimeSheetDailyReport

                                if (resultId == null)     //INSERT dell'elemento corrente in TimeSheetDailyReport
                                {

                                    String sqlUpdateDataLogout = @"UPDATE C_TimeSheets "
                                                                    + "SET Evento='Logout' "
                                                                    + "WHERE Id='" + currentId + "'";

                                    SqlCommand commandUpdate = new SqlCommand(sqlUpdateDataLogout, connection);
                                    commandUpdate.ExecuteScalar();

                                    String sqlInsertTSDR = @"INSERT INTO C_TimeSheetsDailyReport
                                                                       ([Id]
                                                                       ,[WorkDate]
                                                                       ,[UserId]
                                                                       ,[TurnoId]
                                                                       ,[OreEffettive]
                                                                       ,[Ore]
                                                                       ,[OreGalleria]
                                                                       ,[MultiTenantId]
                                                                       ,[EffectiveHour]
                                                                       ,[Hour]
                                                                       ,[GalleryHour]
                                                                       ,[isAutoClosed])
                                                                VALUES (
                                                                        NEWID() " +
                                                                       ",'" + currentWorkDate + "'" +
                                                                       ",'" + currentCodiceOperatore + "'" +
                                                                       ",'" + currentTurnoId + "'" +
                                                                       "," + decOreEffettiveWithDot + "" +
                                                                       ",8" +
                                                                       "," + decOreGalleriaWithDot + "" +
                                                                       ",'" + MultiTenantId + "'" +
                                                                       ",'" + OreEffettive + "'" +
                                                                       ",'08:00:00'" +
                                                                       ",'" + OreGalleria + "'" +
                                                                       ",1)"; // controllare su CDI_TEST colonna [FK_C_TimeSheetsDailyReport_C_Giustificativi_GiustificativoId], non presente su CDI

                                    SqlCommand commandInsertTSDR = new SqlCommand(sqlInsertTSDR, connection);
                                    commandInsertTSDR.ExecuteScalar(); //ritorna l'ID della riga di TimeSheetDailyReport
                                }
                                else    //UPDATE dell'elemento corrente in TimeSheetDailyReport
                                {
                                    String sqlUpdateDataLogout = @"UPDATE C_TimeSheets "
                                                                    + "SET Evento='Logout' "
                                                                    + "WHERE Id='" + currentId + "'";

                                    SqlCommand commandUpdate = new SqlCommand(sqlUpdateDataLogout, connection);
                                    commandUpdate.ExecuteScalar();

                                    String sqlUpdateTSDR = @"UPDATE [C_TimeSheetsDailyReport]
                                                               SET [OreEffettive] = '" + decOreEffettiveWithDot + "'" +
                                                                  ",[Ore] = '8'" +
                                                                  ",[OreGalleria] = '" + decOreGalleriaWithDot + "'" +
                                                                  ",[EffectiveHour] = '" + OreEffettive + "'" +
                                                                  ",[Hour] = '08:00:00'" +
                                                                  ",[GalleryHour] = '" + OreGalleria + "'" +
                                                                  ",[isAutoClosed] = 1 " +
                                                                  "WHERE Id = '" + resultId + "'"; //aggiungere flag isAutoClosed su CDI

                                    SqlCommand commandUpdateTSDR = new SqlCommand(sqlUpdateTSDR, connection);
                                    commandUpdateTSDR.ExecuteScalar(); //ritorna l'ID della riga di TimeSheetDailyReport                                    
                                }

                            }//fine ciclo dizionari

                        }//fine secondo reader

                    }
                }
                return json;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine();
            return null;

        }

    }

}