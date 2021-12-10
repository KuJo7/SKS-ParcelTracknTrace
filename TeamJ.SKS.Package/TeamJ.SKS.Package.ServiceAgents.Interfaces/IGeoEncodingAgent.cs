using NetTopologySuite.Geometries;
using System;
using TeamJ.SKS.Package.Services.DTOs.Models;

namespace TeamJ.SKS.Package.ServiceAgents.Interfaces
{
    public interface IGeoEncodingAgent
    {
        Point EncodeAddress(string address);
    }
}
