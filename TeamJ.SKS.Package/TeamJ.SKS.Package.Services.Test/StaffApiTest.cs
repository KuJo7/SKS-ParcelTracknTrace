using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TeamJ.SKS.Package.BusinessLogic.DTOs;
using TeamJ.SKS.Package.BusinessLogic.Interfaces;
using TeamJ.SKS.Package.Services.Controllers;
using TeamJ.SKS.Package.Services.DTOs.Models;

namespace TeamJ.SKS.Package.Services.Test
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
            Mock<IParcelLogic> mockParcelLogic = new Mock<IParcelLogic>();
            mockParcelLogic.Setup(pl => pl.ReportParcelDelivery(It.IsAny<string>())).Returns(true);

            Mock<ILogger<StaffApiController>> mockLogger = new Mock<ILogger<StaffApiController>>();
            var controller = new StaffApiController(mockParcelLogic.Object, mockLogger.Object);
            var result = (ObjectResult)controller.ReportParcelDelivery("123456789");
            Assert.AreEqual(200, result.StatusCode);
        }
        [Test]
        public void ReportParcelDelivery_WrongTrackingID_Error()
        {
            Mock<IParcelLogic> mockParcelLogic = new Mock<IParcelLogic>();
            mockParcelLogic.Setup(pl => pl.ReportParcelDelivery(It.IsAny<string>())).Returns(false);

            Mock<ILogger<StaffApiController>> mockLogger = new Mock<ILogger<StaffApiController>>();
            var controller = new StaffApiController(mockParcelLogic.Object, mockLogger.Object);
            var result = (ObjectResult)controller.ReportParcelDelivery("1234");
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public void ReportParcelHop_ValidTrackingID_Success()
        {
            Mock<IParcelLogic> mockParcelLogic = new Mock<IParcelLogic>();
            mockParcelLogic.Setup(pl => pl.ReportParcelHop(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            Mock<ILogger<StaffApiController>> mockLogger = new Mock<ILogger<StaffApiController>>();
            var controller = new StaffApiController(mockParcelLogic.Object, mockLogger.Object);
            var result = (ObjectResult)controller.ReportParcelHop("123456789", "ABCD12");
            Assert.AreEqual(200, result.StatusCode);
        }
        [Test]
        public void ReportParcelHop_WrongTrackingID_Error()
        {
            Mock<IParcelLogic> mockParcelLogic = new Mock<IParcelLogic>();
            mockParcelLogic.Setup(pl => pl.ReportParcelHop(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            Mock<ILogger<StaffApiController>> mockLogger = new Mock<ILogger<StaffApiController>>();
            var controller = new StaffApiController(mockParcelLogic.Object, mockLogger.Object);
            var result = (ObjectResult)controller.ReportParcelHop("1234", "wrongCode");
            Assert.AreEqual(400, result.StatusCode);
        }
    }
}
