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
                .HasAnnotation("ProductVersion", "3.1.9");

            modelBuilder.Entity("UTTAF.API.Models.AttendeeModel", b =>
                {
                    b.Property<Guid>("AttendeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SessionReference")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("AttendeeId");

                    b.ToTable("Attendees");
                });

            modelBuilder.Entity("UTTAF.API.Models.RobotModel", b =>
                {
                    b.Property<Guid>("RobotId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataOperation")
                        .HasColumnType("TEXT");

                    b.Property<int>("RobotStatus")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SessionReference")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("RobotId");

                    b.ToTable("Robots");
                });

            modelBuilder.Entity("UTTAF.API.Models.SessionModel", b =>
                {
                    b.Property<Guid>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("SessionDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("SessionReference")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SessionStatus")
                        .HasColumnType("INTEGER");

                    b.HasKey("SessionId");

                    b.ToTable("Sessions");
                });
#pragma warning restore 612, 618
        }
    }
}
