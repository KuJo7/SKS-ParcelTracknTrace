using System;
using System.Collections.Generic;
using TeamJ.SKS.Package.DataAccess.DTOs;

namespace TeamJ.SKS.Package.DataAccess.Interfaces
{
    public interface IParcelRepository
    {
        public void Create(DALParcel dalParcel);
        public void Update(string trackingID);
        public void Delete(DALParcel dalParcel);
        public DALParcel GetById(string trackingID);
        public List<DALParcel> GetAllParcels();
        //public List<DALParcel> GetByState(DALParcel parcel);

    }
}
