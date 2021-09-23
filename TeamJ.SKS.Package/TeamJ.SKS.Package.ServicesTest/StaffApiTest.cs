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
    class StaffApiTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ReportParcelDelivery_ValidTrackingID_Success()
        {
            StaffApiController staff = new StaffApiController();
            var result = staff.ReportParcelDelivery("123456789");
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        public void ReportParcelDelivery_WrongTrackingID_Error()
        {
            StaffApiController staff = new StaffApiController();
            var result = staff.ReportParcelDelivery( "123");
            var badResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badResult);
            Assert.AreEqual(400, badResult.StatusCode);
        }

        [Test]
        public void ReportParcelHop_ValidTrackingID_Success()
        {
            StaffApiController staff = new StaffApiController();
            var result = staff.ReportParcelHop("123456789", "code");
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        public void ReportParcelHop_WrongTrackingID_Error()
        {
            StaffApiController staff = new StaffApiController();
            var result = staff.ReportParcelHop("123", "code");
            var badResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badResult);
            Assert.AreEqual(400, badResult.StatusCode);
        }
    }
}
