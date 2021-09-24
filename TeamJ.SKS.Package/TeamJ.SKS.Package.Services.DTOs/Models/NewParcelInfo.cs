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
using System.Diagnostics.CodeAnalysis;

namespace TeamJ.SKS.Package.Services.DTOs.Models
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    [DataContract]
    public partial class NewParcelInfo
    { 
        /// <summary>
        /// The tracking ID of the parcel. 
        /// </summary>
        /// <value>The tracking ID of the parcel. </value>
        [RegularExpression("/^[A-Z0-9]{9}$/")]
        [DataMember(Name="trackingId")]
        public string TrackingId { get; set; }
    }
}
