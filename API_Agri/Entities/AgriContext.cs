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

    public virtual DbSet<Terrain> Terrains { get; set; }

    public virtual DbSet<Reserve> Reserves { get; set; }

    public virtual DbSet<TerrainReserve> TerrainsReserves { get; set; }


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

            entity.HasIndex(e => e.StadePlanteId, "stade_plante_id");

            entity.Property(e => e.StadePlanteId).HasColumnName("stade_plante_id");
            entity.Property(e => e.StadeKc).HasColumnName("stade_kc");

            entity.HasOne(d => d.Plante).WithMany()
                .HasForeignKey(d => d.StadePlanteId)
                .HasConstraintName("plante_id");
        });

        modelBuilder.Entity<Terrain>(entity =>
        {
            entity.HasKey(e => e.TerrainId).HasName("PRIMARY");

            entity.ToTable("terrain");

            entity.HasIndex(e => e.TerrainPlanteId, "plante_id_idx");

            entity.Property(e => e.TerrainId)
                .HasColumnName("terrain_id");
            entity.Property(e => e.TerrainInsee)
                .HasMaxLength(5)
                .HasColumnName("terrain_insee");
            entity.Property(e => e.TerrainNom)
                .HasMaxLength(45)
                .HasColumnName("terrain_nom");
            entity.Property(e => e.TerrainSurface)
                .HasColumnName("terrain_surface");
            entity.Property(e => e.TerrainPlanteId).HasColumnName("terrain_plante_id");

            entity.HasOne(d => d.Plante).WithMany()
                .HasForeignKey(d => d.TerrainPlanteId)
                .HasConstraintName("plante_id");
        });

        modelBuilder.Entity<Reserve>(entity =>
        {
            entity.HasKey(e => e.ReserveId).HasName("PRIMARY");

            entity.ToTable("reserve");

            entity.Property(e => e.ReserveId)
                .HasColumnName("reserve_id");
            entity.Property(e => e.ReserveMax)
                .HasColumnName("reserve_max");
            entity.Property(e => e.ReserveActuel)
                .HasColumnName("reserve_actuel");
        });
        
        modelBuilder.Entity<TerrainReserve>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("terrain_reserve");

            entity.HasIndex(e => e.TerrainId, "terrain_id_idx");

            entity.HasIndex(e => e.ReserveId, "reserve_id_idx");

            entity.Property(e => e.TerrainId)
                .HasColumnName("terrain_id");
            entity.Property(e => e.ReserveId)
                .HasColumnName("reserve_id");

            entity.HasOne(d => d.Terrain).WithMany()
                .HasForeignKey(d => d.TerrainId)
                .HasConstraintName("terrain_id");

            entity.HasOne(d => d.Reserve).WithMany()
                .HasForeignKey(d => d.ReserveId)
                .HasConstraintName("reserve_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
