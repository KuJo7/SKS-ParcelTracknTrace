using Microsoft.Extensions.Logging;
using System;
using System.Configuration;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using TeamJ.SKS.Package.DataAccess.Interfaces;
using TeamJ.SKS.Package.ServiceAgents.Interfaces;
using TeamJ.SKS.Package.Services.DTOs.Models;

namespace TeamJ.SKS.Package.ServiceAgents
{
    public class OpenStreetMapEncodingAgent : IGeoEncodingAgent
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        private readonly ILogger<OpenStreetMapEncodingAgent> _logger;
        private string msg;

        public OpenStreetMapEncodingAgent(ILogger<OpenStreetMapEncodingAgent> logger, HttpClient client)
        {
            _logger = logger;
            _client = client;
            _baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
        }

        public GeoCoordinate EncodeAddress(string address)
        {
            try
            {
                _client.DefaultRequestHeaders.Add("User-Agent", "ParcelTracknTrace");
                address = Uri.EscapeDataString(address);
                var url = _baseUrl + address + "&format=json&limit=1";
                var response = _client.GetAsync(url).Result;

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
                    
                    return new GeoCoordinate { Lat = lat, Lon = lon };
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
