using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamJ.SKS.Package.BusinessLogic.DTOs;
using TeamJ.SKS.Package.BusinessLogic.Interfaces;

namespace TeamJ.SKS.Package.BusinessLogic
{
    public class HopLogic : IHopLogic
    {
        public List<BLHop> ExportWarehouses()
        {
            return new List<BLHop>();
        }

        public bool ImportWarehouses(BLHop blHop)
        {
            if (blHop != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public BLHop GetWarehouse(string code)
        {
            if (code == "test")
            {
                return new BLHop();
            }
            else
            {
                return null;
            }
        }
    }
}
