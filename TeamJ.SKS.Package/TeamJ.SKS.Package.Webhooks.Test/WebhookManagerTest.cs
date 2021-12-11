using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FizzWare.NBuilder;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TeamJ.SKS.Package.DataAccess.DTOs;
using TeamJ.SKS.Package.DataAccess.Interfaces;
using TeamJ.SKS.Package.Services.DTOs.MapperProfiles;
using TeamJ.SKS.Package.Webhooks.Interfaces;

namespace TeamJ.SKS.Package.Webhooks.Test
{
    public class WebhookManagerTest
    {
        [SetUp]
        public void Setup()
        {
           

        }

        [Test]
        public void UnsubscribeParcelWebhook_Success()
        {
            Mock<IWebhookRepository> mockWebhookRepository = new Mock<IWebhookRepository>();
            Mock<ILogger<WebhookManager>> mockLogger = new Mock<ILogger<WebhookManager>>();
            mockWebhookRepository.Setup(pl => pl.Delete(2)).Returns(true);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            IWebhookManager webhookManager = new WebhookManager(mockWebhookRepository.Object, new Mapper(config), mockLogger.Object);

            Assert.IsTrue(webhookManager.UnsubscribeParcelWebhook(2));
        }

        [Test]
        public void UnsubscribeParcelWebhook_Unsuccessful()
        {
            Mock<IWebhookRepository> mockWebhookRepository = new Mock<IWebhookRepository>();
            Mock<ILogger<WebhookManager>> mockLogger = new Mock<ILogger<WebhookManager>>();
            mockWebhookRepository.Setup(pl => pl.Delete(-1)).Returns(false);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            IWebhookManager webhookManager = new WebhookManager(mockWebhookRepository.Object, new Mapper(config), mockLogger.Object);

            Assert.IsFalse(webhookManager.UnsubscribeParcelWebhook(-1));
        }

        [Test]
        public void ListParcelWebHooks_Successful()
        {
            Mock<IWebhookRepository> mockWebhookRepository = new Mock<IWebhookRepository>();
            Mock<ILogger<WebhookManager>> mockLogger = new Mock<ILogger<WebhookManager>>();

            var webhook = Builder<DALWebhookResponse>.CreateNew()
                .With(p => p.Url = "test")
                .With(p => p.CreatedAt = Builder<DateTime>.CreateNew().Build())
                .With(p => p.Id = 2)
                .With(p => p.TrackingId = "PYJRB4HZ6")
                .Build();
            var webhookList = new List<DALWebhookResponse>();
            webhookList.Add(webhook);
            mockWebhookRepository.Setup(pl => pl.ListParcelWebhooks("PYJRB4HZ6")).Returns(webhookList);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            IWebhookManager webhookManager = new WebhookManager(mockWebhookRepository.Object, new Mapper(config), mockLogger.Object);

            Assert.AreEqual(1 ,webhookManager.ListParcelWebHooks("PYJRB4HZ6").Count);
            Assert.AreEqual(webhookList[0].Url, webhookManager.ListParcelWebHooks("PYJRB4HZ6")[0].Url);
        }
    }
}
