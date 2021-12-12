using NUnit.Framework;
using System.Net.Http;

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