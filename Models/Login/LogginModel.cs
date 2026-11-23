using Drim.Interfaces.MVC.Data;
using Drim.Interfaces.MVC.Entities;
using Drim.Interfaces.MVC.ViewModels;
using System.Data;

namespace Drim.Interfaces.MVC.Models
{
    public class LoginModel
    {

        public LoginModel()
        {
            Login = new Login();
        }

        public Login Login { get; set; }

     


        public string RegistraUsuarioLogin(int UserId, string Ip)
        {
            string result = string.Empty;

            using (APPDRIMDbContext context = new APPDRIMDbContext())
            {
                context.UsuarioLogins.Add(new UsuarioLogins()
                {
                    UserId = UserId,
                    Ip = Ip,
                    FechaAcceso = DateTime.Now

                });

                context.SaveChanges();
            }

            return result;
        }


        //public bool AutentificateUser(string user, string pass, string ip)
        //{
        //    bool result = false;
        //    int userID = 0;

        //    try
        //    {
        //        using (DAL dal = new DAL(ConnectionCatalogDB.PDV))
        //        {

        //            dal.AddParameters("@user", user, SqlDbType.NVarChar, 100);
        //            dal.AddParameters("@pass", pass, SqlDbType.NVarChar, 100);
        //            dal.AddParameters("@IP", ip, SqlDbType.NVarChar, 50);
        //            dal.AddParameters("@IsValidation", pass, SqlDbType.Bit, 1, ParameterDirection.Output);
        //            dal.AddParameters("@Token", Login.Token, SqlDbType.NVarChar, 100, ParameterDirection.Output);
        //            dal.AddParameters("@IdUsuario", userID, SqlDbType.Int, 10, ParameterDirection.Output);

        //            dal.ExecuteQueries("dbo.AutentificateUser", CommandType.StoredProcedure);

        //            var parameterValidate = dal.GetParameter("@IsValidation");
        //            result = parameterValidate != null ? (bool)parameterValidate.Value : false;

        //            if (result)
        //            {
        //                var parameterToken = dal.GetParameter("@Token");
        //                Login.Token = parameterToken != null ? (string)parameterToken.Value : string.Empty;

        //                var parameterId = dal.GetParameter("@IdUsuario");
        //                Login.Usuario.UsuariID = parameterId != null ? (int)parameterId.Value : 0;
        //                Login.Usuario.NomUsuari = user;
        //                Login.Ip = ip;

        //            }
        //            else
        //            {
        //                Login.Usuario = null;
        //                Login.IsAutentification = false;
        //                result = false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result = false;
        //        Login.Usuario = null;
        //        Login.IsAutentification = false;
        //        Login.Error = ex.Message;
        //    }

        //    return result;
        //}



        //public Login LoadUserAutentificate()
        //{

        //    try
        //    {
        //        using (DAL dal = new DAL(ConnectionCatalogDB.PDV, true))
        //        {

        //            dal.AddParameters("@IdUsuario", Login.Usuario.UsuariID, SqlDbType.Int, 100);
        //            dal.AddParameters("@user", Login.Usuario.NomUsuari, SqlDbType.NVarChar, 100);
        //            dal.AddParameters("@pass", Login.Usuario.Contrasenya, SqlDbType.NVarChar, 100);
        //            dal.AddParameters("@IP", Login.Ip, SqlDbType.NVarChar, 100);
        //            dal.AddParameters("@Token", Login.Token, SqlDbType.NVarChar, 100);

        //            using (var rdr = dal.DataReader("dbo.GetAutentificateUser", CommandType.StoredProcedure))
        //            {
        //                while (rdr.Read())
        //                {

        //                    if (Login.Usuario.UsuariID == rdr.GetInt32(0))
        //                    {
        //                        Login.Usuario.NomComplet = rdr.GetString(1);

        //                        Login.IsAutentification = true;
        //                        Login.Usuario.Departaments = rdr.IsDBNull(4) ? string.Empty : rdr.GetString(4);
        //                    }
        //                    else
        //                    {
        //                        Login.IsAutentification = false;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Login.Error = ex.Message;
        //        Login.IsAutentification = false;
        //    }

        //    return Login;
        //}

    }
}
