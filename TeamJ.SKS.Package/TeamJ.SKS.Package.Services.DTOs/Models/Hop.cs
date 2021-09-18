/*
 * Parcel Logistics Service
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.20.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace TeamJ.SKS.Package.Services.DTOs.Models
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class Hop
    { 
        /// <summary>
        /// Gets or Sets HopType
        /// </summary>
        [Required]

        [DataMember(Name="hopType")]
        public string HopType { get; set; }

        /// <summary>
        /// Unique CODE of the hop.
        /// </summary>
        /// <value>Unique CODE of the hop.</value>
        [Required]
        [RegularExpression("/^[A-Z]{4}\\d{1,4}$/")]
        [DataMember(Name="code")]
        public string Code { get; set; }

        /// <summary>
        /// Description of the hop.
        /// </summary>
        /// <value>Description of the hop.</value>
        [Required]

        [DataMember(Name="description")]
        public string Description { get; set; }

        /// <summary>
        /// Delay processing takes on the hop.
        /// </summary>
        /// <value>Delay processing takes on the hop.</value>
        [Required]

        [DataMember(Name="processingDelayMins")]
        public int? ProcessingDelayMins { get; set; }

        /// <summary>
        /// Name of the location (village, city, ..) of the hop.
        /// </summary>
        /// <value>Name of the location (village, city, ..) of the hop.</value>
        [Required]

        [DataMember(Name="locationName")]
        public string LocationName { get; set; }

        /// <summary>
        /// Gets or Sets LocationCoordinates
        /// </summary>
        [Required]

        [DataMember(Name="locationCoordinates")]
        public GeoCoordinate LocationCoordinates { get; set; }
    }
}
