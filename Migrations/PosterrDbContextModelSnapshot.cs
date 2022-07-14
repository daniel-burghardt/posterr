﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Posterr.Models;

#nullable disable

namespace Posterr.Migrations
{
    [DbContext(typeof(PosterrDbContext))]
    partial class PosterrDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Posterr.Models.Posts.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostId"), 1L, 1);

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Post");
                });

            modelBuilder.Entity("Posterr.Models.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Username = "user_one"
                        },
                        new
                        {
                            Id = 2,
                            Username = "user_two"
                        },
                        new
                        {
                            Id = 3,
                            Username = "user_three"
                        });
                });

            modelBuilder.Entity("Posterr.Models.Posts.OriginalPost", b =>
                {
                    b.HasBaseType("Posterr.Models.Posts.Post");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(777)
                        .HasColumnType("nvarchar(777)");

                    b.HasDiscriminator().HasValue("OriginalPost");
                });

            modelBuilder.Entity("Posterr.Models.Posts.QuotePost", b =>
                {
                    b.HasBaseType("Posterr.Models.Posts.Post");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(777)
                        .HasColumnType("nvarchar(777)")
                        .HasColumnName("QuotePost_Content");

                    b.Property<int>("QuotedPostId")
                        .HasColumnType("int");

                    b.HasIndex("QuotedPostId");

                    b.HasDiscriminator().HasValue("QuotePost");
                });

            modelBuilder.Entity("Posterr.Models.Posts.Repost", b =>
                {
                    b.HasBaseType("Posterr.Models.Posts.Post");

                    b.Property<int>("RepostedPostId")
                        .HasColumnType("int");

                    b.HasIndex("RepostedPostId");

                    b.HasDiscriminator().HasValue("Repost");
                });

            modelBuilder.Entity("Posterr.Models.Posts.Post", b =>
                {
                    b.HasOne("Posterr.Models.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Posterr.Models.Posts.QuotePost", b =>
                {
                    b.HasOne("Posterr.Models.Posts.Post", "QuotedPost")
                        .WithMany()
                        .HasForeignKey("QuotedPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuotedPost");
                });

            modelBuilder.Entity("Posterr.Models.Posts.Repost", b =>
                {
                    b.HasOne("Posterr.Models.Posts.Post", "RepostedPost")
                        .WithMany()
                        .HasForeignKey("RepostedPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RepostedPost");
                });
#pragma warning restore 612, 618
        }
    }
}
