using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tugas3_Api_reza.Models;

public partial class SimpleCRUD : DbContext
{
    public SimpleCRUD()
    {
    }

    public SimpleCRUD(DbContextOptions<SimpleCRUD> options)
        : base(options)
    {
    }

    public virtual DbSet<AsalSekolah> AsalSekolahs { get; set; }

    public virtual DbSet<Siswa> Siswas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-ND5V00I\\SQLEXPRESS;Database=SimpleCRUD;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Siswa>(entity =>
        {
            entity.Property(e => e.Sex).IsFixedLength();

            entity.HasOne(d => d.AsalSekolah).WithMany(p => p.Siswas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Siswa_AsalSekolah");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
