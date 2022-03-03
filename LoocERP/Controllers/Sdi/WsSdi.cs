using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace LoocERP.Controllers.Sdi
{
    public class WsSdi : Controller
    {

        private readonly string token;

        private IConfiguration _config;




        public WsSdi() {

            var config  = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                .AddJsonFile("appsettings.json",true).Build();

            _config     = config.GetSection("WS_SDI");
            token       = login();
        }


        private string login()
        {

            #if DEBUG
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            #endif

            string url  = $"{_config.GetValue<string>("url")}/api/user/token";
            var client  = new RestClient(url);
            var request = new RestRequest(Method.POST);

            request.RequestFormat = DataFormat.Json;

            JObject jsonReq = new JObject();


            jsonReq.Add("Email", _config.GetValue<string>("email"));
            jsonReq.Add("Password", _config.GetValue<string>("pwd"));


            request.AddParameter("text/json", jsonReq.ToString(), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            dynamic res = JObject.Parse(response.Content);

            if ( res.result.isAuthenticated == false) {
                throw new Exception("Errore di autenticazione");
            }

            return res.result.token;

        }


        public string getPdf (string filename){

            
            #if DEBUG
             ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            #endif

            var res = string.Empty;
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                using (var httpClient = new HttpClient(clientHandler))
                {
                    var parameters = new
                    {
                        filename = filename,
                        idclient = "1",
                        includePdf = "true"
                    };

                    string requestBody = JsonConvert.SerializeObject(parameters);

                    var requestMessage = new HttpRequestMessage()
                    {
                        Method      = new HttpMethod("GET"),
                        RequestUri  = new Uri($"{_config.GetValue<string>("url")}/api/invoice/getByFileName"),
                        Content     = new StringContent(requestBody, Encoding.UTF8, "application/json")

                    };
                    requestMessage.Headers.Add("Authorization", $"Bearer {token}");
                    var response = httpClient.SendAsync(requestMessage).Result;
                    

                    if (response != null) {

                        var responseContent = response.Content;
                        var responseBody    = responseContent.ReadAsStringAsync().Result;
                        var httpResponse    = JObject.Parse(responseBody);
                        response.EnsureSuccessStatusCode();
                        return JsonConvert.SerializeObject(new { result = "OK", base64 = (string)httpResponse["result"]["pdfFile"] });
                    } else {
                        return JsonConvert.SerializeObject(new { result = "KO" });
                    }
                    
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception("Errore nella chiamata WS");
            }

        }
    }
}