using Drim.Interfaces.MVC.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Drim.Interfaces.MVC.Connection
{
    public class DAL : IDisposable
    {
        SqlConnection con;
        SqlCommand cmd;
        public List<SqlParameter> parameters;

        private bool _IsProduccion;



        public DAL()
        {

            con = new SqlConnection(ConnectionCatalogDB.PDV.GetConectionString());
            cmd = new SqlCommand();
            parameters = new List<SqlParameter>();
            con.Open();
        }

        public DAL(bool IsAsync)
        {

            con = new SqlConnection(ConnectionCatalogDB.PDV.GetConectionString());
            cmd = new SqlCommand();
            parameters = new List<SqlParameter>();
            if (IsAsync)
                con.OpenAsync();
            else
                con.Open();
        }

        public DAL(ConnectionCatalogDB connDB, bool IsProduccion)
        {
            this._IsProduccion = IsProduccion;
            con = new SqlConnection(connDB.GetConectionString(IsProduccion));
            cmd = new SqlCommand();
            parameters = new List<SqlParameter>();
            con.Open();
        }

        public DAL(ConnectionCatalogDB connDB)
        {

            con = new SqlConnection(connDB.GetConectionString());
            cmd = new SqlCommand();
            parameters = new List<SqlParameter>();
            con.Open();
        }

        public DAL(string conecctionString)
        {

            con = new SqlConnection(conecctionString); 
             cmd = new SqlCommand();
            parameters = new List<SqlParameter>();
            con.Open();
        }



        public void ExecuteQueries(string Query_, CommandType commandType)
        {
            LoadCommand(Query_, commandType);
            cmd.ExecuteNonQuery();
        }



        public object ExecuteScalar(string Query_, CommandType commandType)
        {
            LoadCommand(Query_, commandType);
            var result = cmd.ExecuteScalar();
            return result;
        }

        public async Task<int> ExecuteQueriesAsync(string Query_, CommandType commandType)
        {
            LoadCommand(Query_, commandType);
            return await cmd.ExecuteNonQueryAsync();
        }


        public SqlDataReader DataReader(string Query_, CommandType commandType)
        {
            LoadCommand(Query_, commandType);
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }


        public DataTable DataTable(string Query_, CommandType commandType)
        {
            LoadCommand(Query_, commandType);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable tabla = new DataTable();
            adapter.Fill(tabla);
            return tabla;
        }



        public async Task<SqlDataReader> DataReaderAsyn(string Query_, CommandType commandType)
        {
            LoadCommand(Query_, commandType);
            return await cmd.ExecuteReaderAsync();
        }


        public void AddParameters(string name, object value, SqlDbType sqltype, int size, ParameterDirection direccion = ParameterDirection.Input)
        {
            SqlParameter paramter = new SqlParameter(name, sqltype, size);
            paramter.Direction = direccion;
            paramter.Value = value;
            parameters.Add(paramter);
        }

        public SqlParameter GetParameter(string nameParameter)
        {
            return parameters.Where(x => x.ParameterName.ToLower() == nameParameter.ToLower()).FirstOrDefault();
        }


        public void Dispose()
        {
            con.Close();
            con.Dispose();
            con = null;
            cmd.Dispose();
            cmd = null;
            GC.SuppressFinalize(this);
        }

        private void LoadCommand(string query, CommandType commandType)
        {
            cmd.Connection = con;
            cmd.CommandTimeout = 0;
            cmd.CommandText = query;
            cmd.CommandType = commandType;
            if (parameters.Count() > 0)
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
            }
        }


    }





    public static class ExtenderDB
    {

        static string ip_Produccion => EntornoHelper.DatabaseProductionServer;
        static string ip_Desarrollo => EntornoHelper.DatabaseDevelopmentServer;

        static string Usuario => EntornoHelper.DatabaseProductionUser;
        static string Password => EntornoHelper.DatabaseProductionPassword;

        static string Usuario_Dev => EntornoHelper.DatabaseDevelopmentUser;
        static string Password_Dev => EntornoHelper.DatabaseDevelopmentPassword;

        static string connectionString => EntornoHelper.DatabaseConnectionTemplate;



        public static ConnectionCatalogDB GetDB(this string db) 
        {
            
            switch (db) 
            {
                case "PDV":
                    return ConnectionCatalogDB.PDV;
                case "WEB_DATOS":
                    return ConnectionCatalogDB.WEB_DATOS;
                case "WEB_PEDIDOS":
                    return ConnectionCatalogDB.WEB_PEDIDOS;
                case "DRIM":
                    return ConnectionCatalogDB.DRIM;
                case "G001":
                    return ConnectionCatalogDB.G001;
                default:
                    return ConnectionCatalogDB.G001;

            }

            
        }


        public static string GetConectionString(this ConnectionCatalogDB conn, bool? _IsProduccion = null)
        {
            bool isProduccion = _IsProduccion ?? EntornoHelper.IsProduccion;
            string ip = isProduccion ? ip_Produccion : ip_Desarrollo;
            string _Usuario = isProduccion ? Usuario : Usuario_Dev;
            string _Password = isProduccion ? Password : Password_Dev;

            switch (conn)
            {


                case ConnectionCatalogDB.G001:
                    //return "Persist Security Info=True;User ID=sa;Initial Catalog=G001;Data Source=" + ip + ";";
                    return string.Format(connectionString, _Password, _Usuario, "G001", ip);

                case ConnectionCatalogDB.PDV:
                    //return "Persist Security Info=True;User ID=sa;Initial Catalog=PDV;Data Source=" + ip + ";";
                    return string.Format(connectionString, _Password, _Usuario, "PDV", ip);

                case ConnectionCatalogDB.A3LABORAL:
                    //return "Persist Security Info=True;User ID=sa;Initial Catalog=A3LABORAL;Data Source=192.168.200.100;";
                    return string.Format(connectionString, _Password, _Usuario, "A3LABORAL", EntornoHelper.DatabaseA3LaboralServer);

                case ConnectionCatalogDB.WEB_DATOS:
                    //return "Persist Security Info=True;User ID=sa;Initial Catalog=WEB_DATOS;Data Source=" + ip + ";";
                    return string.Format(connectionString, _Password, _Usuario, "WEB_DATOS", ip);

                case ConnectionCatalogDB.WEB_ESTADISTICA:
                    //return "Persist Security Info=True;User ID=sa;Initial Catalog=WEB_ESTADISTICA;Data Source=" + ip + ";";
                    return string.Format(connectionString, _Password, _Usuario, "WEB_ESTADISTICA", ip);

                case ConnectionCatalogDB.WEB_PEDIDOS_HISTORICO:
                    //return "Persist Security Info=True;User ID=sa;Initial Catalog=WEB_PEDIDOS_HISTORICO;Data Source=" + ip + ";";
                    return string.Format(connectionString, _Password, _Usuario, "WEB_PEDIDOS_HISTORICO", ip);

                case ConnectionCatalogDB.WEB_PEDIDOS:
                    //return "Persist Security Info=True;User ID=sa;Initial Catalog=WEB_PEDIDOS;Data Source=" + ip + ";";
                    return string.Format(connectionString, _Password, _Usuario, "WEB_PEDIDOS", ip);

                case ConnectionCatalogDB.APPDRIM:
                    //return "Persist Security Info=True;User ID=sa;Initial Catalog=APPDRIM;Data Source=192.168.200.100;";
                    return string.Format(connectionString, _Password, _Usuario, "APPDRIM", EntornoHelper.DatabaseAppDrimServer);

                case ConnectionCatalogDB.DRIM:
                    //return "Persist Security Info=True;User ID=sa;Initial Catalog=DRIM;Data Source=" + ip + ";";
                    return string.Format(connectionString, _Password, _Usuario, "DRIM", ip);

                case ConnectionCatalogDB.A3COMMON:
                    //return "Persist Security Info=True;User ID=sa;Initial Catalog=A3COMMON;Data Source=192.168.200.100;";
                    return string.Format(connectionString, _Password, _Usuario, "A3COMMON", EntornoHelper.DatabaseA3CommonServer);

                case ConnectionCatalogDB.ELMUNDODEDRIM:
                    return $"server={EntornoHelper.ElMundoDeDrimServer}; database={EntornoHelper.ElMundoDeDrimDatabase}; user={EntornoHelper.ElMundoDeDrimUser}; password={EntornoHelper.ElMundoDeDrimPassword}";

                default:
                    //return "Persist Security Info=True;User ID=sa;Initial Catalog=G001;Data Source=" + ip + ";";
                    return string.Format(connectionString, _Password, _Usuario, "G001", "192.168.200.39");
            }
        }


        public static CommandType GetCommandType(this char tipo) 
        {
            switch (tipo) 
            {
                case 't':
                    return CommandType.Text;
                case 's':
                    return CommandType.StoredProcedure;
                default:
                    return CommandType.StoredProcedure;
            }
        }


        public static SqlDbType GetSqlDbType(this string tipo)
        {
            switch (tipo.ToLower())
            {
                case "bigint":
                    return SqlDbType.BigInt;
                case "int":
                    return SqlDbType.Int;
                case "nvarchar":
                    return SqlDbType.NVarChar;
                case "bit":
                    return SqlDbType.Bit;
                case "decimal":
                    return SqlDbType.Decimal;
                case "datetime":
                    return SqlDbType.DateTime;
                case "char":
                    return SqlDbType.Char;
                default:
                    return SqlDbType.NVarChar;
            }
        }
    }


    public enum ConnectionCatalogDB
    {
        G001 = 0,
        PDV = 1,
        A3LABORAL = 2,
        WEB_PEDIDOS = 3,
        WEB_DATOS = 4,
        WEB_PEDIDOS_HISTORICO = 5,
        WEB_ESTADISTICA = 6,
        APPDRIM = 7,
        DRIM = 8,
        A3COMMON = 9,
        ELMUNDODEDRIM = 10
    }






}