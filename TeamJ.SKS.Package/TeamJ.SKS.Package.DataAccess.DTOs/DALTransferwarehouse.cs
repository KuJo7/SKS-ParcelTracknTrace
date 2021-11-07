using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.DataAccess.DTOs
{
    [ExcludeFromCodeCoverage]
    public class DALTransferwarehouse : DALHop
    {
        [Key]
        public string Id { get; set; }

        public string RegionGeoJson { get; set; }

        public string LogisticsPartner { get; set; }

        public string LogisticsPartnerUrl { get; set; }
    }
}
