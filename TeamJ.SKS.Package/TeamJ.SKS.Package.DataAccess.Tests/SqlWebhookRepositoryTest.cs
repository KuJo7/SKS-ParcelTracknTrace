using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TeamJ.SKS.Package.DataAccess.DTOs;
using TeamJ.SKS.Package.DataAccess.Interfaces;
using TeamJ.SKS.Package.DataAccess.Sql;

namespace TeamJ.SKS.Package.DataAccess.Test
{
    public class SqlWebhookRepositoryTest
    {
        private IWebhookRepository webhook_repo;
        private List<DALWebhookResponse> webhooks;
        private List<DALParcel> parcels;
        private string validTrackingId = "PYJRB4HZ6";
        private string invalidTrackingId = "invalidId";


        [SetUp]
        public void Setup()
        {
            var webhook = Builder<DALWebhookResponse>.CreateNew()
                .With(p => p.Url = "test")
                .With(p => p.CreatedAt = Builder<DateTime>.CreateNew().Build())
                .With(p => p.Id = 2)
                .With(p => p.TrackingId = validTrackingId)
                .Build();

            webhooks = new List<DALWebhookResponse>();
            webhooks.Add(
                webhook
            );

            var parcel = Builder<DALParcel>.CreateNew()
                .With(p => p.Recipient = Builder<DALRecipient>.CreateNew().Build())
                .With(p => p.Sender = Builder<DALRecipient>.CreateNew().Build())
                .With(p => p.Weight = 1)
                .With(p => p.FutureHops = Builder<List<DALHopArrival>>.CreateNew().Build())
                .With(p => p.VisitedHops = Builder<List<DALHopArrival>>.CreateNew().Build())
                .With(p => p.TrackingId = validTrackingId)
                .Build();

            parcels = new List<DALParcel>();
            parcels.Add(
                parcel
            );

            var mockDbContext = new Mock<IContext>();
            mockDbContext.Setup(c => c.WebhookResponse).Returns(DbContextMock.GetQueryableMockDbSet<DALWebhookResponse>(webhooks));
            mockDbContext.Setup(p => p.Parcels.Find(webhook.TrackingId)).Returns(parcel);
            mockDbContext.Setup(c => c.SaveChanges()).Returns(1);
            var mockLogger = new Mock<ILogger<SqlWebhookRepository>>();
            webhook_repo = new SqlWebhookRepository(mockDbContext.Object, mockLogger.Object);


        }

        [Test]
        public void Create_Success()
        {
            var webhook = new DALWebhookResponse();
            webhook.TrackingId = validTrackingId;
            webhook.Url = "test";
            webhook.Id = 3;
            var parcel = new DALParcel();
            parcel.TrackingId = validTrackingId;

            webhook_repo.Create(webhook);

            Assert.AreEqual(2, webhooks.Count);
            Assert.AreEqual(webhook, webhooks[1]);
        }

        [Test]
        public void Delete_Success()
        {
            var webhook = new DALWebhookResponse();
            webhook.TrackingId = invalidTrackingId;
            
            Assert.IsTrue(webhook_repo.Delete(2));
            Assert.AreEqual(1, webhooks.Count);
        }

        [Test]
        public void Delete_Unsuccessful()
        {
            Assert.IsTrue(webhook_repo.Delete(-1));
            Assert.AreEqual(1, webhooks.Count);
        }

        [Test]
        public void ListParcelWebhooks_Successful()
        {
            Assert.AreEqual(webhooks.Count, webhook_repo.ListParcelWebhooks("PYJRB4HZ6").Count);
        }

    }
}
