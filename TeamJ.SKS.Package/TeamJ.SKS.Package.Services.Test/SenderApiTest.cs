﻿using System;
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
    class SenderApiTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SubmitParcel_ValidParcelBody_Success()
        {
            Mock<IParcelLogic> mockParcelLogic = new Mock<IParcelLogic>();
            var trackingId = "";
            mockParcelLogic.Setup(pl => pl.SubmitParcel(It.IsAny<BLParcel>(), out trackingId)).Returns(true);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            Mock<ILogger<SenderApiController>> mockLogger = new Mock<ILogger<SenderApiController>>();
            var controller = new SenderApiController(new Mapper(config), mockParcelLogic.Object, mockLogger.Object);
            var parcel = Builder<Parcel>.CreateNew()
                .With(x => x.Recipient = Builder<Recipient>.CreateNew().Build())
                .With(x => x.Sender = Builder<Recipient>.CreateNew().Build())
                .With(x => x.Weight = Builder<int>.CreateNew().Build())
                .Build();
            var result = (ObjectResult)controller.SubmitParcel(parcel);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void SubmitParcel_WrongParcelBody_Error()
        {
            Mock<IParcelLogic> mockParcelLogic = new Mock<IParcelLogic>();
            var trackingId = "";
            mockParcelLogic.Setup(pl => pl.SubmitParcel(It.IsAny<BLParcel>(), out trackingId)).Returns(false);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            Mock<ILogger<SenderApiController>> mockLogger = new Mock<ILogger<SenderApiController>>();
            var controller = new SenderApiController(new Mapper(config), mockParcelLogic.Object, mockLogger.Object);
            var parcel = Builder<Parcel>.CreateNew()
                .With(x => x.Recipient = Builder<Recipient>.CreateNew().Build())
                .With(x => x.Sender = Builder<Recipient>.CreateNew().Build())
                .With(x => x.Weight = Builder<int>.CreateNew().Build())
                .Build();
            var result = (ObjectResult)controller.SubmitParcel(parcel);
            Assert.AreEqual(400, result.StatusCode);
        }
    }
}
