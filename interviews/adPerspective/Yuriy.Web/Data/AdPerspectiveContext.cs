using Microsoft.EntityFrameworkCore;
using Yuriy.Core;

namespace Yuriy.Web.Data
{
    public partial class AdPerspectiveContext : DbContext
    {
        public AdPerspectiveContext() {}

        public AdPerspectiveContext(DbContextOptions<AdPerspectiveContext> options): base(options) {}

        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<NotificationType> NotificationType { get; set; }
        public virtual DbSet<NotificationUnsubscribe> NotificationUnsubscribe { get; set; }
        public virtual DbSet<NotificationWhileUnsubscribed> NotificationWhileUnsubscribed { get; set; }
        public virtual DbSet<User> User { get; set; }

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

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NotificationType>().HasData(
                new NotificationType { Id = (int)NotificationTypes.NewComment, Name = "New Comment" },
                new NotificationType { Id = (int)NotificationTypes.CampaignStatusChanged, Name = "Campaign Status Changed " },
                new NotificationType { Id = (int)NotificationTypes.NewReportAvailable, Name = "New Report Available" }
            );

            // I know very well that storing user passwords in plain text
            // instead of a hashed value in database is a big NO-NO in real software,
            // but I decided it is acceptable for this mock situation.
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "William", LastName = "Brown", Password = "WilliamPassword" },
                new User { Id = 2, FirstName = "Kyle", LastName = "Burnham", Password = "KylePassword" }
            );
        }
    }
}
