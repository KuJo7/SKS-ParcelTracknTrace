﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamJ.SKS.Package.DataAccess.Interfaces;
using TeamJ.SKS.Package.DataAccess.DTOs;

namespace TeamJ.SKS.Package.DataAccess.Sql
{
    public class Context : DbContext, IContext
    {
        public DbSet<DALHop> Hops { get; set; }
        public DbSet<DALParcel> Parcels { get; set; }

        public Context(DbContextOptions<Context> opt) : base(opt)
        {
            Database.EnsureCreated();
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

            builder.Entity<DALHop>().HasDiscriminator(h => h.HopType);
            builder.Entity<DALTruck>().HasBaseType<DALHop>();
            builder.Entity<DALTransferwarehouse>().HasBaseType<DALHop>();
            builder.Entity<DALWarehouse>().HasBaseType<DALHop>();
        }
    }
}
