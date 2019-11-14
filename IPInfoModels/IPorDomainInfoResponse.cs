using System;
using System.Collections.Generic;
using System.Text;

namespace IPInfoModels
{
    public class IPorDomainInfoResponse: BaseResponse
    {
        public BaseRequest OrginalRequest { get; set; }
        public List<string> JsonResults { get; set; } = new List<string>();
    }
}
