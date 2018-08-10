using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Diagnostique.BO.ModelLogin
{
    public partial class DiagnoInfoLoginTerrainContext : DbContext
    {
        public DiagnoInfoLoginTerrainContext()
        {
        }

        public DiagnoInfoLoginTerrainContext(DbContextOptions<DiagnoInfoLoginTerrainContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LoginUtilisateur> LoginUtilisateur { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer("Server=NDV1289;Database=DiagnoInfoTerrain;User Id=sa;Password=Draja2310@@;");
                 IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("destinationConnection"));

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginUtilisateur>(entity =>
            {
                entity.HasKey(e => e.IdUtilisateur);

                entity.ToTable("login_utilisateur");

                entity.Property(e => e.IdUtilisateur).HasColumnName("id_utilisateur");

                entity.Property(e => e.MailUtil)
                    .IsRequired()
                    .HasColumnName("mail_util")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MdpUtil)
                    .IsRequired()
                    .HasColumnName("mdp_util")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NomUtil)
                    .IsRequired()
                    .HasColumnName("nom_util")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrenomUtil)
                    .IsRequired()
                    .HasColumnName("prenom_util")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
