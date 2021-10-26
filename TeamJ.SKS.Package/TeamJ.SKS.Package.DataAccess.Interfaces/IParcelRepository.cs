using System;
using TeamJ.SKS.Package.DataAccess.DTOs;

namespace TeamJ.SKS.Package.DataAccess.Interfaces
{
    public interface IParcelRepository
    {
        public Boolean Create(DALParcel parcel);
        public void Update(DALParcel parcel);
        public void Delete(DALParcel parcel);

        public DALParcel TrackParcel(string trackingID);

        public DALParcel TransitionParcel(DALParcel blParcel);

        public DALParcel SubmitParcel(DALParcel blParcel);

        public DALParcel ReportParcelDelivery(string trackingID);

        public DALParcel ReportParcelHop(string trackingID, string code);
    }
}
