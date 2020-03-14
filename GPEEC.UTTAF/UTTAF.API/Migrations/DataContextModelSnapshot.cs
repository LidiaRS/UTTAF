﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UTTAF.API.Data;

namespace UTTAF.API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2");

            modelBuilder.Entity("UTTAF.Dependencies.Models.AuthModel", b =>
                {
                    b.Property<Guid>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("SessionDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SessionName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SessionPassword")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("SessionId");

                    b.ToTable("Auths");
                });
#pragma warning restore 612, 618
        }
    }
}
