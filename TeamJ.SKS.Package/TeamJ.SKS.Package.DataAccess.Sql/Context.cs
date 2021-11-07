using System;
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

            builder.Entity<DALHopArrival>()
                .HasKey(hopArrival => new { hopArrival.ParcelId, hopArrival.HopCode });

            builder.Entity<DALWarehouseNextHops>()
                .HasKey(warehouseNextHops => new { warehouseNextHops.FromHopCode, warehouseNextHops.ToHopCode });

            builder.Entity<DALHopArrival>()
                .HasOne(hopArrival => hopArrival.Parcel)
                .WithMany(hopArrival => hopArrival.Hops);

            builder.Entity<DALHop>()
                .HasMany(hop => hop.NextHops)
                .WithOne(hop => hop.FromHop);

            builder.Entity<DALWarehouseNextHops>()
                .HasOne(warehouseNextHops => warehouseNextHops.FromHop)
                .WithMany(warehouseNextHops => warehouseNextHops.NextHops)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<DALWarehouseNextHops>()
                .HasOne(warehouseNextHops => warehouseNextHops.ToHop)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<DALParcel>()
                .HasOne(parcel => parcel.Recipient)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<DALParcel>()
                .HasOne(parcel => parcel.Sender)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
