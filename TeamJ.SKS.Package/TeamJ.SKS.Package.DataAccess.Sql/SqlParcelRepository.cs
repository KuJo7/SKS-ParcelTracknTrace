using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using TeamJ.SKS.Package.DataAccess.DTOs;
using TeamJ.SKS.Package.DataAccess.Interfaces;

namespace TeamJ.SKS.Package.DataAccess.Sql
{
    public class SqlParcelRepository : IParcelRepository
    {
        private readonly IContext _context;
        private readonly ILogger<SqlParcelRepository> _logger;
        private string msgSQL;
        private string msgException;

        public SqlParcelRepository(IContext context, ILogger<SqlParcelRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Create(DALParcel dalParcel)
        {
            try
            {
                _logger.LogInformation("SqlParcelRepository Create started.");
                _context.Parcels.Add(dalParcel);
                _context.SaveChanges();
            }
            catch (SqlException ex)
            {
                msgSQL = "An error occured while trying to create a parcel.";
                _logger.LogError(msgSQL, ex);
                throw new DataAccessException(nameof(SqlParcelRepository), nameof(Create), msgSQL, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to create a parcel.";
                _logger.LogError(msgException, ex);
                throw new DataAccessException(nameof(SqlParcelRepository), nameof(Create), msgException, ex);
            }
            
            _logger.LogInformation("SqlParcelRepository Create ended.");
        }

        public void Delete(DALParcel dalParcel)
        {
            try
            {
                _logger.LogInformation("SqlParcelRepository Delete started.");
                _context.Parcels.Remove(dalParcel);
                _context.SaveChanges();
            }
            catch (SqlException ex)
            {
                msgSQL = "An error occured while trying to delete a parcel.";
                _logger.LogError(msgSQL, ex);
                throw new DataAccessException(nameof(SqlParcelRepository), nameof(Delete), msgSQL, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to delete a parcel.";
                _logger.LogError(msgException, ex);
                throw new DataAccessException(nameof(SqlParcelRepository), nameof(Delete), msgException, ex);
            }
            
            _logger.LogInformation("SqlParcelRepository Delete ended.");
        }

        public void Update(DALParcel dalParcel)
        {
            try
            {
                _logger.LogInformation("SqlParcelRepository Update started.");
                _context.Parcels.Update(dalParcel);
                _context.SaveChanges();
            }
            catch (SqlException ex)
            {
                msgSQL = "An error occured while trying to update a parcel.";
                _logger.LogError(msgSQL, ex);
                throw new DataAccessException(nameof(SqlParcelRepository), nameof(Update), msgSQL, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to update a parcel.";
                _logger.LogError(msgException, ex);
                throw new DataAccessException(nameof(SqlParcelRepository), nameof(Update), msgException, ex);
            }

            _logger.LogInformation("SqlParcelRepository Update ended.");
        }

        public DALParcel GetById(string trackingID) 
        {
            try
            {
                _logger.LogInformation("SqlParcelRepository GetById started.");
                return _context.Parcels.Find(trackingID);
            }
            catch (SqlException ex)
            {
                msgSQL = "An error occured while trying to get a parcel by id.";
                _logger.LogError(msgSQL, ex);
                throw new DataAccessException(nameof(SqlParcelRepository), nameof(GetById), msgSQL, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to get a parcel by id.";
                _logger.LogError(msgException, ex);
                throw new DataAccessException(nameof(SqlParcelRepository), nameof(GetById), msgException, ex);
            }
            
        }

        /*public List<DALParcel> GetByState(DALParcel parcel)
        {
            return (List<DALParcel>)_context.Parcels.Where(p => p.State == parcel.State);
        }
        */

        public List<DALParcel> GetAllParcels()
        {
            try
            {
                _logger.LogInformation("SqlParcelRepository GetAllParcels started.");
                return new List<DALParcel>(_context.Parcels);
            }
            catch (SqlException ex)
            {
                msgSQL = "An error occured while trying to get all parcels.";
                _logger.LogError(msgSQL, ex);
                throw new DataAccessException(nameof(SqlParcelRepository), nameof(GetAllParcels), msgSQL, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to get all parcels.";
                _logger.LogError(msgException, ex);
                throw new DataAccessException(nameof(SqlParcelRepository), nameof(GetAllParcels), msgException, ex);
            }
            
        }

    }
}
