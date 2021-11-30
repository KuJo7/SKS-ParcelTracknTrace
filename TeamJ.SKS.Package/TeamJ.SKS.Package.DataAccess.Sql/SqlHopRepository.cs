using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Tracing;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TeamJ.SKS.Package.DataAccess.DTOs;
using TeamJ.SKS.Package.DataAccess.Interfaces;

namespace TeamJ.SKS.Package.DataAccess.Sql
{
    public class SqlHopRepository : IHopRepository
    {
        private readonly IContext _context;
        private readonly ILogger<SqlHopRepository> _logger;
        private string msgSQL;
        private string msgException;

        public SqlHopRepository(IContext context, ILogger<SqlHopRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Create(DALHop dalHop)
        {
            try
            {
                _logger.LogInformation("SqlHopRepository Create started.");
                _context.Hops.Add(dalHop);
                _context.SaveChanges();
            }
            catch (SqlException ex)
            {
                msgSQL = "An error occured while trying to create a hop.";
                _logger.LogError(msgSQL, ex);
                throw new DataAccessException(nameof(SqlHopRepository), nameof(Create), msgSQL, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to create a hop.";
                _logger.LogError(msgException, ex);
                throw new DataAccessException(nameof(SqlHopRepository), nameof(Create), msgException, ex);
            }
            _logger.LogInformation("SqlHopRepository Create ended.");
        }

        public void Update(DALHop dalHop)
        {
            try
            {
                _logger.LogInformation("SqlHopRepository Update started.");
                _context.Hops.Update(dalHop);
                _context.SaveChanges();
            }
            catch (SqlException ex)
            {
                msgSQL = "An error occured while trying to update a hop.";
                _logger.LogError(msgSQL, ex);
                throw new DataAccessException(nameof(SqlHopRepository), nameof(Update), msgSQL, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to update a hop.";
                _logger.LogError(msgException, ex);
                throw new DataAccessException(nameof(SqlHopRepository), nameof(Update), msgException, ex);
            }
            _logger.LogInformation("SqlHopRepository Update ended.");
        }

        public void Delete(DALHop dalHop)
        {
            try
            {
                
                _logger.LogInformation("SqlHopRepository Delete started.");
                _context.Hops.Remove(dalHop);
                _context.SaveChanges();
            }
            catch (SqlException ex)
            {
                msgSQL = "An error occured while trying to delete a hop.";
                _logger.LogError(msgSQL, ex);
                throw new DataAccessException(nameof(SqlHopRepository), nameof(Delete), msgSQL, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to delete a hop.";
                _logger.LogError(msgException, ex);
                throw new DataAccessException(nameof(SqlHopRepository), nameof(Delete), msgException, ex);
            }
            _logger.LogInformation("SqlHopRepository Delete ended.");
        }

        public void DeleteAllHops()
        {
            try
            {
                _logger.LogInformation("SqlHopRepository DeleteAllHops started.");
                _context.deleteAll();
            }
            catch (SqlException ex)
            {
                msgSQL = "An error occured while trying to delete all hops.";
                _logger.LogError(msgSQL, ex);
                throw new DataAccessException(nameof(SqlHopRepository), nameof(Delete), msgSQL, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to delete all hops.";
                _logger.LogError(msgException, ex);
                throw new DataAccessException(nameof(SqlHopRepository), nameof(Delete), msgException, ex);
            }
            _logger.LogInformation("SqlHopRepository DeleteAllHops ended.");
        }

        public DALWarehouse GetRootWarehouse()
        {
            try
            {
                _context.Hops.Load();
                _logger.LogInformation("SqlHopRepository GetAllHops started.");
                var root = _context.Hops.OfType<DALWarehouse>().Include(wh => wh.NextHops).Single(r => r.Level == 0);
                return root;
            }
            catch (SqlException ex)
            {
                msgSQL = "An error occured while trying to get Root Warehouse.";
                _logger.LogError(msgSQL, ex);
                throw new DataAccessException(nameof(SqlHopRepository), nameof(GetRootWarehouse), msgSQL, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to get Root Warehouse.";
                _logger.LogError(msgException, ex);
                throw new DataAccessException(nameof(SqlHopRepository), nameof(GetRootWarehouse), msgException, ex);
            }
            
        }

        public DALHop GetByCode(string code)
        {
            try
            {
                _logger.LogInformation("SqlHopRepository GetByCode started.");
                return _context.Hops.Where(hop => hop.Code == code).ToList().First();
            }
            catch (SqlException ex)
            {
                msgSQL = "An error occured while trying to get a hop by code.";
                _logger.LogError(msgSQL, ex);
                throw new DataAccessException(nameof(SqlHopRepository), nameof(GetByCode), msgSQL, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to get a hop by code.";
                _logger.LogError(msgException, ex);
                throw new DataAccessException(nameof(SqlHopRepository), nameof(GetByCode), msgException, ex);
            }
            
        }

        public List<DALHop> GetByHopType(string hopType)
        {
            try
            {
                _logger.LogInformation("SqlHopRepository GetByHopType started.");
                return _context.Hops.Where(hop => hop.HopType == hopType).ToList<DALHop>();

            }
            catch (SqlException ex)
            {
                msgSQL = "An error occured while trying to get a hop by type.";
                _logger.LogError(msgSQL, ex);
                throw new DataAccessException(nameof(SqlHopRepository), nameof(GetByHopType), msgSQL, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to get a hop by type.";
                _logger.LogError(msgException, ex);
                throw new DataAccessException(nameof(SqlHopRepository), nameof(GetByHopType), msgException, ex);
            }
        }

        public DALHop GetFirstHop()
        {
            try
            {
                _logger.LogInformation("SqlHopRepository GetFirstHop started.");
                return _context.Hops.First();
            }
            catch (SqlException ex)
            {
                msgSQL = "An error occured while trying to get the first hop.";
                _logger.LogError(msgSQL, ex);
                throw new DataAccessException(nameof(SqlHopRepository), nameof(GetFirstHop), msgSQL, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to get the first hop.";
                _logger.LogError(msgException, ex);
                throw new DataAccessException(nameof(SqlHopRepository), nameof(GetFirstHop), msgException, ex);
            }
            
        }

        /*public List<DALHop> GetByLevel(int level)
        {
            return _context.Hops.Where(hop => hop.Level == level);
        }*/

    }
}
