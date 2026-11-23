
using Drim.Interfaces.MVC.Connection;
using Drim.Interfaces.MVC.Data;
using Drim.Interfaces.MVC.Entities;
using Drim.Interfaces.MVC.ViewModels;
using System.Data;

namespace Drim.Interfaces.MVC.Models
{
    public class MenuRolModel
    {
        public MenuRolModel()
        {
            MenuRolList = new List<MenuRol>();
        }


        public List<MenuRol> MenuRolList { get; set; }



        //public void LoadMenuRol(int UserId)
        //{

        //    using (DAL dal = new DAL())
        //    {
        //        dal.AddParameters("@UserId", UserId, SqlDbType.Int, 1);

        //        using (var rdr = dal.DataReader("dbo.GetRolPagina", CommandType.StoredProcedure))
        //        {

        //            while (rdr.Read())
        //            {
        //                MenuRolList.Add(new MenuRol()
        //                {
        //                    IdRol = rdr.GetInt32(0),
        //                    IdPagina = rdr.GetInt32(1),
        //                    Pagina = rdr.GetString(2),
        //                    URL = rdr.GetString(3),
        //                    DisplayTextPestayas = rdr.IsDBNull(4) ? "": rdr.GetString(4),
        //                    OrderDisplay = rdr.IsDBNull(5) ? 0 : rdr.GetInt32(5),
        //                    IsMenuPrincipal = rdr.IsDBNull(6)? false: rdr.GetBoolean(6),
        //                    IsMenuPestayas = rdr.IsDBNull(7) ? false : rdr.GetBoolean(7),
        //                    IdTipoAcceso = rdr.IsDBNull(8) ? 2 : rdr.GetInt32(8)
        //                });
        //            }
        //        }
        //    }
        //}


        public List<MenuRol> GetMenuRol(int UserId)
        {
            List<MenuRol> menuRol = new List<MenuRol>();

            using (APPDRIMDbContext appContext = new APPDRIMDbContext(ConnectionCatalogDB.APPDRIM))
            {
                var userRoles = appContext.UserRoles.Where(x => x.UserId == UserId).ToList();

                if (userRoles.Any())
                {
                    //Administrador: Acceso a todas  las paginas excepto la de Account
                    if (userRoles.Where(x => x.RoleId == 1).Any())
                    {
                        menuRol = (
                            from ap in appContext.AplicacionPaginas.Where(x => x.IdAplicacion == 3 && x.IsActivada.HasValue && x.IsActivada.Value)
                            join p in appContext.Paginas on ap.IdPagina equals p.IdPagina
                            where p.IdPagina != 1
                            select new MenuRol
                            {
                                IdPagina = p.IdPagina,
                                DisplayText = p.TituloDisplay,
                                IdRol = 1,
                                IdTipoAcceso = 2,
                                IsMenuPestayas = p.IsMenuPestayas,
                                IsMenuPrincipal = p.IsMenuPrincipal,
                                OrderDisplay = p.OrderDisplay,
                                Pagina = p.NombrePagina,
                                URL = p.URL,
                                IdPaginaPadre = p.IdPaginaPadre,
                                IsSubMenuPrincipal = p.IsSubMenu,
                                Icono = p.Icono,
                                 Action = p.Action,
                                  Controller = p.Controller
                            }).ToList();

                    }
                    //Super Administrador a todas las paginas
                    else if (userRoles.Where(x => x.RoleId == 15).Any())
                    {
                        menuRol = (
                            from ap in appContext.AplicacionPaginas.Where(x => x.IdAplicacion == 3 && x.IsActivada.HasValue && x.IsActivada.Value)
                            join p in appContext.Paginas on ap.IdPagina equals p.IdPagina
                            select new MenuRol
                            {
                                IdPagina = p.IdPagina,
                                DisplayText = p.TituloDisplay,
                                IdRol = 15,
                                IdTipoAcceso = 2,
                                IsMenuPestayas = p.IsMenuPestayas,
                                IsMenuPrincipal = p.IsMenuPrincipal,
                                OrderDisplay = p.OrderDisplay,
                                Pagina = p.NombrePagina,
                                URL = p.URL,
                                IdPaginaPadre = p.IdPaginaPadre,
                                IsSubMenuPrincipal = p.IsSubMenu,
                                Icono = p.Icono,
                                Action = p.Action,
                                Controller = p.Controller
                            }).ToList();

                    }
                    else
                    {

                        menuRol = (from user in appContext.UserRoles.Where(x => x.UserId == UserId && x.IdAplicacion == 3)
                                   join rolPagina in appContext.RolPaginas on user.RoleId equals rolPagina.IdRol into userRolPagina
                                   from urp in userRolPagina
                                   join pagina in appContext.Paginas on urp.IdPagina equals pagina.IdPagina into paginas
                                   from p in paginas
                                   join ap in appContext.AplicacionPaginas.Where(x => x.IdAplicacion == 3 && x.IsActivada.HasValue && x.IsActivada.Value) on p.IdPagina equals ap.IdPagina

                                   select new MenuRol
                                   {
                                       IdPagina = p.IdPagina,
                                       DisplayText = p.TituloDisplay,
                                       IdRol = urp.IdRol,
                                       IdTipoAcceso = urp.IdTipoAcceso,
                                       IsMenuPestayas = p.IsMenuPestayas,
                                       IsMenuPrincipal = p.IsMenuPrincipal,
                                       OrderDisplay = p.OrderDisplay,
                                       Pagina = p.NombrePagina,
                                       URL = p.URL,
                                       IdPaginaPadre = p.IdPaginaPadre,
                                       IsSubMenuPrincipal = p.IsSubMenu,
                                       Icono = p.Icono,
                                       Action = p.Action,
                                       Controller = p.Controller
                                   }).ToList();

                    }

                }

            }

            return menuRol;
        }

    }
}
