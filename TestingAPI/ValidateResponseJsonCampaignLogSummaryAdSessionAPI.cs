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
    public class ValidateResponseJsonCampaignLogSummaryAdSessionAPI
    {

        public ValidateResponseJsonCampaignLogSummaryAdSessionAPI()
        {
        }

        public static void ValidateResponseJsonCampaignLogSummaryAPIASessionMainMethod(IRestResponse restResponse, string URL, int loop, string jSonRequest, string apiName)
        {
            //variable decleration
            var contentType = "";
            var responseContent = "";

            char[] splitCharacter = { ',' };
            char[] splitCharacterColon = { ':' };
            char[] splitCharacterCurly = { '}' };
            char[] splitCharacterCommas = { '"' };
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
                    Debug.WriteLine("Status code 0, the underlying connection was closed at " + URL + " " + loop, jSonRequest);
                }

                if (restResponse.StatusCode.ToString().Equals(" InternalServerError"))
                {
                    Debug.WriteLine("Body contains internal server error, hence" + URL + " API is failed, pleaes check your internet connection " + loop, jSonRequest);
                }
                else
                {
                    Debug.WriteLine("Status code not OK," + URL + " API has failed at " + loop + " " + restResponse.StatusCode.ToString(), jSonRequest);
                    Extension.CreateLogFile(loop, jSonRequest, restResponse.Content.ToString(), restResponse.StatusCode.ToString(), apiName, URL);
                }
            }

            contentType = restResponse.ContentType.ToString();
            responseContent = restResponse.Content.ToString();
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

            if (responseContent.ToString().Contains("A PHP Error was encountered"))
            {
                Debug.WriteLine(URL + "API failed as it is not correct json response in content type. A PHP error has encoutered. A PHP error has encoutered " + loop + responseContent.ToString(), jSonRequest);
                Extension.CreateLogFile(loop, jSonRequest, restResponse.Content.ToString(), restResponse.StatusCode.ToString(), apiName, URL);
            }

            if (responseContent.Contains("{\"message\":\"Sync Successful! Please note that changes will not be applied to Live App.You must make changes live from portal in App Placeholders & Ads tab.\"}"))
            {
                Debug.WriteLine(URL + "exeuction successful");
            }
            else
            {
                Extension.CreateLogFile(loop, jSonRequest, restResponse.Content.ToString(), restResponse.StatusCode.ToString(), apiName, URL);
            }

            //Starting key level validations for all key values
            foreach (var item in separatedKeyValues)
            {
                splitValue = item.Split(splitCharacterColon);
                string ttlValue = "{"+"\"" + "cache_ttl" + "\"";
                if (splitValue[0].ToString().Contains(ttlValue))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("\ncache_ttl value appearing as null," + URL + "api is failed", jSonRequest);
                    }
                    else
                    {
                        Debug.WriteLine("\n" + jSonRequest + "\ncache_ttl value is correct");
                    }

                }

                if (splitValue[0].ToString().Contains("cache_ttl_Rewarded"))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("cache_ttl_Rewarded value appearing as null," + URL + "api is failed", jSonRequest);
                    }
                    else
                    {
                        Debug.WriteLine("\ncache_ttl_Rewarded value is correct");
                    }

                }

                if (splitValue[0].ToString().Equals("\"refreshAdRate\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("refreshAdRate value appearing as null," + URL + "api is failed", jSonRequest);
                    }
                    else
                    {
                        Debug.WriteLine("\nRefresh Ad Rate value is correct");
                    }

                }

                //validating appid should not be null
                if (splitValue[0].ToString().Contains("\"appID\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("Appid value appearing as null," + URL + "api is failed", jSonRequest);
                    }
                    else
                    {
                        Debug.WriteLine("\nAppID value is correct");
                    }

                }
                // device id should not be null
                if (splitValue[0].ToString().Contains("\"deviceID\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("Device id value appearing as null," + URL + "api is failed", jSonRequest);
                    }
                    else
                    {
                        Debug.WriteLine("\nDevice ID value is correct");
                    }

                }

                if (splitValue[0].ToString().Contains("\"aspectRatio\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("aspectRatio value appearing as null," + URL + "api is failed", jSonRequest);
                    }
                    else
                    {
                        Debug.WriteLine("\n Aspect ratio value is correct");
                    }

                }

                // Reigon should not be null
                if (splitValue[0].ToString().Contains("\"region\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("Region value appearing as null," + URL + "api is failed", jSonRequest);
                    }
                    else
                    {
                        Debug.WriteLine("\nRegion value is correct");
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
                        Debug.WriteLine("isAM value appearing is not set," + URL + "api is failed", jSonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("\"package\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("package value appearing as null," + URL + "api is failed", jSonRequest);
                    }
                    else
                    {
                        Debug.WriteLine("\nPackage value is correct");
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
                        Debug.WriteLine("Child directed value is not set," + URL + "api is failed", jSonRequest);
                    }
                }


                if (splitValue[0].ToString().Contains("\"title\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("title value appearing as null," + URL + "api is failed", jSonRequest);
                    }
                    else
                    {
                        Debug.WriteLine("\nTitle value is correct");
                    }

                }

                if (splitValue[0].ToString().Contains("\"totalsequences\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("sequences value appearing as null," + URL + "api is failed", jSonRequest);
                    }
                    else
                    {
                        Debug.WriteLine("\nTotal Sequences value is correct");
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
                        Debug.WriteLine("Sequences value is not set," + URL + "api is failed", jSonRequest);
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
                        Debug.WriteLine("Is hide ads value is not set," + URL + "api is failed", jSonRequest);
                    }
                }

                if (splitValue[0].ToString().Contains("\"mode\""))
                {
                    if (splitValue[1].ToString().Contains("Production") || splitValue[1].ToString().Contains("Test"))
                    {
                        Debug.WriteLine("mode value is set");
                    }
                    else
                    {
                        Debug.WriteLine("Mediation value is not set," + URL + "api is failed", jSonRequest);
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
                        Debug.WriteLine("Mediation value is not set," + URL + "api is failed", jSonRequest);
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
                        Debug.WriteLine("Ads mediation queue is not set," + URL + "api is failed", jSonRequest);
                    }
                }

                if (splitValue[0].ToString().Contains("\"asRateUsURL\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("As Rate Us URL is appearing null," + URL + "api is failed", jSonRequest);
                    }
                    else
                    {
                        Debug.WriteLine("\nAs Rate US URL value is correct");
                    }

                }

                if (splitValue[0].ToString().Contains("\"gpRateUsURL\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("gp Rate Us URL is appearing null," + URL + "api is failed", jSonRequest);
                    }
                    else
                    {
                        Debug.WriteLine("\nGP Rate Us URL value is correct");
                    }

                }


                if (splitValue[0].ToString().Contains("\"supportURL\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("Support URL is appearing null," + URL + "api is failed", jSonRequest);
                    }
                    else
                    {
                        Debug.WriteLine("\nSupport URL value is correct");
                    }
                }

                if (splitValue[0].ToString().Contains("\"gpMoreAppsURL\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("Go more Apps URL is appearing null," + URL + "api is failed", jSonRequest);
                    }
                    else
                    {
                        Debug.WriteLine("\nGP More Apps URL value is correct");
                    }

                }

                if (splitValue[0].ToString().Contains("\"amMoreAppsURL\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("AM more Apps URL is appearing null," + URL + "api is failed", jSonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("\"asMoreAppsURL\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("AS more Apps URL is appearing null," + URL + "api is failed", jSonRequest);
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
                        Debug.WriteLine("message value is other then completed," + URL + "api is failed", jSonRequest);
                    }

                }
                if (splitValue[0].ToString().Contains("\"appID\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("Appid value appearing as null," + URL + "api is failed", jSonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("\"rewardCurrency\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("rewardCurrency value appearing as null," + URL + "api is failed", jSonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("\"rewardValue\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("rewardValue value appearing as null," + URL + "api is failed", jSonRequest);
                    }

                }


                if (splitValue[0].ToString().Contains("\"privacyPolicyURL\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("privacyPolicyURL value appearing as null," + URL + "api is failed", jSonRequest);
                    }
                }

                // Reigon should not be null
                if (splitValue[0].ToString().Contains("\"refreshAdRate\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("refreshAdRate value appearing as null," + URL + "api is failed", jSonRequest);
                    }

                }


                if (splitValue[0].ToString().Contains("aspectRatio"))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("aspectRatio value appearing as null," + URL + "api is failed", jSonRequest);
                    }
                }

                if (splitValue[0].ToString().Contains("cdnPath"))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("cdnPath value appearing as null," + URL + "api is failed", jSonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("app_id"))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("app_id value appearing as null," + URL + "api is failed", jSonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("sentry_dsn"))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("sentry_dsn value appearing as null," + URL + "api is failed", jSonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("sentry_log"))
                {
                    if (splitValue[1].ToString().Contains("0") || splitValue[1].ToString().Contains("1"))
                    {
                        Debug.WriteLine("sentry_log value is set");
                    }
                    else
                    {
                        Debug.WriteLine("sentry_log value is not set," + URL + "api is failed", jSonRequest);
                    }
                }

                if (splitValue[0].ToString().Contains("sentry_initialization"))
                {
                    if (splitValue[1].ToString().Contains("0") || splitValue[1].ToString().Contains("1"))
                    {
                        Debug.WriteLine("sentry_initialization value is set");
                    }
                    else
                    {
                        Debug.WriteLine("sentry_initialization value is not set," + URL + "api is failed", jSonRequest);
                    }
                }

                if (splitValue[0].ToString().Contains("sentry_debugging"))
                {
                    if (splitValue[1].ToString().Contains("0") || splitValue[1].ToString().Contains("1"))
                    {
                        Debug.WriteLine("sentry_debugging value is set");
                    }
                    else
                    {
                        Debug.WriteLine("sentry_debugging value is not set," + URL + "api is failed", jSonRequest);
                    }
                }

                if (splitValue[0].ToString().Contains("local_logging"))
                {
                    if (splitValue[1].ToString().Contains("0") || splitValue[1].ToString().Contains("1"))
                    {
                        Debug.WriteLine("local_logging value is set");
                    }
                    else
                    {
                        Debug.WriteLine("local_logging value is not set," + URL + "api is failed", jSonRequest);
                    }
                }

                if (splitValue[0].ToString().Contains("showPreRewardedDialogue"))
                {
                    if (splitValue[1].ToString().Contains("0") || splitValue[1].ToString().Contains("1"))
                    {
                        Debug.WriteLine("showPreRewardedDialogue value is set");
                    }
                    else
                    {
                        Debug.WriteLine("showPreRewardedDialogue value is not set," + URL + "api is failed", jSonRequest);
                    }
                }

                if (splitValue[0].ToString().Contains("showPostRewardedDialogue"))
                {
                    if (splitValue[1].ToString().Contains("0") || splitValue[1].ToString().Contains("1"))
                    {
                        Debug.WriteLine("showPostRewardedDialogue value is set");
                    }
                    else
                    {
                        Debug.WriteLine("showPostRewardedDialogue value is not set," + URL + "api is failed", jSonRequest);
                    }
                }


                if (splitValue[0].ToString().Contains("isSkipable"))
                {
                    if (splitValue[1].ToString().Contains("0") || splitValue[1].ToString().Contains("1"))
                    {
                        Debug.WriteLine("isSkipable value is set\n" + restResponse.Content.ToString());
                    }
                    else
                    {
                        Debug.WriteLine("isSkipable value is not set," + URL + "api is failed", jSonRequest);
                    }
                }
                // session token should not be null
                if (splitValue[0].ToString().Contains("\"sessionToken\""))
                {
                    Debug.WriteLine("Session token value is set\n");
                    string[] curly = splitValue[1].Split(splitCharacterCurly);
                    string[] commas = curly[0].Split(splitCharacterCommas);
                    SessionToken.sessionToken = commas[1];
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("sessionToken value appearing as null," + URL + "api is failed", jSonRequest);
                    }
                }
            }
        }

        public static void ValidateResponseJsonCampaignLogSummaryAPIASessionTokenMainMethod(IRestResponse restResponse, string URL, int loop, string jSonRequest)
        {
            //variable decleration
            var contentType = "";
            var responseContent = "";

            char[] splitCharacter = { ',' };
            char[] splitCharacterColon = { ':' };
            string[] splitValue = null;
            char[] splitCharacterCurlyBracket = { '}' };

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
                    Debug.WriteLine("Status code not OK," + URL + " API has failed at " + loop + " " + restResponse.StatusCode.ToString());
                }
            }

            contentType = restResponse.ContentType.ToString();
            responseContent = restResponse.Content.ToString();
            string[] separatedKeyValues = responseContent.Split(splitCharacter);

            if (responseContent.Contains("\"Page not found\""))
            {
                Debug.WriteLine("Body contains page not found error, hence" + URL + " API is failed " + loop + responseContent.ToString());
            }

            if (responseContent.Contains("\"appJson not found\""))
            {
                Debug.WriteLine("Body contains json not found error, hence" + URL + " API is failed " + loop + responseContent.ToString());
            }

            if (separatedKeyValues[0].ToString().Equals("\"Invalid Request\""))
            {
                Debug.WriteLine("Body contains json not found error, hence" + URL + " API is failed " + loop + responseContent.ToString());
            }

            if (responseContent.ToString().Contains("A PHP Error was encountered"))
            {
                Debug.WriteLine(URL + "API failed as it is not correct json response in content type. A PHP error has encoutered. A PHP error has encoutered " + loop + responseContent.ToString(), jSonRequest);
            }

            if (responseContent.Contains("{\"message\":\"Sync Successful! Please note that changes will not be applied to Live App.You must make changes live from portal in App Placeholders & Ads tab.\"}"))
            {
                Debug.WriteLine(URL + "exeuction successful");
            }

            //Starting key level validations for all key values

            foreach (var item in separatedKeyValues)
            {
                splitValue = item.Split(splitCharacterColon);

                if (splitValue[0].ToString().Contains("\"message\""))
                {
                    if (splitValue[1].ToString().Contains("completed"))
                    {
                        Debug.WriteLine("message value is correct" + jSonRequest);
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
                        Debug.WriteLine("sessionToken value appearing as null," + URL + "api is failed", jSonRequest);

                    }
                }

                if (splitValue[0].ToString().Equals("\"{cache_ttl\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("cache_ttl value appearing as null," + URL + "api is failed", jSonRequest);
                    }

                }

                if (splitValue[0].ToString().Equals("\"refreshAdRate\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("refreshAdRate value appearing as null," + URL + "api is failed", jSonRequest);
                    }

                }

                //validating appid should not be null
                if (splitValue[0].ToString().Contains("\"appID\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("Appid value appearing as null," + URL + "api is failed", jSonRequest);
                    }

                }
                // device id should not be null
                if (splitValue[0].ToString().Contains("\"deviceID\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("Device id value appearing as null," + URL + "api is failed", jSonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("\"aspectRatio\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("aspectRatio value appearing as null," + URL + "api is failed", jSonRequest);
                    }

                }

                // Reigon should not be null
                if (splitValue[0].ToString().Contains("\"region\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("Region value appearing as null," + URL + "api is failed", jSonRequest);
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
                        Debug.WriteLine("isAM value appearing is not set," + URL + "api is failed", jSonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("\"package\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("package value appearing as null," + URL + "api is failed", jSonRequest);
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
                        Debug.WriteLine("Child directed value is not set," + URL + "api is failed", jSonRequest);
                    }
                }


                if (splitValue[0].ToString().Contains("\"title\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("title value appearing as null," + URL + "api is failed", jSonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("\"totalsequences\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("sequences value appearing as null," + URL + "api is failed", jSonRequest);
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
                        Debug.WriteLine("Sequences value is not set," + URL + "api is failed", jSonRequest);
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
                        Debug.WriteLine("Is hide ads value is not set," + URL + "api is failed", jSonRequest);
                    }
                }

                if (splitValue[0].ToString().Contains("\"mode\""))
                {
                    if (splitValue[1].ToString().Contains("Production") || splitValue[1].ToString().Contains("Test"))
                    {
                        Debug.WriteLine("mode value is set");
                    }
                    else
                    {
                        Debug.WriteLine("Mediation value is not set," + URL + "api is failed", jSonRequest);
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
                        Debug.WriteLine("Mediation value is not set," + URL + "api is failed", jSonRequest);
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
                        Debug.WriteLine("Ads mediation queue is not set," + URL + "api is failed", jSonRequest);
                    }
                }

                if (splitValue[0].ToString().Contains("\"asRateUsURL\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("As Rate Us URL is appearing null," + URL + "api is failed", jSonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("\"gpRateUsURL\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("gp Rate Us URL is appearing null," + URL + "api is failed", jSonRequest);
                    }

                }


                if (splitValue[0].ToString().Contains("\"supportURL\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("Support URL is appearing null," + URL + "api is failed", jSonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("\"gpMoreAppsURL\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("Go more Apps URL is appearing null," + URL + "api is failed");
                    }

                }

                if (splitValue[0].ToString().Contains("\"amMoreAppsURL\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("AM more Apps URL is appearing null," + URL + "api is failed", jSonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("\"asMoreAppsURL\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("AS more Apps URL is appearing null," + URL + "api is failed", jSonRequest);
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
                        Debug.WriteLine("message value is other then completed," + URL + "api is failed", jSonRequest);
                    }

                }
                if (splitValue[0].ToString().Contains("\"appID\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("Appid value appearing as null," + URL + "api is failed", jSonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("\"rewardCurrency\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("rewardCurrency value appearing as null," + URL + "api is failed", jSonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("\"rewardValue\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("rewardValue value appearing as null," + URL + "api is failed", jSonRequest);
                    }

                }
                // device id should not be null
                if (splitValue[0].ToString().Contains("\"cache_ttl\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("cache_ttl value appearing as null," + URL + "api is failed", jSonRequest);
                    }
                }

                if (splitValue[0].ToString().Contains("\"privacyPolicyURL\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("privacyPolicyURL value appearing as null," + URL + "api is failed", jSonRequest);
                    }
                }

                // Reigon should not be null
                if (splitValue[0].ToString().Contains("\"refreshAdRate\""))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("refreshAdRate value appearing as null," + URL + "api is failed", jSonRequest);
                    }

                }


                if (splitValue[0].ToString().Contains("aspectRatio"))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("aspectRatio value appearing as null," + URL + "api is failed", jSonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("cdnPath"))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("cdnPath value appearing as null," + URL + "api is failed", jSonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("app_id"))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("app_id value appearing as null," + URL + "api is failed", jSonRequest);
                    }

                }

                if (splitValue[0].ToString().Contains("sentry_dsn"))
                {
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("sentry_dsn value appearing as null," + URL + "api is failed");
                    }

                }

                if (splitValue[0].ToString().Contains("sentry_log"))
                {
                    if (splitValue[1].ToString().Contains("0") || splitValue[1].ToString().Contains("1"))
                    {
                        Debug.WriteLine("sentry_log value is set");
                    }
                    else
                    {
                        Debug.WriteLine("sentry_log value is not set," + URL + "api is failed");
                    }
                }

                if (splitValue[0].ToString().Contains("sentry_initialization"))
                {
                    if (splitValue[1].ToString().Contains("0") || splitValue[1].ToString().Contains("1"))
                    {
                        Debug.WriteLine("sentry_initialization value is set");
                    }
                    else
                    {
                        Debug.WriteLine("sentry_initialization value is not set," + URL + "api is failed", jSonRequest);
                    }
                }

                if (splitValue[0].ToString().Contains("sentry_debugging"))
                {
                    if (splitValue[1].ToString().Contains("0") || splitValue[1].ToString().Contains("1"))
                    {
                        Debug.WriteLine("sentry_debugging value is set");
                    }
                    else
                    {
                        Debug.WriteLine("sentry_debugging value is not set," + URL + "api is failed", jSonRequest);
                    }
                }

                if (splitValue[0].ToString().Contains("local_logging"))
                {
                    if (splitValue[1].ToString().Contains("0") || splitValue[1].ToString().Contains("1"))
                    {
                        Debug.WriteLine("local_logging value is set");
                    }
                    else
                    {
                        Debug.WriteLine("local_logging value is not set," + URL + "api is failed", jSonRequest);
                    }
                }

                if (splitValue[0].ToString().Contains("local_logging"))
                {
                    if (splitValue[1].ToString().Contains("0") || splitValue[1].ToString().Contains("1"))
                    {
                        Debug.WriteLine("local_logging value is set");
                    }
                    else
                    {
                        Debug.WriteLine("local_logging value is not set," + URL + "api is failed", jSonRequest);
                    }
                }

                if (splitValue[0].ToString().Contains("showPreRewardedDialogue"))
                {
                    if (splitValue[1].ToString().Contains("0") || splitValue[1].ToString().Contains("1"))
                    {
                        Debug.WriteLine("showPreRewardedDialogue value is set");
                    }
                    else
                    {
                        Debug.WriteLine("showPreRewardedDialogue value is not set," + URL + "api is failed", jSonRequest);
                    }
                }

                if (splitValue[0].ToString().Contains("showPostRewardedDialogue"))
                {
                    if (splitValue[1].ToString().Contains("0") || splitValue[1].ToString().Contains("1"))
                    {
                        Debug.WriteLine("showPostRewardedDialogue value is set");
                    }
                    else
                    {
                        Debug.WriteLine("showPostRewardedDialogue value is not set," + URL + "api is failed", jSonRequest);
                    }
                }


                if (splitValue[0].ToString().Contains("isSkipable"))
                {
                    if (splitValue[1].ToString().Contains("0") || splitValue[1].ToString().Contains("1"))
                    {
                        Debug.WriteLine("isSkipable value is set");
                       
                    }
                    else
                    {
                        Debug.WriteLine("isSkipable value is not set," + URL + "api is failed", jSonRequest);
                    }
                }
            }
        }
    }
}
