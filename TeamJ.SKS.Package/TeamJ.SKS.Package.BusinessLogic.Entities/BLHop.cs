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
    public class BLHop
    {
        [Required]
        [DataMember(Name = "hopType")]
        public string HopType { get; set; }

        [Required]
        [DataMember(Name = "processingDelayMins")]
        public int? ProcessingDelayMins { get; set; }

        [Required]
        [DataMember(Name = "locationName")]
        public string LocationName { get; set; }

        [Required]
        [RegularExpression("/^[A-Z]{4}\\d{1,4}$/")]
        [DataMember(Name = "code")]
        public string Code { get; set; }

        [Required]
        [DataMember(Name = "description")]
        public string Description { get; set; }

        [Required]
        [DataMember(Name = "dateTime")]
        public DateTime? DateTime { get; set; }

        [Required]
        [DataMember(Name = "regionGeoJson")]
        public string RegionGeoJson { get; set; }

        [Required]
        [DataMember(Name = "numberPlate")]
        public string NumberPlate { get; set; }

        [Required]
        [DataMember(Name = "level")]
        public int? Level { get; set; }

        [Required]
        [DataMember(Name = "traveltimeMins")]
        public int? TraveltimeMins { get; set; }

        [Required]
        [DataMember(Name = "logisticsPartner")]
        public string LogisticsPartner { get; set; }

        [Required]
        [DataMember(Name = "logisticsPartnerUrl")]
        public string LogisticsPartnerUrl { get; set; }

        [Required]
        [DataMember(Name = "lat")]
        public double? Lat { get; set; }

        [Required]
        [DataMember(Name = "lon")]
        public double? Lon { get; set; }
    }
}
