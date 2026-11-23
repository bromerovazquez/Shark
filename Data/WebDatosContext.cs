
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Drim.Interfaces.MVC.Connection;
using Drim.Interfaces.MVC.Entities;
using Microsoft.EntityFrameworkCore;

namespace Drim.Interfaces.MVC.Data
{
    public class WebDatosContext : DbContext
    {

        public WebDatosContext()
        {
        }

        public WebDatosContext(DbContextOptions<WebDatosContext> options)
        : base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer(ConnectionCatalogDB.WEB_DATOS.GetConectionString());
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<dtArticulosPorIdioma>(b =>
            {
                b.HasKey(e => new { e.CODARTICULO, e.ID_IDIOMA });
            });

        }



        public DbSet<dtArticulosPorIdioma> dtArticulosPorIdiomas { get; set; }
    }
}
