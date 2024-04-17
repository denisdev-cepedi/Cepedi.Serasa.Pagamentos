﻿// <auto-generated />
using System;
using Cepedi.Serasa.Pagamento.Dados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cepedi.Serasa.Pagamento.Dados.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240417130319_fiveMigration")]
    partial class fiveMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Cepedi.Serasa.Pagamento.Dominio.Entidades.CredorEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Credor", (string)null);
                });

            modelBuilder.Entity("Cepedi.Serasa.Pagamento.Dominio.Entidades.DividaEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataDeVencimento")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCredor")
                        .HasColumnType("int");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("IdCredor");

                    b.ToTable("Divida", (string)null);
                });

            modelBuilder.Entity("Cepedi.Serasa.Pagamento.Dominio.Entidades.PagamentoEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataDePagamento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataDeVencimento")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCredor")
                        .HasColumnType("int");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("IdCredor");

                    b.ToTable("Pagamento", (string)null);
                });

            modelBuilder.Entity("Cepedi.Serasa.Pagamento.Dominio.Entidades.PessoaEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Pessoa", (string)null);
                });

            modelBuilder.Entity("Cepedi.Serasa.Pagamento.Dominio.Entidades.UsuarioEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<bool>("CelularValidado")
                        .HasColumnType("bit");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Usuario", (string)null);
                });

            modelBuilder.Entity("Cepedi.Serasa.Pagamento.Dominio.Entidades.DividaEntity", b =>
                {
                    b.HasOne("Cepedi.Serasa.Pagamento.Dominio.Entidades.CredorEntity", "Credor")
                        .WithMany()
                        .HasForeignKey("IdCredor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Credor");
                });

            modelBuilder.Entity("Cepedi.Serasa.Pagamento.Dominio.Entidades.PagamentoEntity", b =>
                {
                    b.HasOne("Cepedi.Serasa.Pagamento.Dominio.Entidades.CredorEntity", "Credor")
                        .WithMany()
                        .HasForeignKey("IdCredor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Credor");
                });
#pragma warning restore 612, 618
        }
    }
}
