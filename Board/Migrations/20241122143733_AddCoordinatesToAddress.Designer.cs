﻿// <auto-generated />
using System;
using Board.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Board.Migrations
{
    [DbContext(typeof(BoardContext))]
    [Migration("20241122143733_AddCoordinatesToAddress")]
    partial class AddCoordinatesToAddress
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Board.Entities.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Board.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("datetime2");

                    b.Property<int>("WorkItemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("WorkItemId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Board.Entities.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("States");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Value = "To Do"
                        },
                        new
                        {
                            Id = 2,
                            Value = "Doing"
                        },
                        new
                        {
                            Id = 3,
                            Value = "Done"
                        });
                });

            modelBuilder.Entity("Board.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Board.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Board.Entities.ViewModels.TopAuthor", b =>
                {
                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkItemsCreated")
                        .HasColumnType("int");

                    b.ToTable((string)null);

                    b.ToView("View_TopAuthors", (string)null);
                });

            modelBuilder.Entity("Board.Entities.WorkItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Area")
                        .HasColumnType("varchar(200)");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("IterationPath")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Iteration_Path");

                    b.Property<int>("Priority")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("StateId");

                    b.ToTable("WorkItems");

                    b.HasDiscriminator().HasValue("WorkItem");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Board.Entities.WorkItemTag", b =>
                {
                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<int>("WorkItemId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublicationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("TagId", "WorkItemId");

                    b.HasIndex("WorkItemId");

                    b.ToTable("WorkItemTag");
                });

            modelBuilder.Entity("Board.Entities.Epic", b =>
                {
                    b.HasBaseType("Board.Entities.WorkItem");

                    b.Property<DateTime?>("EndDate")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasDiscriminator().HasValue("Epic");
                });

            modelBuilder.Entity("Board.Entities.Issue", b =>
                {
                    b.HasBaseType("Board.Entities.WorkItem");

                    b.Property<decimal>("Efford")
                        .HasColumnType("decimal(5,2)");

                    b.HasDiscriminator().HasValue("Issue");
                });

            modelBuilder.Entity("Board.Entities.Task", b =>
                {
                    b.HasBaseType("Board.Entities.WorkItem");

                    b.Property<string>("Activity")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("RemaningWork")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.HasDiscriminator().HasValue("Task");
                });

            modelBuilder.Entity("Board.Entities.Address", b =>
                {
                    b.HasOne("Board.Entities.User", "User")
                        .WithOne("Address")
                        .HasForeignKey("Board.Entities.Address", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Board.Entities.Coordinate", "Coordinate", b1 =>
                        {
                            b1.Property<Guid>("AddressId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal?>("Latitude")
                                .HasPrecision(18, 7)
                                .HasColumnType("decimal(18,7)");

                            b1.Property<decimal?>("Longitude")
                                .HasPrecision(18, 7)
                                .HasColumnType("decimal(18,7)");

                            b1.HasKey("AddressId");

                            b1.ToTable("Addresses");

                            b1.WithOwner()
                                .HasForeignKey("AddressId");
                        });

                    b.Navigation("Coordinate");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Board.Entities.Comment", b =>
                {
                    b.HasOne("Board.Entities.User", "Author")
                        .WithMany("Comments")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Board.Entities.WorkItem", "WorkItem")
                        .WithMany("Comments")
                        .HasForeignKey("WorkItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("WorkItem");
                });

            modelBuilder.Entity("Board.Entities.WorkItem", b =>
                {
                    b.HasOne("Board.Entities.User", "Author")
                        .WithMany("WorkItems")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Board.Entities.State", "State")
                        .WithMany("WorkItems")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("State");
                });

            modelBuilder.Entity("Board.Entities.WorkItemTag", b =>
                {
                    b.HasOne("Board.Entities.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Board.Entities.WorkItem", "WorkItem")
                        .WithMany()
                        .HasForeignKey("WorkItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");

                    b.Navigation("WorkItem");
                });

            modelBuilder.Entity("Board.Entities.State", b =>
                {
                    b.Navigation("WorkItems");
                });

            modelBuilder.Entity("Board.Entities.User", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("Comments");

                    b.Navigation("WorkItems");
                });

            modelBuilder.Entity("Board.Entities.WorkItem", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
