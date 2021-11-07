using System;
using System.Collections.Generic;
using TeamJ.SKS.Package.DataAccess.DTOs;

namespace TeamJ.SKS.Package.DataAccess.Interfaces
{
    public interface IParcelRepository
    {
        public void Create(DALParcel dalParcel);
        public void Update(DALParcel dalParcel);
        public void Delete(DALParcel dalParcel);


        //public DALHop GetByCode(string code);
        //public IEnumerable<DALHop> GetAllHops();
        //public IEnumerable<DALHop> GetByHopType(string hopType);
        //public IEnumerable<DALHop> GetByLevel(int level);

        //public DALParcel GetByCode(string code);
        public DALParcel GetById(string trackingID);
        //public List<DALParcel> GetByState(DALParcel parcel);
        public List<DALParcel> GetAllParcels();

        //Person GetSinglePersonByFirstName(string searchPattern);
        //Person GetSinglePersonByLastName(string searchPattern);
        //ICollection<Person> GetAllPeopleWithEmptyCompany();
    }
}
