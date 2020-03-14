﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UTTAF.API.Data;

namespace UTTAF.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200314051825_RefatoringAuthModel")]
    partial class RefatoringAuthModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2");

            modelBuilder.Entity("UTTAF.Dependencies.Models.AuthModel", b =>
                {
                    b.Property<string>("SessionReference")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("SessionDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("SessionReference");

                    b.ToTable("Auths");
                });
#pragma warning restore 612, 618
        }
    }
}