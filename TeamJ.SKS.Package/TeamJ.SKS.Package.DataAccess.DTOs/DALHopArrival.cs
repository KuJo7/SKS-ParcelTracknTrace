using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.DataAccess.DTOs
{
    [ExcludeFromCodeCoverage]
    public class DALHopArrival
    {
        [Key]
        public string Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }
    }
}
