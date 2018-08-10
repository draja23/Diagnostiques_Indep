using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Diagnostique.BO.ModelSource
{
    public partial class DiagnoInfoTerrainSourceContext : DbContext
    {
        public DiagnoInfoTerrainSourceContext()
        {
        }

        public DiagnoInfoTerrainSourceContext(DbContextOptions<DiagnoInfoTerrainSourceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DonneeSource> Donneesource { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //   optionsBuilder.UseSqlServer("Server=NDV1289;Database=DiagnoInfoTerrainSource;User Id=sa;Password=Draja2310@@;");
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                   .AddJsonFile("appsettings.json")
                   .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("sourceConnection"));

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DonneeSource>(entity =>
            {
                entity.HasKey(e => e.ColId);

                entity.Property(e => e.ColId).HasColumnName("col_id");

                entity.Property(e => e.EstCopieDestination)
                    .HasColumnName("est_copie_destination")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SourceCatalAutrePrincipal)
                    .HasColumnName("s_catal_autre_principal")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SourceCatalPrincipal)
                    .HasColumnName("s_catal_principal")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SourceComDernierDateId).HasColumnName("s_com_dernier_date_id");

                entity.Property(e => e.SourceComDernierFactureDateId).HasColumnName("s_com_dernier_facture_date_id");

                entity.Property(e => e.SourceComDerniereDate)
                    .HasColumnName("s_com_derniere_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SourceComDerniereFactureDate)
                    .HasColumnName("s_com_derniere_facture_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SourceComPremierDate)
                    .HasColumnName("s_com_premier_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SourceComPremierDateId).HasColumnName("s_com_premier_date_id");

                entity.Property(e => e.SourceComPremierFactureDate)
                    .HasColumnName("s_com_premier_facture_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SourceComPremierFactureDateId).HasColumnName("s_com_premier_facture_date_id");

                entity.Property(e => e.SourceSectNom)
                    .HasColumnName("s_sect_nom")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SourceTabLigneComptage)
                    .HasColumnName("s_tab_ligne_comptage")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.SourceTabNom)
                    .HasColumnName("s_tab_nom")
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.SourceTabNomIndex)
                    .HasColumnName("s_tab_nom_index")
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.SourceUtilNom)
                    .HasColumnName("s_util_nom")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SourceUtilPrenom)
                    .HasColumnName("s_util_prenom")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SourceVersNom)
                    .HasColumnName("s_vers_nom")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SourceVisDate)
                    .HasColumnName("s_vis_date")
                    .HasColumnType("datetime");
            });
        }
    }
}
