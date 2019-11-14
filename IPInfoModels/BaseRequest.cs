using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IPInfoModels
{
    public class BaseRequest
    {
        public string IpOrDomain { get; set; }
        public List<SupportedServices> LookUpServices { get; set; }
    }
}
