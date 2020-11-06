using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monetization_Automation.Utils;
using RestSharp;
using System;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp.Serialization.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Runtime.Remoting.Messaging;
using Newtonsoft.Json.Schema;
using Json;


namespace Monetization_Automation.Test
{
    public class ValidateResponseJsonRecordBraodCardInstall
    {

        public ValidateResponseJsonRecordBraodCardInstall()
        {
        }

        public static void ValidateResponseJsonValidateResponseJsonRecordBraodCardInstall(IRestResponse restResponse, string URL, int loop, string JsonRequest, string apiName)
        {
            var contentType = restResponse.ContentType.ToString();
            var responseContent = restResponse.Content.ToString();
            if (restResponse.StatusCode.ToString() != "OK")
            {
                if (restResponse.StatusCode.ToString() == "Forbidden")
                {
                    Debug.WriteLine("Status code forbidden, internet connectivity not available" + URL + " please check your internet connection " + loop, JsonRequest);
                }

                if (restResponse.StatusCode.ToString() == "0")
                {
                    Debug.WriteLine("Status code 0, the underlying connection was closed at " + URL + " " + loop);
                }

                if (restResponse.StatusCode.ToString() == "internal server error")
                {
                    Debug.WriteLine("Body contains internal server error, hence" + URL + " API is failed, pleaes check your internet connection " + loop, JsonRequest);
                }
                else
                {
                Debug.WriteLine("Status code not OK," + URL + " API has failed at " + loop + " " + restResponse.StatusCode.ToString() + restResponse.Content.ToString());
                    Extension.CreateLogFile(loop, JsonRequest, restResponse.Content.ToString(), restResponse.StatusCode.ToString(), apiName, URL);
                }
            }
            if (!contentType.Equals("application/json; charset=utf-8"))
            {
                Debug.WriteLine(URL + "API failed as it is not correct json response in content type " + loop, JsonRequest);
                Extension.CreateLogFile(loop, JsonRequest, restResponse.Content.ToString(), restResponse.StatusCode.ToString(), apiName, URL);

            }
            if (responseContent.Contains("Page not found"))
            {
                Debug.WriteLine("Body contains page not found error, hence" + URL + " API is failed " + loop, JsonRequest);
                Extension.CreateLogFile(loop, JsonRequest, restResponse.Content.ToString(), restResponse.StatusCode.ToString(), apiName, URL);

            }

            if (responseContent.Contains("appJson not found "))
            {
                Debug.WriteLine("Body contains json not found error, hence" + URL + " API is failed " + loop, JsonRequest);
                Extension.CreateLogFile(loop, JsonRequest, restResponse.Content.ToString(), restResponse.StatusCode.ToString(), apiName, URL);
            }

            if (responseContent.Contains("invalid request"))
            {
                Debug.WriteLine("Body contains json not found error, hence" + URL + " API is failed " + loop, JsonRequest);
                Extension.CreateLogFile(loop, JsonRequest, restResponse.Content.ToString(), restResponse.StatusCode.ToString(), apiName, URL);
            }
            else
            {
                Extension.CreateLogFile(loop, JsonRequest, restResponse.Content.ToString(), restResponse.StatusCode.ToString(), apiName, URL);
            }
        }
    }
}
