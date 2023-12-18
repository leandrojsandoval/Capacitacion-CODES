﻿// <auto-generated />
using System;
using ARQ.Datos.EFScafolding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ARQ.Datos.Migrations
{
    [DbContext(typeof(ARQContext))]
    [Migration("20231215235201_CreacionTablaClientes")]
    partial class CreacionTablaClientes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ARQ.Entidades.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_CLIENTE")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit")
                        .HasColumnName("ACTIVO");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("APELLIDO");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("datetime2")
                        .HasColumnName("FECHA_ALTA");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime2")
                        .HasColumnName("FECHA_MODIFICACION");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2")
                        .HasColumnName("FECHA_NACIMIENTO");

                    b.Property<int>("IdUsuarioAlta")
                        .HasColumnType("int")
                        .HasColumnName("ID_USUARIO_ALTA");

                    b.Property<int?>("IdUsuarioModificacion")
                        .HasColumnType("int")
                        .HasColumnName("ID_USUARIO_MODIFICACION");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("NOMBRE");

                    b.HasKey("Id");

                    b.HasIndex("IdUsuarioAlta");

                    b.HasIndex("IdUsuarioModificacion");

                    b.ToTable("TBL_CLIENTES");
                });

            modelBuilder.Entity("ARQ.Entidades.Funcionalidad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_FUNCIONALIDAD")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit")
                        .HasColumnName("ACTIVO");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("DESCRIPCION");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("datetime2")
                        .HasColumnName("FECHA_ALTA");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime2")
                        .HasColumnName("FECHA_MODIFICACION");

                    b.Property<int>("IdUsuarioAlta")
                        .HasColumnType("int")
                        .HasColumnName("ID_USUARIO_ALTA");

                    b.Property<int?>("IdUsuarioModificacion")
                        .HasColumnType("int")
                        .HasColumnName("ID_USUARIO_MODIFICACION");

                    b.HasKey("Id");

                    b.HasIndex("IdUsuarioAlta");

                    b.HasIndex("IdUsuarioModificacion");

                    b.ToTable("TBL_FUNCIONALIDADES");
                });

            modelBuilder.Entity("ARQ.Entidades.FuncionalidadRol", b =>
                {
                    b.Property<int>("IdFuncionalidad")
                        .HasColumnType("int")
                        .HasColumnName("ID_FUNCIONALIDAD");

                    b.Property<int>("IdRol")
                        .HasColumnType("int")
                        .HasColumnName("ID_ROL");

                    b.Property<int>("IdTipoAcceso")
                        .HasColumnType("int")
                        .HasColumnName("ID_TIPO_ACCESO");

                    b.HasKey("IdFuncionalidad", "IdRol");

                    b.HasIndex("IdTipoAcceso");

                    b.ToTable("TBL_FUNCIONALIDADES_ROL");
                });

            modelBuilder.Entity("ARQ.Entidades.TipoAcceso", b =>
                {
                    b.Property<int>("IdTipoAcceso")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_TIPO_ACCESO")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit")
                        .HasColumnName("ACTIVO");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("DESCRIPCION");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("datetime2")
                        .HasColumnName("FECHA_ALTA");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime2")
                        .HasColumnName("FECHA_MODIFICACION");

                    b.Property<int>("IdUsuarioAlta")
                        .HasColumnType("int")
                        .HasColumnName("ID_USUARIO_ALTA");

                    b.Property<int?>("IdUsuarioModificacion")
                        .HasColumnType("int")
                        .HasColumnName("ID_USUARIO_MODIFICACION");

                    b.HasKey("IdTipoAcceso");

                    b.HasIndex("IdUsuarioAlta");

                    b.HasIndex("IdUsuarioModificacion");

                    b.ToTable("TBL_TIPOS_ACCESO");
                });

            modelBuilder.Entity("ARQ.Entidades.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_USUARIO")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit")
                        .HasColumnName("ACTIVO");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)")
                        .HasColumnName("DIR_CORREO");

                    b.Property<int?>("IdTurnoTrabajo")
                        .HasColumnType("int")
                        .HasColumnName("ID_TURNO_TRABAJO");

                    b.Property<int>("IdUsuarioSGAA")
                        .HasColumnType("int")
                        .HasColumnName("ID_USUARIO_SGAA");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("LOGIN");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("NOMBRE");

                    b.HasKey("Id");

                    b.ToTable("TBL_USUARIOS");
                });

            modelBuilder.Entity("ARQ.Entidades.Cliente", b =>
                {
                    b.HasOne("ARQ.Entidades.Usuario", "UsuarioAlta")
                        .WithMany()
                        .HasForeignKey("IdUsuarioAlta")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ARQ.Entidades.Usuario", "UsuarioModificacion")
                        .WithMany()
                        .HasForeignKey("IdUsuarioModificacion");

                    b.Navigation("UsuarioAlta");

                    b.Navigation("UsuarioModificacion");
                });

            modelBuilder.Entity("ARQ.Entidades.Funcionalidad", b =>
                {
                    b.HasOne("ARQ.Entidades.Usuario", "UsuarioAlta")
                        .WithMany()
                        .HasForeignKey("IdUsuarioAlta")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ARQ.Entidades.Usuario", "UsuarioModificacion")
                        .WithMany()
                        .HasForeignKey("IdUsuarioModificacion");

                    b.Navigation("UsuarioAlta");

                    b.Navigation("UsuarioModificacion");
                });

            modelBuilder.Entity("ARQ.Entidades.FuncionalidadRol", b =>
                {
                    b.HasOne("ARQ.Entidades.Funcionalidad", "Funcionalidad")
                        .WithMany()
                        .HasForeignKey("IdFuncionalidad")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ARQ.Entidades.TipoAcceso", "TipoAcceso")
                        .WithMany()
                        .HasForeignKey("IdTipoAcceso")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Funcionalidad");

                    b.Navigation("TipoAcceso");
                });

            modelBuilder.Entity("ARQ.Entidades.TipoAcceso", b =>
                {
                    b.HasOne("ARQ.Entidades.Usuario", "UsuarioAlta")
                        .WithMany()
                        .HasForeignKey("IdUsuarioAlta")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ARQ.Entidades.Usuario", "UsuarioModificacion")
                        .WithMany()
                        .HasForeignKey("IdUsuarioModificacion");

                    b.Navigation("UsuarioAlta");

                    b.Navigation("UsuarioModificacion");
                });
#pragma warning restore 612, 618
        }
    }
}
