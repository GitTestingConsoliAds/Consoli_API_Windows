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
    public class ValidateResponseJsonSyncUserDevice
    {

        public ValidateResponseJsonSyncUserDevice()
        {
        }

        public static void ValidateResponseJsonSyncUserDeviceMainMethod(IRestResponse restResponse, string URL, int loop, string jsonRequest, string apiName)
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
                    Debug.WriteLine("Status code forbidden, internet connectivity not available" + URL + " please check your internet connection " + loop);
                }

                if (restResponse.StatusCode.ToString() == "0")
                {
                    Debug.WriteLine("Status code 0, the underlying connection was closed at " + URL + " " + loop);
                }

                if (restResponse.StatusCode.ToString() == "internal server error")
                {
                    Debug.WriteLine("Body contains internal server error, hence" + URL + " API is failed, pleaes check your internet connection " + loop);
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
            else
            {
                Extension.CreateLogFile(loop, jsonRequest, restResponse.Content.ToString(), restResponse.StatusCode.ToString(), apiName, URL);

            }



            //Starting key level validations for all key values
            separatedKeyValues = responseContent.Split(splitCharacter);
            foreach (var item in separatedKeyValues)
            {
                splitValue = item.Split(splitCharacterColon);
                //validating appid should not be null
                if (splitValue[0].ToString().Equals("{\"appID\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("Appid value appearing as null," + URL + "api is failed", jsonRequest);
                    }

                }
                // device id should not be null
                if (splitValue[0].ToString().Contains("\"deviceID\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("Device id value appearing as null," + URL + "api is failed", jsonRequest);
                    }

                }

                // Reigon should not be null
                if (splitValue[0].ToString().Contains("\"region\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("Region value appearing as null," + URL + "api is failed", jsonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("\"isAM\""))
                {
                    if (splitValue[1].ToString().Contains("0") || splitValue[1].ToString().Contains("1") || splitValue[1].ToString().Contains("2"))
                    {
                        Debug.WriteLine("AM mode is working fine");
                    }
                    else
                    {
                        Debug.WriteLine("isAM value appearing is not set," + URL + "api is failed", jsonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("\"package\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("package value appearing as null," + URL + "api is failed", jsonRequest);
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


                    if (splitValue[0].ToString().Contains("\"title\""))
                    {
                        if (splitValue[1] == null)
                        {
                            Debug.WriteLine("title value appearing as null," + URL + "api is failed", jsonRequest);
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

                            if (splitValue[0].ToString().Contains("\"adsQueueType\""))
                            {
                                if (splitValue[1].ToString().Contains("round_robin") || splitValue[1].ToString().Contains("priority"))
                                {
                                    Debug.WriteLine("Ads mediation queue value is set");
                                }
                                else
                                {
                                    Debug.WriteLine("Ads mediation queue is not set," + URL + "api is failed", jsonRequest);
                                }
                            }

                            if (splitValue[0].ToString().Contains("\"asRateUsUR\""))
                            {
                                if (splitValue[1] == null)
                                {
                                    Debug.WriteLine("As Rate Us URL is appearing null," + URL + "api is failed", jsonRequest);
                                }

                            }

                            if (splitValue[0].ToString().Contains("gpRateUsURL"))
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

                if (splitValue[0].ToString().Contains("\"message\""))
                {
                    if (splitValue[1].ToString().Contains("completed"))
                    {
                        Debug.WriteLine("Message value is completed");
                    }
                    else
                    {
                        Debug.WriteLine("message value is other then completed," + URL + "api is failed", jsonRequest);
                    }

                }
            }
        }


        public static void ValidateResponseJsonSyncUserDeviceSessionTokenMainMethod(IRestResponse restResponse, string URL, int loop, string jsonRequest)
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
                    Debug.WriteLine("Status code forbidden, internet connectivity not available" + URL + " please check your internet connection " + loop);
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
                    Debug.WriteLine("Status code not OK," + URL + " API has failed at " + loop + " " + restResponse.StatusCode.ToString(), jsonRequest);
                }
            }
            string[] separatedKeyValues = responseContent.Split(splitCharacter);

            if (!contentType.Equals("application/json; charset=utf-8"))
            {
                Debug.WriteLine(URL + "API failed as it is not correct json response in content type " + loop + responseContent.ToString(), jsonRequest);
            }
            if (responseContent.Contains("\"Page not found\""))
            {
                Debug.WriteLine("Body contains page not found error, hence" + URL + " API is failed " + loop + responseContent.ToString(), jsonRequest);
            }

            if (responseContent.Contains("\"appJson not found\""))
            {
                Debug.WriteLine("Body contains json not found error, hence" + URL + " API is failed " + loop + responseContent.ToString(), jsonRequest);
            }

            if (separatedKeyValues[0].ToString().Equals("\"Invalid Request\""))
            {
                Debug.WriteLine("Body contains json not found error, hence" + URL + " API is failed " + loop + responseContent.ToString(), jsonRequest);
            }



            //Starting key level validations for all key values
            separatedKeyValues = responseContent.Split(splitCharacter);
            foreach (var item in separatedKeyValues)
            {
                splitValue = item.Split(splitCharacterColon);
                //validating appid should not be null
                if (splitValue[0].ToString().Equals("{\"appID\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("Appid value appearing as null," + URL + "api is failed", jsonRequest);
                    }

                }
                // device id should not be null
                if (splitValue[0].ToString().Contains("\"deviceID\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("Device id value appearing as null," + URL + "api is failed", jsonRequest);
                    }

                }

                // Reigon should not be null
                if (splitValue[0].ToString().Contains("\"region\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("Region value appearing as null," + URL + "api is failed", jsonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("\"isAM\""))
                {
                    if (splitValue[1].ToString().Contains("0") || splitValue[1].ToString().Contains("1") || splitValue[1].ToString().Contains("2"))
                    {
                        Debug.WriteLine("AM mode is working fine");
                    }
                    else
                    {
                        Debug.WriteLine("isAM value appearing is not set," + URL + "api is failed", jsonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("\"package\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("package value appearing as null," + URL + "api is failed", jsonRequest);
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


                if (splitValue[0].ToString().Contains("\"title\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("title value appearing as null," + URL + "api is failed", jsonRequest);
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

                if (splitValue[0].ToString().Contains("\"adsQueueType\""))
                {
                    if (splitValue[1].ToString().Contains("round_robin") || splitValue[1].ToString().Contains("priority"))
                    {
                        Debug.WriteLine("Ads mediation queue value is set");
                    }
                    else
                    {
                        Debug.WriteLine("Ads mediation queue is not set," + URL + "api is failed", jsonRequest);
                    }
                }

                if (splitValue[0].ToString().Contains("\"asRateUsUR\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("As Rate Us URL is appearing null," + URL + "api is failed", jsonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("gpRateUsURL"))
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

                if (splitValue[0].ToString().Contains("\"message\""))
                {
                    if (splitValue[1].ToString().Contains("completed"))
                    {
                        Debug.WriteLine("message value is correct");
                    }
                    else
                    {
                        Debug.WriteLine("Message value not appearing correctly");
                    }

                }
                // session token should not be null
                if (splitValue[0].ToString().Contains("\"sessionToken\""))
                {
                    SessionToken.sessionToken = splitValue[1];
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("sessionToken value appearing as null," + URL + "api is failed", jsonRequest);

                    }
                }
            }
        }
    }
}
    
