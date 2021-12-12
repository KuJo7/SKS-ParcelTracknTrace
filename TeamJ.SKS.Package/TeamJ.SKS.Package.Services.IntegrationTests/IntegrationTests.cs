using Newtonsoft.Json;
using NUnit.Framework;
using System.Net.Http;
using System.Text;
using TeamJ.SKS.Package.Services.DTOs.Models;

namespace TeamJ.SKS.Package.Services.IntegrationTests
{
    public class Tests
    {
        private string baseUrl;
        private HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            baseUrl = TestContext.Parameters.Get("baseUrl", "http://localhost:5000");
            _httpClient = new HttpClient()
            {
                BaseAddress = new System.Uri(baseUrl)
            };

        }

        [Test]
        public void ParcelJourney()
        {

            //CALL API (https://baseUrl<param>)// 
            /*var param = "/parcel";
            using var req = new HttpRequestMessage(HttpMethod.Post, param);
            var parcel = new Parcel()
            {
                Weight = 10,
                Recipient = new Recipient()
                {
                    Name = "Recipient",
                    Street = "Karlsplatz",
                    PostalCode = "1010",
                    City = "Wien",
                    Country = "Österreich"
                },
                Sender = new Recipient() 
                {
                    Name = "Recipient",
                    Street = "Karlsplatz",
                    PostalCode = "1010",
                    City = "Wien",
                    Country = "Österreich"
                }
            };
            var json = JsonConvert.SerializeObject(parcel);
            var body = new StringContent(json, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync(param, body);
            Assert.AreEqual(response, "");*/



            //Submit

            //Track


            //Report ParcelHop

            //Track

            //ReportParcelHop

            //Track

            //ReportFinalDelivery

            //Track
        }
    }
}