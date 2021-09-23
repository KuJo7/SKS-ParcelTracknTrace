using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using TeamJ.SKS.Package.Services.Controllers;
using TeamJ.SKS.Package.Services.DTOs.Models;

namespace TeamJ.SKS.Package.ServicesTest
{
    class ReceipientApiTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TrackParcel_ValidTrackingID_Success()
        {
            ReceipientApiController receipient = new ReceipientApiController();
            var result = receipient.TrackParcel("123456789");
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        public void TrackParcel_WrongTrackingID_Error()
        {
            ReceipientApiController receipient = new ReceipientApiController();
            var result = receipient.TrackParcel("123");
            var badResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badResult);
            Assert.AreEqual(400, badResult.StatusCode);
        }
    }
}
