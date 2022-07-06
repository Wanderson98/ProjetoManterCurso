﻿// <auto-generated />
using System;
using AndradeCursosApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AndradeCursosApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220706172657_InclusaoTabelaLog")]
    partial class InclusaoTabelaLog
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AndradeCursosApi.Models.Categoria", b =>
                {
                    b.Property<int>("CategoriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoriaId"), 1L, 1);

                    b.Property<string>("CategoriaNome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CategoriaId");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("AndradeCursosApi.Models.Curso", b =>
                {
                    b.Property<int>("CursoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CursoId"), 1L, 1);

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CursoDataFinal")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CursoDataInicial")
                        .HasColumnType("datetime2");

                    b.Property<string>("CursoDescricao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("CursoQuantidadeAlunos")
                        .HasColumnType("int");

                    b.Property<bool>("IsAtivo")
                        .HasColumnType("bit");

                    b.HasKey("CursoId");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("AndradeCursosApi.Models.Log", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LogId"), 1L, 1);

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LogDataAtualizacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LogDataInclusao")
                        .HasColumnType("datetime2");

                    b.HasKey("LogId");

                    b.HasIndex("CursoId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("AndradeCursosApi.Models.Curso", b =>
                {
                    b.HasOne("AndradeCursosApi.Models.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("AndradeCursosApi.Models.Log", b =>
                {
                    b.HasOne("AndradeCursosApi.Models.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curso");
                });
#pragma warning restore 612, 618
        }
    }
}
