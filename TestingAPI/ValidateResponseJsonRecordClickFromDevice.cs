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
    public class ValidateResponseJsonRecordClickFromDevice
    {

        public ValidateResponseJsonRecordClickFromDevice()
        {
        }

        public static void ValidateResponseJsonRecordCickFromDevice(IRestResponse restResponse, string URL, int loop, string jSonRequest, string apiName)
        {
            //variable decleration
            var contentType = restResponse.ContentType.ToString();
            var responseContent = restResponse.Content.ToString();
            char[] splitCharacter = { ',' };
            char[] splitCharacterColon = { ':' };
            string[] splitValue = null;

            //verifying status code and other major responses from API specifically status code
            if (restResponse.StatusCode.ToString() != "OK")
            {
                if (restResponse.StatusCode.ToString() == "Forbidden")
                {
                    Debug.WriteLine("Status code forbidden, internet connectivity not available" + URL + " please check your internet connection " + loop, jSonRequest);
                }

                if (restResponse.StatusCode.ToString() == "0")
                {
                    Debug.WriteLine("Status code 0, the underlying connection was closed at " + URL + " " + loop);
                }

                if (restResponse.StatusCode.ToString() == "internal server error")
                {
                    Debug.WriteLine("Body contains internal server error, hence" + URL + " API is failed, pleaes check your internet connection " + loop, jSonRequest);
                }
                else
                {
                    Debug.WriteLine("Status code not OK," + URL + " API has failed at " + loop + " " + restResponse.StatusCode.ToString() + restResponse.Content.ToString(), jSonRequest);
                    Extension.CreateLogFile(loop, jSonRequest, restResponse.Content.ToString(), restResponse.StatusCode.ToString(), apiName, URL);

                }
            }
            string[] separatedKeyValues = responseContent.Split(splitCharacter);

            if (responseContent.Contains("\"Page not found\""))
            {
                Debug.WriteLine("Body contains page not found error, hence" + URL + " API is failed " + loop + responseContent.ToString(), jSonRequest);
                Extension.CreateLogFile(loop, jSonRequest, restResponse.Content.ToString(), restResponse.StatusCode.ToString(), apiName, URL);
            }

            if (responseContent.Contains("\"appJson not found\""))
            {
                Debug.WriteLine("Body contains json not found error, hence" + URL + " API is failed " + loop + responseContent.ToString(), jSonRequest);
                Extension.CreateLogFile(loop, jSonRequest, restResponse.Content.ToString(), restResponse.StatusCode.ToString(), apiName, URL);
            }

            if (separatedKeyValues[0].ToString().Equals("\"Invalid Request\""))
            {
                Debug.WriteLine("Body contains json not found error, hence" + URL + " API is failed " + loop + responseContent.ToString(), jSonRequest);
                Extension.CreateLogFile(loop, jSonRequest, restResponse.Content.ToString(), restResponse.StatusCode.ToString(), apiName, URL);
            }
            else
            {
                Extension.CreateLogFile(loop, jSonRequest, restResponse.Content.ToString(), restResponse.StatusCode.ToString(), apiName, URL);
            }


            //Starting key level validations for all key values
            separatedKeyValues = responseContent.Split(splitCharacter);
            foreach (var item in separatedKeyValues)
            {
                splitValue = item.Split(splitCharacterColon);
                //validating appid should not be null
                if (splitValue[0].ToString().Contains("\"Update\""))
                {
                    if (splitValue[1].ToString().Contains("Date and status updated App already instlled"))
                    {
                        Debug.WriteLine(URL + "Update Date and status updated App already instlled, API is successful.");
                    }
                    else
                    {
                        Debug.WriteLine("Appid value appearing as null," + URL + "api is failed", jSonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("\"sessionToken\""))
                {
                    SessionToken.sessionToken = splitValue[1];
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("sessionToken value appearing as null," + URL + "api is failed", jSonRequest);

                    }
                }

            }
        }
    }
}
