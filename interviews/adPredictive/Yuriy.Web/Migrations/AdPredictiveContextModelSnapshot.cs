﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Yuriy.Web.Data;

namespace Yuriy.Web.Migrations
{
    [DbContext(typeof(AdPredictiveContext))]
    partial class AdPredictiveContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Yuriy.Web.Data.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<DateTimeOffset>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset(0)")
                        .HasDefaultValueSql("(sysdatetimeoffset())");

                    b.Property<int>("Type");

                    b.Property<int>("User");

                    b.HasKey("Id");

                    b.HasIndex("Type");

                    b.HasIndex("User");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("Yuriy.Web.Data.NotificationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("NotificationType");

                    b.HasData(
                        new { Id = 1, Name = "New Comment" },
                        new { Id = 2, Name = "Campaign Status Changed " },
                        new { Id = 3, Name = "New Report Available" }
                    );
                });

            modelBuilder.Entity("Yuriy.Web.Data.NotificationUnsubscribe", b =>
                {
                    b.Property<int>("User");

                    b.Property<int>("Type");

                    b.Property<int?>("Id");

                    b.HasKey("User", "Type");

                    b.HasIndex("Type");

                    b.ToTable("NotificationUnsubscribe");
                });

            modelBuilder.Entity("Yuriy.Web.Data.NotificationWhileUnsubscribed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<DateTimeOffset>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset(0)")
                        .HasDefaultValueSql("(sysdatetimeoffset())");

                    b.Property<int>("Type");

                    b.Property<int>("User");

                    b.HasKey("Id");

                    b.HasIndex("Type");

                    b.HasIndex("User");

                    b.ToTable("NotificationWhileUnsubscribed");
                });

            modelBuilder.Entity("Yuriy.Web.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new { Id = 1, FirstName = "William", LastName = "Brown" },
                        new { Id = 2, FirstName = "Kyle", LastName = "Burnham" }
                    );
                });

            modelBuilder.Entity("Yuriy.Web.Data.Notification", b =>
                {
                    b.HasOne("Yuriy.Web.Data.NotificationType", "TypeNavigation")
                        .WithMany("Notification")
                        .HasForeignKey("Type")
                        .HasConstraintName("FK_Notification_NotificationType");

                    b.HasOne("Yuriy.Web.Data.User", "UserNavigation")
                        .WithMany("Notification")
                        .HasForeignKey("User")
                        .HasConstraintName("FK_Notification_User");
                });

            modelBuilder.Entity("Yuriy.Web.Data.NotificationUnsubscribe", b =>
                {
                    b.HasOne("Yuriy.Web.Data.NotificationType", "TypeNavigation")
                        .WithMany("NotificationUnsubscribe")
                        .HasForeignKey("Type")
                        .HasConstraintName("FK_NotificationUnsubscribe_NotificationType");

                    b.HasOne("Yuriy.Web.Data.User", "UserNavigation")
                        .WithMany("NotificationUnsubscribe")
                        .HasForeignKey("User")
                        .HasConstraintName("FK_NotificationUnsubscribe_User");
                });

            modelBuilder.Entity("Yuriy.Web.Data.NotificationWhileUnsubscribed", b =>
                {
                    b.HasOne("Yuriy.Web.Data.NotificationType", "TypeNavigation")
                        .WithMany("NotificationWhileUnsubscribed")
                        .HasForeignKey("Type")
                        .HasConstraintName("FK_NotificationWhileUnsubscribed_NotificationType");

                    b.HasOne("Yuriy.Web.Data.User", "UserNavigation")
                        .WithMany("NotificationWhileUnsubscribed")
                        .HasForeignKey("User")
                        .HasConstraintName("FK_NotificationWhileUnsubscribed_User");
                });
#pragma warning restore 612, 618
        }
    }
}
