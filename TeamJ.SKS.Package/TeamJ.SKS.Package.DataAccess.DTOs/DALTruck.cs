using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;

namespace TeamJ.SKS.Package.DataAccess.DTOs
{
    [ExcludeFromCodeCoverage]
    public class DALTruck : DALHop
    {
        [Column(TypeName = "Geometry")]
        public virtual Geometry RegionGeoJson { get; set; }
        public string NumberPlate { get; set; }
    }
}
