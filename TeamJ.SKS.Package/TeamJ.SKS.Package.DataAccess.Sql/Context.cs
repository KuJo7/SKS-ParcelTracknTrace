﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamJ.SKS.Package.DataAccess.Interfaces;
using TeamJ.SKS.Package.DataAccess.DTOs;

namespace TeamJ.SKS.Package.DataAccess.Sql
{
    [ExcludeFromCodeCoverage]
    public class Context : DbContext, IContext
    {
        public DbSet<DALHop> Hops { get; set; }
        public DbSet<DALWarehouse> Warehouses { get; set; }
        public DbSet<DALParcel> Parcels { get; set; }
        public DbSet<DALWebhookResponse> WebhookResponse { get; set; }

        public Context(DbContextOptions<Context> opt) : base(opt)
        {
            Database.EnsureCreated();
        }

        public void DeleteAll()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
            
            /*for(int i = 0; i < Hops.Count(); i++)
            {
                Hops.Remove(Hops.First());
            }
            for (int i = 0; i < Warehouses.Count(); i++)
            {
                Warehouses.Remove(Warehouses.First());
            }
            for (int i = 0; i < Parcels.Count(); i++)
            {
                Parcels.Remove(Parcels.First());
            }
            for (int i = 0; i < WebhookResponse.Count(); i++)
            {
                WebhookResponse.Remove(WebhookResponse.First());
            }*/
            


        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.AddInterceptors(new[] { new LogInterceptor() });
        //    optionsBuilder.UseSqlServer("Initial Catalog=Sample;User=sa;Password=pass@word1;Data Source=localhost;MultipleActiveResultSets=True"
        //        );
        // ,sopt => sopt.UseNetTopologySuite());
        //}

        protected override void OnModelCreating(ModelBuilder builder)
         {
            //builder.Entity<DALHop>()
            //    .HasKey(hop => hop.HopCode);

            //builder.Entity<DALParcel>()
            //    .HasKey(hop => hop.TrackingId);

            //builder.Entity<DALRecipient>()
            //    .HasKey(recipient => recipient.RecipientId);

            //builder.HasServiceTier("Basic");
            //builder.HasDatabaseMaxSize("2 GB");
            //builder.HasServiceTierSql("Basic");

            builder.Entity<DALHop>()
                .HasDiscriminator<string>(h => h.HopType)
                .HasValue<DALTruck>("Truck")
                .HasValue<DALWarehouse>("Warehouse")
                .HasValue<DALTransferwarehouse>("TransferWarehouse");
            builder.Entity<DALTruck>().HasBaseType<DALHop>();
            builder.Entity<DALTransferwarehouse>().HasBaseType<DALHop>();
            builder.Entity<DALWarehouse>().HasBaseType<DALHop>();

            builder.Entity<DALParcel>().Navigation(p => p.FutureHops).AutoInclude();
            builder.Entity<DALParcel>().Navigation(p => p.VisitedHops).AutoInclude();
            builder.Entity<DALParcel>().Navigation(p => p.Recipient).AutoInclude();
            builder.Entity<DALParcel>().Navigation(p => p.Sender).AutoInclude();

            builder.Entity<DALWarehouse>().Navigation(p => p.NextHops).AutoInclude();

            builder.Entity<DALWarehouse>().HasMany<DALWarehouseNextHops>(h => h.NextHops);
            builder.Entity<DALWarehouseNextHops>().HasOne<DALHop>(wnh => wnh.Hop);
         }
    }
}
