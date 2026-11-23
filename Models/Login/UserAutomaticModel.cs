using Drim.Interfaces.MVC.Entities;
using Drim.Interfaces.MVC.ViewModels;

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Drim.Interfaces.MVC.Models
{
    public class UserAutomaticModel
    {
        private readonly UserManager<UsuarioDrim> _userManager;

        public UserAutomaticModel(UserManager<UsuarioDrim> userManager)
        {
            _userManager = userManager;
        }


        public async Task<bool> CrearContraseñasTemporales(string password, int? IdUser = null)
        {

            bool result = true;

            try
            {
                PasswordHasher<UsuarioDrim> passwordHasher = new Microsoft.AspNetCore.Identity.PasswordHasher<UsuarioDrim>();
                var userASpNet = await _userManager.FindByIdAsync(IdUser.Value.ToString());
                userASpNet.PasswordHash = password;
                userASpNet.SecurityStamp = Guid.NewGuid().ToString();
                userASpNet.PasswordHash = passwordHasher.HashPassword(userASpNet, userASpNet.PasswordHash);
                var resultUpdate = await _userManager.UpdateAsync(userASpNet);
            }

            catch (Exception ex)
            {
                result = false;
            }

            return result;

        }

    }
}
