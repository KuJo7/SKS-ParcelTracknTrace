using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using NUnit.Framework;
using TeamJ.SKS.Package.BusinessLogic.DTOs;
using TeamJ.SKS.Package.BusinessLogic.Interfaces;
using FizzWare.NBuilder;
using Moq;
using TeamJ.SKS.Package.DataAccess.DTOs;
using TeamJ.SKS.Package.DataAccess.Interfaces;
using TeamJ.SKS.Package.Services.DTOs.MapperProfiles;
using TeamJ.SKS.Package.Services.DTOs.Models;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;

namespace TeamJ.SKS.Package.BusinessLogic.Test
{
    public class HopLogicTest
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void ExportWarehouse_DoesNotThrowException()
        {

            Mock<IHopRepository> mockHopRepository = new Mock<IHopRepository>();
            Mock<ILogger<HopLogic>> mockLogger = new Mock<ILogger<HopLogic>>();
            
            var dalHop = Builder<DALWarehouse>.CreateNew()
                .With(p => p.LocationCoordinates = new Point(52, -0.9))
                .With(p => p.Code = "rightcode")
                .With(p => p.Id = 2)
                .With(p => p.Description = "description")
                .With(p => p.HopType = "Warehouse")
                .With(p => p.ProcessingDelayMins = 3)
                .With(p => p.LocationName = "locationname")
                .Build();

            var blHop = Builder<BLHop>.CreateNew()
                .With(p => p.LocationCoordinates = new Point(52, -0.9))
                .With(p => p.Code = "rightcode")
                .With(p => p.Description = "description")
                .With(p => p.HopType = "Warehouse")
                .With(p => p.ProcessingDelayMins = 3)
                .With(p => p.LocationName = "locationname")
                .Build();
            /*List<DALHop> dalHops = new List<DALHop>();
            DALHop newDalHop = new DALHop();
            newDalHop.Code = "rightcode";
            dalHops.Add(newDalHop);
            DALHop newDalHop2 = new DALHop();
            dalHops.Add(newDalHop2);

            List<BLHop> blHops = new List<BLHop>();
            BLHop newBlHop = new BLHop();
            newBlHop.Code = "rightcode";
            blHops.Add(newBlHop);
            BLHop newBlHop2 = new BLHop();
            blHops.Add(newBlHop2);*/

            mockHopRepository.Setup(pl => pl.GetRootWarehouse()).Returns(dalHop);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            IHopLogic hopLogic = new HopLogic(mockHopRepository.Object, new Mapper(config), mockLogger.Object);
            //Assert.DoesNotThrow(() => hopLogic.ExportWarehouses());
            Assert.AreEqual(blHop.Code, hopLogic.ExportWarehouses().Code);
            //Assert.AreEqual(blHops, hopLogic.ExportWarehouses());
        }
        
        [Test]
        public void ExportWarehouses_ReturnsList_Error()
        {

            Mock<IHopRepository> mockHopRepository = new Mock<IHopRepository>();
            Mock<ILogger<HopLogic>> mockLogger = new Mock<ILogger<HopLogic>>();
            /*List<DALHop> dalHops = new List<DALHop>();
            DALHop newDalHop = new DALHop();
            newDalHop.Code = "rightcode";
            dalHops.Add(newDalHop);
            DALHop newDalHop2 = new DALHop();
            dalHops.Add(newDalHop2);

            List<BLHop> blHops = new List<BLHop>();
            BLHop newBlHop = new BLHop();
            newBlHop.Code = "wrongcode";
            blHops.Add(newBlHop);*/
            var dalHop = Builder<DALWarehouse>.CreateNew()
                .With(p => p.LocationCoordinates = new Point(52, -0.9))
                .With(p => p.Code = "rightcode")
                .With(p => p.Id = 2)
                .With(p => p.Description = "description")
                .With(p => p.HopType = "Warehouse")
                .With(p => p.ProcessingDelayMins = 3)
                .With(p => p.LocationName = "locationname")
                .Build();

            var blHop = Builder<BLHop>.CreateNew()
                .With(p => p.LocationCoordinates = new Point(52, -0.9))
                .With(p => p.Code = "wrongcode")
                .With(p => p.Description = "description")
                .With(p => p.HopType = "Warehouse")
                .With(p => p.ProcessingDelayMins = 3)
                .With(p => p.LocationName = "locationname")
                .Build();

            mockHopRepository.Setup(pl => pl.GetRootWarehouse()).Returns(dalHop);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            IHopLogic hopLogic = new HopLogic(mockHopRepository.Object, new Mapper(config), mockLogger.Object);
            //Assert.DoesNotThrow(() => hopLogic.ExportWarehouses());
            Assert.AreNotEqual(blHop.Code, hopLogic.ExportWarehouses().Code);
        }

        [Test]
        public void ImportWarehouse_NextHopsNotNull_Success()
        {
            Mock<IHopRepository> mockHopRepository = new Mock<IHopRepository>();
            Mock<ILogger<HopLogic>> mockLogger = new Mock<ILogger<HopLogic>>();
            BLWarehouse warehouse = new BLWarehouse();
            warehouse.NextHops = new List<BLWarehouseNextHops>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });

            IHopLogic hopLogic = new HopLogic(mockHopRepository.Object, new Mapper(config), mockLogger.Object);
            Assert.IsTrue(hopLogic.ImportWarehouses(warehouse));
        }
        
        [Test]
        public void ImportWarehouse_NextHopsNull_Error()
        {
            Mock<IHopRepository> mockHopRepository = new Mock<IHopRepository>();
            Mock<ILogger<HopLogic>> mockLogger = new Mock<ILogger<HopLogic>>();
            BLWarehouse warehouse = new BLWarehouse();
            warehouse.NextHops = null;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });

            IHopLogic hopLogic = new HopLogic(mockHopRepository.Object, new Mapper(config), mockLogger.Object);
            Assert.IsFalse(hopLogic.ImportWarehouses(warehouse));
        }
        
        [Test]
        public void ImportWarehouse_WrongRegex_Error()
        {
            Mock<IHopRepository> mockHopRepository = new Mock<IHopRepository>();
            Mock<ILogger<HopLogic>> mockLogger = new Mock<ILogger<HopLogic>>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            IHopLogic hopLogic = new HopLogic(mockHopRepository.Object, new Mapper(config), mockLogger.Object);
            BLWarehouse warehouse = new BLWarehouse();
            warehouse.Description = "/´´";
            warehouse.NextHops = null;
            Assert.IsFalse(hopLogic.ImportWarehouses(warehouse));
        }
        
        [Test]
        public void ImportWarehouse_ValidParameter_Success()
        {
            Mock<IHopRepository> mockHopRepository = new Mock<IHopRepository>();
            Mock<ILogger<HopLogic>> mockLogger = new Mock<ILogger<HopLogic>>();
            BLWarehouse warehouse = new BLWarehouse();
            warehouse.Description = "Hauptlager 27-12";

            warehouse.NextHops = new List<BLWarehouseNextHops>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });

            IHopLogic hopLogic = new HopLogic(mockHopRepository.Object, new Mapper(config), mockLogger.Object);
            Assert.IsTrue(hopLogic.ImportWarehouses(warehouse));
        }
        
        [Test]
        public void GetWarehouse_ValidCode_Success()
        {
            Mock<IHopRepository> mockHopRepository = new Mock<IHopRepository>();
            Mock<ILogger<HopLogic>> mockLogger = new Mock<ILogger<HopLogic>>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            DALWarehouse dalWarehouse = new DALWarehouse();
            dalWarehouse.Code = "test";
            mockHopRepository.Setup(pl => pl.GetByCode("ABCD12")).Returns(dalWarehouse);
            IHopLogic hopLogic = new HopLogic(mockHopRepository.Object, new Mapper(config), mockLogger.Object);
            Assert.AreEqual(dalWarehouse.Code, hopLogic.GetWarehouse("ABCD12").Code);
            Assert.IsNotNull(hopLogic.GetWarehouse("ABCD12"));
        }
        
        [Test]
        public void GetWarehouse_WrongCode_Error()
        {
            Mock<IHopRepository> mockHopRepository = new Mock<IHopRepository>();
            Mock<ILogger<HopLogic>> mockLogger = new Mock<ILogger<HopLogic>>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            IHopLogic hopLogic = new HopLogic(mockHopRepository.Object, new Mapper(config), mockLogger.Object);
            Assert.IsNull(hopLogic.GetWarehouse("wrongCode"));
        }

    }
}
