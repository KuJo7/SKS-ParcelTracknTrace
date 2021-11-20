﻿// <auto-generated />
using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeamJ.SKS.Package.DataAccess.Sql;

namespace TeamJ.SKS.Package.DataAccess.Sql.Migrations
{
    [ExcludeFromCodeCoverage]
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TeamJ.SKS.Package.DataAccess.DTOs.DALHop", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HopType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Lat")
                        .HasColumnType("float");

                    b.Property<string>("LocationName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Lon")
                        .HasColumnType("float");

                    b.Property<int>("ProcessingDelayMins")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Hops");

                    b.HasDiscriminator<string>("HopType").HasValue("DALHop");
                });

            modelBuilder.Entity("TeamJ.SKS.Package.DataAccess.DTOs.DALHopArrival", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DALParcelTrackingId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DALParcelTrackingId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DALParcelTrackingId");

                    b.HasIndex("DALParcelTrackingId1");

                    b.ToTable("DALHopArrival");
                });

            modelBuilder.Entity("TeamJ.SKS.Package.DataAccess.DTOs.DALParcel", b =>
                {
                    b.Property<string>("TrackingId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RecipientId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SenderId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("TrackingId");

                    b.HasIndex("RecipientId");

                    b.HasIndex("SenderId");

                    b.ToTable("Parcels");
                });

            modelBuilder.Entity("TeamJ.SKS.Package.DataAccess.DTOs.DALRecipient", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DALRecipient");
                });

            modelBuilder.Entity("TeamJ.SKS.Package.DataAccess.DTOs.DALWarehouseNextHops", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DALWarehouseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HopId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TraveltimeMins")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DALWarehouseId");

                    b.HasIndex("HopId");

                    b.ToTable("DALWarehouseNextHops");
                });

            modelBuilder.Entity("TeamJ.SKS.Package.DataAccess.DTOs.DALTransferwarehouse", b =>
                {
                    b.HasBaseType("TeamJ.SKS.Package.DataAccess.DTOs.DALHop");

                    b.Property<string>("LogisticsPartner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogisticsPartnerUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegionGeoJson")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("DALTransferwarehouse");
                });

            modelBuilder.Entity("TeamJ.SKS.Package.DataAccess.DTOs.DALTruck", b =>
                {
                    b.HasBaseType("TeamJ.SKS.Package.DataAccess.DTOs.DALHop");

                    b.Property<string>("NumberPlate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegionGeoJson")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DALTruck_RegionGeoJson");

                    b.HasDiscriminator().HasValue("DALTruck");
                });

            modelBuilder.Entity("TeamJ.SKS.Package.DataAccess.DTOs.DALWarehouse", b =>
                {
                    b.HasBaseType("TeamJ.SKS.Package.DataAccess.DTOs.DALHop");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("DALWarehouse");
                });

            modelBuilder.Entity("TeamJ.SKS.Package.DataAccess.DTOs.DALHopArrival", b =>
                {
                    b.HasOne("TeamJ.SKS.Package.DataAccess.DTOs.DALParcel", null)
                        .WithMany("FutureHops")
                        .HasForeignKey("DALParcelTrackingId");

                    b.HasOne("TeamJ.SKS.Package.DataAccess.DTOs.DALParcel", null)
                        .WithMany("VisitedHops")
                        .HasForeignKey("DALParcelTrackingId1");
                });

            modelBuilder.Entity("TeamJ.SKS.Package.DataAccess.DTOs.DALParcel", b =>
                {
                    b.HasOne("TeamJ.SKS.Package.DataAccess.DTOs.DALRecipient", "Recipient")
                        .WithMany()
                        .HasForeignKey("RecipientId");

                    b.HasOne("TeamJ.SKS.Package.DataAccess.DTOs.DALRecipient", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId");

                    b.Navigation("Recipient");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("TeamJ.SKS.Package.DataAccess.DTOs.DALWarehouseNextHops", b =>
                {
                    b.HasOne("TeamJ.SKS.Package.DataAccess.DTOs.DALWarehouse", null)
                        .WithMany("NextHops")
                        .HasForeignKey("DALWarehouseId");

                    b.HasOne("TeamJ.SKS.Package.DataAccess.DTOs.DALHop", "Hop")
                        .WithMany()
                        .HasForeignKey("HopId");

                    b.Navigation("Hop");
                });

            modelBuilder.Entity("TeamJ.SKS.Package.DataAccess.DTOs.DALParcel", b =>
                {
                    b.Navigation("FutureHops");

                    b.Navigation("VisitedHops");
                });

            modelBuilder.Entity("TeamJ.SKS.Package.DataAccess.DTOs.DALWarehouse", b =>
                {
                    b.Navigation("NextHops");
                });
#pragma warning restore 612, 618
        }
    }
}