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
    [Migration("20231219212406_CreacionDeTablas")]
    partial class CreacionDeTablas
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("ARQ.Entidades.Horario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_HORARIO")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit")
                        .HasColumnName("ACTIVO");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("datetime2")
                        .HasColumnName("FECHA_ALTA");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime2")
                        .HasColumnName("FECHA_MODIFICACION");

                    b.Property<string>("Hora")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("HORA");

                    b.Property<int>("IdPelicula")
                        .HasColumnType("int")
                        .HasColumnName("ID_PELICULA");

                    b.Property<int>("IdSucursal")
                        .HasColumnType("int")
                        .HasColumnName("ID_SUCURSAL");

                    b.Property<int>("IdUsuarioAlta")
                        .HasColumnType("int")
                        .HasColumnName("ID_USUARIO_ALTA");

                    b.Property<int?>("IdUsuarioModificacion")
                        .HasColumnType("int")
                        .HasColumnName("ID_USUARIO_MODIFICACION");

                    b.HasKey("Id");

                    b.HasIndex("IdPelicula");

                    b.HasIndex("IdSucursal");

                    b.HasIndex("IdUsuarioAlta");

                    b.HasIndex("IdUsuarioModificacion");

                    b.ToTable("TBL_HORARIOS");
                });

            modelBuilder.Entity("ARQ.Entidades.Orden", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_ORDEN")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cantidad")
                        .HasColumnType("int")
                        .HasColumnName("CANTIDAD");

                    b.Property<int>("IdHorario")
                        .HasColumnType("int")
                        .HasColumnName("ID_HORARIO");

                    b.Property<double>("Total")
                        .HasColumnType("float")
                        .HasColumnName("TOTAL");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdHorario");

                    b.HasIndex("UsuarioId");

                    b.ToTable("TBL_ORDENES");
                });

            modelBuilder.Entity("ARQ.Entidades.Pelicula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_PELICULA")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit")
                        .HasColumnName("ACTIVO");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
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

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("IMAGEN");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("NOMBRE");

                    b.HasKey("Id");

                    b.HasIndex("IdUsuarioAlta");

                    b.HasIndex("IdUsuarioModificacion");

                    b.ToTable("TBL_PELICULAS");
                });

            modelBuilder.Entity("ARQ.Entidades.Sucursal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_SUCURSAL")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo")
                        .HasColumnType("bit")
                        .HasColumnName("ACTIVO");

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

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("NOMBRE");

                    b.Property<double>("Precio")
                        .HasColumnType("float")
                        .HasColumnName("PRECIO");

                    b.HasKey("Id");

                    b.HasIndex("IdUsuarioAlta");

                    b.HasIndex("IdUsuarioModificacion");

                    b.ToTable("TBL_SUCURSALES");
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

            modelBuilder.Entity("ARQ.Entidades.Horario", b =>
                {
                    b.HasOne("ARQ.Entidades.Pelicula", "Pelicula")
                        .WithMany("Horarios")
                        .HasForeignKey("IdPelicula")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ARQ.Entidades.Sucursal", "Sucursal")
                        .WithMany("Horarios")
                        .HasForeignKey("IdSucursal")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ARQ.Entidades.Usuario", "UsuarioAlta")
                        .WithMany()
                        .HasForeignKey("IdUsuarioAlta")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ARQ.Entidades.Usuario", "UsuarioModificacion")
                        .WithMany()
                        .HasForeignKey("IdUsuarioModificacion");

                    b.Navigation("Pelicula");

                    b.Navigation("Sucursal");

                    b.Navigation("UsuarioAlta");

                    b.Navigation("UsuarioModificacion");
                });

            modelBuilder.Entity("ARQ.Entidades.Orden", b =>
                {
                    b.HasOne("ARQ.Entidades.Horario", "Horario")
                        .WithMany("Ordenes")
                        .HasForeignKey("IdHorario")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ARQ.Entidades.Usuario", null)
                        .WithMany("Ordenes")
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Horario");
                });

            modelBuilder.Entity("ARQ.Entidades.Pelicula", b =>
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

            modelBuilder.Entity("ARQ.Entidades.Sucursal", b =>
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

            modelBuilder.Entity("ARQ.Entidades.Horario", b =>
                {
                    b.Navigation("Ordenes");
                });

            modelBuilder.Entity("ARQ.Entidades.Pelicula", b =>
                {
                    b.Navigation("Horarios");
                });

            modelBuilder.Entity("ARQ.Entidades.Sucursal", b =>
                {
                    b.Navigation("Horarios");
                });

            modelBuilder.Entity("ARQ.Entidades.Usuario", b =>
                {
                    b.Navigation("Ordenes");
                });
#pragma warning restore 612, 618
        }
    }
}
