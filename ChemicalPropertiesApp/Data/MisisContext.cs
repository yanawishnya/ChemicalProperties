using System;
using System.Collections.Generic;
using ChemicalPropertiesApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChemicalPropertiesApp
{
    public partial class MisisContext : DbContext
    {
        public MisisContext()
        {
        }

        public MisisContext(DbContextOptions<MisisContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GroupsInfo> GroupsInfos { get; set; } = null!;
        public virtual DbSet<LitReference> LitReferences { get; set; } = null!;
        public virtual DbSet<ObjectsInfo> ObjectsInfos { get; set; } = null!;
        public virtual DbSet<ObjectsReference> ObjectsReferences { get; set; } = null!;
        public virtual DbSet<PhaseEquilibriaDetail> PhaseEquilibriaDetails { get; set; } = null!;
        public virtual DbSet<PhaseEquilibrium> PhaseEquilibria { get; set; } = null!;
        public virtual DbSet<RubricsInfo> RubricsInfos { get; set; } = null!;
        public virtual DbSet<SolidPhase> SolidPhases { get; set; } = null!;
        public virtual DbSet<ThermodynamicDataActivity> ThermodynamicDataActivities { get; set; } = null!;
        public virtual DbSet<ThermodynamicDataEnthalpy> ThermodynamicDataEnthalpies { get; set; } = null!;
        public virtual DbSet<ThermodynamicDataHeatCapacity> ThermodynamicDataHeatCapacities { get; set; } = null!;
        public virtual DbSet<TypesInfo> TypesInfos { get; set; } = null!;
        public virtual DbSet<UsersInfo> UsersInfos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=Misis");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Cyrillic_General_CI_AS");

            modelBuilder.Entity<GroupsInfo>(entity =>
            {
                entity.HasKey(e => e.GroupId);

                entity.ToTable("GroupsInfo");

                entity.Property(e => e.GroupId)
                    .ValueGeneratedNever()
                    .HasColumnName("GroupID");

                entity.Property(e => e.Comments)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.GroupName)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasMany(d => d.Objects)
                    .WithMany(p => p.Groups)
                    .UsingEntity<Dictionary<string, object>>(
                        "GroupsObject",
                        l => l.HasOne<ObjectsInfo>().WithMany().HasForeignKey("ObjectId")
                            .OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_GroupsObject_ObjectsInfo"),
                        r => r.HasOne<GroupsInfo>().WithMany().HasForeignKey("GroupId")
                            .OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_GroupsObject_GroupsInfo"),
                        j =>
                        {
                            j.HasKey("GroupId", "ObjectId");

                            j.ToTable("GroupsObject");

                            j.IndexerProperty<int>("GroupId").HasColumnName("GroupID");

                            j.IndexerProperty<int>("ObjectId").HasColumnName("ObjectID");
                        });

                entity.HasMany(d => d.Users)
                    .WithMany(p => p.Groups)
                    .UsingEntity<Dictionary<string, object>>(
                        "UsersGroup",
                        l => l.HasOne<UsersInfo>().WithMany().HasForeignKey("UserId")
                            .OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UsersGroups_UsersInfo"),
                        r => r.HasOne<GroupsInfo>().WithMany().HasForeignKey("GroupId")
                            .OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UsersGroups_GroupsInfo"),
                        j =>
                        {
                            j.HasKey("GroupId", "UserId");

                            j.ToTable("UsersGroups");

                            j.IndexerProperty<int>("GroupId").HasColumnName("GroupID");

                            j.IndexerProperty<int>("UserId").HasColumnName("UserID");
                        });
            });

            modelBuilder.Entity<LitReference>(entity =>
            {
                entity.HasKey(e => e.ReferenceId);

                entity.Property(e => e.ReferenceId)
                    .ValueGeneratedNever()
                    .HasColumnName("ReferenceID");

                entity.Property(e => e.Authors)
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.BibTeX)
                    .HasMaxLength(4096)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("_date")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Дата последнего обновления записи");

                entity.Property(e => e.Doi)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("DOI");

                entity.Property(e => e.EndPage)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Journal)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Num)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.StartPage)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Vol)
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ObjectsInfo>(entity =>
            {
                entity.HasKey(e => e.ObjectId);

                entity.ToTable("ObjectsInfo");

                entity.Property(e => e.ObjectId)
                    .ValueGeneratedNever()
                    .HasColumnName("ObjectID");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FilePath)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.IsPublished)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ObjectContent).IsUnicode(false);

                entity.Property(e => e.ObjectDescription)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.ObjectName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.RubricId).HasColumnName("RubricID");

                entity.Property(e => e.SortType).HasDefaultValueSql("((0))");

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.HasOne(d => d.Object)
                    .WithOne(p => p.ObjectsInfo)
                    .HasForeignKey<ObjectsInfo>(d => d.ObjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObjectsInfo_ThermodynamicData_Activity");

                entity.HasOne(d => d.ObjectNavigation)
                    .WithOne(p => p.ObjectsInfo)
                    .HasForeignKey<ObjectsInfo>(d => d.ObjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObjectsInfo_ThermodynamicData_Enthalpy");

                entity.HasOne(d => d.Object1)
                    .WithOne(p => p.ObjectsInfo)
                    .HasForeignKey<ObjectsInfo>(d => d.ObjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObjectsInfo_ThermodynamicData_HeatCapacity");

                entity.HasOne(d => d.Object2)
                    .WithOne(p => p.ObjectsInfo)
                    .HasForeignKey<ObjectsInfo>(d => d.ObjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObjectsInfo_PhaseEquilibria");

                entity.HasOne(d => d.Object3)
                    .WithOne(p => p.ObjectsInfo)
                    .HasForeignKey<ObjectsInfo>(d => d.ObjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObjectsInfo_LitReferences");

                entity.HasOne(d => d.Object4)
                    .WithOne(p => p.ObjectsInfo)
                    .HasForeignKey<ObjectsInfo>(d => d.ObjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObjectsInfo_SolidPhases");

                entity.HasOne(d => d.Rubric)
                    .WithMany(p => p.ObjectsInfos)
                    .HasForeignKey(d => d.RubricId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObjectsInfo_RubricsInfo");
            });

            modelBuilder.Entity<ObjectsReference>(entity =>
            {
                entity.HasKey(e => new { e.ObjectId, e.ReferencedObjectId });

                entity.Property(e => e.ObjectId).HasColumnName("ObjectID");

                entity.Property(e => e.ReferencedObjectId).HasColumnName("ReferencedObjectID");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("_date");

                entity.HasOne(d => d.Object)
                    .WithMany(p => p.ObjectsReferenceObjects)
                    .HasForeignKey(d => d.ObjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObjectsReferences_ObjectsInfo");

                entity.HasOne(d => d.ReferencedObject)
                    .WithMany(p => p.ObjectsReferenceReferencedObjects)
                    .HasForeignKey(d => d.ReferencedObjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObjectsReferences_ObjectsInfo1");
            });

            modelBuilder.Entity<PhaseEquilibriaDetail>(entity =>
            {
                entity.HasKey(e => new { e.PhaseEquilibriaId, e.PhaseDetailId, e.Element });

                entity.ToTable("PhaseEquilibriaDetail");

                entity.Property(e => e.PhaseEquilibriaId).HasColumnName("PhaseEquilibriaID");

                entity.Property(e => e.PhaseDetailId).HasColumnName("PhaseDetailID");

                entity.Property(e => e.Element)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.HasOne(d => d.PhaseEquilibria)
                    .WithMany(p => p.PhaseEquilibriaDetails)
                    .HasForeignKey(d => d.PhaseEquilibriaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhaseEquilibriaDetail_PhaseEquilibria");
            });

            modelBuilder.Entity<PhaseEquilibrium>(entity =>
            {
                entity.HasKey(e => e.PhaseEquilibriaId);

                entity.Property(e => e.PhaseEquilibriaId)
                    .ValueGeneratedNever()
                    .HasColumnName("PhaseEquilibriaID");

                entity.Property(e => e.Comment)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Formula)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RubricsInfo>(entity =>
            {
                entity.HasKey(e => e.RubricId);

                entity.ToTable("RubricsInfo");

                entity.Property(e => e.RubricId)
                    .ValueGeneratedNever()
                    .HasColumnName("RubricID");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsPublished)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.RubricName)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.RubricPath)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.RubricsInfos)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RubricsInfo_TypesInfo");

                entity.HasMany(d => d.Objects)
                    .WithMany(p => p.Rubrics)
                    .UsingEntity<Dictionary<string, object>>(
                        "ObjectsRubric",
                        l => l.HasOne<ObjectsInfo>().WithMany().HasForeignKey("ObjectId")
                            .OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_ObjectsRubric_ObjectsInfo"),
                        r => r.HasOne<RubricsInfo>().WithMany().HasForeignKey("RubricId")
                            .OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_ObjectsRubric_RubricsInfo"),
                        j =>
                        {
                            j.HasKey("RubricId", "ObjectId");

                            j.ToTable("ObjectsRubric");

                            j.IndexerProperty<int>("RubricId").HasColumnName("RubricID");

                            j.IndexerProperty<int>("ObjectId").HasColumnName("ObjectID");
                        });
            });

            modelBuilder.Entity<SolidPhase>(entity =>
            {
                entity.Property(e => e.SolidPhaseId)
                    .ValueGeneratedNever()
                    .HasColumnName("SolidPhaseID");

                entity.Property(e => e.Comment)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Formula)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Modification)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.PearsonSymbol)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Prototype)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.SpaceGroup)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ThermodynamicDataActivity>(entity =>
            {
                entity.HasKey(e => e.ActivityId);

                entity.ToTable("ThermodynamicData_Activity");

                entity.Property(e => e.ActivityId)
                    .ValueGeneratedNever()
                    .HasColumnName("ActivityID");

                entity.Property(e => e.Comment)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Element)
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ThermodynamicDataEnthalpy>(entity =>
            {
                entity.HasKey(e => e.EnthalpyId);

                entity.ToTable("ThermodynamicData_Enthalpy");

                entity.Property(e => e.EnthalpyId)
                    .ValueGeneratedNever()
                    .HasColumnName("EnthalpyID");

                entity.Property(e => e.Comment)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Reaction)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ThermodynamicDataHeatCapacity>(entity =>
            {
                entity.HasKey(e => e.HeatCapacityId);

                entity.ToTable("ThermodynamicData_HeatCapacity");

                entity.Property(e => e.HeatCapacityId)
                    .ValueGeneratedNever()
                    .HasColumnName("HeatCapacityID");

                entity.Property(e => e.Comment)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Phase)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TypesInfo>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.ToTable("TypesInfo");

                entity.Property(e => e.TypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("TypeID");

                entity.Property(e => e.TypeComment)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.TypeName)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UsersInfo>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("UsersInfo");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("UserID");

                entity.Property(e => e.Address)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Area)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Category).HasDefaultValueSql("((1))");

                entity.Property(e => e.City)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.DateRecord).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.IndexCode)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Login)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.RegDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}