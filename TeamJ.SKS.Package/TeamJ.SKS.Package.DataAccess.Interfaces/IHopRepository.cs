using System;
using System.Collections.Generic;
using TeamJ.SKS.Package.DataAccess.DTOs;

namespace TeamJ.SKS.Package.DataAccess.Interfaces
{
    public interface IHopRepository
    {
        public void Create(DALHop dalHop);
        public void Update(DALHop dalHop);
        public void Delete(DALHop dalHop);


        public DALHop GetByCode(string code);
        public IEnumerable<DALHop> GetAllHops();
        public IEnumerable<DALHop> GetByHopType(string hopType);
        public IEnumerable<DALHop> GetByLevel(int level);

        //public IEnumerable<DALHop> ExportWarehouses();
        //public DALHop GetWarehouse(string code);
        //public void ImportWarehouse(DALWarehouse dalWarehouse);

        //Person GetSinglePersonByFirstName(string searchPattern);
        //Person GetSinglePersonByLastName(string searchPattern);
        //ICollection<Person> GetAllPeopleWithEmptyCompany();
    }
}
