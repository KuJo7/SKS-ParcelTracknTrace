using FizzWare.NBuilder;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamJ.SKS.Package.DataAccess.DTOs;
using TeamJ.SKS.Package.DataAccess.Interfaces;
using TeamJ.SKS.Package.DataAccess.Sql;

namespace TeamJ.SKS.Package.DataAccess.Test
{
    class SqlParcelRepositoryTest
    {
        private IParcelRepository parcel_repo;
        private List<DALParcel> parcels;
        private string validTrackingId = "PYJRB4HZ6";
        private string invalidTrackingId = "invalidId";


        [SetUp]
        public void Setup()
        {
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
            mockDbContext.Setup(c => c.Parcels).Returns(DbContextMock.GetQueryableMockDbSet<DALParcel>(parcels));
            mockDbContext.Setup(c => c.SaveChanges()).Returns(1);
            var mockLogger = new Mock<ILogger<SqlParcelRepository>>();
            parcel_repo = new SqlParcelRepository(mockDbContext.Object, mockLogger.Object);

        }

        [Test]
        public void Create_Success()
        {
            var parcel = new DALParcel();
            parcel.TrackingId = validTrackingId;
            parcel_repo.Create(parcel);

            Assert.AreEqual(2, parcels.Count);
            Assert.AreEqual(parcel, parcels[1]);
        }

        [Test]
        public void Update_Success()
        {
            Assert.DoesNotThrow(() => parcel_repo.Update(parcels[0]));
        }

        [Test]
        public void Delete_Success()
        {
            var parcel = new DALParcel();
            parcel_repo.Create(parcel);
            parcel_repo.Delete(parcel);
            Assert.AreEqual(1, parcels.Count);
        }

        [Test]
        public void Delete_Failed()
        {
            var parcel = new DALParcel();
            parcel_repo.Delete(parcel);
            Assert.AreEqual(1, parcels.Count);
        }

        [Test]
        public void GetParcelByTrackingID_Success()
        {
            var parcel = parcel_repo.GetById(validTrackingId);
            var dalparcel = new DALParcel();
            dalparcel.TrackingId = validTrackingId;
            Assert.AreEqual(parcels[0].TrackingId, dalparcel.TrackingId);
        }

        [Test]
        public void GetParcelByTrackingID_Failed()
        {
            Assert.AreEqual(null, parcel_repo.GetById(invalidTrackingId));
        }

        [Test]
        public void GetAllParcels_Success()
        {
            var parcel = parcel_repo.GetAllParcels();
            Assert.AreEqual(parcels, parcel.ToList());
        }

        [Test]
        public void GetAllHops_Success()
        {
            var parcels = parcel_repo.GetAllParcels();
            Assert.AreEqual(parcels, parcels.ToList());
        }
    }
}
