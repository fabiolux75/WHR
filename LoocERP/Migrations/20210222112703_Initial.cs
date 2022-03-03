using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoocERP.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "C_Claims",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_Claims", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "C_Multitenant",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    LogoUrl = table.Column<string>(maxLength: 200, nullable: true),
                    LoocId = table.Column<string>(maxLength: 50, nullable: true),
                    slughost = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_Multitenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idDevice = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CodiceVettore = table.Column<string>(nullable: true),
                    CodiceCliente = table.Column<string>(nullable: true),
                    CodiceAutista = table.Column<string>(nullable: true),
                    StatoDevice = table.Column<string>(nullable: false),
                    DescStatoDevice = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RemoteSetup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDevice = table.Column<int>(nullable: false),
                    ImeiDevice = table.Column<string>(nullable: true),
                    SetupType = table.Column<int>(nullable: true),
                    CodiceAutista = table.Column<string>(nullable: true),
                    NomeAutista = table.Column<string>(nullable: true),
                    CodiceVettore = table.Column<string>(nullable: true),
                    Targa = table.Column<string>(nullable: true),
                    ModelloVettore = table.Column<string>(nullable: true),
                    CodiceAzienda = table.Column<string>(nullable: true),
                    ConnectionType = table.Column<string>(nullable: true),
                    UriTest = table.Column<string>(nullable: true),
                    UriProd = table.Column<string>(nullable: true),
                    Stato = table.Column<bool>(nullable: true),
                    RequestStatus = table.Column<string>(nullable: true),
                    DeviceModel = table.Column<string>(nullable: true),
                    CodiceCliente = table.Column<string>(nullable: true),
                    DataRichiesta = table.Column<DateTime>(nullable: true),
                    DataLastConnection = table.Column<DateTime>(nullable: true),
                    RequestLatitude = table.Column<string>(nullable: true),
                    RequestLongitude = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    StatoAccount = table.Column<string>(nullable: true),
                    TimeoutAccount = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Provenienza = table.Column<string>(nullable: true),
                    SimSerialNumber = table.Column<string>(nullable: true),
                    TelephoneNumber = table.Column<string>(nullable: true),
                    AlertMessage = table.Column<string>(nullable: true),
                    PrivacyFlag = table.Column<bool>(nullable: true),
                    LogoAz = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemoteSetup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "C_ANA_Companies",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    InternalCode = table.Column<string>(maxLength: 200, nullable: true),
                    RagioneSociale = table.Column<string>(maxLength: 200, nullable: false),
                    PIva = table.Column<string>(maxLength: 20, nullable: false),
                    FiscalCode = table.Column<string>(maxLength: 20, nullable: true),
                    EmailPec = table.Column<string>(maxLength: 50, nullable: true),
                    CodiceSdi = table.Column<string>(maxLength: 20, nullable: true),
                    Nazione = table.Column<string>(maxLength: 200, nullable: true),
                    Regione = table.Column<string>(maxLength: 200, nullable: true),
                    Provincia = table.Column<string>(maxLength: 200, nullable: true),
                    Citta = table.Column<string>(maxLength: 200, nullable: true),
                    Indirizzo = table.Column<string>(maxLength: 200, nullable: true),
                    EmailAziendale = table.Column<string>(maxLength: 50, nullable: true),
                    SitoWeb = table.Column<string>(maxLength: 200, nullable: true),
                    Telefono = table.Column<string>(maxLength: 50, nullable: true),
                    Fax = table.Column<string>(maxLength: 50, nullable: true),
                    isSupplier = table.Column<bool>(nullable: true),
                    isCustomer = table.Column<bool>(nullable: true),
                    isOfficina = table.Column<bool>(nullable: true),
                    isExternal = table.Column<bool>(nullable: true),
                    ParentID = table.Column<Guid>(nullable: true),
                    Img = table.Column<byte[]>(nullable: true),
                    active = table.Column<bool>(nullable: false),
                    MultiTenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_ANA_Companies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_C_ANA_Companies_C_Multitenant_MultiTenantId",
                        column: x => x.MultiTenantId,
                        principalTable: "C_Multitenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_ANA_Companies_C_ANA_Companies_ParentID",
                        column: x => x.ParentID,
                        principalTable: "C_ANA_Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "C_Domains",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Tipo = table.Column<string>(maxLength: 200, nullable: false),
                    Code = table.Column<string>(maxLength: 200, nullable: true),
                    ParentId = table.Column<Guid>(nullable: true),
                    MultiTenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_Domains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_C_Domains_C_Multitenant_MultiTenantId",
                        column: x => x.MultiTenantId,
                        principalTable: "C_Multitenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_Domains_C_Multitenant_ParentId",
                        column: x => x.ParentId,
                        principalTable: "C_Multitenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "C_Giustificativi",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    FonteNormativa = table.Column<string>(maxLength: 4000, nullable: true),
                    Destinatari = table.Column<string>(maxLength: 4000, nullable: true),
                    ModalitaDiImputazione = table.Column<string>(maxLength: 4000, nullable: true),
                    AdempimentiCaricoInteressato = table.Column<string>(maxLength: 4000, nullable: true),
                    AdempimentiResponsabileUOG = table.Column<string>(maxLength: 4000, nullable: true),
                    AdempimentiResponsabilePersonale = table.Column<string>(maxLength: 4000, nullable: true),
                    MultiTenantId = table.Column<Guid>(nullable: true),
                    Active = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_Giustificativi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_C_Giustificativi_C_Multitenant_MultiTenantId",
                        column: x => x.MultiTenantId,
                        principalTable: "C_Multitenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "C_Mansioni",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Codice = table.Column<string>(maxLength: 200, nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Descrizione = table.Column<string>(maxLength: 200, nullable: true),
                    isAssignedAsDefault = table.Column<int>(maxLength: 200, nullable: true),
                    isEnabledManageHour = table.Column<int>(nullable: true),
                    MultiTenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_Mansioni", x => x.ID);
                    table.ForeignKey(
                        name: "FK_C_Mansioni_C_Multitenant_MultiTenantId",
                        column: x => x.MultiTenantId,
                        principalTable: "C_Multitenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "C_Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    Codice = table.Column<string>(maxLength: 200, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Active = table.Column<int>(nullable: true),
                    MultiTenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_C_Projects_C_Multitenant_MultiTenantId",
                        column: x => x.MultiTenantId,
                        principalTable: "C_Multitenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "C_Specializzazioni",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Codice = table.Column<string>(maxLength: 200, nullable: true),
                    Descrizione = table.Column<string>(maxLength: 2000, nullable: true),
                    MultiTenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_Specializzazioni", x => x.ID);
                    table.ForeignKey(
                        name: "FK_C_Specializzazioni_C_Multitenant_MultiTenantId",
                        column: x => x.MultiTenantId,
                        principalTable: "C_Multitenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "C_TimeSheets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeOperatore = table.Column<string>(nullable: true),
                    CodiceOperatore = table.Column<string>(nullable: true),
                    DataLogin = table.Column<DateTime>(nullable: true),
                    OraLogin = table.Column<TimeSpan>(nullable: true),
                    DataLogout = table.Column<DateTime>(nullable: true),
                    OraLogout = table.Column<TimeSpan>(nullable: true),
                    IdDevice = table.Column<int>(nullable: true),
                    CodiceVettore = table.Column<string>(nullable: true),
                    TargaVettore = table.Column<string>(nullable: true),
                    CodiceAzienda = table.Column<string>(nullable: true),
                    Evento = table.Column<string>(nullable: true),
                    Stato = table.Column<string>(nullable: true),
                    CodiceCantiere = table.Column<string>(nullable: true),
                    Latitude = table.Column<decimal>(nullable: true),
                    Longitude = table.Column<decimal>(nullable: true),
                    MultiTenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_TimeSheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_C_TimeSheets_C_Multitenant_MultiTenantId",
                        column: x => x.MultiTenantId,
                        principalTable: "C_Multitenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    InternalCode = table.Column<string>(maxLength: 50, nullable: true),
                    FirstName = table.Column<string>(maxLength: 200, nullable: false),
                    LastName = table.Column<string>(maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: true),
                    isEmployee = table.Column<int>(nullable: true),
                    isExternal = table.Column<int>(nullable: true),
                    ProfilePicture = table.Column<byte[]>(nullable: true),
                    IDCompany = table.Column<Guid>(nullable: false),
                    active = table.Column<int>(nullable: true),
                    Gender = table.Column<int>(nullable: true),
                    CodiceFiscale = table.Column<string>(maxLength: 200, nullable: true),
                    NfcCode = table.Column<string>(maxLength: 200, nullable: true),
                    CodiceCartaCarb = table.Column<string>(maxLength: 200, nullable: true),
                    Nazione = table.Column<string>(maxLength: 200, nullable: true),
                    Regione = table.Column<string>(maxLength: 200, nullable: true),
                    Provincia = table.Column<string>(maxLength: 200, nullable: true),
                    Citta = table.Column<string>(maxLength: 200, nullable: true),
                    Cap = table.Column<string>(maxLength: 50, nullable: true),
                    Indirizzo = table.Column<string>(maxLength: 200, nullable: true),
                    TipoPatente = table.Column<string>(maxLength: 200, nullable: true),
                    NumeroPatente = table.Column<string>(maxLength: 200, nullable: true),
                    DataScadenzaPatente = table.Column<DateTime>(maxLength: 200, nullable: true),
                    DataCreazione = table.Column<DateTime>(nullable: true),
                    IDCreatoDa = table.Column<string>(nullable: true),
                    DataModifica = table.Column<DateTime>(nullable: true),
                    IDModificatoDa = table.Column<string>(nullable: true),
                    ModificatoDaId = table.Column<string>(nullable: true),
                    DataEliminazione = table.Column<DateTime>(nullable: true),
                    IDEliminatoDa = table.Column<string>(nullable: true),
                    IBAN = table.Column<string>(nullable: true),
                    matricola = table.Column<string>(nullable: true),
                    Note = table.Column<string>(maxLength: 2000, nullable: true),
                    StimaOraria = table.Column<decimal>(nullable: true),
                    StimaOrariaStraordinaria = table.Column<decimal>(nullable: true),
                    StimaOrariaGalleria = table.Column<decimal>(nullable: true),
                    StimaOrariaNotturna = table.Column<decimal>(nullable: true),
                    MultiTenantId = table.Column<Guid>(nullable: true),
                    InternalCompanyReferenceId = table.Column<Guid>(nullable: true),
                    ComuneNascita = table.Column<string>(maxLength: 100, nullable: true),
                    DataNascita = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_C_ANA_Companies_IDCompany",
                        column: x => x.IDCompany,
                        principalTable: "C_ANA_Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_C_ANA_Companies_InternalCompanyReferenceId",
                        column: x => x.InternalCompanyReferenceId,
                        principalTable: "C_ANA_Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_ModificatoDaId",
                        column: x => x.ModificatoDaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_C_Multitenant_MultiTenantId",
                        column: x => x.MultiTenantId,
                        principalTable: "C_Multitenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "C_MansioneMacchina",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Codice = table.Column<string>(maxLength: 200, nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    MansioneId = table.Column<Guid>(nullable: false),
                    MultiTenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_MansioneMacchina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_C_MansioneMacchina_C_Mansioni_MansioneId",
                        column: x => x.MansioneId,
                        principalTable: "C_Mansioni",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_C_MansioneMacchina_C_Multitenant_MultiTenantId",
                        column: x => x.MultiTenantId,
                        principalTable: "C_Multitenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "C_Cantieri",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    Codice = table.Column<string>(maxLength: 200, nullable: false),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    Round = table.Column<string>(nullable: true),
                    Nazione = table.Column<string>(maxLength: 200, nullable: true),
                    Regione = table.Column<string>(maxLength: 200, nullable: true),
                    Provincia = table.Column<string>(maxLength: 200, nullable: true),
                    Citta = table.Column<string>(maxLength: 200, nullable: true),
                    Indirizzo = table.Column<string>(maxLength: 200, nullable: true),
                    ProjectId = table.Column<Guid>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    CapocantiereId = table.Column<Guid>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    MultiTenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_Cantieri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_C_Cantieri_C_Multitenant_MultiTenantId",
                        column: x => x.MultiTenantId,
                        principalTable: "C_Multitenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_Cantieri_C_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "C_Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "C_Rel_FabbisognoSicurezza",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CantiereId = table.Column<Guid>(nullable: false),
                    SpecializzazioneId = table.Column<Guid>(nullable: true),
                    SpecializzazioneDesc = table.Column<string>(nullable: true),
                    Quantita = table.Column<int>(nullable: true),
                    TurnoId = table.Column<Guid>(nullable: true),
                    MultiTenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_Rel_FabbisognoSicurezza", x => x.Id);
                    table.ForeignKey(
                        name: "FK_C_Rel_FabbisognoSicurezza_C_Multitenant_MultiTenantId",
                        column: x => x.MultiTenantId,
                        principalTable: "C_Multitenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_Rel_FabbisognoSicurezza_C_Specializzazioni_SpecializzazioneId",
                        column: x => x.SpecializzazioneId,
                        principalTable: "C_Specializzazioni",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "C_ContractUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 200, nullable: true),
                    LevelId = table.Column<Guid>(nullable: true),
                    RetribuzioneTypeId = table.Column<Guid>(nullable: true),
                    OreDiLavoroTypeId = table.Column<Guid>(nullable: true),
                    ContractTypeId = table.Column<Guid>(nullable: true),
                    LawNumberTypeId = table.Column<Guid>(nullable: true),
                    CategoryCodeId = table.Column<Guid>(nullable: true),
                    FineRapportoId = table.Column<Guid>(nullable: true),
                    ValidFrom = table.Column<DateTime>(nullable: true),
                    ValidTo = table.Column<DateTime>(nullable: true),
                    Stato = table.Column<int>(nullable: true),
                    FineContrattoNota = table.Column<string>(maxLength: 2000, nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    MultiTenantId = table.Column<Guid>(nullable: true),
                    FileName = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_ContractUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_C_ContractUser_C_Domains_CategoryCodeId",
                        column: x => x.CategoryCodeId,
                        principalTable: "C_Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_ContractUser_C_Domains_ContractTypeId",
                        column: x => x.ContractTypeId,
                        principalTable: "C_Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_ContractUser_C_Domains_FineRapportoId",
                        column: x => x.FineRapportoId,
                        principalTable: "C_Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_ContractUser_C_Domains_LawNumberTypeId",
                        column: x => x.LawNumberTypeId,
                        principalTable: "C_Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_ContractUser_C_Domains_LevelId",
                        column: x => x.LevelId,
                        principalTable: "C_Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_ContractUser_C_Multitenant_MultiTenantId",
                        column: x => x.MultiTenantId,
                        principalTable: "C_Multitenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_ContractUser_C_Domains_OreDiLavoroTypeId",
                        column: x => x.OreDiLavoroTypeId,
                        principalTable: "C_Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_ContractUser_C_Domains_RetribuzioneTypeId",
                        column: x => x.RetribuzioneTypeId,
                        principalTable: "C_Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_ContractUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "C_Documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DocumentGroup = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Numero = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    Details = table.Column<string>(maxLength: 200, nullable: true),
                    DocumentTypeId = table.Column<Guid>(nullable: true),
                    ValidFrom = table.Column<DateTime>(nullable: true),
                    ValidTo = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    MultiTenantId = table.Column<Guid>(nullable: true),
                    FileName = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_C_Documents_C_Domains_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "C_Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_Documents_C_Multitenant_MultiTenantId",
                        column: x => x.MultiTenantId,
                        principalTable: "C_Multitenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_Documents_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "C_MalattiaUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Tipologia = table.Column<int>(nullable: false),
                    TipoEvento = table.Column<int>(nullable: false),
                    ValidFrom = table.Column<DateTime>(nullable: true),
                    ValidTo = table.Column<DateTime>(nullable: true),
                    Certificato = table.Column<string>(maxLength: 200, nullable: true),
                    CertificatoDiRiferimento = table.Column<string>(maxLength: 200, nullable: true),
                    DataRilascioCertificato = table.Column<DateTime>(nullable: true),
                    DataConsegnaCertificato = table.Column<DateTime>(nullable: true),
                    DataPartoPresunta = table.Column<DateTime>(nullable: true),
                    DataPartoEffettiva = table.Column<DateTime>(nullable: true),
                    NomeFiglio = table.Column<string>(nullable: true),
                    Medico = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    MultiTenantId = table.Column<Guid>(nullable: true),
                    FileName = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_MalattiaUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_C_MalattiaUser_C_Multitenant_MultiTenantId",
                        column: x => x.MultiTenantId,
                        principalTable: "C_Multitenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_MalattiaUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "C_Rel_SpecializzazioniUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 200, nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    ValidFrom = table.Column<DateTime>(nullable: true),
                    ValidTo = table.Column<DateTime>(nullable: true),
                    ReleasedFrom = table.Column<string>(maxLength: 200, nullable: true),
                    ReleasedAt = table.Column<DateTime>(nullable: true),
                    Vote = table.Column<string>(maxLength: 50, nullable: true),
                    isPromosso = table.Column<bool>(nullable: false),
                    SpecializzazioneId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    MultiTenantId = table.Column<Guid>(nullable: true),
                    FileName = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_Rel_SpecializzazioniUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_C_Rel_SpecializzazioniUser_C_Multitenant_MultiTenantId",
                        column: x => x.MultiTenantId,
                        principalTable: "C_Multitenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_Rel_SpecializzazioniUser_C_Specializzazioni_SpecializzazioneId",
                        column: x => x.SpecializzazioneId,
                        principalTable: "C_Specializzazioni",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_C_Rel_SpecializzazioniUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "C_Rel_MansioniUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MansioneId = table.Column<Guid>(nullable: false),
                    MansioneMacchinaId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<string>(nullable: false),
                    MultiTenantId = table.Column<Guid>(nullable: true),
                    DataAssegnazione = table.Column<DateTime>(nullable: true),
                    DataInizioAttivita = table.Column<DateTime>(nullable: true),
                    DataFineAttivita = table.Column<DateTime>(nullable: true),
                    SempreValido = table.Column<bool>(nullable: false),
                    LivelloCompetenza = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_Rel_MansioniUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_C_Rel_MansioniUser_C_Mansioni_MansioneId",
                        column: x => x.MansioneId,
                        principalTable: "C_Mansioni",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_C_Rel_MansioniUser_C_MansioneMacchina_MansioneMacchinaId",
                        column: x => x.MansioneMacchinaId,
                        principalTable: "C_MansioneMacchina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_Rel_MansioniUser_C_Multitenant_MultiTenantId",
                        column: x => x.MultiTenantId,
                        principalTable: "C_Multitenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_Rel_MansioniUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "C_Turni",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    OraInizio = table.Column<TimeSpan>(nullable: false),
                    OraFine = table.Column<TimeSpan>(nullable: false),
                    CantiereId = table.Column<Guid>(nullable: true),
                    dataInizio = table.Column<DateTime>(nullable: false),
                    dataFine = table.Column<DateTime>(nullable: false),
                    Active = table.Column<int>(nullable: true),
                    Stato = table.Column<int>(nullable: true),
                    MultiTenantId = table.Column<Guid>(nullable: true),
                    oraAutoChiusura = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_Turni", x => x.Id);
                    table.ForeignKey(
                        name: "FK_C_Turni_C_Cantieri_CantiereId",
                        column: x => x.CantiereId,
                        principalTable: "C_Cantieri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_Turni_C_Multitenant_MultiTenantId",
                        column: x => x.MultiTenantId,
                        principalTable: "C_Multitenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "C_Rel_FabbisognoMansione",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CantiereId = table.Column<Guid>(nullable: false),
                    MansioneId = table.Column<Guid>(nullable: true),
                    MansioneDesc = table.Column<string>(nullable: true),
                    Quantita = table.Column<int>(nullable: true),
                    TurnoId = table.Column<Guid>(nullable: true),
                    MultiTenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_Rel_FabbisognoMansione", x => x.Id);
                    table.ForeignKey(
                        name: "FK_C_Rel_FabbisognoMansione_C_Cantieri_CantiereId",
                        column: x => x.CantiereId,
                        principalTable: "C_Cantieri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_C_Rel_FabbisognoMansione_C_Mansioni_MansioneId",
                        column: x => x.MansioneId,
                        principalTable: "C_Mansioni",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_Rel_FabbisognoMansione_C_Multitenant_MultiTenantId",
                        column: x => x.MultiTenantId,
                        principalTable: "C_Multitenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_Rel_FabbisognoMansione_C_Turni_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "C_Turni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "C_Rel_TurniUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    WorkDate = table.Column<DateTime>(nullable: false),
                    TurnoId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    MansioneId = table.Column<Guid>(nullable: true),
                    SpecializzazioneId = table.Column<Guid>(nullable: true),
                    MultiTenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_Rel_TurniUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_C_Rel_TurniUsers_C_Mansioni_MansioneId",
                        column: x => x.MansioneId,
                        principalTable: "C_Mansioni",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_Rel_TurniUsers_C_Multitenant_MultiTenantId",
                        column: x => x.MultiTenantId,
                        principalTable: "C_Multitenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_Rel_TurniUsers_C_Specializzazioni_SpecializzazioneId",
                        column: x => x.SpecializzazioneId,
                        principalTable: "C_Specializzazioni",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_Rel_TurniUsers_C_Turni_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "C_Turni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_Rel_TurniUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "C_TimeSheetsDailyReport",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    WorkDate = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    TurnoId = table.Column<Guid>(nullable: true),
                    OreEffettive = table.Column<decimal>(nullable: true),
                    Ore = table.Column<decimal>(nullable: true),
                    OreGalleria = table.Column<decimal>(nullable: true),
                    OreTrasferta = table.Column<decimal>(nullable: true),
                    OreStraordinarie = table.Column<decimal>(nullable: true),
                    OreNotturne = table.Column<decimal>(nullable: true),
                    Indennita = table.Column<decimal>(nullable: true),
                    GiustificativoId = table.Column<Guid>(nullable: true),
                    Cigo = table.Column<string>(nullable: true),
                    Ec = table.Column<string>(nullable: true),
                    StatoUtente = table.Column<int>(nullable: true),
                    StatoTurno = table.Column<int>(nullable: true),
                    MultiTenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_TimeSheetsDailyReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_C_TimeSheetsDailyReport_C_Giustificativi_GiustificativoId",
                        column: x => x.GiustificativoId,
                        principalTable: "C_Giustificativi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_TimeSheetsDailyReport_C_Multitenant_MultiTenantId",
                        column: x => x.MultiTenantId,
                        principalTable: "C_Multitenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_TimeSheetsDailyReport_C_Turni_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "C_Turni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_C_TimeSheetsDailyReport_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "C_Claims",
                columns: new[] { "ID", "description", "title", "type" },
                values: new object[,]
                {
                    { new Guid("74c41de4-cda7-4f2a-9be5-62ffa9dd728f"), "Visualizza Utenti", "Users.Show", "Users.Show" },
                    { new Guid("d59dc4e5-a585-47bc-b393-95766b0428b1"), "Crea Ruoli", "Roles.Create", "Roles.Create" },
                    { new Guid("b7c4dd9e-7af3-4a03-b465-2387ce77fbf0"), "Modifica Ruoli", "Roles.Edit", "Roles.Edit" },
                    { new Guid("c763d52e-fa93-4060-81a9-52675c071679"), "Visualizza Scheda Attività, Timesheet", "Timesheet.Show", "Timesheet.Show" },
                    { new Guid("bbf29f0f-4f67-4359-bcef-457bc4931268"), "Crea Scheda Attività, Timesheet", "Timesheet.Create", "Timesheet.Create" },
                    { new Guid("3924c0d8-056d-478a-a9b5-1cee51b14882"), "Modifica Scheda Attività, Timesheet", "Timesheet.Edit", "Timesheet.Edit" },
                    { new Guid("3b6acf0e-3a0f-4f62-ac66-0e845f9f5136"), "Visualizza Scheda Attività Giornaliera, TimesheetDailyReport", "TimesheetDailyReport.Show", "TimesheetDailyReport.Show" },
                    { new Guid("fe69536b-b4ef-49ac-a7eb-999895aac265"), "Crea Scheda Attività Giornaliera, TimesheetDailyReport", "TimesheetDailyReport.Create", "TimesheetDailyReport.Create" },
                    { new Guid("a8a0e2a8-90cd-495e-8704-d392a25ecff0"), "Modifica Scheda Attività Giornaliera, TimesheetDailyReport", "TimesheetDailyReport.Edit", "TimesheetDailyReport.Edit" },
                    { new Guid("f888c968-58ef-4f8d-8034-9759f325e974"), "Visualizza Turni", "Turni.Show", "Turni.Show" },
                    { new Guid("37984e75-d281-404b-afa5-29a63489b915"), "Crea Turni", "Turni.Create", "Turni.Create" },
                    { new Guid("8e3dc15b-0cd0-4987-9f9b-0f61c76b18c7"), "Modifica Turni", "Turni.Edit", "Turni.Edit" },
                    { new Guid("703359a7-ce97-4f58-ba6e-fe9ac5479d55"), "Visualizza Mansioni Utente, RelMansioneUser", "RelMansioneUser.Show", "RelMansioneUser.Show" },
                    { new Guid("1fa660ea-f47b-49a1-ab93-e335bf8cd8ea"), "Crea Mansioni Utente, RelMansioneUser", "RelMansioneUser.Create", "RelMansioneUser.Create" },
                    { new Guid("56a001ac-5007-4cf3-8a8e-b419e42c4917"), "Modifica Mansioni Utente, RelMansioneUser", "RelMansioneUser.Edit", "RelMansioneUser.Edit" },
                    { new Guid("cca2bc88-4311-41fc-8a9e-a80b5202b6ef"), "Visualizza Specializzazione Utente, RelSpecializzazioneUser", "RelSpecializzazioneUser.Show", "RelSpecializzazioneUser.Show" },
                    { new Guid("a54e611f-839b-42ce-82ea-fed1cc2d6e50"), "Crea Specializzazione Utente, RelSpecializzazioneUser", "RelSpecializzazioneUser.Create", "RelSpecializzazioneUser.Create" },
                    { new Guid("7f34d9f3-a1da-4656-858c-c90884070ba2"), "Modifica Specializzazione Utente, RelSpecializzazioneUser", "RelSpecializzazioneUser.Edit", "RelSpecializzazioneUser.Edit" },
                    { new Guid("9c06626d-3812-4532-9860-b7587adb02b4"), "Visualizza Ruoli", "Roles.Show", "Roles.Show" },
                    { new Guid("71ba2680-71c9-49a3-bd7c-c0904688a79c"), "Crea Turno Utente, RelTurnoUser", "RelTurnoUser.Create", "RelTurnoUser.Create" },
                    { new Guid("a38c153f-1e09-4584-b0f8-8ebc482810cc"), "Visualizza Turno Utente, RelTurnoUser", "RelTurnoUser.Show", "RelTurnoUser.Show" },
                    { new Guid("2210dbd6-3893-45b7-8484-389c89277ae0"), "Crea Commesse", "Project.Create", "Project.Create" },
                    { new Guid("63a8d1f0-2a87-4277-b8f6-bfb287cd07b9"), "Visualizza Cantiere", "Cantiere.Show", "Cantiere.Show" },
                    { new Guid("21fe8b3a-db8b-494b-8c90-e636cbd6d21b"), "Crea Cantiere", "Cantiere.Create", "Cantiere.Create" },
                    { new Guid("bed0e6c4-40d1-40b8-98d9-ad74d9eb69d8"), "Modifica Cantiere", "Cantiere.Edit", "Cantiere.Edit" },
                    { new Guid("190df19f-08a6-4a20-926b-6f8285ce9665"), "Visualizza Aziende", "Companies.Show", "Companies.Show" },
                    { new Guid("81a716cf-fdad-47a4-9ad7-0915416bb1d0"), "Crea Aziende", "Companies.Create", "Companies.Create" },
                    { new Guid("c49eba26-8736-4a8d-aec1-cce7641eef8c"), "Modifica Aziende", "Companies.Edit", "Companies.Edit" },
                    { new Guid("ba385d95-89f5-4a9c-bb54-516756438550"), "Visualizza Commesse", "Project.Show", "Project.Show" },
                    { new Guid("0d7f74d3-1dfe-4ef7-a16e-fcb04b999d84"), "Modifica Malattie Utente", "MalattiaUser.Edit", "MalattiaUser.Edit" },
                    { new Guid("27b11d30-c698-4256-a9ff-64b1d32261f4"), "Modifica Commesse", "Project.Edit", "Project.Edit" },
                    { new Guid("6fdcda12-61db-4c9e-91a6-5d686a9a936e"), "Visualizza Contratti Utente", "ContractUser.Show", "ContractUser.Show" },
                    { new Guid("d155a8c2-82eb-4610-aebb-652cb580b78d"), "Crea Contratti Utente", "ContractUser.Create", "ContractUser.Create" },
                    { new Guid("6a4024b4-550a-4278-8ed0-abf7d594566c"), "Modifica Contratti Utente", "ContractUser.Edit", "ContractUser.Edit" },
                    { new Guid("66b6b76f-a1ea-4cd1-8301-9ca9319a9cf6"), "Visualizza Documenti Utente", "Document.Show", "Document.Show" },
                    { new Guid("711ae8b0-0eef-4d10-a6cf-94119e0b97ee"), "Crea Documenti Utente", "Document.Create", "Document.Create" },
                    { new Guid("77115fc4-7158-4674-86a0-43becc95b524"), "Modifica Documenti Utente", "Document.Edit", "Document.Edit" },
                    { new Guid("1b937324-9854-4012-a6d7-ee9081b3b4f6"), "Visualizza Malattie Utente", "MalattiaUser.Show", "MalattiaUser.Show" },
                    { new Guid("39a6f905-4ac8-4bc5-b4d2-bbf8bc464b78"), "Crea Malattie Utente", "MalattiaUser.Create", "MalattiaUser.Create" },
                    { new Guid("4fb3636a-9a1c-45ce-8eb6-45d61eb721cb"), "Modifica Utenti", "Users.Edit", "Users.Edit" },
                    { new Guid("87887436-12d5-4311-900e-bd5b29d6cac6"), "Crea Utenti", "Users.Create", "Users.Create" }
                });

            migrationBuilder.InsertData(
                table: "C_Multitenant",
                columns: new[] { "Id", "LogoUrl", "LoocId", "Name", "slughost" },
                values: new object[,]
                {
                    { new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "/looc/images/loghi/logoventura200.png", null, "Gruppo Francesco Ventura", null },
                    { new Guid("46ce0547-fa27-4bce-92d1-8a32e15fe95e"), "/looc/images/loghi/kresearch.png", null, "KResearch", null }
                });

            migrationBuilder.InsertData(
                table: "C_ANA_Companies",
                columns: new[] { "ID", "Citta", "CodiceSdi", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "EmailAziendale", "EmailPec", "Fax", "FiscalCode", "Img", "Indirizzo", "InternalCode", "MultiTenantId", "Nazione", "PIva", "ParentID", "Provincia", "RagioneSociale", "Regione", "SitoWeb", "Telefono", "UpdatedAt", "UpdatedBy", "active", "isCustomer", "isExternal", "isOfficina", "isSupplier" },
                values: new object[,]
                {
                    { new Guid("cf66eaff-5955-4ce1-a92b-091215e298a5"), "Lecce", "", null, null, null, null, "", "", "", "", null, "", null, new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "Italia", "0", null, "Le", "Gruppo Francesco Ventura", "Puglia", "", "", null, null, true, false, false, false, false },
                    { new Guid("bc1eab39-75d2-4763-999f-dbfa95878a6f"), "Lecce", "", null, null, null, null, "", "", "", "", null, "", null, new Guid("46ce0547-fa27-4bce-92d1-8a32e15fe95e"), "Italia", "0", null, "Le", "Polo 4.0", "Puglia", "", "", null, null, true, false, false, false, false }
                });

            migrationBuilder.InsertData(
                table: "C_Domains",
                columns: new[] { "Id", "Code", "MultiTenantId", "Name", "ParentId", "Tipo" },
                values: new object[,]
                {
                    { new Guid("c66cd9e8-6773-44ed-a3f8-b6a8ca9abf05"), null, new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "Patente", null, "document_profile" },
                    { new Guid("7a8447f6-33d1-4527-ad63-423ae67212e4"), null, new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "Passaporto", null, "document_profile" },
                    { new Guid("9f6d3370-302a-4d34-ab92-d64a476392ac"), null, new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "Documento Generico", null, "document_index" },
                    { new Guid("99c7150b-2424-4e9a-af01-1c37152d1175"), null, new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "Diploma", null, "document_titolo_studio" },
                    { new Guid("628da834-f730-464b-a533-a08c6e2099f5"), "M3", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "METALMECCANICO LIVELLO 3", null, "contract_user_level" },
                    { new Guid("e7c5a7e5-80f6-40e5-8566-da409765f004"), "OR", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "Oraria", null, "contract_user_tipo_retribuzione" },
                    { new Guid("012256f5-4c8b-4dd0-8a78-23e9d2edf665"), "FP", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "Tempo pieno", null, "contract_user_tipo_orario" },
                    { new Guid("d6273c9b-a459-4457-9f1e-ae96f224f0b8"), "TE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "Tempo determinato", null, "contract_user_tipologia" },
                    { new Guid("c1ad61b4-3eb4-4a92-9a37-6267fca03618"), "TE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "Tempo determinato", null, "contract_user_tipologia" },
                    { new Guid("be3fffd7-e1a9-4e24-9d97-9c1ff9a9573d"), "68", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "68", null, "contract_user_law_number" },
                    { new Guid("44730230-fb83-48e2-9ebc-ce6b4fab55b0"), "Impiegato", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "Impiegato", null, "contract_user_category" },
                    { new Guid("45a09b15-1bc4-4244-a56c-da2e417adbeb"), null, new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "Carta D'Identita", null, "document_profile" }
                });

            migrationBuilder.InsertData(
                table: "C_Giustificativi",
                columns: new[] { "Id", "Active", "AdempimentiCaricoInteressato", "AdempimentiResponsabilePersonale", "AdempimentiResponsabileUOG", "Code", "Description", "Destinatari", "FonteNormativa", "ModalitaDiImputazione", "MultiTenantId", "Name" },
                values: new object[,]
                {
                    { new Guid("37017210-62a6-4ecf-ba7f-409db3e8f333"), true, null, null, null, "MAL", null, null, null, null, new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "MALATTIA" },
                    { new Guid("060274ba-e860-473f-bd0d-73d7ddf7ffea"), true, null, null, null, "PCR", null, null, null, null, new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PERMESSO CONTRATTUALE RETRIBUITO" },
                    { new Guid("f36477e6-b3ae-48db-a65b-63d1cdd2f8c0"), true, null, null, null, "MIN", null, null, null, null, new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "INFORTUNIO SUL LAVORO" },
                    { new Guid("124c98b5-a5e5-48ae-b052-ebcb6e6a3a08"), true, null, null, null, "TIM", null, null, null, null, new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PRESENZA SENZA TIMBRATURE" },
                    { new Guid("2e6a4b1d-a2d3-4de9-ae0f-483768c4d874"), true, null, null, null, "SER", null, null, null, null, new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "SERVIZIO" },
                    { new Guid("d0ef1825-a2aa-429b-b16c-c39035bb4874"), true, null, null, null, "FER", null, null, null, null, new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "FERIE" }
                });

            migrationBuilder.InsertData(
                table: "C_Mansioni",
                columns: new[] { "ID", "Codice", "Descrizione", "MultiTenantId", "Name", "isAssignedAsDefault", "isEnabledManageHour" },
                values: new object[,]
                {
                    { new Guid("b12da303-a228-4ae1-a460-ec8d4afe6a98"), "MAN053", "SALDATORE AD ARCO", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "SALDATORE AD ARCO", 0, null },
                    { new Guid("da843ef1-eea3-415b-b3be-89794fa4eff9"), "MAN052", "RSPP", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RSPP", 0, null },
                    { new Guid("6bf78f3c-46af-4fd7-a7b2-b5d86bc2e77f"), "MAN051", "PORTALINI SCAMBI", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PORTALINI SCAMBI", 0, null },
                    { new Guid("942ed7b9-15d2-4241-ad71-ad17ed3ff2e8"), "MAN050", "OPERATORE TRENO RINNOVATORE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERATORE TRENO RINNOVATORE", 0, null },
                    { new Guid("aac31f28-cea2-4205-ac52-e003f4ed5d72"), "MAN049", "OPERATORE SALDATRICE SCINTILLIO", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERATORE SALDATRICE SCINTILLIO", 0, null },
                    { new Guid("428769de-f4d9-41f3-b205-647666ddc864"), "MAN048", "OPERATORE RISANATRICE ESTERNO", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERATORE RISANATRICE ESTERNO", 0, null },
                    { new Guid("860a8bdd-4d2b-488b-ab08-748c4914a042"), "MAN047", "OPERATORE RISANATRICE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERATORE RISANATRICE", 0, null },
                    { new Guid("7511ac24-b75b-445a-abf2-3f871204ecbd"), "MAN046", "OPERATORE RINCALZATORE PINSE E SCAMBI", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERATORE RINCALZATORE PINSE E SCAMBI", 0, null },
                    { new Guid("59cd42a7-4cb7-4f79-8831-c0d2152870f9"), "MAN044", "OPERATORE PROFILATRICE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERATORE PROFILATRICE", 0, null },
                    { new Guid("baa2a9bb-e87d-41f4-8a1f-bbd356f225c7"), "MAN043", "OPERATORE PORTALINI SCAMBI", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERATORE PORTALINI SCAMBI", 0, null },
                    { new Guid("5385c641-9a4e-4201-a47b-722209f41f40"), "MAN042", "OPERATORE PORTALINI", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERATORE PORTALINI", 0, null },
                    { new Guid("ea052b6c-42f4-4b8c-91d8-bfd4f22ab189"), "MAN041", "OPERATORE PORTALE RINNOVO SCART RIDOTTO", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERATORE PORTALE RINNOVO SCART RIDOTTO", 0, null },
                    { new Guid("71be202e-5de3-4623-b2e8-db24a2f67594"), "MAN040", "OPERATORE PINSE E SCAMBI", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERATORE PINSE E SCAMBI", 0, null },
                    { new Guid("0be565d8-cd9d-411f-906e-394a0314ad28"), "MAN054", "SALDATORE ALLUMINOTERMICO", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "SALDATORE ALLUMINOTERMICO", 0, null },
                    { new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), "MAN045", "OPERATORE RINCALZATORE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERATORE RINCALZATORE", 0, null },
                    { new Guid("df84bfd4-c35d-439c-9870-8aaac2bf8412"), "MAN055", "SALDATORE CAVA", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "SALDATORE CAVA", 0, null },
                    { new Guid("d47bdb67-19db-4bb0-b560-408bb5d61de0"), "MAN063", "UFFICIO CONTROLLO DI GESTIONE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "UFFICIO CONTROLLO DI GESTIONE", 0, null },
                    { new Guid("5d8c8f1a-a290-44b8-9ec5-3ea70f48389c"), "MAN057", "SCARTAMENTISTA", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "SCARTAMENTISTA", 0, null },
                    { new Guid("dc930044-acfb-47e8-8eb0-0e2dae104502"), "MAN072", "UFFICIO TECNICO", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "UFFICIO TECNICO", 0, null },
                    { new Guid("bcd5ba2f-1b27-412d-b642-8e6097425c34"), "MAN071", "UFFICIO SISTEMA INFORMATIVO", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "UFFICIO SISTEMA INFORMATIVO", 0, null },
                    { new Guid("7724d562-9c72-4d8e-a3bc-bf09157f16fd"), "MAN070", "UFFICIO QUALITA", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "UFFICIO QUALITA", 0, null },
                    { new Guid("15be68e6-3a4a-49ba-aaf5-4cf3334c79ae"), "MAN069", "UFFICIO PROTOCOLLO", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "UFFICIO PROTOCOLLO", 0, null },
                    { new Guid("1b25c42f-304b-44b5-8fbb-438db071bf49"), "MAN068", "UFFICIO PERSONALE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "UFFICIO PERSONALE", 0, null },
                    { new Guid("e7c4eb60-d9ed-461e-9882-248de34b86ad"), "MAN067", "UFFICIO PAGHE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "UFFICIO PAGHE", 0, null },
                    { new Guid("7fb2c0f5-7369-4f41-a4a6-74d2605097a9"), "MAN066", "UFFICIO LOGISTICA PERSONALE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "UFFICIO LOGISTICA PERSONALE", 0, null },
                    { new Guid("e4a48625-1ab9-4526-9933-273b34fc8faf"), "MAN065", "UFFICIO INFRASTRUTTURE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "UFFICIO INFRASTRUTTURE", 0, null },
                    { new Guid("5d6b8b30-852c-479a-92d6-30aaf2537165"), "MAN064", "UFFICIO GARE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "UFFICIO GARE", 0, null },
                    { new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), "MAN037", "OPERATORE LIVELLISTA", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERATORE LIVELLISTA", 0, null },
                    { new Guid("c5d1aab6-70fd-4a25-a565-fd089e8a8eed"), "MAN062", "UFFICIO CONTABILITA LAVORI", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "UFFICIO CONTABILITA LAVORI", 0, null },
                    { new Guid("10dddfa4-a6a1-4c54-9a64-549aa13ef92f"), "MAN061", "UFFICIO APPROVIGIONAMENTO", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "UFFICIO APPROVIGIONAMENTO", 0, null },
                    { new Guid("f94c7c83-6c7b-4496-9e1a-5e0654a681a6"), "MAN060", "UFFICIO AMMINISTRAZIONE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "UFFICIO AMMINISTRAZIONE", 0, null },
                    { new Guid("afb65cd1-1b1b-4f87-90b4-e135e7ef2f35"), "MAN059", "UFFICIO AFFARI GENERALI", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "UFFICIO AFFARI GENERALI", 0, null },
                    { new Guid("abafe7bb-9b77-4fd1-9c2b-03eda91ba992"), "MAN058", "TORNITORE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "TORNITORE", 0, null },
                    { new Guid("96133186-46ad-4f61-a56f-a8a9106e1a9b"), "MAN056", "SALDATORE OFFICINA", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "SALDATORE OFFICINA", 0, null },
                    { new Guid("e988f39c-e3f5-4497-97dc-43a1839faad9"), "MAN036", "OPERATORE ESCAVATORE FINO A 80 QT", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERATORE ESCAVATORE FINO A 80 QT", 0, null },
                    { new Guid("f6999d2a-28d9-4134-a280-a24ece5e8a2c"), "MAN039", "OPERATORE PALA", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERATORE PALA", 0, null },
                    { new Guid("63d8d6eb-3a4c-4949-b0bf-a5c8c12fcb79"), "MAN034", "OPERATORE COMPATTATRICE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERATORE COMPATTATRICE", 0, null },
                    { new Guid("bebb005d-1b5a-4858-9fde-ddb5b608287f"), "MAN014", "CARRI WINDHOFF", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "CARRI WINDHOFF", 0, null },
                    { new Guid("8bb6e628-c02d-41d3-9726-8f7a9775c96f"), "MAN013", "CARRI RISANATRICE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "CARRI RISANATRICE", 0, null },
                    { new Guid("8803244b-741f-4606-a0f7-6d8483a1f1c5"), "MAN012", "CARRI GRUETTE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "CARRI GRUETTE", 0, null },
                    { new Guid("2fe3364f-d8cf-4425-95d6-87f085e89bae"), "MAN011", "CAPO SQUADRA", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "CAPO SQUADRA", 0, null },
                    { new Guid("72ed47c4-4286-4c34-a867-291b85bdb5d5"), "MAN010", "CAPO CANTIERE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "CAPO CANTIERE", 0, null },
                    { new Guid("88619738-1336-4b98-bff7-4c8baa8e4c3f"), "MAN009", "AUTISTA DUMPER", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "AUTISTA DUMPER", 0, null },
                    { new Guid("ed278b71-093a-49b1-9eea-bb31a24127e8"), "MAN008", "AUTISTA CAMION", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "AUTISTA CAMION", 0, null },
                    { new Guid("7177a0ae-f046-43c2-85cd-42ceb4b84c30"), "MAN007", "AUTISTA", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "AUTISTA", 0, null },
                    { new Guid("567c5203-09f9-4699-9696-773cc0c90370"), "MAN035", "OPERATORE ESCAVATORE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERATORE ESCAVATORE", 0, null },
                    { new Guid("b90642fe-1684-402b-b429-757f7df85e9c"), "MAN005", "ASSISTENTE DI CANTIERE E ADDETTO ALLA QUALITA'", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "ASSISTENTE DI CANTIERE E ADDETTO ALLA QUALITA'", 0, null },
                    { new Guid("02703a2a-18ec-4c09-81bd-c36478837f70"), "MAN004", "ASPP", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "ASPP", 0, null },
                    { new Guid("70c797fa-9909-4266-a1ab-00e74fa1ac6e"), "MAN003", "ADDETTO RILIEVI", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "ADDETTO RILIEVI", 0, null },
                    { new Guid("9eb584c8-a372-49b9-8290-4a2c70ed8513"), "MAN002", "ADDETTO PONTEGGI", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "ADDETTO PONTEGGI", 0, null },
                    { new Guid("8b2b8e6d-b7f5-4d1b-abc1-c625da10e99f"), "MAN001", "ADDETTA PULIZIE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "ADDETTA PULIZIE", 0, null },
                    { new Guid("5b0491e8-3fe6-4240-a4aa-9b4a89b6b0d1"), "MAN000", "Responsabile gestione ore turno", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "Responsabile gestione ore turno", 1, 1 },
                    { new Guid("e8e942db-a381-4da0-8843-e849cbf5c6e8"), "MAN015", "CARROZZIERE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "CARROZZIERE", 0, null },
                    { new Guid("79842194-de56-4abe-bd07-540951051edf"), "MAN016", "DIRETTORE DI CANTIERE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "DIRETTORE DI CANTIERE", 0, null },
                    { new Guid("6f612119-1704-47c2-92a5-a8327b0391f9"), "MAN006", "ASSISTENTE MECCANICO", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "ASSISTENTE MECCANICO", 0, null },
                    { new Guid("96e446c3-353a-49f2-8957-50dff43928e8"), "MAN018", "DIREZIONE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "DIREZIONE", 0, null },
                    { new Guid("c7cb2e01-2ec7-42f4-9619-562c1a69258c"), "MAN017", "DIRETTORE TECNICO", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "DIRETTORE TECNICO", 0, null },
                    { new Guid("58e546e7-57ea-4e2b-bf68-9bcb20744a67"), "MAN033", "OPERATORE CARRELLO WINDHOFF", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERATORE CARRELLO WINDHOFF", 0, null },
                    { new Guid("fbd7a922-cb24-4add-8cef-a5fc0da2b81b"), "MAN032", "OPERATORE CARICATORE DA PIAZZALE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERATORE CARICATORE DA PIAZZALE", 0, null },
                    { new Guid("c28da311-584a-4eae-a1ce-4a3884d0f921"), "MAN031", "OPERATORE CARICATORE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERATORE CARICATORE", 0, null },
                    { new Guid("b06b1a62-da0b-43ed-add8-89439476ad72"), "MAN030", "OPERAIO TRENO RINNOVATORE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERAIO TRENO RINNOVATORE", 0, null },
                    { new Guid("330944a0-a6ce-4cd7-9f08-2e84729b6c62"), "MAN029", "OPERAIO TRAMVIA", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERAIO TRAMVIA", 0, null },
                    { new Guid("72590be4-38aa-432b-a2dd-73784b32cf7c"), "MAN028", "OPERAIO MAGAZZINO", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERAIO MAGAZZINO", 0, null },
                    { new Guid("b3211b66-685d-4522-8996-2c04b73c6ab5"), "MAN027", "OPERAIO CAVA", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERAIO CAVA", 0, null },
                    { new Guid("dcb5a0b8-2272-4ca0-b3b3-8a621953d165"), "MAN038", "OPERATORE LOCOMOTORE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERATORE LOCOMOTORE", 0, null },
                    { new Guid("b7eb46f4-dbe5-49fe-a253-d078b6848e14"), "MAN025", "OPERAIO - MANUTENZIONE RISANATRICI E CARRI", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERAIO - MANUTENZIONE RISANATRICI E CARRI", 0, null },
                    { new Guid("bc68716a-634a-429f-97eb-08e5d2140d6d"), "MAN024", "OPERAIO", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERAIO", 0, null },
                    { new Guid("a3158132-8698-4987-8f80-068b2828a6c5"), "MAN023", "MECCANICO", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "MECCANICO", 0, null },
                    { new Guid("8c96b155-e015-4116-a3ca-a3dd366fa038"), "MAN022", "IMPIANTO DI ILLUMINAZIONE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "IMPIANTO DI ILLUMINAZIONE", 0, null },
                    { new Guid("b435269b-f845-4979-a51a-49dbb077e00f"), "MAN021", "IMPIANTI ELETTRICI", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "IMPIANTI ELETTRICI", 0, null },
                    { new Guid("b8aeb862-474f-45a2-819a-1604c9272d01"), "MAN020", "GUARDIANO", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "GUARDIANO", 0, null },
                    { new Guid("44aec8ba-48d8-49b2-a42c-b8ba80b3c508"), "MAN026", "OPERAIO CARRI RISANATRICE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "OPERAIO CARRI RISANATRICE", 0, null },
                    { new Guid("301c4883-fe51-4339-813a-d7231fd2b98d"), "MAN019", "ELETTRICISTA", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "ELETTRICISTA", 0, null }
                });

            migrationBuilder.InsertData(
                table: "C_Specializzazioni",
                columns: new[] { "ID", "Codice", "Descrizione", "MultiTenantId", "Name" },
                values: new object[,]
                {
                    { new Guid("03904a23-65bc-487c-bc58-54c098631700"), "S014", "PAT. D", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PAT. D" },
                    { new Guid("72a1d36a-774c-44c7-9b96-3fb6f8769ea1"), "S015", "PAT. D+E", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PAT. D+E" },
                    { new Guid("b629950f-2b8e-4f16-9dfe-1dd2c5fb3235"), "S016", "ATTESTATO ESCAVATORISTA", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "ATTESTATO ESCAVATORISTA" },
                    { new Guid("d5b9923f-a0ae-4d32-a2f8-de3609480169"), "S019", "PATENTINO FGAS", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PATENTINO FGAS" },
                    { new Guid("ddbd8756-e8c6-44ae-8abc-a4176479b776"), "S018", "PREPOSTO DI CANTIERE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PREPOSTO DI CANTIERE" },
                    { new Guid("979c452b-2498-460d-95a0-db69bdf61c47"), "S013", "TECNICO ORGANI SICUREZZA", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "TECNICO ORGANI SICUREZZA" },
                    { new Guid("854d9e50-f28a-4ea5-baa1-5f330de34aea"), "S020", "ATTESTATO SCORTA E TOLTA TENSIONE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "ATTESTATO SCORTA E TOLTA TENSIONE" },
                    { new Guid("b77e31b0-ccfa-4f36-9993-6aee022eb2e0"), "S021", "ATTESTATO GRU PER AUTOCARRO", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "ATTESTATO GRU PER AUTOCARRO" },
                    { new Guid("da8037af-735b-49eb-b21d-2a08805d5515"), "S017", "ATTESTATO MULETTO", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "ATTESTATO MULETTO" },
                    { new Guid("804f759b-b1bf-4545-9e33-a1356411968c"), "S012", "ABILITAZIONE SALDATURA MATERIALI", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "ABILITAZIONE SALDATURA MATERIALI" },
                    { new Guid("5118b6a7-34d3-4540-baf3-efa0da4192b0"), "S008", "ABILITAZIONE PRIMI FORMATORI", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "ABILITAZIONE PRIMI FORMATORI" },
                    { new Guid("b7739c92-b1a6-4828-92f6-244ecffc45eb"), "S010", "ARMDITTE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "ARMDITTE" },
                    { new Guid("96ea3e72-3887-4f84-9129-078899bccbef"), "S009", "ABILITAZIONE ULTRASUONI", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "ABILITAZIONE ULTRASUONI" },
                    { new Guid("52d76fe6-5e4d-4caf-af7b-dbb018c90a23"), "S007", "PROT. CANTIERI FC", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PROT. CANTIERI FC" },
                    { new Guid("696dddac-2c53-4997-aa47-387f7582c490"), "S006", "PAT. C", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PAT. C" },
                    { new Guid("3584324a-d1e9-43e5-b7be-821ead97d14e"), "S005", "ABILITAZIONE GUIDA MEZZI", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "ABILITAZIONE GUIDA MEZZI" },
                    { new Guid("1a80cdbc-4f8e-4321-970b-cd0b98b2ba4a"), "S004", "ABILITAZIONE PROT CANTIERE", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "ABILITAZIONE PROT CANTIERE" },
                    { new Guid("9144a055-579e-4964-b5e1-59aa8380625a"), "S002", "ABILITAZIONE ANTINCENDIO", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "ABILITAZIONE ANTINCENDIO" },
                    { new Guid("2c7615cd-4aab-44b4-8b72-ee2e18c83f1a"), "S001", "ABILITAZIONE PRIMO SOCCORSO", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "ABILITAZIONE PRIMO SOCCORSO" },
                    { new Guid("d5dc6b26-7dbd-49cf-91e5-3f44bcb7322f"), "S022", "ATTESTATO PONTI E VIADOTTI", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "ATTESTATO PONTI E VIADOTTI" },
                    { new Guid("e48e0d9d-f20b-4c02-a77b-7f0c88f28771"), "S011", "ADD.PONTEGGIO", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "ADD.PONTEGGIO" },
                    { new Guid("810c4a73-3ed2-4aa0-b38f-af03a23b60ef"), "S003", "ABILITAZIONE SALDATURA ALLUMINOTERMICA", new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "ABILITAZIONE SALDATURA ALLUMINOTERMICA" }
                });

            migrationBuilder.InsertData(
                table: "C_ANA_Companies",
                columns: new[] { "ID", "Citta", "CodiceSdi", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "EmailAziendale", "EmailPec", "Fax", "FiscalCode", "Img", "Indirizzo", "InternalCode", "MultiTenantId", "Nazione", "PIva", "ParentID", "Provincia", "RagioneSociale", "Regione", "SitoWeb", "Telefono", "UpdatedAt", "UpdatedBy", "active", "isCustomer", "isExternal", "isOfficina", "isSupplier" },
                values: new object[,]
                {
                    { new Guid("157ca439-3132-49a7-a620-3804de61b1be"), "Lecce", "", null, null, null, null, "", "", "", "", null, "", null, new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "Italia", "1", new Guid("cf66eaff-5955-4ce1-a92b-091215e298a5"), "Le", "MedTech", "Puglia", "", "", null, null, true, false, false, false, false },
                    { new Guid("111c6341-215f-4fbd-8641-ec468199219f"), "Lecce", "", null, null, null, null, "", "", "", "", null, "", null, new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "Italia", "2", new Guid("cf66eaff-5955-4ce1-a92b-091215e298a5"), "Le", "Binaria", "Puglia", "", "", null, null, true, false, false, false, false },
                    { new Guid("a1c60dba-1f39-4687-9ce6-d2876af4033d"), "Lecce", "", null, null, null, null, "", "", "", "", null, "", null, new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "Italia", "2", new Guid("cf66eaff-5955-4ce1-a92b-091215e298a5"), "Le", "Francesco Ventura", "Puglia", "", "", null, null, true, false, false, false, false },
                    { new Guid("c9e8c9fa-a29c-418b-b136-e9905869e44b"), "Lecce", "", null, null, null, null, "", "", "", "", null, "", null, new Guid("46ce0547-fa27-4bce-92d1-8a32e15fe95e"), "Italia", "1", new Guid("bc1eab39-75d2-4763-999f-dbfa95878a6f"), "Le", "KRD", "Puglia", "", "", null, null, true, false, false, false, false },
                    { new Guid("efee2afb-d85e-4bc0-b065-a9c25d427abe"), "Lecce", "", null, null, null, null, "", "", "", "", null, "", null, new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "Italia", "2", new Guid("cf66eaff-5955-4ce1-a92b-091215e298a5"), "Le", "Fleet", "Puglia", "", "", null, null, true, false, false, false, false },
                    { new Guid("d679bdbd-53dc-4a12-a3ad-cd31bc93366f"), "Lecce", "", null, null, null, null, "", "", "", "", null, "", null, new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "Italia", "2", new Guid("cf66eaff-5955-4ce1-a92b-091215e298a5"), "Le", "Paola Real Estate", "Puglia", "", "", null, null, true, false, false, false, false },
                    { new Guid("4294de0c-71db-441b-8b52-9cc7e3f06c4c"), "Lecce", "", null, null, null, null, "", "", "", "", null, "", null, new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "Italia", "2", new Guid("cf66eaff-5955-4ce1-a92b-091215e298a5"), "Le", "Ventura Mineraria", "Puglia", "", "", null, null, true, false, false, false, false }
                });

            migrationBuilder.InsertData(
                table: "C_MansioneMacchina",
                columns: new[] { "Id", "Codice", "Description", "MansioneId", "MultiTenantId", "Name" },
                values: new object[,]
                {
                    { new Guid("b53cc836-f4aa-4820-af76-62a195de2087"), "MAC051", "RINCALZATRICE LINEA E SCAMBI PLASSER 09 4S - E3", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA E SCAMBI PLASSER 09 4S - E3" },
                    { new Guid("013fa7d1-b55d-404d-b734-6bddfbbb16b9"), "MAC053", "RINCALZATRICE LINEA MATISA B 242", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA MATISA B 242" },
                    { new Guid("ecb01df8-9860-4f25-ba1b-35b0c1e52cd3"), "MAC056", "RINCALZATRICE LINEA MATISA B 45", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA MATISA B 45" },
                    { new Guid("f54a337a-07e4-4e93-b004-87cc5dc6db54"), "MAC086", "RINCALZATRICE SCAMBI MATISA B 40 UE", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE SCAMBI MATISA B 40 UE" },
                    { new Guid("9ac31738-c17c-4221-8f6d-21a38175e5ec"), "MAC050", "RINCALZATRICE LINEA E SCAMBI PLASSER 09 4S - 32", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA E SCAMBI PLASSER 09 4S - 32" },
                    { new Guid("668d3af2-f1ba-428c-8ad7-4d36c8748de8"), "MAC049", "RINCALZATRICE LINEA E SCAMBI MATISA B 30", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA E SCAMBI MATISA B 30" },
                    { new Guid("15bab16d-6f55-466f-8130-7869759bac68"), "MAC055", "RINCALZATRICE LINEA MATISA B 40 D", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA MATISA B 40 D" },
                    { new Guid("55e0a581-3c06-4360-afc2-011448bc74f2"), "MAC047", "RINCALZATRICE LINEA BEAWER 700 a scart ridotto", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA BEAWER 700 a scart ridotto" },
                    { new Guid("b080253b-e5a1-4f55-a6e1-f5d02ccd167d"), "MAC089", "RINCALZATRICE SCAMBI PLASSER 09 4S 32", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE SCAMBI PLASSER 09 4S 32" },
                    { new Guid("eff0d631-47d1-4acd-ae83-48a1d7c18772"), "MAC062", "RINCALZATRICE LINEA-SCAMBI ZAMIR 08 16 SNA ", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA-SCAMBI ZAMIR 08 16 SNA " },
                    { new Guid("80c93c01-337d-4d34-ba2e-8c04bd2acffe"), "MAC087", "RINCALZATRICE SCAMBI PLASSER 08 16 a scart ridotto", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE SCAMBI PLASSER 08 16 a scart ridotto" },
                    { new Guid("682d5a84-f487-421f-919d-6bf7a3818c43"), "MAC060", "RINCALZATRICE LINEA ZAMIR 08 SNA a scart ridotto", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA ZAMIR 08 SNA a scart ridotto" },
                    { new Guid("84c56f30-b30b-4952-b00f-705d8bca2954"), "MAC074", "RINCALZATRICE PLASSER 08 275", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE PLASSER 08 275" },
                    { new Guid("111ad477-0bd0-4a7d-b6f3-12288e635401"), "MAC063", "RINCALZATRICE LINEA-SCAMBI ZAMIR 08 275 a scart ridotto", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA-SCAMBI ZAMIR 08 275 a scart ridotto" },
                    { new Guid("c25d3f00-ccea-41b8-8c52-5a17c70ae9ad"), "MAC076", "RINCALZATRICE PLASSER 08 32", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE PLASSER 08 32" },
                    { new Guid("d9b44c55-0d82-4f81-84f5-6a89d2ff2788"), "MAC094", "RINCALZATRICE SCAMBI ZAMIR SNA", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE SCAMBI ZAMIR SNA" },
                    { new Guid("59eb8212-c377-46db-913d-5b4f856e7dbd"), "MAC066", "RINCALZATRICE MATISA B 30", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE MATISA B 30" },
                    { new Guid("730cedaf-3d66-4ddd-b6b9-d858c756a951"), "MAC061", "RINCALZATRICE LINEA ZAMIR B40 DE", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA ZAMIR B40 DE" },
                    { new Guid("90a43fd8-8f24-48b1-97d6-5aaa07f664b7"), "MAC044", "RINCALZATRICE B 40 UE", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE B 40 UE" },
                    { new Guid("529a35f4-695a-4268-8c33-2443a2477c2c"), "MAC092", "RINCALZATRICE SCAMBI ZAMIR 08 275 SP", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE SCAMBI ZAMIR 08 275 SP" },
                    { new Guid("dca865c5-e158-4a32-b20e-37288767b6ce"), "MAC059", "RINCALZATRICE LINEA PLASSER 09 3X", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA PLASSER 09 3X" },
                    { new Guid("bc91a398-1ba0-4d46-8ac1-377789f30c9b"), "MAC034", "PROFILATRICE PLASSER SSP 80", new Guid("59cd42a7-4cb7-4f79-8831-c0d2152870f9"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PROFILATRICE PLASSER SSP 80" },
                    { new Guid("ed887b37-277a-4142-a5b8-278f84fb59ca"), "MAC035", "PROFILATRICE PLASSER SSP 90", new Guid("59cd42a7-4cb7-4f79-8831-c0d2152870f9"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PROFILATRICE PLASSER SSP 90" },
                    { new Guid("eb08fc08-deef-4c77-a3bf-a785832aa08a"), "MAC029", "PROFILATRICE MATISA R 780", new Guid("59cd42a7-4cb7-4f79-8831-c0d2152870f9"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PROFILATRICE MATISA R 780" },
                    { new Guid("28ebafc1-4328-4969-8470-28662b333b6d"), "MAC022", "PROFILATRCE PLASSER SP 80", new Guid("59cd42a7-4cb7-4f79-8831-c0d2152870f9"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PROFILATRCE PLASSER SP 80" },
                    { new Guid("3b4b737e-c09f-44cd-9843-aa18cc8dadfa"), "MAC023", "PROFILATRCE PLASSER SP 90", new Guid("59cd42a7-4cb7-4f79-8831-c0d2152870f9"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PROFILATRCE PLASSER SP 90" },
                    { new Guid("a7fb6a68-06f8-4d7b-9d78-c1dff54fc19d"), "MAC030", "PROFILATRICE MATISA R 783", new Guid("59cd42a7-4cb7-4f79-8831-c0d2152870f9"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PROFILATRICE MATISA R 783" },
                    { new Guid("38dae7c6-6048-4e2c-894d-745fc35c7a26"), "MAC026", "PROFILATRICE DONELLI PSD 4", new Guid("59cd42a7-4cb7-4f79-8831-c0d2152870f9"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PROFILATRICE DONELLI PSD 4" },
                    { new Guid("6fb7c44e-6cc4-4bea-9360-cf6a3064a79b"), "MAC032", "PROFILATRICE PLASSER SP 80", new Guid("59cd42a7-4cb7-4f79-8831-c0d2152870f9"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PROFILATRICE PLASSER SP 80" },
                    { new Guid("0522697c-4ff7-4d82-a823-8df019dd7a69"), "MAC033", "PROFILATRICE PLASSER SP 90", new Guid("59cd42a7-4cb7-4f79-8831-c0d2152870f9"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PROFILATRICE PLASSER SP 90" },
                    { new Guid("b0c93a74-0926-4c7a-b338-87907a73dd0a"), "MAC037", "PROFILATRICE RD7 a scart ridotto", new Guid("59cd42a7-4cb7-4f79-8831-c0d2152870f9"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PROFILATRICE RD7 a scart ridotto" },
                    { new Guid("8379f46b-b0d4-4672-9ce5-251ff5d82735"), "MAC027", "PROFILATRICE GRL 400 a scart ridotto", new Guid("59cd42a7-4cb7-4f79-8831-c0d2152870f9"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PROFILATRICE GRL 400 a scart ridotto" },
                    { new Guid("cf4a75f7-090a-4c41-acab-1ff0737e79dc"), "MAC025", "PROFILATRICE DONELLI", new Guid("59cd42a7-4cb7-4f79-8831-c0d2152870f9"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PROFILATRICE DONELLI" },
                    { new Guid("87b219f0-2e84-4a69-ab1c-bd66b145eed9"), "MAC038", "PROFILATRICE SP 120", new Guid("59cd42a7-4cb7-4f79-8831-c0d2152870f9"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PROFILATRICE SP 120" },
                    { new Guid("a63e6f15-ba68-4b8f-b9a0-8e7bd9aa0157"), "MAC036", "PROFILATRICE R 783 a scart ridotto", new Guid("59cd42a7-4cb7-4f79-8831-c0d2152870f9"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PROFILATRICE R 783 a scart ridotto" },
                    { new Guid("f21b0dde-7c63-4d57-a320-444e11443014"), "MAC052", "RINCALZATRICE LINEA E SCAMBI ZAMIR 08 275 a scart ridotto", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA E SCAMBI ZAMIR 08 275 a scart ridotto" },
                    { new Guid("9ee8c188-5e8b-4350-bec2-482699cd8b52"), "MAC057", "RINCALZATRICE LINEA PLASSER 08 32", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA PLASSER 08 32" },
                    { new Guid("f33d0fca-559c-4af9-a117-48ab5ccc7426"), "MAC058", "RINCALZATRICE LINEA PLASSER 09 32", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA PLASSER 09 32" },
                    { new Guid("0425291b-8e4a-4121-93a2-29ffc46d852a"), "MAC090", "RINCALZATRICE SCAMBI ZAMIR 08 16 sna", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE SCAMBI ZAMIR 08 16 sna" },
                    { new Guid("06e7e85b-887a-4a27-91b2-0bfe87d90136"), "MAC114", "TRENO RINNOVATORE", new Guid("942ed7b9-15d2-4241-ad71-ad17ed3ff2e8"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "TRENO RINNOVATORE" },
                    { new Guid("ce74aab8-2afb-41d1-a561-cc9a0c9d45b2"), "MAC100", "RINCAZATRICE MATISA B 30", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCAZATRICE MATISA B 30" },
                    { new Guid("d941fad6-1db4-44b2-a5c3-85a7a4ac714d"), "MAC040", "RINCALZATRICE 08 16 SNA", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE 08 16 SNA" },
                    { new Guid("c5aa52de-8e30-4737-a946-f213279cb23e"), "MAC051", "RINCALZATRICE LINEA E SCAMBI PLASSER 09 4S - E3", new Guid("7511ac24-b75b-445a-abf2-3f871204ecbd"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA E SCAMBI PLASSER 09 4S - E3" },
                    { new Guid("3bbb03b7-e072-4457-8615-fc01e26fb17a"), "MAC102", "RISANATRICE C 330", new Guid("860a8bdd-4d2b-488b-ab08-748c4914a042"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RISANATRICE C 330" },
                    { new Guid("62884cf6-b54a-48f3-9238-6a01447bfe79"), "MAC103", "RISANATRICE C 45", new Guid("860a8bdd-4d2b-488b-ab08-748c4914a042"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RISANATRICE C 45" },
                    { new Guid("9c5af769-db62-445b-8df9-cf5854eef662"), "MAC105", "RISANATRICE RM 80", new Guid("860a8bdd-4d2b-488b-ab08-748c4914a042"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RISANATRICE RM 80" },
                    { new Guid("48df9bf3-ef3a-47fb-82f4-a00472ed055a"), "MAC101", "RISANATRICE C 14", new Guid("860a8bdd-4d2b-488b-ab08-748c4914a042"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RISANATRICE C 14" },
                    { new Guid("06d40e0c-3531-4825-abee-bc71f658d9ca"), "MAC101", "RISANATRICE C 14", new Guid("428769de-f4d9-41f3-b205-647666ddc864"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RISANATRICE C 14" },
                    { new Guid("bb0e96a5-f38b-4633-bfd5-7003e2be69d9"), "MAC102", "RISANATRICE C 330", new Guid("428769de-f4d9-41f3-b205-647666ddc864"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RISANATRICE C 330" },
                    { new Guid("040b9ecb-b82a-4b27-8d90-b9c00102f554"), "MAC081", "RINCALZATRICE PLASSER R 09 4S 32", new Guid("7511ac24-b75b-445a-abf2-3f871204ecbd"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE PLASSER R 09 4S 32" },
                    { new Guid("3cac80e5-8042-411b-a80b-e8a578141715"), "MAC103", "RISANATRICE C 45", new Guid("428769de-f4d9-41f3-b205-647666ddc864"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RISANATRICE C 45" },
                    { new Guid("88ae3346-a6bb-4dc3-86b7-0fd1ca32d3c0"), "MAC104", "RISANATRICE C330", new Guid("428769de-f4d9-41f3-b205-647666ddc864"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RISANATRICE C330" },
                    { new Guid("85d0748b-d2a9-436a-9bce-5cb932445807"), "MAC110", "SALDATRICE PLASSER APT 600", new Guid("aac31f28-cea2-4205-ac52-e003f4ed5d72"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "SALDATRICE PLASSER APT 600" },
                    { new Guid("2e348c70-a5b5-4c8c-aedd-97f85124d9a9"), "MAC111", "SALDATRICE PLASSER K 355 APT", new Guid("aac31f28-cea2-4205-ac52-e003f4ed5d72"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "SALDATRICE PLASSER K 355 APT" },
                    { new Guid("44bf21c3-884e-4e12-99c8-97a5f4b53076"), "MAC113", "SALDATRICE VAIACAR SPARK RAIL", new Guid("aac31f28-cea2-4205-ac52-e003f4ed5d72"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "SALDATRICE VAIACAR SPARK RAIL" },
                    { new Guid("da0cb5d5-fd38-4090-b3eb-ab0dba460eb2"), "MAC108", "SALDATRICE A SCINTILLIO ATP 600", new Guid("aac31f28-cea2-4205-ac52-e003f4ed5d72"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "SALDATRICE A SCINTILLIO ATP 600" },
                    { new Guid("bbfb8002-bb6d-47a6-923d-fbe9b77be5e4"), "MAC109", "SALDATRICE ATP 600", new Guid("aac31f28-cea2-4205-ac52-e003f4ed5d72"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "SALDATRICE ATP 600" },
                    { new Guid("2d456715-4bee-45ec-a9b4-ceefbf3f8e3e"), "MAC112", "SALDATRICE SPARK RAIL", new Guid("aac31f28-cea2-4205-ac52-e003f4ed5d72"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "SALDATRICE SPARK RAIL" },
                    { new Guid("55de9419-f577-482d-8dd6-de474fe2a3de"), "MAC105", "RISANATRICE RM 80", new Guid("428769de-f4d9-41f3-b205-647666ddc864"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RISANATRICE RM 80" },
                    { new Guid("fdc5d5d6-7cf1-46a0-b9dd-fddc22d661ab"), "MAC096", "RINCALZATRICE ZAMIR 08 16 SNA", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE ZAMIR 08 16 SNA" },
                    { new Guid("2551f3a5-442b-481c-bc49-5895d0ec2ac5"), "MAC081", "RINCALZATRICE PLASSER R 09 4S 32", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE PLASSER R 09 4S 32" },
                    { new Guid("bb535212-eb21-4592-ab5c-92c6f676baf6"), "MAC071", "RINCALZATRICE MATISA B 45", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE MATISA B 45" },
                    { new Guid("a7988ec4-0f5c-4a8a-a0ea-35ef2c87265d"), "MAC042", "RINCALZATRICE 08 275 SP", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE 08 275 SP" },
                    { new Guid("50177834-6d57-42b4-9776-7a94375a34ec"), "MAC073", "RINCALZATRICE PLASSER 08", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE PLASSER 08" },
                    { new Guid("cd0a3a74-0aae-4043-9d1e-eec6729fd581"), "MAC075", "RINCALZATRICE PLASSER 08 275 a scart ridotto", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE PLASSER 08 275 a scart ridotto" },
                    { new Guid("c6d9c1ef-f158-4bed-a2b2-2922cdb5824d"), "MAC079", "RINCALZATRICE PLASSER 09 4S - 32", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE PLASSER 09 4S - 32" },
                    { new Guid("8a87b42f-fd44-458a-a199-58d41489e24c"), "MAC046", "RINCALZATRICE LINE E SCAMBI ZAMIR 08 275 a scart ridotto", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINE E SCAMBI ZAMIR 08 275 a scart ridotto" },
                    { new Guid("bc41637c-342e-449f-8dd5-023ca43a6c34"), "MAC082", "RINCALZATRICE SCAMBI 08 16 SNA", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE SCAMBI 08 16 SNA" },
                    { new Guid("42d1c15b-2326-4502-9818-038b9e39025c"), "MAC084", "RINCALZATRICE SCAMBI 08 275 SP", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE SCAMBI 08 275 SP" },
                    { new Guid("52876fd1-99fb-434d-932f-ea508501d96b"), "MAC048", "RINCALZATRICE LINEA E SCAMBI 08 275 a scart ridotto", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA E SCAMBI 08 275 a scart ridotto" },
                    { new Guid("4ba3c314-eb6f-4526-ac48-12d207131775"), "MAC067", "RINCALZATRICE MATISA B 38 a scart ridotto", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE MATISA B 38 a scart ridotto" },
                    { new Guid("95cb7185-9d2c-4b42-9723-f70053168099"), "MAC068", "RINCALZATRICE MATISA B 40", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE MATISA B 40" },
                    { new Guid("6f13d824-6295-4d89-a392-88b29b9c9075"), "MAC069", "RINCALZATRICE MATISA B 40 D", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE MATISA B 40 D" },
                    { new Guid("810d286a-2c1c-458e-b603-b0dfe651c81e"), "MAC085", "RINCALZATRICE SCAMBI MATISA B 30", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE SCAMBI MATISA B 30" },
                    { new Guid("ae3a3c28-1568-4d16-8af7-08b799d70156"), "MAC099", "RINCALZATRICE ZAMIR B 40 DE", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE ZAMIR B 40 DE" },
                    { new Guid("ed418961-9e93-441b-9271-febd49540ef8"), "MAC078", "RINCALZATRICE PLASSER 09 3X", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE PLASSER 09 3X" },
                    { new Guid("2b4a9a2f-c4c2-4262-897c-66f21cc9bbde"), "MAC065", "RINCALZATRICE MATISA B 242", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE MATISA B 242" },
                    { new Guid("5d37f2b7-fdf5-4b32-b273-9124ac2c3315"), "MAC093", "RINCALZATRICE SCAMBI ZAMIR B40 DE", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE SCAMBI ZAMIR B40 DE" },
                    { new Guid("4a9ae1bb-2ddc-4d67-9a87-6f5aeefe8419"), "MAC070", "RINCALZATRICE MATISA B 40 UE", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE MATISA B 40 UE" },
                    { new Guid("dd79c4b8-7a5d-491f-9527-250b6cbd6d9f"), "MAC028", "PROFILATRICE MATISA R 21", new Guid("59cd42a7-4cb7-4f79-8831-c0d2152870f9"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PROFILATRICE MATISA R 21" },
                    { new Guid("089db731-a2aa-4eb3-b3b3-96d19e0c6069"), "MAC039", "RINCALATRICE PLASSER 08 32", new Guid("e9f40781-ecbb-4bf8-bb1a-7df80835e87e"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALATRICE PLASSER 08 32" },
                    { new Guid("ca792536-e2b2-4ede-bc21-3da6d844bbda"), "MAC024", "PROFILATRCE R 12", new Guid("59cd42a7-4cb7-4f79-8831-c0d2152870f9"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PROFILATRCE R 12" },
                    { new Guid("36cbcc55-a3d1-4a15-bd24-540851fb8341"), "MAC021", "PORTALINI SCAMBI", new Guid("6bf78f3c-46af-4fd7-a7b2-b5d86bc2e77f"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PORTALINI SCAMBI" },
                    { new Guid("05e59c21-94e8-4e4a-88f3-3c480b1c436b"), "MAC074", "RINCALZATRICE PLASSER 08 275", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE PLASSER 08 275" },
                    { new Guid("d6559610-c471-412d-9e8a-391f568e5eb0"), "MAC076", "RINCALZATRICE PLASSER 08 32", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE PLASSER 08 32" },
                    { new Guid("d2123686-c742-4445-9dde-417035c6a379"), "MAC090", "RINCALZATRICE SCAMBI ZAMIR 08 16 sna", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE SCAMBI ZAMIR 08 16 sna" },
                    { new Guid("98a80913-83db-489c-aec3-b80bbe02bb43"), "MAC094", "RINCALZATRICE SCAMBI ZAMIR SNA", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE SCAMBI ZAMIR SNA" },
                    { new Guid("c63aec34-841c-46f9-bf82-e3c964988ac7"), "MAC066", "RINCALZATRICE MATISA B 30", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE MATISA B 30" },
                    { new Guid("c190c36d-3b70-47fa-856d-5a229fce286d"), "MAC086", "RINCALZATRICE SCAMBI MATISA B 40 UE", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE SCAMBI MATISA B 40 UE" },
                    { new Guid("3cfae45e-d451-4ede-addb-4be8bc5e409a"), "MAC088", "RINCALZATRICE SCAMBI PLASSER 08 32", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE SCAMBI PLASSER 08 32" },
                    { new Guid("32ef09b2-1262-49c8-b9d9-97c268f860f0"), "MAC092", "RINCALZATRICE SCAMBI ZAMIR 08 275 SP", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE SCAMBI ZAMIR 08 275 SP" },
                    { new Guid("eb98dee1-6b38-4d90-876f-9e6b911b43a3"), "MAC040", "RINCALZATRICE 08 16 SNA", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE 08 16 SNA" },
                    { new Guid("95a138c3-01b2-49bd-aa83-e90ac8e0f140"), "MAC081", "RINCALZATRICE PLASSER R 09 4S 32", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE PLASSER R 09 4S 32" },
                    { new Guid("69ff5f41-9406-46c0-a0e8-27b4cd14629c"), "MAC106", "RNCALZATRICE 08 275 SP", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RNCALZATRICE 08 275 SP" },
                    { new Guid("196bfb4b-d0af-4ef9-90f5-414385a7d96b"), "MAC042", "RINCALZATRICE 08 275 SP", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE 08 275 SP" },
                    { new Guid("887d4bac-8467-4db7-bb31-ce8259d58693"), "MAC073", "RINCALZATRICE PLASSER 08", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE PLASSER 08" },
                    { new Guid("0e2ad871-a22d-4cc7-b05d-cd05fa24bd45"), "MAC075", "RINCALZATRICE PLASSER 08 275 a scart ridotto", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE PLASSER 08 275 a scart ridotto" },
                    { new Guid("81963b4d-2b2b-42ae-8a88-d3daeafaec38"), "MAC048", "RINCALZATRICE LINEA E SCAMBI 08 275 a scart ridotto", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA E SCAMBI 08 275 a scart ridotto" },
                    { new Guid("76bb3aae-9b65-4784-93a7-e59ceb60670c"), "MAC056", "RINCALZATRICE LINEA MATISA B 45", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA MATISA B 45" },
                    { new Guid("da612103-0c6f-46bb-a192-7f92b17e525b"), "MAC041", "RINCALZATRICE 08 275 a scart ridotto", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE 08 275 a scart ridotto" },
                    { new Guid("4a1604b4-1985-4bfa-acaf-fc6b94acea4f"), "MAC091", "RINCALZATRICE SCAMBI ZAMIR 08 275 a scart ridotto", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE SCAMBI ZAMIR 08 275 a scart ridotto" },
                    { new Guid("d788cd9e-60ef-47ca-8ea7-653b5e07186c"), "MAC089", "RINCALZATRICE SCAMBI PLASSER 09 4S 32", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE SCAMBI PLASSER 09 4S 32" },
                    { new Guid("f09b27dd-0b08-48e6-aa2c-1f2772742076"), "MAC064", "RINCALZATRICE LINEA-SCAMBI ZAMIR 08 275 FS", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA-SCAMBI ZAMIR 08 275 FS" },
                    { new Guid("6ca6fd80-b275-44f6-a1e3-a85a7e283d9d"), "MAC006", "CARRI RISANATRICE", new Guid("44aec8ba-48d8-49b2-a42c-b8ba80b3c508"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "CARRI RISANATRICE" },
                    { new Guid("c8b73e1d-c943-407a-bbfd-7f12a28f53c8"), "MAC114", "TRENO RINNOVATORE", new Guid("b06b1a62-da0b-43ed-add8-89439476ad72"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "TRENO RINNOVATORE" },
                    { new Guid("8aa8ab4e-b3e4-44b3-bac8-6a9c0bfd036a"), "MAC107", "RUOTA", new Guid("b06b1a62-da0b-43ed-add8-89439476ad72"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RUOTA" },
                    { new Guid("ced6f581-83a9-4784-8606-31fe18ec557c"), "MAC003", "CARICATORI COLMAR TUTTI", new Guid("c28da311-584a-4eae-a1ce-4a3884d0f921"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "CARICATORI COLMAR TUTTI" },
                    { new Guid("4b7b52e0-f572-4b57-8e8c-72c194ce2a83"), "MAC004", "CARICATORI VAIACAR TUTTI", new Guid("c28da311-584a-4eae-a1ce-4a3884d0f921"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "CARICATORI VAIACAR TUTTI" },
                    { new Guid("dacbd747-f88c-468d-b9da-f6737ef093ad"), "MAC001", "CARICATORE ENZO LA FALCO", new Guid("c28da311-584a-4eae-a1ce-4a3884d0f921"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "CARICATORE ENZO LA FALCO" },
                    { new Guid("b368b6a9-606b-4222-a0e6-616229c7f563"), "MAC002", "CARICATORE KGT DONELLI", new Guid("c28da311-584a-4eae-a1ce-4a3884d0f921"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "CARICATORE KGT DONELLI" },
                    { new Guid("b03394f4-2f74-4732-9199-1631b95bf495"), "MAC005", "CARRELLO WINDHOFF", new Guid("58e546e7-57ea-4e2b-bf68-9bcb20744a67"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "CARRELLO WINDHOFF" },
                    { new Guid("a3ce68f4-27e3-4dcb-a2dc-f663dba53616"), "MAC061", "RINCALZATRICE LINEA ZAMIR B40 DE", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA ZAMIR B40 DE" },
                    { new Guid("317ab361-55d0-491e-87a2-02dc693cb925"), "MAC093", "RINCALZATRICE SCAMBI ZAMIR B40 DE", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE SCAMBI ZAMIR B40 DE" },
                    { new Guid("1024eee5-a280-49fc-a2bb-3e3e6ed137a0"), "MAC050", "RINCALZATRICE LINEA E SCAMBI PLASSER 09 4S - 32", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA E SCAMBI PLASSER 09 4S - 32" },
                    { new Guid("5d865ac7-c7b2-47e3-ac6e-93d8bb4eecd2"), "MAC051", "RINCALZATRICE LINEA E SCAMBI PLASSER 09 4S - E3", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA E SCAMBI PLASSER 09 4S - E3" },
                    { new Guid("6bab2616-3d7a-4236-bc96-c17553385525"), "MAC057", "RINCALZATRICE LINEA PLASSER 08 32", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA PLASSER 08 32" },
                    { new Guid("9b139406-7786-47bd-98d3-a93854099bec"), "MAC058", "RINCALZATRICE LINEA PLASSER 09 32", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA PLASSER 09 32" },
                    { new Guid("22872ef5-ce2f-40f4-9970-b620c2a1e01d"), "MAC059", "RINCALZATRICE LINEA PLASSER 09 3X", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA PLASSER 09 3X" },
                    { new Guid("7dad2875-a2c8-4f66-900c-7f9728d6c53f"), "MAC060", "RINCALZATRICE LINEA ZAMIR 08 SNA a scart ridotto", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA ZAMIR 08 SNA a scart ridotto" },
                    { new Guid("3907ff53-f9fa-467b-8c64-0b0a9a933de5"), "MAC063", "RINCALZATRICE LINEA-SCAMBI ZAMIR 08 275 a scart ridotto", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA-SCAMBI ZAMIR 08 275 a scart ridotto" },
                    { new Guid("b4c58077-994a-4a76-9437-8f51859be162"), "MAC067", "RINCALZATRICE MATISA B 38 a scart ridotto", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE MATISA B 38 a scart ridotto" },
                    { new Guid("54c52f3e-8961-4496-b2ac-cb9d65158cc2"), "MAC068", "RINCALZATRICE MATISA B 40", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE MATISA B 40" },
                    { new Guid("8e6d153e-8b5f-4f3e-99d5-170f4476426b"), "MAC069", "RINCALZATRICE MATISA B 40 D", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE MATISA B 40 D" },
                    { new Guid("ccd23e75-d179-4b8e-b517-5d436793219c"), "MAC082", "RINCALZATRICE SCAMBI 08 16 SNA", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE SCAMBI 08 16 SNA" },
                    { new Guid("9d5584f9-3aef-4d43-9b64-8785da8719f7"), "MAC012", "LOCOMOTORE COCKERIL AUGREE 2200 CV", new Guid("dcb5a0b8-2272-4ca0-b3b3-8a621953d165"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "LOCOMOTORE COCKERIL AUGREE 2200 CV" },
                    { new Guid("a8f4338e-c5dd-4471-bac7-9818519def31"), "MAC009", "LOCOMOTORE 500 CV", new Guid("dcb5a0b8-2272-4ca0-b3b3-8a621953d165"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "LOCOMOTORE 500 CV" },
                    { new Guid("c4c4ec7f-558e-49dc-8349-ab1fb6ddb375"), "MAC013", "LOCOMOTORE CZ LOKO", new Guid("dcb5a0b8-2272-4ca0-b3b3-8a621953d165"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "LOCOMOTORE CZ LOKO" },
                    { new Guid("1d72a492-84a3-4506-9dc4-8a26691b3c44"), "MAC015", "LOCOMOTORE MAK CATERPILLAR", new Guid("dcb5a0b8-2272-4ca0-b3b3-8a621953d165"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "LOCOMOTORE MAK CATERPILLAR" },
                    { new Guid("424ba81a-9e5c-4941-9966-4a7e8537058e"), "MAC020", "LOCOMOTORE TK", new Guid("dcb5a0b8-2272-4ca0-b3b3-8a621953d165"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "LOCOMOTORE TK" },
                    { new Guid("8884c89b-8180-4696-81b0-50a86aded6aa"), "MAC007", "LOCOMOTORE 128 a scartamento ridotto", new Guid("dcb5a0b8-2272-4ca0-b3b3-8a621953d165"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "LOCOMOTORE 128 a scartamento ridotto" },
                    { new Guid("47f4115e-f44f-488f-b250-2a9260843876"), "MAC010", "LOCOMOTORE CF 200 a scartamento ridotto", new Guid("dcb5a0b8-2272-4ca0-b3b3-8a621953d165"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "LOCOMOTORE CF 200 a scartamento ridotto" },
                    { new Guid("bf473352-bb57-4e12-9788-ac1be55d8d63"), "MAC016", "LOCOMOTORE T 05 a scart ridotto", new Guid("dcb5a0b8-2272-4ca0-b3b3-8a621953d165"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "LOCOMOTORE T 05 a scart ridotto" },
                    { new Guid("1be6c30a-d34c-414a-b8f0-16e657fa48ae"), "MAC017", "LOCOMOTORE T 07 a scartamento ridotto", new Guid("dcb5a0b8-2272-4ca0-b3b3-8a621953d165"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "LOCOMOTORE T 07 a scartamento ridotto" },
                    { new Guid("6de211c0-779f-49e8-bc3f-c8e78ed47785"), "MAC018", "LOCOMOTORE T 08 a scartamento ridotto", new Guid("dcb5a0b8-2272-4ca0-b3b3-8a621953d165"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "LOCOMOTORE T 08 a scartamento ridotto" },
                    { new Guid("d4e1b298-9829-47b7-af88-424913d4f4a7"), "MAC014", "LOCOMOTORE LM 4FC", new Guid("dcb5a0b8-2272-4ca0-b3b3-8a621953d165"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "LOCOMOTORE LM 4FC" },
                    { new Guid("85a370b0-a0f3-49d8-a007-9462176294fb"), "MAC019", "LOCOMOTORE T 09 a scartamento ridotto", new Guid("dcb5a0b8-2272-4ca0-b3b3-8a621953d165"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "LOCOMOTORE T 09 a scartamento ridotto" },
                    { new Guid("37e0e2b5-60e4-4c2c-8186-c3fcfdefc6b4"), "MAC051", "RINCALZATRICE LINEA E SCAMBI PLASSER 09 4S - E3", new Guid("71be202e-5de3-4623-b2e8-db24a2f67594"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA E SCAMBI PLASSER 09 4S - E3" },
                    { new Guid("2923654f-0e1c-45f7-b0b6-9d8c05d03a4e"), "MAC050", "RINCALZATRICE LINEA E SCAMBI PLASSER 09 4S - 32", new Guid("71be202e-5de3-4623-b2e8-db24a2f67594"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA E SCAMBI PLASSER 09 4S - 32" },
                    { new Guid("947355be-70a7-4a29-952b-6389062c12b9"), "MAC089", "RINCALZATRICE SCAMBI PLASSER 09 4S 32", new Guid("71be202e-5de3-4623-b2e8-db24a2f67594"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE SCAMBI PLASSER 09 4S 32" },
                    { new Guid("6411e480-837f-48e3-930a-0401d5aba40b"), "MAC079", "RINCALZATRICE PLASSER 09 4S - 32", new Guid("71be202e-5de3-4623-b2e8-db24a2f67594"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE PLASSER 09 4S - 32" },
                    { new Guid("cfaa7b52-e61b-452f-a04c-5d17511980c3"), "MAC021", "PORTALINI SCAMBI", new Guid("ea052b6c-42f4-4b8c-91d8-bfd4f22ab189"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PORTALINI SCAMBI" },
                    { new Guid("7d44cd85-27d3-4aa2-9cff-dc887f84d5c2"), "MAC011", "LOCOMOTORE COCKERIL AUGREE 1400 CV", new Guid("dcb5a0b8-2272-4ca0-b3b3-8a621953d165"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "LOCOMOTORE COCKERIL AUGREE 1400 CV" },
                    { new Guid("b6fb560b-30e4-4cd0-a9c0-58e890031841"), "MAC031", "PROFILATRICE MATISA R 783 a scart ridotto", new Guid("59cd42a7-4cb7-4f79-8831-c0d2152870f9"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PROFILATRICE MATISA R 783 a scart ridotto" },
                    { new Guid("b2cf39c6-4d53-49d6-b885-3347167c3d1a"), "MAC008", "LOCOMOTORE 128 CV KOFF II", new Guid("dcb5a0b8-2272-4ca0-b3b3-8a621953d165"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "LOCOMOTORE 128 CV KOFF II" },
                    { new Guid("d0f1bbb1-0371-4336-81c9-ff86280e8008"), "MAC043", "RINCALZATRICE 08 32", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE 08 32" },
                    { new Guid("ead89b32-4f79-463c-9da0-b0ae63f56d2c"), "MAC084", "RINCALZATRICE SCAMBI 08 275 SP", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE SCAMBI 08 275 SP" },
                    { new Guid("addf7e03-08a0-46e8-a161-cc333ce23724"), "MAC085", "RINCALZATRICE SCAMBI MATISA B 30", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE SCAMBI MATISA B 30" },
                    { new Guid("04464813-4bc0-417e-832f-9d6272e71aba"), "MAC099", "RINCALZATRICE ZAMIR B 40 DE", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE ZAMIR B 40 DE" },
                    { new Guid("4e29fe86-a5fd-48fc-a2c3-9ed772476833"), "MAC045", "RINCALZATRICE BEAWEAR 700", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE BEAWEAR 700" },
                    { new Guid("1a5456b2-c9ab-43fa-9970-315671c2b824"), "MAC053", "RINCALZATRICE LINEA MATISA B 242", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA MATISA B 242" },
                    { new Guid("ff854d73-0955-418b-8e4e-d2eabb02b3fa"), "MAC072", "RINCALZATRICE PLASSE 09 3X ", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE PLASSE 09 3X " },
                    { new Guid("32c9f305-1f2c-44ba-a906-6a1236026aa9"), "MAC052", "RINCALZATRICE LINEA E SCAMBI ZAMIR 08 275 a scart ridotto", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA E SCAMBI ZAMIR 08 275 a scart ridotto" },
                    { new Guid("e25bb81b-5ab1-48ad-b2f5-f8ef9e344f37"), "MAC077", "RINCALZATRICE PLASSER 09 32", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE PLASSER 09 32" },
                    { new Guid("e19ac643-5ca1-4d2f-9291-6f0ad67b0396"), "MAC078", "RINCALZATRICE PLASSER 09 3X", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE PLASSER 09 3X" },
                    { new Guid("a40a066b-c59c-45ff-a1ec-caa9ff2235c8"), "MAC095", "RINCALZATRICE ZAMIR 08 16 a scart ridotto", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE ZAMIR 08 16 a scart ridotto" },
                    { new Guid("0f0e66c6-c69d-4c06-9f62-3a1123941de3"), "MAC096", "RINCALZATRICE ZAMIR 08 16 SNA", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE ZAMIR 08 16 SNA" },
                    { new Guid("36bb906f-ae8f-4cdf-b7b5-3eeb8649a115"), "MAC097", "RINCALZATRICE ZAMIR 08 275 a scart ridotto", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE ZAMIR 08 275 a scart ridotto" },
                    { new Guid("1fc6693f-fc73-4af5-9089-2274902b26fd"), "MAC098", "RINCALZATRICE ZAMIR 08 275 SP", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE ZAMIR 08 275 SP" },
                    { new Guid("8bffad03-457b-47d8-b960-8eb6c7e2a461"), "MAC054", "RINCALZATRICE LINEA MATISA B 30", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA MATISA B 30" },
                    { new Guid("b4c315a9-3a05-493e-9b49-a68ea15b66be"), "MAC055", "RINCALZATRICE LINEA MATISA B 40 D", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE LINEA MATISA B 40 D" },
                    { new Guid("6532d82b-02a4-43b4-9293-9a344e26240d"), "MAC065", "RINCALZATRICE MATISA B 242", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE MATISA B 242" },
                    { new Guid("476427cc-dfd1-488d-be0f-7c563b3f6d2d"), "MAC080", "RINCALZATRICE PLASSER 09 4S E3", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE PLASSER 09 4S E3" },
                    { new Guid("93bc9cdd-9ada-49f5-ba80-a78d93da9cf8"), "MAC083", "RINCALZATRICE SCAMBI 08 275", new Guid("81b5d5f0-57e7-4514-8fbf-145d5fb5afc4"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "RINCALZATRICE SCAMBI 08 275" },
                    { new Guid("83f7223a-c487-4994-a296-0dae28120cc4"), "MAC021", "PORTALINI SCAMBI", new Guid("5385c641-9a4e-4201-a47b-722209f41f40"), new Guid("4c299c7d-47c6-4282-9289-b281ce220feb"), "PORTALINI SCAMBI" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IDCompany",
                table: "AspNetUsers",
                column: "IDCompany");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_InternalCompanyReferenceId",
                table: "AspNetUsers",
                column: "InternalCompanyReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ModificatoDaId",
                table: "AspNetUsers",
                column: "ModificatoDaId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MultiTenantId",
                table: "AspNetUsers",
                column: "MultiTenantId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_C_ANA_Companies_MultiTenantId",
                table: "C_ANA_Companies",
                column: "MultiTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_C_ANA_Companies_ParentID",
                table: "C_ANA_Companies",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_C_Cantieri_MultiTenantId",
                table: "C_Cantieri",
                column: "MultiTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Cantieri_ProjectId",
                table: "C_Cantieri",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_C_ContractUser_CategoryCodeId",
                table: "C_ContractUser",
                column: "CategoryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_C_ContractUser_ContractTypeId",
                table: "C_ContractUser",
                column: "ContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_C_ContractUser_FineRapportoId",
                table: "C_ContractUser",
                column: "FineRapportoId");

            migrationBuilder.CreateIndex(
                name: "IX_C_ContractUser_LawNumberTypeId",
                table: "C_ContractUser",
                column: "LawNumberTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_C_ContractUser_LevelId",
                table: "C_ContractUser",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_C_ContractUser_MultiTenantId",
                table: "C_ContractUser",
                column: "MultiTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_C_ContractUser_OreDiLavoroTypeId",
                table: "C_ContractUser",
                column: "OreDiLavoroTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_C_ContractUser_RetribuzioneTypeId",
                table: "C_ContractUser",
                column: "RetribuzioneTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_C_ContractUser_UserId",
                table: "C_ContractUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Documents_DocumentTypeId",
                table: "C_Documents",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Documents_MultiTenantId",
                table: "C_Documents",
                column: "MultiTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Documents_UserId",
                table: "C_Documents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Domains_MultiTenantId",
                table: "C_Domains",
                column: "MultiTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Domains_ParentId",
                table: "C_Domains",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Giustificativi_MultiTenantId",
                table: "C_Giustificativi",
                column: "MultiTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_C_MalattiaUser_MultiTenantId",
                table: "C_MalattiaUser",
                column: "MultiTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_C_MalattiaUser_UserId",
                table: "C_MalattiaUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_C_MansioneMacchina_MansioneId",
                table: "C_MansioneMacchina",
                column: "MansioneId");

            migrationBuilder.CreateIndex(
                name: "IX_C_MansioneMacchina_MultiTenantId",
                table: "C_MansioneMacchina",
                column: "MultiTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Mansioni_MultiTenantId",
                table: "C_Mansioni",
                column: "MultiTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Projects_MultiTenantId",
                table: "C_Projects",
                column: "MultiTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Rel_FabbisognoMansione_CantiereId",
                table: "C_Rel_FabbisognoMansione",
                column: "CantiereId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Rel_FabbisognoMansione_MansioneId",
                table: "C_Rel_FabbisognoMansione",
                column: "MansioneId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Rel_FabbisognoMansione_MultiTenantId",
                table: "C_Rel_FabbisognoMansione",
                column: "MultiTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Rel_FabbisognoMansione_TurnoId",
                table: "C_Rel_FabbisognoMansione",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Rel_FabbisognoSicurezza_MultiTenantId",
                table: "C_Rel_FabbisognoSicurezza",
                column: "MultiTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Rel_FabbisognoSicurezza_SpecializzazioneId",
                table: "C_Rel_FabbisognoSicurezza",
                column: "SpecializzazioneId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Rel_MansioniUser_MansioneId",
                table: "C_Rel_MansioniUser",
                column: "MansioneId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Rel_MansioniUser_MansioneMacchinaId",
                table: "C_Rel_MansioniUser",
                column: "MansioneMacchinaId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Rel_MansioniUser_MultiTenantId",
                table: "C_Rel_MansioniUser",
                column: "MultiTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Rel_MansioniUser_UserId",
                table: "C_Rel_MansioniUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Rel_SpecializzazioniUser_MultiTenantId",
                table: "C_Rel_SpecializzazioniUser",
                column: "MultiTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Rel_SpecializzazioniUser_SpecializzazioneId",
                table: "C_Rel_SpecializzazioniUser",
                column: "SpecializzazioneId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Rel_SpecializzazioniUser_UserId",
                table: "C_Rel_SpecializzazioniUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Rel_TurniUsers_MansioneId",
                table: "C_Rel_TurniUsers",
                column: "MansioneId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Rel_TurniUsers_MultiTenantId",
                table: "C_Rel_TurniUsers",
                column: "MultiTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Rel_TurniUsers_SpecializzazioneId",
                table: "C_Rel_TurniUsers",
                column: "SpecializzazioneId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Rel_TurniUsers_TurnoId",
                table: "C_Rel_TurniUsers",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Rel_TurniUsers_UserId",
                table: "C_Rel_TurniUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Specializzazioni_MultiTenantId",
                table: "C_Specializzazioni",
                column: "MultiTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_C_TimeSheets_MultiTenantId",
                table: "C_TimeSheets",
                column: "MultiTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_C_TimeSheetsDailyReport_GiustificativoId",
                table: "C_TimeSheetsDailyReport",
                column: "GiustificativoId");

            migrationBuilder.CreateIndex(
                name: "IX_C_TimeSheetsDailyReport_MultiTenantId",
                table: "C_TimeSheetsDailyReport",
                column: "MultiTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_C_TimeSheetsDailyReport_TurnoId",
                table: "C_TimeSheetsDailyReport",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_C_TimeSheetsDailyReport_UserId",
                table: "C_TimeSheetsDailyReport",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Turni_CantiereId",
                table: "C_Turni",
                column: "CantiereId");

            migrationBuilder.CreateIndex(
                name: "IX_C_Turni_MultiTenantId",
                table: "C_Turni",
                column: "MultiTenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "C_Claims");

            migrationBuilder.DropTable(
                name: "C_ContractUser");

            migrationBuilder.DropTable(
                name: "C_Documents");

            migrationBuilder.DropTable(
                name: "C_MalattiaUser");

            migrationBuilder.DropTable(
                name: "C_Rel_FabbisognoMansione");

            migrationBuilder.DropTable(
                name: "C_Rel_FabbisognoSicurezza");

            migrationBuilder.DropTable(
                name: "C_Rel_MansioniUser");

            migrationBuilder.DropTable(
                name: "C_Rel_SpecializzazioniUser");

            migrationBuilder.DropTable(
                name: "C_Rel_TurniUsers");

            migrationBuilder.DropTable(
                name: "C_TimeSheets");

            migrationBuilder.DropTable(
                name: "C_TimeSheetsDailyReport");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "RemoteSetup");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "C_Domains");

            migrationBuilder.DropTable(
                name: "C_MansioneMacchina");

            migrationBuilder.DropTable(
                name: "C_Specializzazioni");

            migrationBuilder.DropTable(
                name: "C_Giustificativi");

            migrationBuilder.DropTable(
                name: "C_Turni");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "C_Mansioni");

            migrationBuilder.DropTable(
                name: "C_Cantieri");

            migrationBuilder.DropTable(
                name: "C_ANA_Companies");

            migrationBuilder.DropTable(
                name: "C_Projects");

            migrationBuilder.DropTable(
                name: "C_Multitenant");
        }
    }
}
