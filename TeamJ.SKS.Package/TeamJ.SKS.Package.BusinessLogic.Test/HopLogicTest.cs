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
            List<DALHop> dalHops = new List<DALHop>();
            DALHop newDalHop = new DALHop();
            dalHops.Add(newDalHop);
            DALHop newDalHop2 = new DALHop();
            dalHops.Add(newDalHop2);

            List<BLHop> blHops = new List<BLHop>();
            BLHop newBlHop = new BLHop();
            blHops.Add(newBlHop);
            BLHop newBlHop2 = new BLHop();
            blHops.Add(newBlHop2);

            mockHopRepository.Setup(pl => pl.GetAllHops()).Returns(dalHops);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            IHopLogic hopLogic = new HopLogic(mockHopRepository.Object, new Mapper(config));
            //Assert.DoesNotThrow(() => hopLogic.ExportWarehouses());
            Assert.AreEqual(blHops.Count, hopLogic.ExportWarehouses().Count);
            //Assert.AreEqual(blHops, hopLogic.ExportWarehouses());
            }
        
        [Test]
        public void ExportWarehouses_ReturnsList_Error()
        {

            Mock<IHopRepository> mockHopRepository = new Mock<IHopRepository>();
            List<DALHop> dalHops = new List<DALHop>();
            DALHop newDalHop = new DALHop();
            dalHops.Add(newDalHop);
            DALHop newDalHop2 = new DALHop();
            dalHops.Add(newDalHop2);

            List<BLHop> blHops = new List<BLHop>();
            BLHop newBlHop = new BLHop();
            blHops.Add(newBlHop);

            mockHopRepository.Setup(pl => pl.GetAllHops()).Returns(dalHops);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            IHopLogic hopLogic = new HopLogic(mockHopRepository.Object, new Mapper(config));
            //Assert.DoesNotThrow(() => hopLogic.ExportWarehouses());
            Assert.AreNotEqual(blHops.Count, hopLogic.ExportWarehouses().Count);
        }

        [Test]
        public void ImportWarehouse_NextHopsNotNull_Success()
        {
            Mock<IHopRepository> mockHopRepository = new Mock<IHopRepository>();
            BLWarehouse warehouse = new BLWarehouse();
            warehouse.NextHops = new List<BLWarehouseNextHops>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });

            IHopLogic hopLogic = new HopLogic(mockHopRepository.Object, new Mapper(config));
            Assert.IsTrue(hopLogic.ImportWarehouses(warehouse));
        }
        
        [Test]
        public void ImportWarehouse_NextHopsNull_Error()
        {
            Mock<IHopRepository> mockHopRepository = new Mock<IHopRepository>();
            BLWarehouse warehouse = new BLWarehouse();
            warehouse.NextHops = null;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });

            IHopLogic hopLogic = new HopLogic(mockHopRepository.Object, new Mapper(config));
            Assert.IsFalse(hopLogic.ImportWarehouses(warehouse));
        }
        
        [Test]
        public void ImportWarehouse_WrongRegex_Error()
        {
            Mock<IHopRepository> mockHopRepository = new Mock<IHopRepository>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            IHopLogic hopLogic = new HopLogic(mockHopRepository.Object, new Mapper(config));
            BLWarehouse warehouse = new BLWarehouse();
            warehouse.Description = "/´´";
            warehouse.NextHops = null;
            Assert.IsFalse(hopLogic.ImportWarehouses(warehouse));
        }
        
        [Test]
        public void ImportWarehouse_ValidParameter_Success()
        {
            Mock<IHopRepository> mockHopRepository = new Mock<IHopRepository>();
            BLWarehouse warehouse = new BLWarehouse();
            warehouse.Description = "Hauptlager 27-12";

            warehouse.NextHops = new List<BLWarehouseNextHops>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });

            IHopLogic hopLogic = new HopLogic(mockHopRepository.Object, new Mapper(config));
            Assert.IsTrue(hopLogic.ImportWarehouses(warehouse));
        }
        
        [Test]
        public void GetWarehouse_ValidCode_Success()
        {
            Mock<IHopRepository> mockHopRepository = new Mock<IHopRepository>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            DALWarehouse dalWarehouse = new DALWarehouse();
            dalWarehouse.Code = "test";
            mockHopRepository.Setup(pl => pl.GetByCode("ABCD12")).Returns(dalWarehouse);
            IHopLogic hopLogic = new HopLogic(mockHopRepository.Object, new Mapper(config));
            Assert.AreEqual(dalWarehouse.Code, hopLogic.GetWarehouse("ABCD12").Code);
            Assert.IsNotNull(hopLogic.GetWarehouse("ABCD12"));
        }
        
        [Test]
        public void GetWarehouse_WrongCode_Error()
        {
            Mock<IHopRepository> mockHopRepository = new Mock<IHopRepository>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            IHopLogic hopLogic = new HopLogic(mockHopRepository.Object, new Mapper(config));
            Assert.IsNull(hopLogic.GetWarehouse("wrongCode"));
        }
    }
}
