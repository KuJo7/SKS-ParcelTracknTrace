using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TeamJ.SKS.Package.BusinessLogic.DTOs;

namespace TeamJ.SKS.Package.BusinessLogic.Interfaces
{
    public interface IHopLogic
    {
        public List<BLHop> ExportWarehouses();
        public bool ImportWarehouses(BLWarehouse blWarehouse, IMapper mapper);
        public BLHop GetWarehouse(string code);
    }
}
