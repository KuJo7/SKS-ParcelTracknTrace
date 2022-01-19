using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
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
            Mock<ILogger<OpenStreetMapEncodingAgent>> mockLogger = new Mock<ILogger<OpenStreetMapEncodingAgent>>();
            var agent = new OpenStreetMapEncodingAgent(mockLogger.Object);
            var point = agent.EncodeAddress("Karlsplatz 1, 1010 Wien, Österreich");
            Assert.AreEqual(point.X, 16.369641399999999d);
            Assert.AreEqual(point.Y, 48.201021500000003d);
        }

        [Test]
        public void EncodeAddress_Error()
        {
            Mock<ILogger<OpenStreetMapEncodingAgent>> mockLogger = new Mock<ILogger<OpenStreetMapEncodingAgent>>();
            var agent = new OpenStreetMapEncodingAgent(mockLogger.Object);
            Assert.IsNull(agent.EncodeAddress("error, error"));
        }

    }
}