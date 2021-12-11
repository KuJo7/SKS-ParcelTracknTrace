using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FizzWare.NBuilder;
using IO.Swagger.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TeamJ.SKS.Package.BusinessLogic.DTOs;
using TeamJ.SKS.Package.BusinessLogic.Interfaces;
using TeamJ.SKS.Package.Services.Controllers;
using TeamJ.SKS.Package.Services.DTOs.MapperProfiles;
using TeamJ.SKS.Package.Services.DTOs.Models;
using TeamJ.SKS.Package.Webhooks.Interfaces;

namespace TeamJ.SKS.Package.Services.Test
{
    public class ParcelWebhookApiTest
    {
        [Test]
        public void ListParcelWebhooks_ValidTrackingID_Success()
        {
            Mock<IWebhookManager> mockWebhookManager = new Mock<IWebhookManager>();
            var webhook = Builder<BLWebhookResponse>.CreateNew()
                .With(p => p.Url = "test")
                .With(p => p.CreatedAt = Builder<DateTime>.CreateNew().Build())
                .With(p => p.Id = 2)
                .With(p => p.TrackingId = "PYJRB4HZ6")
                .Build();
            var webhookList = new List<BLWebhookResponse>();
            webhookList.Add(webhook);
            mockWebhookManager.Setup(pl => pl.ListParcelWebHooks("PYJRB4HZ6")).Returns(webhookList);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            Mock<ILogger<ParcelWebhookApiController>> mockLogger = new Mock<ILogger<ParcelWebhookApiController>>();
            var controller = new ParcelWebhookApiController(new Mapper(config), mockWebhookManager.Object, mockLogger.Object);

            var parcel = Builder<Parcel>.CreateNew()
                .With(x => x.Recipient = Builder<Recipient>.CreateNew().Build())
                .With(x => x.Sender = Builder<Recipient>.CreateNew().Build())
                .With(x => x.Weight = Builder<int>.CreateNew().Build())
                .Build();
            var result = (ObjectResult)controller.ListParcelWebhooks("PYJRB4HZ6");
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void ListParcelWebhooks_WrongTrackingID_Unsuccessful()
        {
            Mock<IWebhookManager> mockWebhookManager = new Mock<IWebhookManager>();
            var webhook = Builder<BLWebhookResponse>.CreateNew()
                .With(p => p.Url = "test")
                .With(p => p.CreatedAt = Builder<DateTime>.CreateNew().Build())
                .With(p => p.Id = 2)
                .With(p => p.TrackingId = "PYJRB4HZ6")
                .Build();
            var webhookList = new List<BLWebhookResponse>();
            webhookList.Add(webhook);
            //mockWebhookManager.Setup(pl => pl.ListParcelWebHooks("1234")).Returns(new List<BLWebhookResponse>());
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            Mock<ILogger<ParcelWebhookApiController>> mockLogger = new Mock<ILogger<ParcelWebhookApiController>>();
            var controller = new ParcelWebhookApiController(new Mapper(config), mockWebhookManager.Object, mockLogger.Object);

            var parcel = Builder<Parcel>.CreateNew()
                .With(x => x.Recipient = Builder<Recipient>.CreateNew().Build())
                .With(x => x.Sender = Builder<Recipient>.CreateNew().Build())
                .With(x => x.Weight = Builder<int>.CreateNew().Build())
                .Build();
            var result = (ObjectResult)controller.ListParcelWebhooks("1234");
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public async Task SubscribeParcelWebhook_ValidURL_Successful()
        {
            Mock<IWebhookManager> mockWebhookManager = new Mock<IWebhookManager>();
            var webhook = Builder<BLWebhookResponse>.CreateNew()
                .With(p => p.Url = @"https://postb.in/1639238601612-9746064785867")
                .With(p => p.CreatedAt = Builder<DateTime>.CreateNew().Build())
                .With(p => p.Id = 2)
                .With(p => p.TrackingId = "PYJRB4HZ6")
                .Build();
            var webhookList = new List<BLWebhookResponse>();
            webhookList.Add(webhook);
            mockWebhookManager.Setup(pl => pl.SubscribeParcelWebhook("PYJRB4HZ6", @"https://postb.in/b/1639238601612-9746064785867")).ReturnsAsync(webhook);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            Mock<ILogger<ParcelWebhookApiController>> mockLogger = new Mock<ILogger<ParcelWebhookApiController>>();
            var controller = new ParcelWebhookApiController(new Mapper(config), mockWebhookManager.Object, mockLogger.Object);
            var result = (ObjectResult)await controller.SubscribeParcelWebhook( "PYJRB4HZ6", "https://postb.in/b/1639238601612-9746064785867");
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async Task SubscribeParcelWebhook_WrongURL_UnsuccessfulAsync()
        {
            Mock<IWebhookManager> mockWebhookManager = new Mock<IWebhookManager>();
            var webhook = Builder<BLWebhookResponse>.CreateNew()
                .With(p => p.Url = "404 - Not Found")
                .With(p => p.CreatedAt = Builder<DateTime>.CreateNew().Build())
                .With(p => p.Id = 2)
                .With(p => p.TrackingId = "PYJRB4HZ6")
                .Build();
            var webhookList = new List<BLWebhookResponse>();
            webhookList.Add(webhook);
            mockWebhookManager.Setup(pl => pl.SubscribeParcelWebhook("PYJRB4HZ6", @"wrongclientrestservice")).ReturnsAsync(webhook);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            Mock<ILogger<ParcelWebhookApiController>> mockLogger = new Mock<ILogger<ParcelWebhookApiController>>();
            var controller = new ParcelWebhookApiController(new Mapper(config), mockWebhookManager.Object, mockLogger.Object);
            var result = (ObjectResult) await controller.SubscribeParcelWebhook("PYJRB4HZ6","wrongclientrestservice");
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public void UnsubscribeParcelWebhook_ValidID_Successful()
        {
            Mock<IWebhookManager> mockWebhookManager = new Mock<IWebhookManager>();
            var webhook = Builder<BLWebhookResponse>.CreateNew()
                .With(p => p.Url = "404 - Not Found")
                .With(p => p.CreatedAt = Builder<DateTime>.CreateNew().Build())
                .With(p => p.Id = 2)
                .With(p => p.TrackingId = "PYJRB4HZ6")
                .Build();
            var webhookList = new List<BLWebhookResponse>();
            webhookList.Add(webhook);
            mockWebhookManager.Setup(pl => pl.UnsubscribeParcelWebhook(1)).Returns(true);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            Mock<ILogger<ParcelWebhookApiController>> mockLogger = new Mock<ILogger<ParcelWebhookApiController>>();
            var controller = new ParcelWebhookApiController(new Mapper(config), mockWebhookManager.Object, mockLogger.Object);
            var result = (ObjectResult)controller.UnsubscribeParcelWebhook(1);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void UnsubscribeParcelWebhook_WrongID_Unsuccessful()
        {
            Mock<IWebhookManager> mockWebhookManager = new Mock<IWebhookManager>();
            var webhook = Builder<BLWebhookResponse>.CreateNew()
                .With(p => p.Url = "404 - Not Found")
                .With(p => p.CreatedAt = Builder<DateTime>.CreateNew().Build())
                .With(p => p.Id = 2)
                .With(p => p.TrackingId = "PYJRB4HZ6")
                .Build();
            var webhookList = new List<BLWebhookResponse>();
            webhookList.Add(webhook);
            mockWebhookManager.Setup(pl => pl.UnsubscribeParcelWebhook(-1)).Returns(false);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            Mock<ILogger<ParcelWebhookApiController>> mockLogger = new Mock<ILogger<ParcelWebhookApiController>>();
            var controller = new ParcelWebhookApiController(new Mapper(config), mockWebhookManager.Object, mockLogger.Object);
            var result = (ObjectResult)controller.UnsubscribeParcelWebhook(-1);
            Assert.AreEqual(404, result.StatusCode);
        }
    }
}
