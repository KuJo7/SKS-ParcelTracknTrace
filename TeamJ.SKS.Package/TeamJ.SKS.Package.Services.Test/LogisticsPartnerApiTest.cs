using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
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
        [SetUp]
        public void Setup()
        {
        }
        //testen ob mapping geht 
        [Test]
        public void TransitionParcel_ValidTrackingID_Success()
        {
            Mock<IParcelLogic> mockParcelLogic = new Mock<IParcelLogic>();
            mockParcelLogic.Setup(pl => pl.TransitionParcel(It.IsAny<BLParcel>()));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            var controller = new LogisticsPartnerApiController(new Mapper(config), mockParcelLogic.Object);
            var parcel = Builder<Parcel>.CreateNew()
                .With(x => x.Recipient = Builder<Recipient>.CreateNew().Build())
                .With(x => x.Sender = Builder<Recipient>.CreateNew().Build())
                .With(x => x.Weight = Builder<int>.CreateNew().Build())
                .Build();
            var result = (ObjectResult)controller.TransitionParcel(parcel, "123456789");
            Assert.AreEqual(200,result.StatusCode);
            /*LogisticsPartnerApiController controller = new LogisticsPartnerApiController();
            var result = controller.TransitionParcel(new Parcel(), "123456789");
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);*/
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
