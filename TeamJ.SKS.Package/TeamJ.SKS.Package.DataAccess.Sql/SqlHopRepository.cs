using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using TeamJ.SKS.Package.DataAccess.DTOs;
using TeamJ.SKS.Package.DataAccess.Interfaces;

namespace TeamJ.SKS.Package.DataAccess.Sql
{
    public class SqlHopRepository : IHopRepository
    {
        private readonly IContext _context;
        private readonly ILogger<SqlHopRepository> _logger;

        public SqlHopRepository(IContext context, ILogger<SqlHopRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Create(DALHop dalHop)
        {
            _logger.LogInformation("SqlHopRepository Create started.");
            _context.Hops.Add(dalHop);
            _context.SaveChanges();
            _logger.LogInformation("SqlHopRepository Create ended.");
        }

        public void Update(DALHop dalHop)
        {
            _logger.LogInformation("SqlHopRepository Update started.");
            _context.Hops.Update(dalHop);
            _context.SaveChanges();
            _logger.LogInformation("SqlHopRepository Update ended.");
        }

        public void Delete(DALHop dalHop)
        {
            _logger.LogInformation("SqlHopRepository Delete started.");
            _context.Hops.Remove(dalHop);
            _context.SaveChanges();
            _logger.LogInformation("SqlHopRepository Delete ended.");
        }

        public List<DALHop> GetAllHops()
        {
            _logger.LogInformation("SqlHopRepository GetAllHops started.");
            return new List<DALHop>(_context.Hops);
        }

        public DALHop GetByCode(string code)
        {
            _logger.LogInformation("SqlHopRepository GetByCode started.");
            return _context.Hops.Where(hop => hop.Code == code).ToList<DALHop>().First();
        }

        public List<DALHop> GetByHopType(string hopType)
        {
            _logger.LogInformation("SqlHopRepository GetByHopType started.");
            
            return _context.Hops.Where(hop => hop.HopType == hopType).ToList<DALHop>();
        }

        public DALHop GetFirstHop()
        {
            _logger.LogInformation("SqlHopRepository GetFirstHop started.");
            return _context.Hops.First();
        }

        /*public List<DALHop> GetByLevel(int level)
        {
            return _context.Hops.Where(hop => hop.Level == level);
        }*/

    }
}
