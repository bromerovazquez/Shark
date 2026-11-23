using Drim.Interfaces.MVC.Entities;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace Drim.Interfaces.MVC.Helpers
{
    public static class EntornoHelper
    {
        private static readonly IConfiguration Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        public static string DatabaseConnectionTemplate => Configuration["Database:ConnectionTemplate"] ?? "";
        public static string DatabaseProductionServer => Configuration["Database:ProductionServer"] ?? "";
        public static string DatabaseDevelopmentServer => Configuration["Database:DevelopmentServer"] ?? "";
        public static string DatabaseProductionUser => Configuration["Database:ProductionUser"] ?? "";
        public static string DatabaseProductionPassword => Configuration["Database:ProductionPassword"] ?? "";
        public static string DatabaseDevelopmentUser => Configuration["Database:DevelopmentUser"] ?? "";
        public static string DatabaseDevelopmentPassword => Configuration["Database:DevelopmentPassword"] ?? "";
        public static string DatabaseA3LaboralServer => Configuration["Database:A3LaboralServer"] ?? "";
        public static string DatabaseAppDrimServer => Configuration["Database:AppDrimServer"] ?? "";
        public static string DatabaseA3CommonServer => Configuration["Database:A3CommonServer"] ?? "";
        public static string ElMundoDeDrimServer => Configuration["Database:ElMundoDeDrim:Server"] ?? "";
        public static string ElMundoDeDrimDatabase => Configuration["Database:ElMundoDeDrim:Database"] ?? "";
        public static string ElMundoDeDrimUser => Configuration["Database:ElMundoDeDrim:User"] ?? "";
        public static string ElMundoDeDrimPassword => Configuration["Database:ElMundoDeDrim:Password"] ?? "";
        public static string DataExchangeEndpoint => Configuration["ExternalServices:DataExchangeEndpoint"] ?? "";
        public static string HttpBaseAddress => Configuration["ExternalServices:HttpBaseAddress"] ?? "";
        public static string SmtpHost => Configuration["ExternalServices:Smtp:Host"] ?? "";
        public static int SmtpPort => Configuration.GetValue("ExternalServices:Smtp:Port", 587);
        public static bool SmtpEnableSsl => Configuration.GetValue("ExternalServices:Smtp:EnableSsl", false);

        public static string EmailsTEST
        {
            get
            {
                var emails = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("EmailsTEST").Value;
                return emails;
            }
        }


        public static string EmailsRRHHTEST
        {
            get
            {
                var emails = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("EmailsRRHHTEST").Value;
                return emails;
            }
        }


        public static bool IsProduccion
        {
            get
            {
                var entorno = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Entorno").Value;
                return entorno == "pro";
            }
        }
        public static int SegundosCheckProcessInterfaces
        {
            get
            {
                int _segundosCheckProcess = 10000;
                var segundosCheckProcess = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SegundosCheckProcessInterfaces").Value;

                int.TryParse(segundosCheckProcess, out _segundosCheckProcess);

                return _segundosCheckProcess;
            }
        }


        public static int SegundosCheckProcessLogInterfaces
        {
            get
            {
                int _segundosCheckProcess = 5000;
                var segundosCheckProcess = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SegundosCheckProcessLogInterfaces").Value;

                int.TryParse(segundosCheckProcess, out _segundosCheckProcess);

                return _segundosCheckProcess;
            }
        }


        public static DateTime FechaInicioCampaña
        {
            get
            {
                var _FechaInicioCampaña = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("FechaInicoCampana").Value;

                if (!string.IsNullOrEmpty(_FechaInicioCampaña) && _FechaInicioCampaña.IndexOf("/") != -1 && _FechaInicioCampaña.Split("/").Length == 2 && !string.IsNullOrEmpty(_FechaInicioCampaña.Split("/")[0]) && !string.IsNullOrEmpty(_FechaInicioCampaña.Split("/")[1]))
                {
                    var Dia = Convert.ToInt32(_FechaInicioCampaña.Split("/")[0]);
                    var Mes = Convert.ToInt32(_FechaInicioCampaña.Split("/")[1]);

                    return new DateTime(DateTime.Now.Year, Mes, Dia, 0, 0, 0);

                }
                else
                {

                    return new DateTime(DateTime.Now.Year, 11, 7, 0, 0, 0);

                }
            }
        }


        public static DateTime FechaFinCampaña
        {
            get
            {
                var _FechaFinCampaña = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("FechaFinCampana").Value;

                if (!string.IsNullOrEmpty(_FechaFinCampaña) && _FechaFinCampaña.IndexOf("/") != -1 && _FechaFinCampaña.Split("/").Length == 2 && !string.IsNullOrEmpty(_FechaFinCampaña.Split("/")[0]) && !string.IsNullOrEmpty(_FechaFinCampaña.Split("/")[1]))
                {
                    var Dia = Convert.ToInt32(_FechaFinCampaña.Split("/")[0]);
                    var Mes = Convert.ToInt32(_FechaFinCampaña.Split("/")[1]);

                    return new DateTime(DateTime.Now.Year + 1, Mes, Dia, 0, 0, 0);

                }
                else
                {

                    return new DateTime(DateTime.Now.Year + 1, 1, 8, 0, 0, 0);

                }
            }
        }


        public static string PathRootVirtual
        {
            get
            {
                string Seccion = "Path" + (EntornoHelper.IsProduccion ? "" : "Dev");

                var _LocalPathRoot = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection(Seccion).GetSection("PathRootVirtual").Value;

                return _LocalPathRoot;
            }
        }



        public static PathConfig Path_Origen_Manager
        {
            get
            {
                PathConfig pathConfig = new PathConfig()
                {
                    url = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Path_Origen_Manager").GetSection("url").Value,
                    usuario = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Path_Origen_Manager").GetSection("usuario").Value,
                    pass = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Path_Origen_Manager").GetSection("pass").Value
                };

                return pathConfig;
            }
        }


        public static PathConfig Path_Destino_Adaia
        {
            get
            {
                PathConfig pathConfig = new PathConfig()
                {
                    url = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Path_Destino_Adaia").GetSection("url").Value,
                    usuario = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Path_Destino_Adaia").GetSection("usuario").Value,
                    pass = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Path_Destino_Adaia").GetSection("pass").Value
                };

                return pathConfig;
            }
        }

        public static PathConfig Path_Origen_Adaia
        {
            get
            {
                PathConfig pathConfig = new PathConfig()
                {
                    url = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Path_Origen_Adaia").GetSection("url").Value,
                    usuario = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Path_Origen_Adaia").GetSection("usuario").Value,
                    pass = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Path_Origen_Adaia").GetSection("pass").Value
                };

                return pathConfig;
            }
        }

        public static PathConfig Path_Destino_Manager
        {
            get
            {
                PathConfig pathConfig = new PathConfig()
                {
                    url = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Path_Destino_Manager").GetSection("url").Value,
                    usuario = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Path_Destino_Manager").GetSection("usuario").Value,
                    pass = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Path_Destino_Manager").GetSection("pass").Value
                };

                return pathConfig;
            }
        }

        public static string EmailAccountAddress(RemoteEmailAccount account)
        {
            var section = account switch
            {
                RemoteEmailAccount.ControlHorario => "ControlHorario",
                RemoteEmailAccount.RecursosHumanos => "RecursosHumanos",
                RemoteEmailAccount.Seleccion => "Seleccion",
                RemoteEmailAccount.Contratacion => "Contratacion",
                RemoteEmailAccount.Informes => "Informes",
                _ => "RecursosHumanos"
            };
            return Configuration[$"EmailAccounts:{section}:Address"] ?? "";
        }

        public static string EmailAccountPassword(RemoteEmailAccount account)
        {
            var section = account switch
            {
                RemoteEmailAccount.ControlHorario => "ControlHorario",
                RemoteEmailAccount.RecursosHumanos => "RecursosHumanos",
                RemoteEmailAccount.Seleccion => "Seleccion",
                RemoteEmailAccount.Contratacion => "Contratacion",
                RemoteEmailAccount.Informes => "Informes",
                _ => ""
            };
            return Configuration[$"EmailAccounts:{section}:Password"] ?? "";
        }

        public enum RemoteEmailAccount
        {
            ControlHorario,
            RecursosHumanos,
            Seleccion,
            Contratacion,
            Informes
        }
    }
}
