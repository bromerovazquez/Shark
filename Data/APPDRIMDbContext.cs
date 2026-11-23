
using Drim.Interfaces.MVC.Connection;
using Drim.Interfaces.MVC.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Drim.Interfaces.MVC.Data
{
    public class APPDRIMDbContext : IdentityDbContext
        <
    UsuarioDrim,
    RolesDrim,
    int,
    IdentityUserClaim<int>,
    UsuarioRolesDrim,
    IdentityUserLogin<int>,
    IdentityRoleClaim<int>,
    IdentityUserToken<int>>

    {

        ConnectionCatalogDB connectionCatalogDB;

        public APPDRIMDbContext()
        {
            connectionCatalogDB = ConnectionCatalogDB.APPDRIM;
        }

        public APPDRIMDbContext(ConnectionCatalogDB connectionCatalogDB)
        {
            this.connectionCatalogDB = connectionCatalogDB;

        }

        public APPDRIMDbContext(DbContextOptions<APPDRIMDbContext> options)
        : base(options)
        {
            connectionCatalogDB = ConnectionCatalogDB.APPDRIM;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer(this.connectionCatalogDB.GetConectionString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Aplicacion>().HasKey(k => k.IdAplicacion);
            modelBuilder.Entity<Pagina>().HasKey(k => k.IdPagina);
            modelBuilder.Entity<TipoAccesoDato>().HasKey(k => k.IdTipoAcceso);
            modelBuilder.Entity<AplicacionPagina>().HasKey(k => new { k.IdAplicacion, k.IdPagina });
            modelBuilder.Entity<RolPagina>().HasKey(k => new { k.IdRol, k.IdPagina, k.IdTipoAcceso });
            modelBuilder.Entity<UsuarioEmpleados>().HasKey(k => new { k.IdUser, k.codigo_empleado });

            modelBuilder.Entity<UsuarioLogins>().HasKey(k => new { k.Id });

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UsuarioRolesDrim>(userRole =>
            {
                userRole.HasKey(pr => new
                {
                    pr.UserId,
                    pr.RoleId,
                    pr.IdAplicacion
                });
            });
        }



        public DbSet<Pagina> Paginas { get; set; }

        public DbSet<AplicacionPagina> AplicacionPaginas { get; set; }

        public DbSet<Aplicacion> Aplicaciones { get; set; }

        public DbSet<TipoAccesoDato> TipoAccesos { get; set; }

        public DbSet<RolPagina> RolPaginas { get; set; }


        public DbSet<UsuarioEmpleados> UsuarioEmpleados { get; set; }

        public DbSet<UsuarioLogins> UsuarioLogins { get; set; }

        

    }
}
