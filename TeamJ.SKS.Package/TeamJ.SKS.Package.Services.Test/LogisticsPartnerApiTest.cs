using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using TeamJ.SKS.Package.Services.Controllers;
using TeamJ.SKS.Package.Services.DTOs.Models;

namespace TeamJ.SKS.Package.Services.Test
{
    class LogisticsPartnerApiTest
    {
        [SetUp]
        public void Setup()
        {
        }
        //testen ob mapping geht 
        [Test]
        public void TransitionParcel_ValidTrackingID_Success()
        {
            LogisticsPartnerApiController controller = new LogisticsPartnerApiController();
            var result = controller.TransitionParcel(new Parcel(), "123456789");
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        public void TransitionParcel_WrongTrackingID_Error()
        {
            LogisticsPartnerApiController controller = new LogisticsPartnerApiController();
            var result = controller.TransitionParcel(new Parcel(), "123");
            var badResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badResult);
            Assert.AreEqual(400, badResult.StatusCode);
        }
    }
}
