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
    class SenderApiTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SubmitParcel_ValidParcelBody_Success()
        {
            SenderApiController sender = new SenderApiController();
            //Parcel body = new Parcel();
            var result = sender.SubmitParcel(new Parcel());
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        public void SubmitParcel_WrongParcelBody_Error()
        {
            SenderApiController sender = new SenderApiController();
            //Parcel body = new Parcel();
            var result = sender.SubmitParcel(null);
            var badResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badResult);
            Assert.AreEqual(400, badResult.StatusCode);
        }
    }
}
