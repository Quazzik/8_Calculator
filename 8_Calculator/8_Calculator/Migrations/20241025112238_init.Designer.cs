﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace _8_Calculator.Migrations
{
    [DbContext(typeof(BatabaseContext))]
    [Migration("20241025112238_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("_8_Calculator.DB.Entities.Calculation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Operand1")
                        .HasColumnType("double");

                    b.Property<double>("Operand2")
                        .HasColumnType("double");

                    b.Property<string>("Operation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Result")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("Calculations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Operand1 = 12.0,
                            Operand2 = 2.0,
                            Operation = "Add",
                            Result = 14.0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
