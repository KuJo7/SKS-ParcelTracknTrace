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
    [ExcludeFromCodeCoverage]
    public class BLWebhookResponse
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [RegularExpression("/^[A-Z0-9]{9}$/")]
        [DataMember(Name = "trackingId")]
        public string TrackingId { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "created_at")]
        public DateTime? CreatedAt { get; set; }
    }
}
