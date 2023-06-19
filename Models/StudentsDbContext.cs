using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SutdManagmentSysAPI.Models;

public partial class StudentsDbContext : DbContext
{
    public StudentsDbContext()
    {
    }

    public StudentsDbContext(DbContextOptions<StudentsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Tblcourse> Tblcourses { get; set; }

    public virtual DbSet<Tblstud> Tblstuds { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=Shreya\\SQLEXPRESS;Initial Catalog=StudentsDB;Integrated Security=True; Encrypt=False;Connection Timeout=3600;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tblcourse>(entity =>
        {
            entity.HasKey(e => e.Cid).HasName("PK__tblcours__D837D05FB11642D6");

            entity.ToTable("tblcourse");

            entity.Property(e => e.Cid)
                .ValueGeneratedNever()
                .HasColumnName("cid");
            entity.Property(e => e.Cname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cname");
            entity.Property(e => e.Cyears).HasColumnName("cyears");
        });

        modelBuilder.Entity<Tblstud>(entity =>
        {
            entity.HasKey(e => e.Sid).HasName("PK__tblstud__DDDFDD367618F414");

            entity.ToTable("tblstud");

            entity.Property(e => e.Sid).HasColumnName("sid");
            entity.Property(e => e.Add)
                .IsUnicode(false)
                .HasColumnName("add");
            entity.Property(e => e.Cid).HasColumnName("cid");
            entity.Property(e => e.Div).HasColumnName("div");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("dob");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Per12)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("per12");
            entity.Property(e => e.Rno)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("rno");
            entity.Property(e => e.Sem).HasColumnName("sem");

            entity.HasOne(d => d.CidNavigation).WithMany(p => p.Tblstuds)
                .HasForeignKey(d => d.Cid)
                .HasConstraintName("FK__tblstud__cid__04E4BC85");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.Rno).HasName("PK__test__C2B7F59B16BEF4A9");

            entity.ToTable("test");

            entity.Property(e => e.Rno)
                .ValueGeneratedNever()
                .HasColumnName("rno");
            entity.Property(e => e.Branch)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("branch");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("dob");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Stadd)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("stadd");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
