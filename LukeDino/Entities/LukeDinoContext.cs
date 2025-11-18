using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LukeDino.Entities;

public partial class LukeDinoContext : DbContext
{
    public LukeDinoContext()
    {
    }

    public LukeDinoContext(DbContextOptions<LukeDinoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dino> Dinos { get; set; }

    public virtual DbSet<UserDino> UserDinos { get; set; }

    public virtual DbSet<Userprofile> Userprofiles { get; set; }    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dino>(entity =>
        {
            entity.HasKey(e => e.DinoId).HasName("PK__dino__06366D0EF819DE50");

            entity.ToTable("dino");

            entity.Property(e => e.DinoId).HasColumnName("dino_id");
            entity.Property(e => e.DietType)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("diet_type");
            entity.Property(e => e.DinoType)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("dino_type");
            entity.Property(e => e.EraLived)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("era_lived");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("image_url");
            entity.Property(e => e.Length).HasColumnName("length");
            entity.Property(e => e.LocationLived)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("location_lived");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Weight).HasColumnName("weight");
        });

        modelBuilder.Entity<UserDino>(entity =>
        {
            entity.HasKey(e => e.UserDinoId).HasName("PK__user_din__1654D5FBD9AA139B");

            entity.ToTable("user_dino");

            entity.Property(e => e.UserDinoId).HasColumnName("user_dino_id");
            entity.Property(e => e.DinoId).HasColumnName("dino_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Dino).WithMany(p => p.UserDinos)
                .HasForeignKey(d => d.DinoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_dino__dino___4E88ABD4");

            entity.HasOne(d => d.User).WithMany(p => p.UserDinos)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_dino__user___4D94879B");
        });

        modelBuilder.Entity<Userprofile>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__user__B9BE370FBD7CC337");

            entity.ToTable("userprofile");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("avatar_url");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirebaseuserId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("firebaseuser_id");
            entity.Property(e => e.IsMember).HasColumnName("is_member");
            entity.Property(e => e.Uid)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("uid");
            entity.Property(e => e.YoutubeAvatarUrl)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("youtube_avatar_URL");
            entity.Property(e => e.YoutubeChannelId)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("youtube_channel_id");
            entity.Property(e => e.YoutubeChannelTitle)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("youtube_channel_title");
            entity.Property(e => e.YoutubeLinkedDate).HasColumnName("youtube_linked_date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
