using AutoMapper;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.Services.DTOs.Converter
{
    internal class GeometrySVCBL : ITypeConverter<string, Geometry>, IValueConverter<string, Geometry>
    {
        private static readonly JsonSerializer _serializer = GeoJsonSerializer.Create(new(new(), 4326));

        public Geometry Convert(string source, Geometry destination, ResolutionContext context)
        {
            return Convert(source, context);
        }

        public Geometry Convert(string sourceMember, ResolutionContext context)
        {
            try
            {
                using var stringReader = new StringReader(sourceMember);
                using var jsonReader = new JsonTextReader(stringReader);
                return _serializer.Deserialize<Feature>(jsonReader).Geometry;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to convert JSON to Geometry.", ex);
            }
        }
    }
}
