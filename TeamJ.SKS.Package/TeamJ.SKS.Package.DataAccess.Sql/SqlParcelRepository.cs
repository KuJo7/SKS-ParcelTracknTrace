using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TeamJ.SKS.Package.DataAccess.DTOs;
using TeamJ.SKS.Package.DataAccess.Interfaces;

namespace TeamJ.SKS.Package.DataAccess.Sql
{
    public class SqlParcelRepository : IParcelRepository
    {
        private readonly IContext _context;
        private readonly ILogger<SqlParcelRepository> _logger;

        public SqlParcelRepository(IContext context, ILogger<SqlParcelRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Create(DALParcel dalParcel)
        {
            _logger.LogInformation("SqlParcelRepository Create started.");
            _context.Parcels.Add(dalParcel);
            _context.SaveChanges();
            _logger.LogInformation("SqlParcelRepository Create ended.");
        }

        public void Delete(DALParcel dalParcel)
        {
            _logger.LogInformation("SqlParcelRepository Delete started.");
            _context.Parcels.Remove(dalParcel);
            _context.SaveChanges();
            _logger.LogInformation("SqlParcelRepository Delete ended.");
        }

        public void Update(DALParcel dalParcel)
        {
            _logger.LogInformation("SqlParcelRepository Update started.");
            _context.Parcels.Update(dalParcel);
            _context.SaveChanges();
            _logger.LogInformation("SqlParcelRepository Update ended.");
        }

        public DALParcel GetById(string trackingID) 
        {
            _logger.LogInformation("SqlParcelRepository GetById started.");
            return _context.Parcels.Find(trackingID);
        }

        /*public List<DALParcel> GetByState(DALParcel parcel)
        {
            return (List<DALParcel>)_context.Parcels.Where(p => p.State == parcel.State);
        }
        */

        public List<DALParcel> GetAllParcels()
        {
            _logger.LogInformation("SqlParcelRepository GetAllParcels started.");
            return new List<DALParcel>(_context.Parcels);
        }

    }
}
