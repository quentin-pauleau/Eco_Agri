using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API_Agri.Entities;

public partial class AgriContext : DbContext
{
    public AgriContext()
    {
    }

    public AgriContext(DbContextOptions<AgriContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Plante> Plantes { get; set; }

    public virtual DbSet<Stade> Stades { get; set; }

    public virtual DbSet<StadePlante> StadePlantes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Plante>(entity =>
        {
            entity.HasKey(e => e.PlanteId).HasName("PRIMARY");

            entity.ToTable("plante");

            entity.Property(e => e.PlanteId).HasColumnName("Plante_ID");
            entity.Property(e => e.PlanteNom)
                .HasMaxLength(45)
                .HasColumnName("Plante_nom");
            entity.Property(e => e.PlanteType)
                .HasMaxLength(45)
                .HasColumnName("Plante_type");
        });

        modelBuilder.Entity<Stade>(entity =>
        {
            entity.HasKey(e => e.StadeId).HasName("PRIMARY");

            entity.ToTable("stade");

            entity.Property(e => e.StadeId).HasColumnName("stade_id");
            entity.Property(e => e.StadeDescription)
                .HasMaxLength(64)
                .HasColumnName("stade_description");
        });

        modelBuilder.Entity<StadePlante>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("stade_plante");

            entity.HasIndex(e => e.PlanteId, "plante_id_idx");

            entity.HasIndex(e => e.StadeId, "stade_id_idx");

            entity.Property(e => e.PlanteId).HasColumnName("plante_id");
            entity.Property(e => e.StadeId).HasColumnName("stade_id");
            entity.Property(e => e.StadePlanteKc)
                .HasDefaultValueSql("'0'")
                .HasColumnName("stade_plante_kc");

            entity.HasOne(d => d.Plante).WithMany()
                .HasForeignKey(d => d.PlanteId)
                .HasConstraintName("plante_id");

            entity.HasOne(d => d.Stade).WithMany()
                .HasForeignKey(d => d.StadeId)
                .HasConstraintName("stade_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
