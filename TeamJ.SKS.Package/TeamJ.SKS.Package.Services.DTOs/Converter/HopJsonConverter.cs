using Newtonsoft.Json;
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
            if (jObject is null) throw new ArgumentNullException(nameof(jObject));

            if (ContainsField(jObject, "regionGeoJson", "logisticsPartner", "logisticsPartnerUrl"))
            {
                return new Transferwarehouse();
            }
            else if (ContainsField(jObject, "regionGeoJson", "numberPlate"))
            {
                return new Truck();
            }
            else if (ContainsField(jObject, "level", "nextHops"))
            {
                return new Warehouse();
            }
            else
            {
                //return new Hop();
                throw new JsonSerializationException("Hop does not contain necessary fields.");
            }
        }
    }
}
