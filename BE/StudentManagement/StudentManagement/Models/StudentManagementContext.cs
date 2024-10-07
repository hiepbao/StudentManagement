using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Models;

public partial class StudentManagementContext : DbContext
{
    public StudentManagementContext()
    {
    }

    public StudentManagementContext(DbContextOptions<StudentManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<ClassDetail> ClassDetails { get; set; }

    public virtual DbSet<Diligence> Diligences { get; set; }

    public virtual DbSet<Funtion> Funtions { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<HomeroomTeacher> HomeroomTeachers { get; set; }

    public virtual DbSet<Parent> Parents { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Semester> Semesters { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudyPoint> StudyPoints { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.Property(e => e.ClassName).IsFixedLength();
            entity.Property(e => e.SchoolYear).IsFixedLength();

            entity.HasOne(d => d.Grade).WithMany(p => p.Classes).HasConstraintName("FK_Class_Grades");
        });

        modelBuilder.Entity<ClassDetail>(entity =>
        {
            entity.HasOne(d => d.Class).WithMany(p => p.ClassDetails).HasConstraintName("FK_ClassDetail_Class");

            entity.HasOne(d => d.HomeroomTeacher).WithMany(p => p.ClassDetails).HasConstraintName("FK_ClassDetail_HomeroomTeacher");

            entity.HasOne(d => d.Teacher).WithMany(p => p.ClassDetails).HasConstraintName("FK_ClassDetail_Teachers");
        });

        modelBuilder.Entity<Diligence>(entity =>
        {
            entity.Property(e => e.ExcusedAbsence).IsFixedLength();
            entity.Property(e => e.UnexcusedAbsence).IsFixedLength();

            entity.HasOne(d => d.Class).WithMany(p => p.Diligences).HasConstraintName("FK_Diligence_Class");

            entity.HasOne(d => d.Semester).WithMany(p => p.Diligences).HasConstraintName("FK_Diligence_Semesters");

            entity.HasOne(d => d.Student).WithMany(p => p.Diligences).HasConstraintName("FK_Diligence_Student");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Diligences).HasConstraintName("FK_Diligence_Teachers");
        });

        modelBuilder.Entity<Funtion>(entity =>
        {
            entity.HasOne(d => d.Role).WithMany(p => p.Funtions).HasConstraintName("FK_Funtions_Roles");
        });

        modelBuilder.Entity<HomeroomTeacher>(entity =>
        {
            entity.HasOne(d => d.Teacher).WithMany(p => p.HomeroomTeachers).HasConstraintName("FK_HomeroomTeacher_Teachers");
        });

        modelBuilder.Entity<Parent>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.Parents).HasConstraintName("FK_Parents_Users");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK_Role");
        });

        modelBuilder.Entity<Semester>(entity =>
        {
            entity.Property(e => e.SemesterName).IsFixedLength();
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasOne(d => d.Class).WithMany(p => p.Students).HasConstraintName("FK_Student_Class");

            entity.HasOne(d => d.Parent).WithMany(p => p.Students).HasConstraintName("FK_Student_Parents");

            entity.HasOne(d => d.User).WithMany(p => p.Students).HasConstraintName("FK_Student_Users");
        });

        modelBuilder.Entity<StudyPoint>(entity =>
        {
            entity.HasOne(d => d.Class).WithMany(p => p.StudyPoints).HasConstraintName("FK_StudyPoints_Class");

            entity.HasOne(d => d.Semester).WithMany(p => p.StudyPoints).HasConstraintName("FK_StudyPoints_Semesters");

            entity.HasOne(d => d.Student).WithMany(p => p.StudyPoints).HasConstraintName("FK_StudyPoints_Student");

            entity.HasOne(d => d.Subject).WithMany(p => p.StudyPoints).HasConstraintName("FK_StudyPoints_Subject");

            entity.HasOne(d => d.Teacher).WithMany(p => p.StudyPoints).HasConstraintName("FK_StudyPoints_Teachers");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasOne(d => d.Subject).WithMany(p => p.Teachers).HasConstraintName("FK_Teachers_Subject");

            entity.HasOne(d => d.User).WithMany(p => p.Teachers).HasConstraintName("FK_Teachers_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_User");

            entity.Property(e => e.Phone).IsFixedLength();
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles).HasConstraintName("FK_UserRole_Roles");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles).HasConstraintName("FK_UserRole_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
