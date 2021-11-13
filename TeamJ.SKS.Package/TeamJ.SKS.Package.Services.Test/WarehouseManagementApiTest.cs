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
    class WarehouseManagementApiTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ExportWarehouses_IsFalse_Success()
        {
            Mock<IHopLogic> mockHopLogic = new Mock<IHopLogic>();
            var listWithAny = new List<BLHop>();
            listWithAny.Add(new BLWarehouse());
            mockHopLogic.Setup(pl => pl.ExportWarehouses()).Returns(listWithAny);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            Mock<ILogger<WarehouseManagementApiController>> mockLogger = new Mock<ILogger<WarehouseManagementApiController>>();
            var controller = new WarehouseManagementApiController(new Mapper(config), mockHopLogic.Object, mockLogger.Object);
            var result = (ObjectResult)controller.ExportWarehouses();
            Assert.AreEqual(200, result.StatusCode);

        }

        [Test]
        public void ExportWarehouses_IsTrue_Error()
        {
            Mock<IHopLogic> mockHopLogic = new Mock<IHopLogic>();
            var emptyList = new List<BLHop>();
            mockHopLogic.Setup(pl => pl.ExportWarehouses()).Returns(emptyList);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            Mock<ILogger<WarehouseManagementApiController>> mockLogger = new Mock<ILogger<WarehouseManagementApiController>>();
            var controller = new WarehouseManagementApiController(new Mapper(config), mockHopLogic.Object, mockLogger.Object);
            var result = (ObjectResult)controller.ExportWarehouses();
            Assert.AreEqual(400, result.StatusCode);

        }

        [Test]
        public void GetWarehouse_ValidCode_Success()
        {
            Mock<IHopLogic> mockHopLogic = new Mock<IHopLogic>();
            mockHopLogic.Setup(pl => pl.GetWarehouse(It.IsAny<string>())).Returns(new BLWarehouse());
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            Mock<ILogger<WarehouseManagementApiController>> mockLogger = new Mock<ILogger<WarehouseManagementApiController>>();
            var controller = new WarehouseManagementApiController(new Mapper(config), mockHopLogic.Object, mockLogger.Object);
            var result = (ObjectResult)controller.GetWarehouse("ABCD12");
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void GetWarehouse_WrongCode_Error()
        {
            Mock<IHopLogic> mockHopLogic = new Mock<IHopLogic>();
            mockHopLogic.Setup(pl => pl.GetWarehouse(It.IsAny<string>())).Returns(value: null);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            Mock<ILogger<WarehouseManagementApiController>> mockLogger = new Mock<ILogger<WarehouseManagementApiController>>();
            var controller = new WarehouseManagementApiController(new Mapper(config), mockHopLogic.Object, mockLogger.Object);
            var result = (ObjectResult)controller.GetWarehouse("wrongCode");
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public void ImportWarehouses_ValidWarehouseBody_Success()
        {
            Mock<IHopLogic> mockHopLogic = new Mock<IHopLogic>();
            mockHopLogic.Setup(pl => pl.ImportWarehouses(It.IsAny<BLWarehouse>())).Returns(true);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            Mock<ILogger<WarehouseManagementApiController>> mockLogger = new Mock<ILogger<WarehouseManagementApiController>>();
            var controller = new WarehouseManagementApiController(new Mapper(config), mockHopLogic.Object, mockLogger.Object);
            var warehouse = Builder<Warehouse>.CreateNew()
                .With(x => x.Code = "code")
                .With(x => x.Description = "")
                .With(x => x.HopType = "")
                .With(x => x.LocationName = "")
                .With(x => x.ProcessingDelayMins = Builder<int>.CreateNew().Build())
                .With(x => x.Level = Builder<int>.CreateNew().Build())
                .With(x => x.LocationCoordinates = Builder<GeoCoordinate>.CreateNew().Build())
                .Build();
            var result = (ObjectResult)controller.ImportWarehouses(warehouse);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void ImportWarehouses_WrongWarehouseBody_Error()
        {
            Mock<IHopLogic> mockHopLogic = new Mock<IHopLogic>();
            mockHopLogic.Setup(pl => pl.ImportWarehouses(It.IsAny<BLWarehouse>())).Returns(false);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            Mock<ILogger<WarehouseManagementApiController>> mockLogger = new Mock<ILogger<WarehouseManagementApiController>>();
            var controller = new WarehouseManagementApiController(new Mapper(config), mockHopLogic.Object, mockLogger.Object);
            var warehouse = Builder<Warehouse>.CreateNew()
                .With(x => x.Code = "wrongCode")
                .With(x => x.Description = "")
                .With(x => x.HopType = "")
                .With(x => x.LocationName = "")
                .With(x => x.ProcessingDelayMins = Builder<int>.CreateNew().Build())
                .With(x => x.Level = Builder<int>.CreateNew().Build())
                .Build();
            var result = (ObjectResult)controller.ImportWarehouses(warehouse);
            Assert.AreEqual(400, result.StatusCode);
        }
    }
}
