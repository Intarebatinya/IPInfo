using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPInfoCommon.Interfaces;
using IPInfoModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IPInfoWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IPorDomainInfoController : ControllerBase
    {
        private readonly ILogger<IPorDomainInfoController> _logger;
        private readonly IIpInfoLookUp _ipInfoLookUp;

        public IPorDomainInfoController(ILogger<IPorDomainInfoController> logger, IIpInfoLookUp ipInfoLookUp)
        {
            _logger = logger;
            _ipInfoLookUp = ipInfoLookUp;
        }

        /// <summary>
        /// This end point processes and provides information about and IP or Domain input using supported providers.
        /// </summary>
        /// <param name="request">Object containing request data</param>
        /// <returns>Domain Info</returns>
        [HttpPost]
        public IPorDomainInfoResponse RetrieveIpOrDomainInfo(BaseRequest request)
        {
            var retVal = new IPorDomainInfoResponse { OrginalRequest = request };
            try
            {
                if (request.LookUpServices?.Count() > 0) //We are using the default type
                {
                    var tasks = new List<Task<string>>();
                    foreach (var service in request.LookUpServices)
                    {
                        switch (service)
                        {
                            case SupportedServices.GeoIP:
                                var task = new Task<string>(() =>
                                {
                                    var ret = string.Empty;
                                    try
                                    {
                                        ret = _ipInfoLookUp.GeoIPLookUp(request.IpOrDomain).Result;
                                    }
                                    catch (Exception ex)
                                    {
                                        _logger.LogError(ex.Message, ex);
                                    }
                                    return ret;
                                });
                                task.Start();
                                tasks.Add(task);
                                break;
                            case SupportedServices.Ping:
                                task = new Task<string>(() =>
                                {
                                    var ret = string.Empty;
                                    try
                                    {
                                        ret = _ipInfoLookUp.PingLookUp(request.IpOrDomain).Result;
                                    }
                                    catch (Exception ex)
                                    {
                                        _logger.LogError(ex.Message, ex);
                                    }
                                    return ret;
                                });
                                task.Start();
                                tasks.Add(task);
                                break;
                            case SupportedServices.RDAP:
                                task = new Task<string>(() =>
                                {
                                    var ret = string.Empty;
                                    try
                                    {
                                        ret = _ipInfoLookUp.RDAPLookUp(request.IpOrDomain).Result;
                                    }
                                    catch (Exception ex)
                                    {
                                        _logger.LogError(ex.Message, ex);
                                    }
                                    return ret;
                                });
                                task.Start();
                                tasks.Add(task);
                                break;
                            case SupportedServices.ReverseDNS:
                                task = new Task<string>(() =>
                                {
                                    var ret = string.Empty;
                                    try
                                    {
                                        ret = _ipInfoLookUp.ReverseDNSLookUp(request.IpOrDomain).Result;
                                    }
                                    catch (Exception ex)
                                    {
                                        _logger.LogError(ex.Message, ex);
                                    }
                                    return ret;
                                });
                                task.Start();
                                tasks.Add(task);
                                break;
                            default:
                                var message = $"{service} is not supported";
                                retVal.Messages.Add(message);
                                _logger.LogError(message);
                                break;
                        }
                    }

                    //Wait for all the tasks
                    Task.WaitAll(tasks.ToArray());

                    //Package the result
                    foreach (var t in tasks)
                    {
                        retVal.JsonResults.Add(t.Result);
                    }
                }
                else
                {
                    //Process the default
                    retVal.JsonResults.Add(_ipInfoLookUp.GeoIPLookUp(request.IpOrDomain).Result);
                }
            }
            catch(Exception ex)
            {
                retVal.Messages.Add("A server error has occurred");
                _logger.LogError(ex.Message, ex);
            }
            return retVal;
        }
    }
}