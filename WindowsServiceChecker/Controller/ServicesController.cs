using System.ServiceProcess;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using System;

namespace WindowsServiceChecker.Controller
{
    public class ServicesController : ApiController
    {
        // GET api/<controller>/<action>
        [HttpGet]
        public HttpResponseMessage Status(string service)
        {
            if(string.IsNullOrEmpty(service))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            ServiceController sc = new ServiceController(service);
            
            try
            {
                switch (sc.Status)
                {
                    case ServiceControllerStatus.Running:
                        return Request.CreateResponse(HttpStatusCode.Accepted);
                        //Add other statuses and their responses.
                    default:
                        return Request.CreateResponse(HttpStatusCode.ServiceUnavailable);
                }
            }
            catch(Exception notAService)
            {
                Console.WriteLine("Not a service:" + notAService);
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            
        }
    }
}