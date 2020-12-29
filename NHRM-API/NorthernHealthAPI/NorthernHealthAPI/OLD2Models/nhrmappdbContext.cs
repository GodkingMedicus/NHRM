﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NorthernHealthAPI.Models
{
    public partial class nhrmappdbContext : DbContext
    {
        public nhrmappdbContext()
        {
        }

        public nhrmappdbContext(DbContextOptions<nhrmappdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DataPoint> DataPoint { get; set; }
        public virtual DbSet<DataPointRecord> DataPointRecord { get; set; }
        public virtual DbSet<Measurement> Measurement { get; set; }
        public virtual DbSet<MeasurementRecord> MeasurementRecord { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<PatientCategory> PatientCategory { get; set; }
        public virtual DbSet<PatientMeasurement> PatientMeasurement { get; set; }
        public virtual DbSet<PatientRecord> PatientRecord { get; set; }
        public virtual DbSet<PatientResource> PatientResource { get; set; }
        public virtual DbSet<RecordCategory> RecordCategory { get; set; }
        public virtual DbSet<RecordType> RecordType { get; set; }
        public virtual DbSet<Resource> Resource { get; set; }
        public virtual DbSet<ResourceDialog> ResourceDialog { get; set; }
        public virtual DbSet<ResourceType> ResourceType { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<StaffRole> StaffRole { get; set; }
        public virtual DbSet<TemplateCategory> TemplateCategory { get; set; }
        public virtual DbSet<TemplateMeasurement> TemplateMeasurement { get; set; }
        public virtual DbSet<TemplateResource> TemplateResource { get; set; }
        public virtual DbSet<Treating> Treating { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:nhrm-app-db.database.windows.net,1433;Initial Catalog=nhrm-app-db;Persist Security Info=false;User ID=nhrm-admin;Password=Password1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataPoint>(entity =>
            {
                entity.HasKey(e => new { e.MeasurementId, e.DataPointNumber });

                entity.Property(e => e.MeasurementId).HasColumnName("MeasurementID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Measurement)
                    .WithMany(p => p.DataPoint)
                    .HasForeignKey(d => d.MeasurementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DataPoint_Measurement");
            });

            modelBuilder.Entity<DataPointRecord>(entity =>
            {
                entity.HasKey(e => new { e.Urnumber, e.CategoryId, e.MeasurementId, e.DataPointNumber });

                entity.Property(e => e.Urnumber).HasMaxLength(50);

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.MeasurementId).HasColumnName("MeasurementID");

                entity.Property(e => e.MeasurementRecordId).HasColumnName("MeasurementRecordID");

                entity.HasOne(d => d.DataPoint)
                    .WithMany(p => p.DataPointRecord)
                    .HasForeignKey(d => new { d.MeasurementId, d.DataPointNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DataPointRecord_DataPoint");

                entity.HasOne(d => d.MeasurementRecord)
                    .WithMany(p => p.DataPointRecord)
                    .HasForeignKey(d => new { d.MeasurementRecordId, d.MeasurementId, d.CategoryId, d.Urnumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DataPointRecord_MeasurementRecord");
            });

            modelBuilder.Entity<Measurement>(entity =>
            {
                entity.HasIndex(e => e.MeasurementName)
                    .HasName("UNIQUE_MeasurementName")
                    .IsUnique();

                entity.Property(e => e.MeasurementId).HasColumnName("MeasurementID");

                entity.Property(e => e.MeasurementName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MeasurementRecord>(entity =>
            {
                entity.HasKey(e => new { e.MeasurementRecordId, e.MeasurementId, e.CategoryId, e.Urnumber });

                entity.Property(e => e.MeasurementRecordId)
                    .HasColumnName("MeasurementRecordID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.MeasurementId).HasColumnName("MeasurementID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Urnumber).HasMaxLength(50);

                entity.Property(e => e.DateTimeRecorded).HasColumnType("datetime");

                entity.HasOne(d => d.PatientMeasurement)
                    .WithMany(p => p.MeasurementRecord)
                    .HasForeignKey(d => new { d.MeasurementId, d.CategoryId, d.Urnumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MeasurementRecord_PatientMeasurement");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.Urnumber);

                entity.HasIndex(e => e.Email)
                    .HasName("UNIQUE_Email")
                    .IsUnique();

                entity.Property(e => e.Urnumber).HasMaxLength(50);

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.CountryOfBirth)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HomeNumber).HasMaxLength(10);

                entity.Property(e => e.MobileNumber).HasMaxLength(10);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsFixedLength();

                entity.Property(e => e.PostCode)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.PreferredLanguage)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RegisteredBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Salt).IsRequired();

                entity.Property(e => e.Suburb)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SurName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.RegisteredByNavigation)
                    .WithMany(p => p.Patient)
                    .HasForeignKey(d => d.RegisteredBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patient_Staff");
            });

            modelBuilder.Entity<PatientCategory>(entity =>
            {
                entity.HasKey(e => new { e.CategoryId, e.Urnumber });

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Urnumber).HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.PatientCategory)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientCategory_TemplateCategory");

                entity.HasOne(d => d.HospitalNumberNavigation)
                    .WithMany(p => p.PatientCategory)
                    .HasForeignKey(d => d.Urnumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientCategory_Patient");
            });

            modelBuilder.Entity<PatientMeasurement>(entity =>
            {
                entity.HasKey(e => new { e.MeasurementId, e.CategoryId, e.Urnumber });

                entity.Property(e => e.MeasurementId).HasColumnName("MeasurementID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Urnumber).HasMaxLength(50);

                entity.HasOne(d => d.Measurement)
                    .WithMany(p => p.PatientMeasurement)
                    .HasForeignKey(d => d.MeasurementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientMeasurement_Measurement");

                entity.HasOne(d => d.PatientCategory)
                    .WithMany(p => p.PatientMeasurement)
                    .HasForeignKey(d => new { d.CategoryId, d.Urnumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientMeasurement_PatientCategory");
            });

            modelBuilder.Entity<PatientRecord>(entity =>
            {
                entity.HasKey(e => new { e.DateTimeRecorded, e.Urnumber, e.RecordTypeId });

                entity.Property(e => e.DateTimeRecorded).HasColumnType("datetime");

                entity.Property(e => e.Urnumber).HasMaxLength(50);

                entity.Property(e => e.RecordTypeId).HasColumnName("RecordTypeID");

                entity.HasOne(d => d.HospitalNumberNavigation)
                    .WithMany(p => p.PatientRecord)
                    .HasForeignKey(d => d.Urnumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientRecord_Patient");

                entity.HasOne(d => d.RecordType)
                    .WithMany(p => p.PatientRecord)
                    .HasForeignKey(d => d.RecordTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientRecord_RecordType");
            });

            modelBuilder.Entity<PatientResource>(entity =>
            {
                entity.HasKey(e => new { e.CategoryId, e.Urnumber, e.ResourceId });

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Urnumber).HasMaxLength(50);

                entity.Property(e => e.ResourceId).HasColumnName("ResourceID");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.PatientResource)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientResource_Resource");

                entity.HasOne(d => d.PatientCategory)
                    .WithMany(p => p.PatientResource)
                    .HasForeignKey(d => new { d.CategoryId, d.Urnumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientResource_PatientCategory");
            });

            modelBuilder.Entity<RecordCategory>(entity =>
            {
                entity.HasIndex(e => e.Category)
                    .HasName("UNIQUE_Category")
                    .IsUnique();

                entity.Property(e => e.RecordCategoryId).HasColumnName("RecordCategoryID");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RecordType>(entity =>
            {
                entity.HasIndex(e => e.RecordType1)
                    .HasName("UNIQUE_RecordType")
                    .IsUnique();

                entity.Property(e => e.RecordTypeId).HasColumnName("RecordTypeID");

                entity.Property(e => e.RecordCategoryId).HasColumnName("RecordCategoryID");

                entity.Property(e => e.RecordType1)
                    .IsRequired()
                    .HasColumnName("RecordType")
                    .HasMaxLength(50);

                entity.HasOne(d => d.RecordCategory)
                    .WithMany(p => p.RecordType)
                    .HasForeignKey(d => d.RecordCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RecordType_RecordCategory");
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.Property(e => e.ResourceId).HasColumnName("ResourceID");

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.Prompt)
                    .IsRequired()
                    .HasMaxLength(12);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(65);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Resource)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Resource_ResourceType");
            });

            modelBuilder.Entity<ResourceDialog>(entity =>
            {
                entity.Property(e => e.ResourceDialogId).HasColumnName("ResourceDialogID");

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.Heading)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.ResourceId).HasColumnName("ResourceID");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.ResourceDialog)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResourceDialog_Resource");
            });

            modelBuilder.Entity<ResourceType>(entity =>
            {
                entity.HasIndex(e => e.TypeName)
                    .HasName("UNIQUE_ResourceType_TypeName")
                    .IsUnique();

                entity.Property(e => e.ResourceTypeId).HasColumnName("ResourceTypeID");

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.Property(e => e.StaffId)
                    .HasColumnName("StaffID")
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsFixedLength();

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Salt).IsRequired();

                entity.Property(e => e.SurName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Staff_StaffRole");
            });

            modelBuilder.Entity<StaffRole>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.HasIndex(e => e.StaffType)
                    .HasName("UNIQUE_StaffType")
                    .IsUnique();

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.StaffType)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TemplateCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.HasIndex(e => e.CategoryName)
                    .HasName("UNIQUE_CategoryName")
                    .IsUnique();

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TemplateMeasurement>(entity =>
            {
                entity.HasKey(e => new { e.MeasurementId, e.CategoryId });

                entity.Property(e => e.MeasurementId).HasColumnName("MeasurementID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TemplateMeasurement)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TemplateMeasurement_TemplateCategory");

                entity.HasOne(d => d.Measurement)
                    .WithMany(p => p.TemplateMeasurement)
                    .HasForeignKey(d => d.MeasurementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TemplateMeasurement_Measurement");
            });

            modelBuilder.Entity<TemplateResource>(entity =>
            {
                entity.HasKey(e => new { e.CategoryId, e.ResourceId });

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ResourceId).HasColumnName("ResourceID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TemplateResource)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TemplateResource_TemplateCategory");

                entity.HasOne(d => d.Resource)
                    .WithMany(p => p.TemplateResource)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TemplateResource_Resource");
            });

            modelBuilder.Entity<Treating>(entity =>
            {
                entity.HasKey(e => new { e.StartDate, e.Urnumber, e.StaffId });

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Urnumber).HasMaxLength(50);

                entity.Property(e => e.StaffId)
                    .HasColumnName("StaffID")
                    .HasMaxLength(50);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.HasOne(d => d.HospitalNumberNavigation)
                    .WithMany(p => p.Treating)
                    .HasForeignKey(d => d.Urnumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Treating_Patient");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Treating)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Treating_Staff");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
