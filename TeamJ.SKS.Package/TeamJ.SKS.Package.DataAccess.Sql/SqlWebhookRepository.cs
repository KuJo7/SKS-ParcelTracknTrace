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
    public class SqlWebhookRepository : IWebhookRepository
    {
        private readonly IContext _context;
        private readonly ILogger<SqlWebhookRepository> _logger;
        private string msgSQL;
        private string msgException;

        public SqlWebhookRepository(IContext context, ILogger<SqlWebhookRepository> logger)
        {

            _context = context;
            _logger = logger;
        }

        public void Create(DALWebhookResponse webhookResponse)
        {
            try
            {
                _logger.LogInformation("SqlWebhookRepository Create started.");
                var result = _context.Parcels.Find(webhookResponse.TrackingId);
                if (result == null)
                {
                    msgSQL = "An error occured while trying to find the tracking ID.";
                    _logger.LogError(msgSQL);
                    throw new ArgumentNullException("There is no such a tracking ID");
                }
                _context.WebhookResponse.Add(webhookResponse); 
                _context.SaveChanges();

            }
            catch (SqlException ex)
            {
                msgSQL = "An error occured while trying to create a webhook.";
                _logger.LogError(msgSQL, ex);
                throw new DataAccessException(nameof(SqlWebhookRepository), nameof(Create), msgSQL, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to create a webhook.";
                _logger.LogError(msgException, ex);
                throw new DataAccessException(nameof(SqlWebhookRepository), nameof(Create), msgException, ex);
            }
            
        }

        public bool Delete(long Id)
        {
            try
            {
                _logger.LogInformation("SqlWebhookRepository Delete started.");
                var result = _context.WebhookResponse.Find(Id);
                _context.WebhookResponse.Remove(result);
                if (_context.SaveChanges() > 0)
                {
                    return true;
                }

                return false;
            }
            catch (SqlException ex)
            {
                msgSQL = "An error occured while trying to delete a webhook.";
                _logger.LogError(msgSQL, ex);
                throw new DataAccessException(nameof(SqlWebhookRepository), nameof(Delete), msgSQL, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to delete a webhook.";
                _logger.LogError(msgException, ex);
                throw new DataAccessException(nameof(SqlWebhookRepository), nameof(Delete), msgException, ex);
            }

            
        }

        public List<DALWebhookResponse> ListParcelWebhooks(string trackingId)
        {
            try
            {
                _logger.LogInformation("SqlWebhookRepository ListParcelWebhooks started.");
                return new List<DALWebhookResponse>(_context.WebhookResponse);
            }
            catch (SqlException ex)
            {
                msgSQL = "An error occured while trying to list all subscribers from a parcel.";
                _logger.LogError(msgSQL, ex);
                throw new DataAccessException(nameof(SqlWebhookRepository), nameof(ListParcelWebhooks), msgSQL, ex);
            }
            catch (Exception ex)
            {
                msgException = "An unknown error occured while trying to list all subscribers from a parcel.";
                _logger.LogError(msgException, ex);
                throw new DataAccessException(nameof(SqlWebhookRepository), nameof(ListParcelWebhooks), msgException, ex);
            }
        }
    }
}
