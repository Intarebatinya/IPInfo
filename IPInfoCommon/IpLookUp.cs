using IPInfoCommon.Helpers;
using IPInfoCommon.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPInfoCommon
{
    /// <summary>
    /// This is the current implementation of supported ip look up APIs
    /// </summary>
    public class IpLookUp : IIpInfoLookUp
    {
        private readonly string _lookupBaseUrl;
        public IpLookUp(string lookupBaseUrl)
        {
            _lookupBaseUrl = lookupBaseUrl;
        }
        public async Task<string> GeoIPLookUp(string ipAddress)
        {
            var url = $"{_lookupBaseUrl}api/GeoIPWorker?input={ipAddress}";
            return await url.GetHttpResponse();
        }

        public async Task<string> PingLookUp(string ipAddress)
        {
            var url = $"{_lookupBaseUrl}api/GRDAPWorker?input={ipAddress}";
            return await url.GetHttpResponse();
        }

        public async Task<string> RDAPLookUp(string ipAddress)
        {
            var url = $"{_lookupBaseUrl}api/PingWorker?input={ipAddress}";
            return await url.GetHttpResponse();
        }

        public async Task<string> ReverseDNSLookUp(string ipAddress)
        {
            var url = $"{_lookupBaseUrl}api/ReverseDNSWorker?input={ipAddress}";
            return await url.GetHttpResponse();
        }

    }
}
