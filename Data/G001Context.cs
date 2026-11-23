
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Drim.Interfaces.MVC.Connection;
using Drim.Interfaces.MVC.Entities;
using Microsoft.EntityFrameworkCore;

namespace Drim.Interfaces.MVC.Data
{
    public class G001Context : DbContext
    {

        public G001Context()
        {
        }

        public G001Context(DbContextOptions<G001Context> options)
        : base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer(ConnectionCatalogDB.G001.GetConectionString());
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Almacen>(b =>
            {
                b.HasKey(e => e.CodAlmacen);
            });

            modelBuilder.Entity<TraspasoCap>(b =>
            {
                b.HasKey(e => new { e.SERIE, e.NUMERO,e.CAJA });
            });

            modelBuilder.Entity<AlbCompraCAB>(b =>
            {
                b.HasKey(e => new { e.NUMSERIE, e.NUMALBARAN, e.N });
            });
            modelBuilder.Entity<Stock>(b =>
            {
                b.HasKey(e => new { e.CODALMACEN, e.CODARTICULO, e.TALLA,e.COLOR });
            });

            modelBuilder.Entity<ReferenciasProveedor>(b =>
            {
                b.HasKey(e => new { e.REFPROVEEDOR, e.CODARTICULO, e.CODPROVEEDOR });
            });

            modelBuilder.Entity<Movimiento>(b =>
            {
                b.HasKey(e => e.ID);
            });

            modelBuilder.Entity<Articulo>(b =>
            {
                b.HasKey(e => e.CODARTICULO);
            });

            modelBuilder.Entity<Proveedor>(b =>
            {
                b.HasKey(e => e.CODPROVEEDOR);
            });

            modelBuilder.Entity<MapDataExchange>(b =>
            {
                b.HasKey(e => e.IDMAP);
            });

            modelBuilder.Entity<MapdataExchangeParametros>(b =>
            {
                b.HasKey(e => new { e.IDMAP, e.IDFILE , e.IDFIELD});
            });

            modelBuilder.Entity<AlbaranVentaCAP>(b =>
            {
                b.HasKey(e => new { e.NUMSERIE, e.NUMALBARAN, e.N });
            });

            modelBuilder.Entity<MapConfigFilters>(b =>
            {
                b.HasKey(e => new { e.IDMAP, e.IDFILE});
            });

            
        }

        public DbSet<Almacen> Almacenes { get; set; }

        public DbSet<TraspasoCap> TraspasosCap { get; set; }

        public DbSet<AlbCompraCAB> AlbComprasCAB { get; set; }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<ReferenciasProveedor> ArticulosReferenciasProveedores { get; set; }


        public DbSet<Movimiento> Movimientos { get; set; }


        public DbSet<Articulo> Articulos { get; set; }

        public DbSet<Proveedor> Proveedores { get; set; }


        public DbSet<MapDataExchange> mapDataExchanges { get; set; }


        public DbSet<MapdataExchangeParametros> mapdataExchangeParametros { get; set; }


        public DbSet<AlbaranVentaCAP> AlbaranesVentasCAP { get; set; }


        public DbSet<MapConfigFilters> MapsConfigsFilters { get; set; }
    }
}
