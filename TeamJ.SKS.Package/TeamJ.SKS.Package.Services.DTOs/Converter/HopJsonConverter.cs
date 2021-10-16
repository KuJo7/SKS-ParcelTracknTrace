using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamJ.SKS.Package.Services.DTOs.Models;

namespace TeamJ.SKS.Package.Services.DTOs.Converter
{
    [ExcludeFromCodeCoverage]
    public class HopJsonConverter : JsonCreationConverter<Hop>
    {
        protected override Hop Create(Type objectType, JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException("jObject");

            if (jObject["regionGeoJson"] != null && jObject["numberPlate"] != null)
            {
                return new Truck();
            }
            else if (jObject["level"] != null && jObject["nextHops"] != null)
            {
                return new Warehouse();
            }
            else if (jObject["regionGeoJson"] != null && jObject["logisticsPartner"] != null && jObject["logisticsPartnerUrl"] != null)
            {
                return new Transferwarehouse();
            }
            else
            {
                return null;
            }
        }
    }
}
