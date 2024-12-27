﻿// <auto-generated />
using Battleship.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace P1_Battleship.Migrations.Grid
{
    [DbContext(typeof(GridContext))]
    [Migration("20241227022718_GridIDGenerated")]
    partial class GridIDGenerated
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Battleship.API.Model.Grid", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.PrimitiveCollection<string>("columns")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("height")
                        .HasColumnType("int");

                    b.Property<int>("width")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("grids");
                });
#pragma warning restore 612, 618
        }
    }
}
