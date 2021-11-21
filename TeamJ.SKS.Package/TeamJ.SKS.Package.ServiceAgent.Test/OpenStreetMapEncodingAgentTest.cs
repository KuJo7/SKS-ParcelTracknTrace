using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TeamJ.SKS.Package.ServiceAgents;

namespace TeamJ.SKS.Package.ServiceAgent.Test
{
    public class OpenStreetMapEncodingAgentTest
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void EncodeAddress_Success()
        {
            /*var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"[{ ""id"": 1, ""title"": ""Cool post!""}, { ""id"": 100, ""title"": ""Some title""}]"),
            };
            Mock<HttpClient> mockHttpClient = new Mock<HttpClient>();
            var baseurl = "https://nominatim.openstreetmap.org/?addressdetails=1&q=";


            mockHttpClient.Setup(t => t.GetAsync(It.Is<string>(s => s.StartsWith(baseurl))))
        .ReturnsAsync(response);

            Mock<ILogger<OpenStreetMapEncodingAgent>> mockLogger = new Mock<ILogger<OpenStreetMapEncodingAgent>>();
            var agent = new OpenStreetMapEncodingAgent(mockLogger.Object, mockHttpClient.Object);
            var address = "bakery+in+berlin+wedding";
            var result = agent.EncodeAddress(address);
            //Assert.DoesNotThrow(() => hopLogic.ExportWarehouses());
            Assert.IsNotNull(result);*/
        }
    }
}