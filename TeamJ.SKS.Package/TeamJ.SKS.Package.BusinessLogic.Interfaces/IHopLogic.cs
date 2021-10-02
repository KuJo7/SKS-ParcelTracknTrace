using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamJ.SKS.Package.BusinessLogic.DTOs;

namespace TeamJ.SKS.Package.BusinessLogic.Interfaces
{
    public interface IHopLogic
    {
        public List<BLHop> exportAllHops();
        public void importAllHops();
        public BLHop exportHops(string code);
    }
}
