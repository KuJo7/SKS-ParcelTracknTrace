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
    class WarehouseManagementApiTest
    {
        [SetUp]
        public void Setup()
        {
        }
        //ein bad case test fehlt noch bei export warehouses
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
            var controller = new WarehouseManagementApiController(new Mapper(config), mockHopLogic.Object);
            var result = (ObjectResult)controller.ExportWarehouses();
            Assert.AreEqual(200, result.StatusCode);


            /*WarehouseManagementApiController controller = new WarehouseManagementApiController();
            var result = controller.ExportWarehouses();
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);*/
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
            var controller = new WarehouseManagementApiController(new Mapper(config), mockHopLogic.Object);
            var result = (ObjectResult)controller.ExportWarehouses();
            Assert.AreEqual(400, result.StatusCode);


            /*WarehouseManagementApiController controller = new WarehouseManagementApiController();
            var result = controller.ExportWarehouses();
            var badResult = result as OkObjectResult;
            Assert.IsNotNull(badResult);
            Assert.AreEqual(400, badResult.StatusCode);*/
        }

        [Test]
        public void GetWarehouse_ValidCode_Success()
        {
            Mock<IHopLogic> mockHopLogic = new Mock<IHopLogic>();
            mockHopLogic.Setup(pl => pl.GetWarehouse(It.IsAny<string>())).Returns(new BLWarehouse());

            var controller = new WarehouseManagementApiController(mockHopLogic.Object);
            var result = (ObjectResult)controller.GetWarehouse("code");
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void GetWarehouse_WrongCode_Error()
        {
            Mock<IHopLogic> mockHopLogic = new Mock<IHopLogic>();
            mockHopLogic.Setup(pl => pl.GetWarehouse(It.IsAny<string>())).Returns(value: null);

            var controller = new WarehouseManagementApiController(mockHopLogic.Object);
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
            var controller = new WarehouseManagementApiController(new Mapper(config), mockHopLogic.Object);
            var warehouse = Builder<Warehouse>.CreateNew()
                .With(x => x.Code = Builder<string>.CreateNew().Build())
                .With(x => x.Description = Builder<string>.CreateNew().Build())
                .With(x => x.HopType = Builder<string>.CreateNew().Build())
                .With(x => x.LocationName = Builder<string>.CreateNew().Build())
                .With(x => x.ProcessingDelayMins = Builder<int>.CreateNew().Build())
                .With(x => x.LocationCoordinates = Builder<GeoCoordinate>.CreateNew().Build())
                .With(x => x.Level = Builder<int>.CreateNew().Build())
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
            var controller = new WarehouseManagementApiController(new Mapper(config), mockHopLogic.Object);
            var warehouse = Builder<Warehouse>.CreateNew()
                .With(x => x.Code = Builder<>.CreateNew().Build())
                .With(x => x.Description = Builder<string>.CreateNew().Build())
                .With(x => x.HopType = Builder<string>.CreateNew().Build())
                .With(x => x.LocationName = Builder<string>.CreateNew().Build())
                .With(x => x.ProcessingDelayMins = Builder<int>.CreateNew().Build())
                .With(x => x.LocationCoordinates = Builder<GeoCoordinate>.CreateNew().Build())
                .Build();
            var result = (ObjectResult)controller.ImportWarehouses(warehouse);
            Assert.AreEqual(400, result.StatusCode);
        }
    }
}
