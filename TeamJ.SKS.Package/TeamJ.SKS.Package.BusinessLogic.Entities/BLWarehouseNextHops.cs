using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.BusinessLogic.DTOs
{
    [ExcludeFromCodeCoverage]
    public class BLWarehouseNextHops
    {
        public int TraveltimeMins { get; set; }

        public BLHop Hop { get; set; }
    }
}
