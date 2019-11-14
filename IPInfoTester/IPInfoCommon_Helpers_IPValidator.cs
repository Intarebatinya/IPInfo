using IPInfoCommon.Helpers;
using System;
using Xunit;

namespace IPInfoTester
{
    public class IPInfoCommon_Helpers_IPValidator
    {
        #region IP Address Tests
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void IsValidIP_ip_cannot_be_null_or_empty_Test(string ip)
        {
            Assert.False(ip.IsValidIp());
        }
        [Theory]
        [InlineData("127.0.0.1")]
        [InlineData("149.157.248.125")]
        [InlineData("198.136.185.180")]
        public void IsValidIP_real_ip_addresses(string ip)
        {
            Assert.True(ip.IsValidIp());
        }

        [Theory]
        [InlineData("127.0.0..")]
        [InlineData("149.157.125.44")]
        [InlineData("149..")]
        public void IsValidIP_bad_ip_addresses(string ip)
        {
            Assert.False(ip.IsValidIp());
        }
        #endregion

        #region Domain Test
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void IsValidDomain_domain_cannot_be_null_or_empty_Test(string ip)
        {
            Assert.False(ip.IsValidDomain());
        }
        #endregion
    }
}
