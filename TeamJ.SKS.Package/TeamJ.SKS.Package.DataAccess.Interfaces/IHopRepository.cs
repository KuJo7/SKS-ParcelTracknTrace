using System;
using TeamJ.SKS.Package.DataAccess.DTOs;

namespace TeamJ.SKS.Package.DataAccess.Interfaces
{
    interface IHopRepository
    {
        public Boolean Create(DALHop hop);
        public void Update(DALHop hop);
        public void Delete(DALHop hop);

        public DALWarehouse ExportWarehouses();
        public DALWarehouse GetWarehouse(string code);

        public DALWarehouse ImportWarehouse(DALWarehouse warehouse);

    }
}
