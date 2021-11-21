using System;
using TeamJ.SKS.Package.Services.DTOs.Models;

namespace TeamJ.SKS.Package.ServiceAgents.Interfaces
{
    public interface IGeoEncodingAgent
    {
        GeoCoordinate EncodeAddress(string address);
    }
}
