using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace IPInfoCommon.Helpers
{
    public static class IPValidator
    {
        public static bool IsValidIp(this string input)
        {
            var ret = false;
            if (!string.IsNullOrEmpty(input))
            {
                return IPAddress.TryParse(input, out IPAddress ip);
            }
            return ret;
        }
        public static bool IsValidDomain(this string input)
        {
            var ret = false;
            if (!string.IsNullOrEmpty(input))
            {
                return input.Split('.').Length >= 2;
            }
            return ret;
        }
    }
}
