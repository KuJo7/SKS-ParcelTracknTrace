using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using TeamJ.SKS.Package.BusinessLogic.DTOs;
using TeamJ.SKS.Package.BusinessLogic.Interfaces;
using TeamJ.SKS.Package.DataAccess.DTOs;
using TeamJ.SKS.Package.DataAccess.Interfaces;
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
            DALParcel dalParcel = new DALParcel();
            dalParcel.Weight = 2;
            dalParcel.Recipient = new DALRecipient();
            dalParcel.TrackingId = "123456789";
            dalParcel.Sender = new DALRecipient();
            //dalParcel.Hops = new List<DALHopArrival>();
            mockParcelRepository.Setup(pl => pl.GetById("123456789")).Returns(dalParcel);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfiles());
            });
            IParcelLogic parcelLogic = new ParcelLogic(mockParcelRepository.Object, new Mapper(config));

            var result = parcelLogic.TrackParcel("123456789");
            Assert.IsNotNull(result);
        }
        /*
        [Test]
        public void TrackParcel_WrongTrackingID_Error()
        {
            IParcelLogic parcelLogic = new ParcelLogic();
            var result = parcelLogic.TrackParcel("1234");
            Assert.AreEqual(null, result);
        }


        [Test]
        public void TransitionParcel_ValidParcel_Success()
        {
            IParcelLogic parcelLogic = new ParcelLogic();
            var parcel = new BLParcel();
            parcel.TrackingId = "123456789";
            parcel.FutureHops = new();
            parcel.VisitedHops = new();
            parcel.Weight = 1;
            parcel.State = BLParcel.StateEnum.PickupEnum;
            parcel.Sender = new();
            parcel.Recipient = new();
            var result = parcelLogic.TransitionParcel(parcel);
            Assert.IsTrue(result);
        }

        [Test]
        public void TransitionParcel_WrongParcel_Error()
        {
            IParcelLogic parcelLogic = new ParcelLogic();
            var parcel = new BLParcel();
            parcel.TrackingId = "1234";
            var result = parcelLogic.TransitionParcel(parcel);
            Assert.IsFalse(result);
        }

        [Test]
        public void SubmitParcel_ValidParcel_Success()
        {
            IParcelLogic parcelLogic = new ParcelLogic();
            var parcel = new BLParcel();
            parcel.TrackingId = "123456789";
            parcel.FutureHops = new();
            parcel.VisitedHops = new();
            parcel.Weight = 1;
            parcel.State = BLParcel.StateEnum.PickupEnum;
            parcel.Sender = new();
            parcel.Recipient = new();
            var result = parcelLogic.SubmitParcel(parcel);
            Assert.IsTrue(result);
        }

        [Test]
        public void SubmitParcel_WrongParcel_Error()
        {
            IParcelLogic parcelLogic = new ParcelLogic();
            var parcel = new BLParcel();
            parcel.TrackingId = "1234";
            var result = parcelLogic.SubmitParcel(parcel);
            Assert.IsFalse(result);
        }

        [Test]
        public void ReportParcelDelivery_ValidTrackingID_Success()
        {
            IParcelLogic parcelLogic = new ParcelLogic();
            var result = parcelLogic.ReportParcelDelivery("123456789");
            Assert.IsTrue(result);
        }

        [Test]
        public void ReportParcelDelivery_WrongTrackingID_Error()
        {
            IParcelLogic parcelLogic = new ParcelLogic();
            var result = parcelLogic.ReportParcelDelivery("1234");
            Assert.IsFalse(result);
        }

        [Test]
        public void ReportParcelHop_ValidTrackingID_Success()
        {
            IParcelLogic parcelLogic = new ParcelLogic();
            var result = parcelLogic.ReportParcelHop("123456789", "ABCD12");
            Assert.IsTrue(result);
        }

        [Test]
        public void ReportParcelHop_WrongTrackingID_Error()
        {
            IParcelLogic parcelLogic = new ParcelLogic();
            var result = parcelLogic.ReportParcelHop("1234", "wrongCode");
            Assert.IsFalse(result);
        }*/
    }
}
