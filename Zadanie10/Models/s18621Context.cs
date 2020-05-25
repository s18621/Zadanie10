﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadanie10.Models
{
    public partial class s18621Context : DbContext
    {
        public s18621Context() { }

        public s18621Context(DbContextOptions<s18621Context> options) : base(options) { }

        public virtual DbSet<Enrollment> Enrollment { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Studies> Studies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer("Data Source=db-mssql;Initial Catalog=s18621;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.IdEnrollment).HasName("Enrollment_pk");
                entity.Property(e => e.IdEnrollment).ValueGeneratedNever();
                entity.Property(e => e.StartDate).HasColumnType("date");
                entity.HasOne(d => d.Nav).WithMany(p => p.EnrollmentList).HasForeignKey(d => d.IdStudy).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Enrollment_Studies");
            });
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.IndexNumber).HasName("Student_pk");
                entity.Property(e => e.IndexNumber).HasMaxLength(100);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.BirthDate).IsRequired().HasColumnType("date");
                entity.HasOne(d => d.Nav).WithMany(p => p.StudentList).HasForeignKey(d => d.IdEnrollment).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Student_Enrollment");
            });
            modelBuilder.Entity<Studies>(entity =>
            {
                entity.HasKey(e => e.IdStudy).HasName("Studies_pk");
                entity.Property(e => e.IdStudy).ValueGeneratedNever();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

