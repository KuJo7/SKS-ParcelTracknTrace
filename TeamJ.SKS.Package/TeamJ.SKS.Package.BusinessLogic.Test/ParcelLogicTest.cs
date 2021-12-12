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
using TeamJ.SKS.Package.BusinessLogic.DTOs;
using TeamJ.SKS.Package.BusinessLogic.Interfaces;
using TeamJ.SKS.Package.DataAccess.DTOs;
using TeamJ.SKS.Package.DataAccess.Interfaces;
using TeamJ.SKS.Package.ServiceAgents.Interfaces;
using TeamJ.SKS.Package.Services.DTOs.MapperProfiles;

namespace TeamJ.SKS.Package.BusinessLogic.Test
{
    class ParcelLogicTest
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void TrackParcel_ValidTrackingID_Success()
        {
            Mock<IParcelRepository> mockParcelRepository = new Mock<IParcelRepository>();
            Mock<IHopRepository> mockHopRepository = new Mock<IHopRepository>();
            Mock<ILogger<ParcelLogic>> mockLogger = new Mock<ILogger<ParcelLogic>>();
            Mock<IGeoEncodingAgent> mockAgent = new Mock<IGeoEncodingAgent>();
            DALParcel dalParcel = new DALParcel();
            dalParcel.Weight = 2;
            dalParcel.Recipient = new DALRecipient();
            dalParcel.TrackingId = "123456789";
            dalParcel.Sender = new DALRecipient();
            mockParcelRepository.Setup(pl => pl.GetById("123456789")).Returns(dalParcel);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            IParcelLogic parcelLogic = new ParcelLogic(mockParcelRepository.Object, mockHopRepository.Object, new Mapper(config), mockLogger.Object, mockAgent.Object);

            var result = parcelLogic.TrackParcel("123456789");
            Assert.IsNotNull(result);
        }
        
        [Test]
        public void TrackParcel_WrongTrackingID_Error()
        {
            Mock<IParcelRepository> mockParcelRepository = new Mock<IParcelRepository>();
            Mock<IHopRepository> mockHopRepository = new Mock<IHopRepository>();
            Mock<ILogger<ParcelLogic>> mockLogger = new Mock<ILogger<ParcelLogic>>();
            Mock<IGeoEncodingAgent> mockAgent = new Mock<IGeoEncodingAgent>();
            DALParcel dalParcel = new DALParcel();
            dalParcel.TrackingId = "1234";
            mockParcelRepository.Setup(pl => pl.GetById("1234")).Returns(dalParcel);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            IParcelLogic parcelLogic = new ParcelLogic(mockParcelRepository.Object, mockHopRepository.Object, new Mapper(config), mockLogger.Object, mockAgent.Object);

            var result = parcelLogic.TrackParcel("1234");
            Assert.IsNull(result);
        }
        

        [Test]
        public void TransitionParcel_ValidParcel_Success()
        {
            /*Mock<IParcelRepository> mockParcelRepository = new Mock<IParcelRepository>();
            Mock<IHopRepository> mockHopRepository = new Mock<IHopRepository>();
            Mock<ILogger<ParcelLogic>> mockParcelLogger = new Mock<ILogger<ParcelLogic>>();
            Mock<ILogger<HopLogic>> mockHopLogger = new Mock<ILogger<HopLogic>>();
            Mock<IGeoEncodingAgent> mockAgent = new Mock<IGeoEncodingAgent>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            IParcelLogic parcelLogic = new ParcelLogic(mockParcelRepository.Object, mockHopRepository.Object, new Mapper(config), mockParcelLogger.Object, mockAgent.Object);
            IHopLogic hopLogic = new HopLogic(mockHopRepository.Object, new Mapper(config), mockHopLogger.Object);
            var parcel = new BLParcel();
            parcel.TrackingId = "123456789";
            parcel.Weight = 1;
            parcel.State = BLParcel.StateEnum.PickupEnum;
            parcel.Sender = new BLRecipient() {Name = "Test1", Street = "Gonzagagasse", City = "Wien", PostalCode="1010", Country = "Österreich"};
            parcel.Recipient = new BLRecipient() {Name = "Test2", Street = "Ackerstraße", City = "Berlin", PostalCode = "13355", Country = "Deutschland" };


            
            var result1 = hopLogic.ImportWarehouses(blWarehouse);
            Assert.IsTrue(result1);
            var result2 = parcelLogic.TransitionParcel(parcel, parcel.TrackingId, true);
            Assert.IsTrue(result2);*/
        }
        
        [Test]
        public void TransitionParcel_WrongParcel_Error()
        {
            Mock<IParcelRepository> mockParcelRepository = new Mock<IParcelRepository>();
            Mock<IHopRepository> mockHopRepository = new Mock<IHopRepository>();
            Mock<ILogger<ParcelLogic>> mockLogger = new Mock<ILogger<ParcelLogic>>();
            Mock<IGeoEncodingAgent> mockAgent = new Mock<IGeoEncodingAgent>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            IParcelLogic parcelLogic = new ParcelLogic(mockParcelRepository.Object, mockHopRepository.Object, new Mapper(config), mockLogger.Object, mockAgent.Object);
            var parcel = new BLParcel();
            parcel.TrackingId = "1234";
            var result = parcelLogic.TransitionParcel(parcel, parcel.TrackingId, true);
            Assert.IsFalse(result);
        }
        
        [Test]
        public void SubmitParcel_ValidParcel_Success()
        {
            /*Mock<IParcelRepository> mockParcelRepository = new Mock<IParcelRepository>();
            Mock<IHopRepository> mockHopRepository = new Mock<IHopRepository>();
            Mock<ILogger<ParcelLogic>> mockLogger = new Mock<ILogger<ParcelLogic>>();
            Mock<IGeoEncodingAgent> mockAgent = new Mock<IGeoEncodingAgent>();

            var blHopArrival = Builder<BLHopArrival>.CreateNew()
                .With(p => p.Code = "test")
                .With(p => p.DateTime = DateTime.Now)
                .With(p => p.Description = "description")
                .Build();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            IParcelLogic parcelLogic = new ParcelLogic(mockParcelRepository.Object, mockHopRepository.Object, new Mapper(config), mockLogger.Object, mockAgent.Object);
            var parcel = new BLParcel();
            var trackingId = "123456789";
            parcel.FutureHops = new List<BLHopArrival>{ blHopArrival };
            parcel.VisitedHops = new List<BLHopArrival>{ blHopArrival };
            parcel.Weight = 1;
            parcel.State = BLParcel.StateEnum.PickupEnum;
            parcel.Sender = new BLRecipient();
            parcel.Recipient = new BLRecipient();
            var result = parcelLogic.SubmitParcel(parcel, out trackingId);
            Assert.AreEqual(true, result);*/
        }
        
        [Test]
        public void SubmitParcel_WrongParcel_Error()
        {
            Mock<IParcelRepository> mockParcelRepository = new Mock<IParcelRepository>();
            Mock<IHopRepository> mockHopRepository = new Mock<IHopRepository>();
            Mock<ILogger<ParcelLogic>> mockLogger = new Mock<ILogger<ParcelLogic>>();
            Mock<IGeoEncodingAgent> mockAgent = new Mock<IGeoEncodingAgent>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            IParcelLogic parcelLogic = new ParcelLogic(mockParcelRepository.Object, mockHopRepository.Object, new Mapper(config), mockLogger.Object, mockAgent.Object);
            var parcel = new BLParcel();
            var trackingId = "1234";
            var result = parcelLogic.SubmitParcel(parcel, out trackingId);
            Assert.IsFalse(result);
        }
        
        [Test]
        public void ReportParcelDelivery_ValidTrackingID_Success()
        {
            Mock<IParcelRepository> mockParcelRepository = new Mock<IParcelRepository>();
            Mock<IHopRepository> mockHopRepository = new Mock<IHopRepository>();
            Mock<ILogger<ParcelLogic>> mockLogger = new Mock<ILogger<ParcelLogic>>();
            Mock<IGeoEncodingAgent> mockAgent = new Mock<IGeoEncodingAgent>();
            DALParcel dalParcel = new DALParcel();
            dalParcel.Weight = 2;
            dalParcel.Recipient = new DALRecipient();
            dalParcel.TrackingId = "123456789";
            dalParcel.Sender = new DALRecipient();
            dalParcel.FutureHops = new List<DALHopArrival>();
            dalParcel.VisitedHops = new List<DALHopArrival>();
            mockParcelRepository.Setup(pl => pl.GetById("123456789")).Returns(dalParcel);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            IParcelLogic parcelLogic = new ParcelLogic(mockParcelRepository.Object, mockHopRepository.Object, new Mapper(config), mockLogger.Object, mockAgent.Object);

            var result = parcelLogic.ReportParcelDelivery("123456789");
            Assert.IsTrue(result);
        }
        
        [Test]
        public void ReportParcelDelivery_WrongTrackingID_Error()
        {
            Mock<IParcelRepository> mockParcelRepository = new Mock<IParcelRepository>();
            Mock<IHopRepository> mockHopRepository = new Mock<IHopRepository>();
            Mock<ILogger<ParcelLogic>> mockLogger = new Mock<ILogger<ParcelLogic>>();
            Mock<IGeoEncodingAgent> mockAgent = new Mock<IGeoEncodingAgent>();
            DALParcel dalParcel = new DALParcel();
            dalParcel.Weight = 2;
            dalParcel.Recipient = new DALRecipient();
            dalParcel.TrackingId = "1234";
            dalParcel.Sender = new DALRecipient();
            dalParcel.FutureHops = new List<DALHopArrival>();
            dalParcel.VisitedHops = new List<DALHopArrival>();
            mockParcelRepository.Setup(pl => pl.GetById("1234")).Returns(new DALParcel() { FutureHops = new List<DALHopArrival>(), VisitedHops = new List<DALHopArrival>()});
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            IParcelLogic parcelLogic = new ParcelLogic(mockParcelRepository.Object, mockHopRepository.Object, new Mapper(config), mockLogger.Object, mockAgent.Object);
            var result = parcelLogic.ReportParcelDelivery("1234");
            //Assert.Throws<NullReferenceException>(result);
            Assert.IsFalse(result);
        }
        
        [Test]
        public void ReportParcelHop_ValidTrackingID_Success()
        {
            Mock<IParcelRepository> mockParcelRepository = new Mock<IParcelRepository>();
            Mock<IHopRepository> mockHopRepository = new Mock<IHopRepository>();
            Mock<ILogger<ParcelLogic>> mockLogger = new Mock<ILogger<ParcelLogic>>();
            Mock<IGeoEncodingAgent> mockAgent = new Mock<IGeoEncodingAgent>();

            var dalHopArrival = Builder<DALHopArrival>.CreateNew()
                .With(p => p.Code = "test")
                .With(p => p.DateTime = DateTime.Now)
                .With(p => p.Id = 1)
                .With(p => p.Description = "description")
                .Build();

            DALParcel dalParcel = new DALParcel();
            dalParcel.Weight = 2;
            dalParcel.Recipient = new DALRecipient();
            dalParcel.TrackingId = "123456789";
            dalParcel.Sender = new DALRecipient();
            dalParcel.VisitedHops = new List<DALHopArrival>{ dalHopArrival };
            dalParcel.FutureHops = new List<DALHopArrival> { dalHopArrival };
            mockParcelRepository.Setup(pl => pl.GetById("123456789")).Returns(dalParcel);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            IParcelLogic parcelLogic = new ParcelLogic(mockParcelRepository.Object, mockHopRepository.Object, new Mapper(config), mockLogger.Object, mockAgent.Object);
            var result = parcelLogic.ReportParcelHop("123456789", "ABCD12");
            Assert.IsTrue(result);
        }
        
        [Test]
        public void ReportParcelHop_WrongTrackingID_Error()
        {
            Mock<IParcelRepository> mockParcelRepository = new Mock<IParcelRepository>();
            Mock<IHopRepository> mockHopRepository = new Mock<IHopRepository>();
            Mock<ILogger<ParcelLogic>> mockLogger = new Mock<ILogger<ParcelLogic>>();
            Mock<IGeoEncodingAgent> mockAgent = new Mock<IGeoEncodingAgent>();
            DALParcel dalParcel = new DALParcel();

            var dalHopArrival = Builder<DALHopArrival>.CreateNew()
                .With(p => p.Code = "test")
                .With(p => p.DateTime = DateTime.Now)
                .With(p => p.Id = 1)
                .With(p => p.Description = "description")
                .Build();

            dalParcel.Weight = 2;
            dalParcel.Recipient = new DALRecipient();
            dalParcel.TrackingId = "1234";
            dalParcel.Sender = new DALRecipient();
            dalParcel.VisitedHops = new List<DALHopArrival> { dalHopArrival };
            dalParcel.FutureHops = new List<DALHopArrival> { dalHopArrival };
            mockParcelRepository.Setup(pl => pl.GetById("1234")).Returns(dalParcel);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            IParcelLogic parcelLogic = new ParcelLogic(mockParcelRepository.Object, mockHopRepository.Object, new Mapper(config), mockLogger.Object, mockAgent.Object);
            var result = parcelLogic.ReportParcelHop("1234", "wrongCode");
            Assert.IsFalse(result);
        }
    }
}
