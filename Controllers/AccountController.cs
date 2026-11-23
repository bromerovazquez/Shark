using Drim.Interfaces.MVC.Entities;
using Drim.Interfaces.MVC.Helpers;
using Drim.Interfaces.MVC.Models;
using Drim.Interfaces.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Drim.Interfaces.MVC.Controllers
{
    [Authorize(Roles = "SuperAdministrador")]
    public class AccountController : Controller
    {
        private IHttpContextAccessor _accessor;
        private UsuarioDrim usDrim = null;
        private readonly UserManager<UsuarioDrim> _userManager;

        private readonly SignInManager<UsuarioDrim> _signInManager;




        public AccountController(
            IHttpContextAccessor accessor,
            UserManager<UsuarioDrim> userManager,
           SignInManager<UsuarioDrim> signInManager)
        {
            _accessor = accessor;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> IsAutentificate(string usuario, string pass)
        {


            if (ModelState.IsValid)
            {
                try
                {

                    bool isAutentificate = false;
                    var result = await _signInManager.PasswordSignInAsync(usuario, pass, false, false);
                    LoginModel loginViewModel = new LoginModel();

                    if (result.Succeeded)
                    {
                        isAutentificate = true;

                        var usDrim = await _userManager.FindByNameAsync(usuario);
                        var rolDrim = await _userManager.GetRolesAsync(usDrim);
                        string ip = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();


                        if (isAutentificate && !rolDrim.Contains("Administrador") && !rolDrim.Contains("SuperAdministrador") && usDrim.IsAutentificacionFichaje.HasValue && usDrim.IsAutentificacionFichaje.Value)
                        {
                            try
                            {

                                //var userEmpleados = loginViewModel.GetUsuarioEmpleados(usDrim.Id);
                                //RegistroEmpleadoViewModel registroEmpleadoViewModel = new RegistroEmpleadoViewModel();
                                //if (!registroEmpleadoViewModel.IsFichadoHoy(userEmpleados))
                                isAutentificate = true;
                            }
                            catch (Exception ex)
                            {
                                isAutentificate = false;
                            }
                        }

                        MenuRolModel menuRolViewModel = new MenuRolModel();
                        if (isAutentificate)
                        {
                            menuRolViewModel.MenuRolList = menuRolViewModel.GetMenuRol(usDrim.Id);

                            if (menuRolViewModel.MenuRolList == null || !menuRolViewModel.MenuRolList.Any())
                                isAutentificate = false;
                        }

                        if (isAutentificate)
                        {
                            _accessor.HttpContext.Session.SetObjectAsJson("usDrim", usDrim);
                            _accessor.HttpContext.Session.SetObjectAsJson("rolDrim", rolDrim);
                            _accessor.HttpContext.Session.SetObjectAsJson("menuUserRol", menuRolViewModel.MenuRolList);

                            _accessor.HttpContext.Session.SetObjectAsJson("IsProduccion", EntornoHelper.IsProduccion);

                            loginViewModel.RegistraUsuarioLogin(usDrim.Id, ip);


                            return RedirectToAction("Index", "Traspasos");

                        }



                    }
                }
                catch (Exception ex) 
                {

                    return Json(new { error = ex.Message });
                
                }
            }

            return RedirectToAction("Login");
        }


        public IActionResult Registrar()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegistrarNew(UsuarioDrim user)
        {
            PasswordHasher<UsuarioDrim> passwordHasher = new Microsoft.AspNetCore.Identity.PasswordHasher<UsuarioDrim>();
            user.PasswordHash = passwordHasher.HashPassword(user, user.PasswordHash);
            //user.ConcurrencyStamp = Guid.NewGuid().ToString();
            user.SecurityStamp = Guid.NewGuid().ToString();
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {

            }

            return View("Login");
        }

        [AllowAnonymous]
        public async Task<IActionResult> LoginOut()
        {
            await _signInManager.SignOutAsync();
            _accessor.HttpContext.Session.SetObjectAsJson("usDrim", null);
            _accessor.HttpContext.Session.SetObjectAsJson("rolDrim", null);
            _accessor.HttpContext.Session.SetObjectAsJson("menuUserRol", null);
            return RedirectToAction("Login");
        }




        [AllowAnonymous]
        public IActionResult Error()
        {


            return RedirectToAction("Login");
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult CheckSession()
        {
            usDrim = _accessor.HttpContext.Session.GetObjectFromJson<UsuarioDrim>("usDrim");

            if (usDrim == null)
            {
                return Json(new { result = "NOK" });

            }


            return Json(new { result = "OK" });
        }

    }
}