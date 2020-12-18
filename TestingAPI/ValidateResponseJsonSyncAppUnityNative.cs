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
    public class ValidateResponseJsonSyncAppUnityNative
    {

        public ValidateResponseJsonSyncAppUnityNative()
        {
        }

        public static void ValidateResponseJsonSyncAppUnityNativeMainMethod(IRestResponse restResponse, string URL, int loop, string jsonRequest, string apiName)
        {

            string contentType = null;
            string responseContent = null;
            if (restResponse.ContentType != null)
            {
                contentType = restResponse.ContentType.ToString();
                responseContent = restResponse.Content.ToString();
            }
            //variable decleration
            char[] splitCharacter = { ',' };
            char[] splitCharacterColon = { ':' };
            string[] splitValue = null;

            //verifying status code and other major responses from API specifically status code
            if (restResponse.StatusCode.ToString() != "OK")
            {
                if (restResponse.StatusCode.ToString() == "Forbidden")
                {
                    Debug.WriteLine("Status code forbidden, internet connectivity not available" + URL + " please check your internet connection " + loop, jsonRequest);
                }

                if (restResponse.StatusCode.ToString() == "0")
                {
                    Debug.WriteLine("Status code 0, the underlying connection was closed at " + URL + " " + loop);
                }

                if (restResponse.StatusCode.ToString() == "internal server error")
                {
                    Debug.WriteLine("Body contains internal server error, hence" + URL + " API is failed, pleaes check your internet connection " + loop, jsonRequest);
                }
                else
                {
                    Debug.WriteLine("Status code not OK," + URL + " API has failed at " + loop + " " + restResponse.StatusCode.ToString() + restResponse.Content.ToString(), jsonRequest);
                    Extension.CreateLogFile(loop, jsonRequest, restResponse.Content.ToString(), restResponse.StatusCode.ToString(), apiName, URL);

                }
            }
            string[] separatedKeyValues = responseContent.Split(splitCharacter);

            if (!contentType.Equals("application/json; charset=utf-8"))
            {
                Debug.WriteLine(URL + "API failed as it is not correct json response in content type " + loop + responseContent.ToString(), jsonRequest);
                Extension.CreateLogFile(loop, jsonRequest, restResponse.Content.ToString(), restResponse.StatusCode.ToString(), apiName, URL);

            }
            if (responseContent.Contains("\"Page not found\""))
            {
                Debug.WriteLine("Body contains page not found error, hence" + URL + " API is failed " + loop + responseContent.ToString(), jsonRequest);
                Extension.CreateLogFile(loop, jsonRequest, restResponse.Content.ToString(), restResponse.StatusCode.ToString(), apiName, URL);

            }

            if (responseContent.Contains("\"appJson not found\""))
            {
                Debug.WriteLine("Body contains json not found error, hence" + URL + " API is failed " + loop + responseContent.ToString(), jsonRequest);
                Extension.CreateLogFile(loop, jsonRequest, restResponse.Content.ToString(), restResponse.StatusCode.ToString(), apiName, URL);

            }

            if (separatedKeyValues[0].ToString().Equals("\"Invalid Request\""))
            {
                Debug.WriteLine("Body contains json not found error, hence" + URL + " API is failed " + loop + responseContent.ToString(), jsonRequest);
                Extension.CreateLogFile(loop, jsonRequest, restResponse.Content.ToString(), restResponse.StatusCode.ToString(), apiName, URL);

            }


            if (responseContent.Contains("{\"message\":\"Sync Failed: Please connect your brand with Admob and enable your API integration toggle in order to use Admob in your mediation\"}"))
            {
                Debug.WriteLine(URL + "exeuction successful");
                Extension.CreateLogFile(loop, jsonRequest, restResponse.Content.ToString(), restResponse.StatusCode.ToString(), apiName, URL);

            }
            else
            {
                Extension.CreateLogFile(loop, jsonRequest, restResponse.Content.ToString(), restResponse.StatusCode.ToString(), apiName, URL);
            }


            //Starting key level validations for all key values
            separatedKeyValues = responseContent.Split(splitCharacter);
            foreach (var item in separatedKeyValues)
            {
                splitValue = item.Split(splitCharacterColon);
                //validating package should not be null

                if (splitValue[0].ToString().Equals("{\"adsQueueType\""))
                {
                    if (splitValue[1].ToString().Contains("round_robin") || splitValue[1].ToString().Contains("priority"))
                    {
                        Debug.WriteLine("Ads mediation queue value is set");
                    }
                    else
                    {
                        Debug.WriteLine("adsqueue type value not set");
                    }
                }

                if (splitValue[0].ToString().Contains("\"childDirected\""))
                {
                    if (splitValue[1].ToString().Contains("0") || splitValue[1].ToString().Contains("1"))
                    {
                        Debug.WriteLine("child directed value is set");
                    }
                    else
                    {
                        Debug.WriteLine("Child directed value is not set," + URL + "api is failed", jsonRequest);
                    }
                }

                if (splitValue[0].ToString().Contains("\"package\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("package value appearing as null," + URL + "api is failed", jsonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("\"mediationMode\""))
                {
                    if (splitValue[1].ToString().Contains("Production") || splitValue[1].ToString().Contains("Test"))
                    {
                        Debug.WriteLine("Mediation mode value is set");
                    }
                    else
                    {
                        Debug.WriteLine("Mediation value is not set," + URL + "api is failed", jsonRequest);
                    }
                }

                if (splitValue[0].ToString().Contains("\"totalsequences\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("sequences value appearing as null," + URL + "api is failed", jsonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("\"store\""))
                {
                    if (splitValue[1].ToString().Contains("41") || splitValue[1].ToString().Contains("42") || splitValue[1].ToString().Contains("43"))
                    {
                        Debug.WriteLine("Sequences value is set");
                    }
                    else
                    {
                        Debug.WriteLine("Sequences value is not set," + URL + "api is failed", jsonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("\"isHideAds\""))
                {
                    if (splitValue[1].ToString().Contains("0") || splitValue[1].ToString().Contains("1"))
                    {
                        Debug.WriteLine("Is hide ads value is set");
                    }
                    else
                    {
                        Debug.WriteLine("Is hide ads value is not set," + URL + "api is failed", jsonRequest);
                    }
                }


                if (splitValue[0].ToString().Contains("\"asRateUsURL\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("As Rate Us URL is appearing null," + URL + "api is failed", jsonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("\"gpRateUsURL\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("gp Rate Us URL is appearing null," + URL + "api is failed", jsonRequest);
                    }

                }


                if (splitValue[0].ToString().Contains("\"supportURL\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("Support URL is appearing null," + URL + "api is failed", jsonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("\"gpMoreAppsURL\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("Go more Apps URL is appearing null," + URL + "api is failed", jsonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("\"amMoreAppsURL\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("AM more Apps URL is appearing null," + URL + "api is failed", jsonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("\"asMoreAppsURL\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("AS more Apps URL is appearing null," + URL + "api is failed", jsonRequest);
                    }
                }

                if (splitValue[0].ToString().Contains("\"package\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("package value appearing as null," + URL + "api is failed", jsonRequest);
                    }

                }
                // gsskdversion should not be null
                if (splitValue[0].ToString().Contains("\"gssdkversion\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("gssdkversion value appearing as null," + URL + "api is failed", jsonRequest);
                    }

                }

                // sdkversionid should not be null
                if (splitValue[0].ToString().Contains("\"sdkVersionID\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("sdkVersionID value appearing as null," + URL + "api is failed", jsonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("\"sdkVersion\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("sdkVersion value appearing as null," + URL + "api is failed", jsonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("\"userSignature\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("userSignature value appearing as null," + URL + "api is failed", jsonRequest);
                    }

                }
            }
        }
    }
}
