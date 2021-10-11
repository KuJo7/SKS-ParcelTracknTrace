using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TeamJ.SKS.Package.BusinessLogic.DTOs;
using TeamJ.SKS.Package.BusinessLogic.Interfaces;
using FizzWare.NBuilder;

namespace TeamJ.SKS.Package.BusinessLogic.Test
{
    public class HopLogicTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ExportWarehouse_ValidMethodCall_Success()
        {
            IHopLogic hopLogic = new HopLogic();
            Assert.AreEqual(new List<BLHop>(), hopLogic.ExportWarehouses());
        }

        [Test]
        public void ImportWarehouse_NextHopsNull_Error()
        {
            IHopLogic hopLogic = new HopLogic();
            BLWarehouse warehouse = new BLWarehouse();
            warehouse.NextHops = null;
            Assert.IsFalse(hopLogic.ImportWarehouses(warehouse));
        }
        [Test]
        public void ImportWarehouse_RegexWrong_Error()
        {
            IHopLogic hopLogic = new HopLogic();
            BLWarehouse warehouse = new BLWarehouse();
            warehouse.Description = "/´´";
            Assert.IsFalse(hopLogic.ImportWarehouses(warehouse));
        }

        [Test]
        public void ImportWarehouse_ParameterCorrect_Success()
        {
            IHopLogic hopLogic = new HopLogic();
            BLWarehouse warehouse = new BLWarehouse();
            warehouse.Description = "Hauptlager 27-12";
            warehouse.NextHops = new List<BLWarehouseNextHops>();
            Assert.IsTrue(hopLogic.ImportWarehouses(warehouse));
        }

        [Test]
        public void ImportWarehouse_NextHopNotNull_Success()
        {
            IHopLogic hopLogic = new HopLogic();
            BLWarehouse warehouse = new BLWarehouse();
            warehouse.NextHops = new List<BLWarehouseNextHops>();
            Assert.IsTrue(hopLogic.ImportWarehouses(warehouse));
        }

        [Test]
        public void GetWarehouse_CodeCorrect_Success()
        {
            IHopLogic hopLogic = new HopLogic();
            Assert.AreEqual(new BLWarehouse(), hopLogic.GetWarehouse("ABCD2222"));
        }

        [Test]
        public void GetWarehouse_CodeWrong_Error()
        {
            IHopLogic hopLogic = new HopLogic();
            BLWarehouse warehouse = new BLWarehouse();
            Assert.IsNull(hopLogic.GetWarehouse("AbCD22"));
        }
    }
}
