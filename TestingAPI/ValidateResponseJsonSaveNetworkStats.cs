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
using System.Data.OleDb;

namespace Monetization_Automation.Test
{
    public class ValidateResponseJsonSaveNetworkStats
    {

        public ValidateResponseJsonSaveNetworkStats()
        {
        }

        public static void ValidateResponseJsonSaveNetworkStatsMainMethod(IRestResponse restResponse, string URL, int loop, string jsonRequest, string apiName)
        {
            //variable decleration
            var contentType = "";
            var responseContent = "";
            //variable decleratio
            char[] splitCharacter = { ',' };
            char[] splitCharacterColon = { ':' };
            char[] splitCharacterCurlyBracket = { '}' };
            string[] splitValue = null;
            char[] splitCharacterCommas = { '"' };

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
                    Debug.WriteLine("Status code not OK," + URL + " API has failed at " + loop + " " + restResponse.StatusCode.ToString(), jsonRequest);
                    Extension.CreateLogFile(loop, jsonRequest, restResponse.Content.ToString(), restResponse.StatusCode.ToString(), apiName, URL);

                }
            }

            contentType = restResponse.ContentType.ToString();
            responseContent = restResponse.Content.ToString();
            string[] separatedKeyValues = responseContent.Split(splitCharacter);

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

            if (responseContent.ToString().Contains("A PHP Error was encountered"))
            {
                Debug.WriteLine(URL + "API failed as it is not correct json response in content type. A PHP error has encoutered  " + loop + responseContent.ToString(), jsonRequest);
                Extension.CreateLogFile(loop, jsonRequest, restResponse.Content.ToString(), restResponse.StatusCode.ToString(), apiName, URL);

            }

            if (responseContent.Contains("{\"message\":\"Sync Successful! Please note that changes will not be applied to Live App.You must make changes live from portal in App Placeholders & Ads tab.\"}"))
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
                //validating message should not be null
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

                    Debug.WriteLine("Session token value is set\n");
                    string[] curly = splitValue[1].Split(splitCharacterCurlyBracket);
                    string[] commas = curly[0].Split(splitCharacterCommas);
                    SessionToken.sessionToken = commas[1];
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("sessionToken value appearing as null," + URL + "api is failed", jsonRequest);
                    }

                }

            }
        }

        public static void ValidateResponseJsonSaveNetworkStatsSessionTokenMainMethod(IRestResponse restResponse, string URL, int loop, string jSonRequest)
        {
            var contentType = "";
            var responseContent = "";
            //variable decleratio
            char[] splitCharacter = { ',' };
            char[] splitCharacterColon = { ':' };
            char[] splitCharacterCurlyBracket = { '}' };
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
                    Debug.WriteLine("Status code 0, the underlying connection was closed at " + URL + " " + loop);
                }

                if (restResponse.StatusCode.ToString() == "internal server error")
                {
                    Debug.WriteLine("Body contains internal server error, hence" + URL + " API is failed, pleaes check your internet connection " + loop, jSonRequest);
                }
                else
                {
                    Debug.WriteLine("Status code not OK," + URL + " API has failed at " + loop + " " + restResponse.StatusCode.ToString() + restResponse.Content.ToString() + jSonRequest);
                }
            }

            contentType = restResponse.ContentType.ToString();
            responseContent = restResponse.Content.ToString();
            string[] separatedKeyValues = responseContent.Split(splitCharacter);

            if (responseContent.Contains("\"Page not found\""))
            {
                Debug.WriteLine("Body contains page not found error, hence" + URL + " API is failed " + loop + responseContent.ToString() + jSonRequest);
            }

            if (responseContent.Contains("\"appJson not found\""))
            {
                Debug.WriteLine("Body contains json not found error, hence" + URL + " API is failed " + loop + responseContent.ToString() + jSonRequest);
            }

            if (separatedKeyValues[0].ToString().Equals("\"Invalid Request\""))
            {
                Debug.WriteLine("Body contains json not found error, hence" + URL + " API is failed " + loop + responseContent.ToString() + jSonRequest);
            }

            if (responseContent.ToString().Contains("A PHP Error was encountered"))
            {
                Debug.WriteLine(URL + "API failed as it is not correct json response in content type. A PHP error has encoutered  " + loop + responseContent.ToString() + jSonRequest);
            }

            if (responseContent.Contains("{\"message\":\"Sync Successful! Please note that changes will not be applied to Live App.You must make changes live from portal in App Placeholders & Ads tab.\"}"))
            {
                Debug.WriteLine(URL + "exeuction successful");
            }

            //Starting key level validations for all key values
            separatedKeyValues = responseContent.Split(splitCharacter);
            foreach (var item in separatedKeyValues)
            {
                splitValue = item.Split(splitCharacterColon);
                //validating message should not be null
                if (splitValue[0].ToString().Contains("\"message\""))
                {
                    if (splitValue[1].ToString().Contains("completed"))
                    {
                        Debug.WriteLine("message value is correct" + jSonRequest);
                    }
                    else
                    {
                        Debug.WriteLine("Message value not appearing correctly" + loop + responseContent.ToString() + jSonRequest);
                    }

                }
                // session token should not be null
                if (splitValue[0].ToString().Contains("\"sessionToken\""))
                {
                    Debug.WriteLine("Session token value is set\n");
                    string[] curly = splitValue[1].Split(splitCharacterCurlyBracket);
                    string[] commas = curly[0].Split(splitCharacterCommas);
                    SessionToken.sessionToken = commas[1];
                    if (splitValue[1] == null)
                    {
                        Debug.WriteLine("sessionToken value appearing as null," + URL + "api is failed" + loop + responseContent.ToString() + jSonRequest);
                        
                    }
                }
            }
        }
    }
}
