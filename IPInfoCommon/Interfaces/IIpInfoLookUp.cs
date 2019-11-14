using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPInfoCommon.Interfaces
{
    public interface IIpInfoLookUp
    {
        Task<string> GeoIPLookUp(string ipAddress);
        Task<string> PingLookUp(string ipAddress);
        Task<string> RDAPLookUp(string ipAddress);
        Task<string> ReverseDNSLookUp(string ipAddress);
    }
}
