using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Net;
using IPInfoCommon.Helpers;

namespace IPInfoWorkerFunctions
{
    public static class IPInfoWorkerFunctions
    {
        /// <summary>
        /// This function process IP request using GeoIP API.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("GeoIPWorker")]
        public static async Task<IActionResult> GeoIPRun([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req, ILogger log, ExecutionContext context)
        {
            dynamic retVal;
            string input = req.Query["input"];
            if (!input.IsValidIp())
            {
                retVal = new BadRequestObjectResult("An IP or Domain is required as in input");
            }
            log.LogInformation($"GeoIPWorker started processing {input}");
            var url = GetConfigUrl(context, "GeoIpUrl");
            if (!string.IsNullOrEmpty(url))
            {
                url = $"{url}?q={input}";  
                retVal = (ActionResult)new OkObjectResult(await url.GetHttpResponse());
            }
            else
            {
                retVal = new BadRequestObjectResult("An internal server error");
                log.LogError("GeoIPWorker is not configured");
            }
            return retVal;
        }

        /// <summary>
        /// This function process IP request using GRDAP API.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("GRDAPWorker")]
        public static async Task<IActionResult> RDAPRun([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req, ILogger log)
        {
            dynamic retVal;
            string input = req.Query["input"];
            if (string.IsNullOrEmpty(input))
            {
                retVal = new BadRequestObjectResult("An IP or Domain is required as in input");
            }
            log.LogInformation($"GeoIPWorker started processing {input}");


            retVal = (ActionResult)new OkObjectResult($"Input, {input}");

            return retVal;
        }

        /// <summary>
        /// This function process IP request using ReverseDNS API.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("ReverseDNSWorker")]
        public static async Task<IActionResult> ReverseDNSRun([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req, ILogger log)
        {
            dynamic retVal;
            string input = req.Query["input"];
            if (string.IsNullOrEmpty(input))
            {
                retVal = new BadRequestObjectResult("An IP or Domain is required as in input");
            }
            log.LogInformation($"GeoIPWorker started processing {input}");


            retVal = (ActionResult)new OkObjectResult($"Input, {input}");

            return retVal;
        }

        /// <summary>
        /// This function process IP request using Ping API.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("PingWorker")]
        public static async Task<IActionResult> PingRun([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req, ILogger log)
        {
            dynamic retVal;
            string input = req.Query["input"];
            if (string.IsNullOrEmpty(input))
            {
                retVal = new BadRequestObjectResult("An IP or Domain is required as in input");
            }
            log.LogInformation($"GeoIPWorker started processing {input}");


            retVal = (ActionResult)new OkObjectResult($"Input, {input}");

            return retVal;
        }

        #region Helpers
        private static string GetConfigUrl(ExecutionContext context, string key)
        {
            return new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build()[key];
        }
        #endregion

    }
}
