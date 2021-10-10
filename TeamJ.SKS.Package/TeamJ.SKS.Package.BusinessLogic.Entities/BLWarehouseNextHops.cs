﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.BusinessLogic.DTOs
{
    public class BLWarehouseNextHops
    {
        public int? TraveltimeMins { get; set; }

        public BLHop Hop { get; set; }
    }
}
