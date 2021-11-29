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

        public void DeleteAllHops();
        public DALHop GetByCode(string code);
        public DALHop GetFirstHop();
        public DALWarehouse GetRootWarehouse();
        public List<DALHop> GetByHopType(string hopType);
        //public IEnumerable<DALHop> GetByLevel(int level);

    }
}
