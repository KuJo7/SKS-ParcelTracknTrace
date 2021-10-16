using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TeamJ.SKS.Package.BusinessLogic.DTOs;
using TeamJ.SKS.Package.BusinessLogic.Interfaces;

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
            IParcelLogic parcelLogic = new ParcelLogic();
            var result = parcelLogic.TrackParcel("123456789");
            Assert.IsNotNull(result);
        }

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
        }
    }
}
