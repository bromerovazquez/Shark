
using Drim.Interfaces.MVC.Connection;
using Drim.Interfaces.MVC.Entities;
using Microsoft.EntityFrameworkCore;

namespace Drim.Interfaces.MVC.Data
{
    public class DRIMDbContext : DbContext
    {

        public DRIMDbContext()
        {
        }

            
        public DRIMDbContext(DbContextOptions<DRIMDbContext> options)
        : base(options)
        { }


        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer(ConnectionCatalogDB.DRIM.GetConectionString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Devolucion>().HasKey(ba => ba.DevolucioID);
            modelBuilder.Entity<Carro>().HasKey(ba => ba.CarroID);
            modelBuilder.Entity<LogOperacionInterface>().HasKey(ba => ba.Id);
            modelBuilder.Entity<Traspaso>().HasKey(ba => ba.IDTraspaso);
            modelBuilder.Entity<Albaran>().HasKey(ba => ba.IDAlbaran);
            modelBuilder.Entity<LogGestionTraspasosAlbaranes>().HasKey(ba => ba.Id);
            modelBuilder.Entity<MatchTraspasosYTicketDevolucion>().HasKey(ba => ba.Id);




            modelBuilder.Entity<Interfaces_Procesos>().HasKey(ba => ba.ArchivoId);
            modelBuilder.Entity<Interfaces_Procesos>()
            .HasMany(b => b.Steps)
            .WithOne(b=> b.Archivo)
            .HasForeignKey(p => p.ArchivoId)
            .HasConstraintName("FK_Interfaces_Archivo_Steps_Interfaces_Archivos");

           


            modelBuilder.Entity<Interfaces_Procesos_Steps>().HasKey(ba => ba.StepId);
            modelBuilder.Entity<Interfaces_Procesos_Steps>()
            .HasMany(b => b.Parametros)
            .WithOne(b=> b.Step)
            .HasForeignKey(p => p.StepId)
            .HasConstraintName("FK_Interfaces_Archivo_Steps_Parametros_Interfaces_Archivo_Steps");


            modelBuilder.Entity<Interfaces_Procesos_Steps_Parametros>().HasKey(ba => ba.ParametroId);


            modelBuilder.Entity<Interfaces_Procesos_Logs_Estados>().HasKey(ba => ba.Id);


            modelBuilder.Entity<vLogInterfaces>(entity => {
                entity.HasKey(e => e.Id);
                entity.ToTable("vLogsInterfaces");
            });


        }

        public DbSet<Devolucion> Devoluciones { get; set; }

        public DbSet<Carro> Carros { get; set; }

        public DbSet<LogOperacionInterface> LogOperacionesInterfaces { get; set; }

        public DbSet<Traspaso> Traspasos { get; set; }

        public DbSet<Albaran> Albaranes { get; set; }

        public DbSet<LogGestionTraspasosAlbaranes> LogGestionesTraspasos { get; set; }

        public DbSet<MatchTraspasosYTicketDevolucion> MatchTraspasosYTicketDevoluciones { get; set; }


        

        public DbSet<Interfaces_Procesos_Steps_Parametros> Interfaces_Procesos_Steps_Parametros { get; set; }

        public DbSet<Interfaces_Procesos> Interfaces_Procesos { get; set; }

        public DbSet<Interfaces_Procesos_Steps> Interfaces_Procesos_Steps { get; set; }

        public DbSet<Interfaces_Procesos_Logs_Estados> Interfaces_Procesos_Logs_Estados { get; set; }


        public DbSet<vLogInterfaces> vLogsInterfaces { get; set; }

        public DbSet<TraspasoIncidencias> TraspasosIncidencias { get; set; }
    }
}
