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


namespace Monetization_Automation.Test
{
    [TestClass]
    public class SaveNetworkStats
    {
        public string jSonbody = null;
        public string URL = null;
        public SaveNetworkStats()
        {
            try
            {
                if (ExcelUtil._dataCollection.Count == 0)
                {
                    var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
                    if (directoryInfo != null)
                    {
                        if (directoryInfo.Parent != null)
                        {
                            ExcelUtil.PopulateInCollection(@"/Volumes/DataDisk/Ahmadkhisal/Consoliads/Automation_Data/APIExcels/SaveNetworkStats.xlsx", Monetization_Automation.TestingAPI.Properties.Settings.Default.Environment);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Excel file is not loaded successfully" + " " + e.Message);
            }
        }


        [TestMethod]
        public void SaveNetworkStatsMainMethod()
        {
            int count = DataTableCollectionLoop.tableCollection.Rows.Count;
            foreach (System.Data.DataRow row in DataTableCollectionLoop.tableCollection.Rows)
            {
                jSonbody = Utils.ExcelUtil.ReadData(count, "JSON");
                DataCollectionAPIKeys.URLSDK = Utils.ExcelUtil.ReadData(count, "URL");
                var client = new RestClient(DataCollectionAPIKeys.URLSDK);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("Cookie", "PHPSESSID=ukk3jhiein0p3fddg824jilep2; ci_session=a%3A5%3A%7Bs%3A10%3A%22session_id%22%3Bs%3A32%3A%2274d3c988d669499a01b875c1bca1abc2%22%3Bs%3A10%3A%22ip_address%22%3Bs%3A14%3A%22182.180.164.20%22%3Bs%3A10%3A%22user_agent%22%3Bs%3A21%3A%22PostmanRuntime%2F7.25.0%22%3Bs%3A13%3A%22last_activity%22%3Bi%3A1591103816%3Bs%3A9%3A%22user_data%22%3Bs%3A0%3A%22%22%3B%7Df63f392ff4facd0290cdfd0e4f42de96acb73f70");
                request.AddParameter("appJson", jSonbody);
                Thread.Sleep(5000);
                SendJsonRequest.SendJsonRequestMainMethod(jSonbody, DataCollectionAPIKeys.URLSDK, request, count, "SaveNetworkStats");
                count = count - 1;
            }
        }

        //[TestMethod]
        public void SaveNetworkStatsReuseTokenSendtoDBMainMethod()
        {
            int count = DataTableCollectionLoop.tableCollection.Rows.Count;
            foreach (System.Data.DataRow row in DataTableCollectionLoop.tableCollection.Rows)
            {
                jSonbody = Utils.ExcelUtil.ReadData(count, "JSON");
                URL = Utils.ExcelUtil.ReadData(count, "URL");
                var client = new RestClient(URL);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("Cookie", "PHPSESSID=ukk3jhiein0p3fddg824jilep2; ci_session=a%3A5%3A%7Bs%3A10%3A%22session_id%22%3Bs%3A32%3A%2274d3c988d669499a01b875c1bca1abc2%22%3Bs%3A10%3A%22ip_address%22%3Bs%3A14%3A%22182.180.164.20%22%3Bs%3A10%3A%22user_agent%22%3Bs%3A21%3A%22PostmanRuntime%2F7.25.0%22%3Bs%3A13%3A%22last_activity%22%3Bi%3A1591103816%3Bs%3A9%3A%22user_data%22%3Bs%3A0%3A%22%22%3B%7Df63f392ff4facd0290cdfd0e4f42de96acb73f70");
                request.AddParameter("appJson", jSonbody);
                SendJsonRequest.SendJsonRequestSessionTaken(jSonbody, URL, request, count);

                //Replacing the sesssion token in json to put the request again
                char[] splitCharacterComma = { ',' };
                string[] jsonParsingAppendSessionToken = jSonbody.Split(splitCharacterComma);
                int totalLength = 0;
                int length = jsonParsingAppendSessionToken.GetLength(totalLength);
                char[] splitCharacterColon = { ':' };
                char[] splitCharacterCurly = { '}' };
                string[] finalSessionToken = null;
                int counter = 0;
                string newJsonRequestSessionTokenReplaced = null;
                string[] curly = null;
                string replacedSessionToken = null;
                foreach (var itemparse in jsonParsingAppendSessionToken)
                {
                    
                    counter = counter + 1;
                    if (itemparse.Contains("sessionToken"))
                    {
                        curly = SessionToken.sessionToken.Split(splitCharacterCurly);
                        finalSessionToken = itemparse.Split(splitCharacterColon);
                        replacedSessionToken = newJsonRequestSessionTokenReplaced + finalSessionToken[0] + ":" + curly[0];
                        newJsonRequestSessionTokenReplaced = replacedSessionToken;
                        int itemParseCount = itemparse.Length;
                        Thread.Sleep(1000);
                        if (counter < length)
                        {
                            newJsonRequestSessionTokenReplaced = newJsonRequestSessionTokenReplaced + ",";
                        }
                    }
                    else
                    {
                        newJsonRequestSessionTokenReplaced = newJsonRequestSessionTokenReplaced + itemparse + ",";
                    }
                }
                newJsonRequestSessionTokenReplaced = newJsonRequestSessionTokenReplaced + "}";
                SendJsonRequest.SendJsonRequestSessionTaken(newJsonRequestSessionTokenReplaced, URL, request, count);
                count = count - 1;
            }
        }
    }
}