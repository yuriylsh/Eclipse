﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Yuriy.Web.Data
{
    public partial class AdPerspectiveContext : DbContext
    {
        public AdPerspectiveContext()
        {
        }

        public AdPerspectiveContext(DbContextOptions<AdPerspectiveContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<NotificationType> NotificationType { get; set; }
        public virtual DbSet<NotificationUnsubscribe> NotificationUnsubscribe { get; set; }
        public virtual DbSet<NotificationWhileUnsubscribed> NotificationWhileUnsubscribed { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Integrated Security=SSPI;Database=adPerspective");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.Date).HasDefaultValueSql("(sysdatetimeoffset())");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Notification)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notification_NotificationType");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.Notification)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notification_User");
            });

            modelBuilder.Entity<NotificationUnsubscribe>(entity =>
            {
                entity.HasKey(e => new { e.User, e.Type });

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.NotificationUnsubscribe)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NotificationUnsubscribe_NotificationType");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.NotificationUnsubscribe)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NotificationUnsubscribe_User");
            });

            modelBuilder.Entity<NotificationWhileUnsubscribed>(entity =>
            {
                entity.Property(e => e.Date).HasDefaultValueSql("(sysdatetimeoffset())");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.NotificationWhileUnsubscribed)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NotificationWhileUnsubscribed_NotificationType");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.NotificationWhileUnsubscribed)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NotificationWhileUnsubscribed_User");
            });
        }
    }
}