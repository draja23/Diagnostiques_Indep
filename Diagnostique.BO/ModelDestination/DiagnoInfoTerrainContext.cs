using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Diagnostique.BO.ModelDestination
{
    public partial class DiagnoInfoTerrainContext : DbContext
    {
        public DiagnoInfoTerrainContext()
        {
        }

        public DiagnoInfoTerrainContext(DbContextOptions<DiagnoInfoTerrainContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Catalogue> Catalogue { get; set; }
        public virtual DbSet<CatalogueAutre> CatalogueAutre { get; set; }
        public virtual DbSet<Commande> Commande { get; set; }
        public virtual DbSet<LoginUtilisateur> LoginUtilisateur { get; set; }
        public virtual DbSet<Secteur> Secteur { get; set; }
        public virtual DbSet<TableIndex> TableIndex { get; set; }
        public virtual DbSet<TableNomComptage> TableNomComptage { get; set; }
        public virtual DbSet<Utilisateur> Utilisateur { get; set; }
        public virtual DbSet<Version> Version { get; set; }
        public virtual DbSet<Visite> Visite { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //  optionsBuilder.UseSqlServer("Server=NDV1289;Database=DiagnoInfoTerrain;User Id=sa;Password=Draja2310@@;");
                IConfigurationRoot configuration = new ConfigurationBuilder()
                  .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                  .AddJsonFile("appsettings.json")
                  .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("destinationConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catalogue>(entity =>
            {
                entity.HasKey(e => e.CatalId);

                entity.ToTable("catalogue");

                entity.Property(e => e.CatalId).HasColumnName("catal_id");

                entity.Property(e => e.CatalPrincipal)
                    .HasColumnName("catal_principal")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdUtil).HasColumnName("id_util");

                entity.HasOne(d => d.IdUtilNavigation)
                    .WithMany(p => p.Catalogue)
                    .HasForeignKey(d => d.IdUtil)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__catalogue__id_ut__3C69FB99");
            });

            modelBuilder.Entity<CatalogueAutre>(entity =>
            {
                entity.HasKey(e => e.CatalAutreId);

                entity.ToTable("catalogue_autre");

                entity.Property(e => e.CatalAutreId).HasColumnName("catal_autre_id");

                entity.Property(e => e.CatalAutrePrincipal)
                    .HasColumnName("catal_autre_principal")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdUtil).HasColumnName("id_util");

                entity.HasOne(d => d.IdUtilNavigation)
                    .WithMany(p => p.CatalogueAutre)
                    .HasForeignKey(d => d.IdUtil)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__catalogue__id_ut__3F466844");
            });

            modelBuilder.Entity<Commande>(entity =>
            {
                entity.HasKey(e => e.ComId);

                entity.ToTable("commande");

                entity.Property(e => e.ComId).HasColumnName("com_id");

                entity.Property(e => e.ComDernierDateId).HasColumnName("com_dernier_date_id");

                entity.Property(e => e.ComDernierFactureDateId).HasColumnName("com_dernier_facture_date_id");

                entity.Property(e => e.ComDerniereDate)
                    .HasColumnName("com_derniere_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ComDerniereFactureDate)
                    .HasColumnName("com_derniere_facture_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ComPremierDate)
                    .HasColumnName("com_premier_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ComPremierDateId).HasColumnName("com_premier_date_id");

                entity.Property(e => e.ComPremierFactureDate)
                    .HasColumnName("com_premier_facture_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ComPremierFactureDateId).HasColumnName("com_premier_facture_date_id");

                entity.Property(e => e.IdUtil).HasColumnName("id_util");

                entity.HasOne(d => d.IdUtilNavigation)
                    .WithMany(p => p.Commande)
                    .HasForeignKey(d => d.IdUtil)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__commande__id_uti__44FF419A");
            });

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

            modelBuilder.Entity<Secteur>(entity =>
            {
                entity.HasKey(e => e.SectId);

                entity.ToTable("secteur");

                entity.Property(e => e.SectId).HasColumnName("sect_id");

                entity.Property(e => e.IdUtil).HasColumnName("id_util");

                entity.Property(e => e.SectNom)
                    .HasColumnName("sect_nom")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdUtilNavigation)
                    .WithMany(p => p.Secteur)
                    .HasForeignKey(d => d.IdUtil)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__secteur__id_util__398D8EEE");
            });

            modelBuilder.Entity<TableIndex>(entity =>
            {
                entity.ToTable("table_index");

                entity.Property(e => e.TableIndexId).HasColumnName("table_index_id");

                entity.Property(e => e.IdNcTable).HasColumnName("id_nc_table");

                entity.Property(e => e.TabNomIndex)
                    .HasColumnName("tab_nom_index")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdNcTableNavigation)
                    .WithMany(p => p.TableIndex)
                    .HasForeignKey(d => d.IdNcTable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__table_ind__id_nc__4D94879B");
            });

            modelBuilder.Entity<TableNomComptage>(entity =>
            {
                entity.HasKey(e => e.TableNcId);

                entity.ToTable("table_nom_comptage");

                entity.Property(e => e.TableNcId).HasColumnName("table_nc_id");

                entity.Property(e => e.IdUtil).HasColumnName("id_util");

                entity.Property(e => e.TabLigneComptage).HasColumnName("tab_ligne_comptage");

                entity.Property(e => e.TabNom)
                    .HasColumnName("tab_nom")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdUtilNavigation)
                    .WithMany(p => p.TableNomComptage)
                    .HasForeignKey(d => d.IdUtil)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__table_nom__id_ut__4AB81AF0");
            });

            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.HasKey(e => e.UtilId);

                entity.ToTable("utilisateur");

                entity.Property(e => e.UtilId).HasColumnName("util_id");

                entity.Property(e => e.SourceId).HasColumnName("source_id");

                entity.Property(e => e.UtilNom)
                    .HasColumnName("util_nom")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UtilPrenom)
                    .HasColumnName("util_prenom")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Version>(entity =>
            {
                entity.HasKey(e => e.VersId);

                entity.ToTable("version");

                entity.Property(e => e.VersId).HasColumnName("vers_id");

                entity.Property(e => e.IdUtil).HasColumnName("id_util");

                entity.Property(e => e.VersNom)
                    .HasColumnName("vers_nom")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdUtilNavigation)
                    .WithMany(p => p.Version)
                    .HasForeignKey(d => d.IdUtil)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__version__id_util__4222D4EF");
            });

            modelBuilder.Entity<Visite>(entity =>
            {
                entity.HasKey(e => e.VisId);

                entity.ToTable("visite");

                entity.Property(e => e.VisId).HasColumnName("vis_id");

                entity.Property(e => e.IdUtil).HasColumnName("id_util");

                entity.Property(e => e.VisDate)
                    .HasColumnName("vis_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.IdUtilNavigation)
                    .WithMany(p => p.Visite)
                    .HasForeignKey(d => d.IdUtil)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__visite__id_util__47DBAE45");
            });
        }
    }
}
