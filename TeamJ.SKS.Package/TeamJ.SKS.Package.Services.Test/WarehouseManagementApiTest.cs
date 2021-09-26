using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using TeamJ.SKS.Package.Services.Controllers;
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
            WarehouseManagementApiController controller = new WarehouseManagementApiController();
            var result = controller.ExportWarehouses();
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetWarehouse_ValidCode_Success()
        {
            WarehouseManagementApiController controller = new WarehouseManagementApiController();
            var result = controller.GetWarehouse("test");
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        public void GetWarehouse_WrongCode_Error()
        {
            WarehouseManagementApiController controller = new WarehouseManagementApiController();
            var result = controller.GetWarehouse("");
            var badResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badResult);
            Assert.AreEqual(400, badResult.StatusCode);
        }

        [Test]
        public void ImportWarehouses_ValidWarehouseBody_Success()
        {
            WarehouseManagementApiController controller = new WarehouseManagementApiController();
            var result = controller.ImportWarehouses(new Warehouse());
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        public void ImportWarehouses_WrongWarehouseBody_Error()
        {
            WarehouseManagementApiController controller = new WarehouseManagementApiController();
            var result = controller.ImportWarehouses(null);
            var badResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badResult);
            Assert.AreEqual(400, badResult.StatusCode);
        }
    }
}
