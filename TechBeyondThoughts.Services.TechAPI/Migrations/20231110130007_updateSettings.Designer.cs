﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechBeyondThoughts.Services.TechAPI.Data;

#nullable disable

namespace TechBeyondThoughts.Services.TechAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231110130007_updateSettings")]
    partial class updateSettings
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TechBeyondThoughts.Services.TechAPI.Models.TechData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InventionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InventorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Techstacks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = "Machine Learning",
                            Description = "A field of computer science that simulates human intelligence in machines.",
                            ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.freepik.com%2Ffree-photos-vectors%2Fhr-ai%2F62&psig=AOvVaw3vymYJOV0QJXmKzYhn1jCM&ust=1699509512867000&source=images&cd=vfe&opi=89978449&ved=0CBIQjRxqFwoTCKjS67jcs4IDFQAAAAAdAAAAABAJ",
                            InventionDate = new DateTime(1956, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            InventorName = "John McCarthy",
                            Title = "Artificial Intelligence"
                        },
                        new
                        {
                            Id = 2,
                            Category = "Cryptocurrency",
                            Description = "A distributed ledger technology for secure and transparent transactions.",
                            ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.infoq.com%2Farticles%2Fblockchain-as-a-service-get-block%2F&psig=AOvVaw3jbqS1fPY4Bxv_HzE9cUwE&ust=1699509637212000&source=images&cd=vfe&opi=89978449&ved=0CBIQjRxqFwoTCMDmgfTcs4IDFQAAAAAdAAAAABAE",
                            InventionDate = new DateTime(2008, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            InventorName = "Satoshi Nakamoto",
                            Title = "Blockchain"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
