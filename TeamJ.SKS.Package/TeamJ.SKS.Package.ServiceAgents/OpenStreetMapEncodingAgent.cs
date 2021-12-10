using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using System;
using System.Configuration;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using TeamJ.SKS.Package.DataAccess.Interfaces;
using TeamJ.SKS.Package.ServiceAgents.Interfaces;
using TeamJ.SKS.Package.Services.DTOs.Models;

namespace TeamJ.SKS.Package.ServiceAgents
{
    public class OpenStreetMapEncodingAgent : IGeoEncodingAgent
    {
        private static readonly HttpClient _client = new HttpClient();
        private static readonly ProductInfoHeaderValue _userAgent = new ("ParcelTracknTrace", "1.0.0");
        private readonly ILogger<OpenStreetMapEncodingAgent> _logger;
        private string msg;

        public OpenStreetMapEncodingAgent(ILogger<OpenStreetMapEncodingAgent> logger)
        {
            _logger = logger;
            _client.DefaultRequestHeaders.Add("User-Agent", "ParcelTracknTrace");
        }

        public Point EncodeAddress(string address)
        {
            try
            {
                
                address = Uri.EscapeDataString(address);
                var url = "https://nominatim.openstreetmap.org/?addressdetails=1&q=" + address + "&format=json&limit=1";
                using var req = new HttpRequestMessage(HttpMethod.Get, url);
                req.Headers.UserAgent.Add(_userAgent);
                var response = _client.SendAsync(req).Result;

                if(response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    if (data == "[]")
                        return null;

                    var json = JsonDocument.Parse(data);
                    var strLat = json.RootElement[0].GetProperty("lat").ToString();
                    var strLon = json.RootElement[0].GetProperty("lon").ToString();
                    var lat = double.Parse(strLat, CultureInfo.InvariantCulture.NumberFormat);
                    var lon = double.Parse(strLon, CultureInfo.InvariantCulture.NumberFormat);

                    return new Point(lon, lat) { SRID = 4326};
                }
                return null;
            }
            catch (Exception ex)
            {

                msg = "An error occured while trying to encode address.";
                _logger.LogError(msg, ex);
                throw new DataAccessException(nameof(OpenStreetMapEncodingAgent), nameof(EncodeAddress), msg, ex);
            }
        }
    }
}
