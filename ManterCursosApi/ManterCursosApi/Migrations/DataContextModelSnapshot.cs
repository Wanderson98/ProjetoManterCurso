﻿// <auto-generated />
using System;
using AndradeCursosApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AndradeCursosApi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.6");

            modelBuilder.Entity("AndradeCursosApi.Models.Categoria", b =>
                {
                    b.Property<int>("CategoriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CategoriaNome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("CategoriaId");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("AndradeCursosApi.Models.Curso", b =>
                {
                    b.Property<int>("CursoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CursoDataFinal")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CursoDataInicial")
                        .HasColumnType("TEXT");

                    b.Property<string>("CursoDescricao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<int?>("CursoQuantidadeAlunos")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAtivo")
                        .HasColumnType("INTEGER");

                    b.HasKey("CursoId");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("AndradeCursosApi.Models.Log", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CursoId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LogDataAtualizacao")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LogDataInclusao")
                        .HasColumnType("TEXT");

                    b.Property<string>("Usuario")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

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
