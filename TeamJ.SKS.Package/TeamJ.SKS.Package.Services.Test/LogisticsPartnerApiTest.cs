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
    class LogisticsPartnerApiTest
    {

        /*[Test]
        public void TransitionParcel_ValidTrackingID_Success()
        {
            Mock<IParcelLogic> mockParcelLogic = new Mock<IParcelLogic>();
            mockParcelLogic.Setup(pl => pl.TransitionParcel(It.IsAny<BLParcel>(), "123456789", true)).Returns(true);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            Mock<ILogger<LogisticsPartnerApiController>> mockLogger = new Mock<ILogger<LogisticsPartnerApiController>>();
            var controller = new LogisticsPartnerApiController(new Mapper(config), mockParcelLogic.Object, mockLogger.Object);
            var parcel = Builder<Parcel>.CreateNew()
                .With(x => x.Recipient = Builder<Recipient>.CreateNew().Build())
                .With(x => x.Sender = Builder<Recipient>.CreateNew().Build())
                .With(x => x.Weight = Builder<int>.CreateNew().Build())
                .Build();
            var result = (ObjectResult)controller.TransitionParcel(parcel, "123456789");
            Assert.AreEqual(200, result.StatusCode);

        }
        [Test]
        public void TransitionParcel_WrongTrackingID_Error()
        {
            Mock<IParcelLogic> mockParcelLogic = new Mock<IParcelLogic>();
            mockParcelLogic.Setup(pl => pl.TransitionParcel(It.IsAny<BLParcel>(), "1234", true)).Returns(false);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            Mock<ILogger<LogisticsPartnerApiController>> mockLogger = new Mock<ILogger<LogisticsPartnerApiController>>();
            var controller = new LogisticsPartnerApiController(new Mapper(config), mockParcelLogic.Object, mockLogger.Object);
            var parcel = Builder<Parcel>.CreateNew()
                .With(x => x.Recipient = Builder<Recipient>.CreateNew().Build())
                .With(x => x.Sender = Builder<Recipient>.CreateNew().Build())
                .With(x => x.Weight = Builder<int>.CreateNew().Build())
                .Build();
            var result = (ObjectResult)controller.TransitionParcel(parcel, "1234");
            Assert.AreEqual(400, result.StatusCode);
        }
        */
    }
}
