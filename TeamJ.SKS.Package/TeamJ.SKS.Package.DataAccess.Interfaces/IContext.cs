using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamJ.SKS.Package.DataAccess.DTOs;

namespace TeamJ.SKS.Package.DataAccess.Interfaces
{
    public interface IContext
    {
        public DbSet<DALHop> Hops { get; set; }
        public DbSet<DALWarehouse> Warehouses { get; set; }
        public DbSet<DALParcel> Parcels { get; set; }

        public DbSet<DALWebhookResponse> WebhookResponse { get; set; }
        public void DeleteAll();
        public int SaveChanges();
    }
}
