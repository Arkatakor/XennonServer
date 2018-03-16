using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DataAccess;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace XennonFunctionsApp
{
    public static class Add
    {
        [FunctionName("Add")]
        public static async Task<HttpResponseMessage> Run(
        [HttpTrigger(
            AuthorizationLevel.Function,
            "get",
            Route = "add/num1/{num1}/num2/{num2}")]
            HttpRequestMessage req,
            int num1,
            int num2,
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // parse query parameter
            string name = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
                .Value;

            // Get request body
            dynamic data = await req.Content.ReadAsAsync<object>();

            //int addition = (data?.num1 != null && data?.num1 != null) ?? data?.num1 + data?.num2;
            var result = new AdditionResult
            {
                Result = num1 + num2,
                TimeOnServer = DateTime.Now
            };

            //// Set name to query string or body data
            return req.CreateResponse(HttpStatusCode.OK, result);
        }


        //  Previous (working) sample
        //public static async Task<HttpResponseMessage> Run(
        //[HttpTrigger(
        //    AuthorizationLevel.Function,
        //    "get",
        //    "post",
        //    Route = "add/num1/{num1}/num2/{num2}")]
        //    HttpRequestMessage req,
        //    int num1,
        //    int num2,
        //    TraceWriter log)
        //{
        //    log.Info("C# HTTP trigger function processed a request.");

        //    // parse query parameter
        //    string name = req.GetQueryNameValuePairs()
        //        .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
        //        .Value;

        //    // Get request body
        //    dynamic data = await req.Content.ReadAsAsync<object>();

        //    //int addition = (data?.num1 != null && data?.num1 != null) ?? data?.num1 + data?.num2;
        //    int addition = num1 + num2;

        //    //// Set name to query string or body data
        //    return req.CreateResponse(HttpStatusCode.OK, addition.ToString());
        //}


        //  TUTORIAL
        //public static IActionResult Run(
        //[HttpTrigger(
        //    AuthorizationLevel.Function,
        //    "get",
        //    Route = "add/num1/{num1}/num2/{num2}")]
        //HttpRequest req,
        //int num1,
        //int num2,
        //TraceWriter log)
        //{
        //    log.Info($"C# HTTP trigger function processed a request with {num1} and {num2}");

        //    var addition = num1 + num2;

        //    return new OkObjectResult(addition);
        //}


        //  ORIGINAL
        //public static async Task<HttpResponseMessage> Run(
        //    [HttpTrigger(AuthorizationLevel.Function, 
        //    "get", 
        //    "post", 
        //    Route = null)]HttpRequestMessage req, TraceWriter log)
        //{
        //    log.Info("C# HTTP trigger function processed a request.");

        //    // parse query parameter
        //    string name = req.GetQueryNameValuePairs()
        //        .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
        //        .Value;

        //    // Get request body
        //    dynamic data = await req.Content.ReadAsAsync<object>();

        //    // Set name to query string or body data
        //    name = name ?? data?.name;

        //    return name == null
        //        ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
        //        : req.CreateResponse(HttpStatusCode.OK, "Hello " + name);
        //}

    }
}
