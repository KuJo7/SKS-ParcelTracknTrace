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

        /*[Test]
        public void ExportWarehouse_DoesNotThrowException()
        {

            Mock<IHopRepository> mockHopRepository = new Mock<IHopRepository>();
            List<DALHop> dalHops = new List<DALHop>();
            DALHop newDalHop = new DALHop();
            dalHops.Add(newDalHop);
            mockHopRepository.Setup(pl => pl.GetAllHops().GetEnumerator()).Returns(dalHops.GetEnumerator());
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            
            IHopLogic hopLogic = new HopLogic(mockHopRepository.Object,new Mapper(config) );
            //Assert.DoesNotThrow(() => hopLogic.ExportWarehouses());
            Assert.AreEqual(dalHops, hopLogic.ExportWarehouses());
            }

        [Test]
        public void ExportWarehouses_ReturnsList_Error()
        {

            IHopLogic hopLogic = new HopLogic();
            var result = hopLogic.ExportWarehouses();
            Assert.NotNull(result);
        }*/

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
        /*
        [Test]
        public void ImportWarehouse_WrongRegex_Error()
        {
            IHopLogic hopLogic = new HopLogic();
            BLWarehouse warehouse = new BLWarehouse();
            warehouse.Description = "/´´";
            Assert.IsFalse(hopLogic.ImportWarehouses(warehouse));
        }

        [Test]
        public void ImportWarehouse_ValidParameter_Success()
        {
            IHopLogic hopLogic = new HopLogic();
            BLWarehouse warehouse = new BLWarehouse();
            warehouse.Description = "Hauptlager 27-12";
            warehouse.NextHops = new List<BLWarehouseNextHops>();
            Assert.IsTrue(hopLogic.ImportWarehouses(warehouse));
        }

        [Test]
        public void GetWarehouse_ValidCode_Success()
        {
            IHopLogic hopLogic = new HopLogic();
            Assert.IsNotNull(hopLogic.GetWarehouse("ABCD12"));
        }

        [Test]
        public void GetWarehouse_WrongCode_Error()
        {
            IHopLogic hopLogic = new HopLogic();
            Assert.IsNull(hopLogic.GetWarehouse("wrongCode"));
        }*/
    }
}
