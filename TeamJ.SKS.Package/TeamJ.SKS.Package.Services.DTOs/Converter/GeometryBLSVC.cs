using AutoMapper;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJ.SKS.Package.Services.DTOs.Converter
{
    [ExcludeFromCodeCoverage]
    internal class GeometryBLSVC : ITypeConverter<Geometry, string>, IValueConverter<Geometry, string>
    {
        private static readonly JsonSerializer _serializer = GeoJsonSerializer.Create(new(new(), 4326));

        public string Convert(Geometry source, string destination, ResolutionContext context)
        {
            return Convert(source, context);
        }

        public string Convert(Geometry sourceMember, ResolutionContext context)
        {
            try
            {
                using var stringWriter = new StringWriter();
                using var jsonWriter = new JsonTextWriter(stringWriter);
                _serializer.Serialize(jsonWriter, sourceMember);
                jsonWriter.Flush();
                stringWriter.Flush();
                return stringWriter.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to convert Geometry to JSON.", ex);
            }
        }
    }
}
