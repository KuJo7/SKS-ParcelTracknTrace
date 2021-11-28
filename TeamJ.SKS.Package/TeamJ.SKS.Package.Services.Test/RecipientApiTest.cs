using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TeamJ.SKS.Package.BusinessLogic.DTOs;
using TeamJ.SKS.Package.BusinessLogic.Interfaces;
using TeamJ.SKS.Package.Services.Controllers;
using TeamJ.SKS.Package.Services.DTOs.MapperProfiles;
using TeamJ.SKS.Package.Services.DTOs.Models;

namespace TeamJ.SKS.Package.Services.Test
{
    class RecipientApiTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TrackParcel_ValidTrackingID_Success()
        {
            Mock<IParcelLogic> mockParcelLogic = new Mock<IParcelLogic>();
            mockParcelLogic.Setup(pl => pl.TrackParcel(It.IsAny<string>())).Returns(new BLParcel());
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            Mock<ILogger<RecipientApiController>> mockLogger = new Mock<ILogger<RecipientApiController>>();
            var controller = new RecipientApiController(new Mapper(config), mockParcelLogic.Object, mockLogger.Object);
            var result = (ObjectResult)controller.TrackParcel("123456789");
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void TrackParcel_WrongTrackingID_Error()
        {
            Mock<IParcelLogic> mockParcelLogic = new Mock<IParcelLogic>();
            mockParcelLogic.Setup(pl => pl.TrackParcel(It.IsAny<string>())).Returns(value:null);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            Mock<ILogger<RecipientApiController>> mockLogger = new Mock<ILogger<RecipientApiController>>();
            var controller = new RecipientApiController(new Mapper(config), mockParcelLogic.Object, mockLogger.Object);
            var result = (ObjectResult)controller.TrackParcel("1234");
            Assert.AreEqual(400, result.StatusCode);
        }
    }
}
