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
using Monetization_Automation.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp.Serialization.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;


namespace Monetization_Automation.Test
{
    [TestClass]
    public class StartNewSessionAPI
    {
        
        public StartNewSessionAPI()
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
                            ExcelUtil.PopulateInCollection(@"/Volumes/DataDisk/Ahmad khisal/Consoliads/Automation_Data/ALLAPI.xlsx", "StartNewSession");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Assert.Fail("Excel file is not loaded successfully" + " " + e.Message);
            }
        }


        [TestMethod]
        public void StartNewSessionMainMethod()
        {
            string jSonbody = Utils.ExcelUtil.ReadData(1, "JSON");
            var client = new RestClient("http://sheeda.consoliads.com/sheeda/admin/analytics/syncUserDevice");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "PHPSESSID=ukk3jhiein0p3fddg824jilep2; ci_session=a%3A5%3A%7Bs%3A10%3A%22session_id%22%3Bs%3A32%3A%2274d3c988d669499a01b875c1bca1abc2%22%3Bs%3A10%3A%22ip_address%22%3Bs%3A14%3A%22182.180.164.20%22%3Bs%3A10%3A%22user_agent%22%3Bs%3A21%3A%22PostmanRuntime%2F7.25.0%22%3Bs%3A13%3A%22last_activity%22%3Bi%3A1591103816%3Bs%3A9%3A%22user_data%22%3Bs%3A0%3A%22%22%3B%7Df63f392ff4facd0290cdfd0e4f42de96acb73f70");
            request.AddParameter("application/json", "{\"package\":\"com.jumbojutt.fps.shooting.strike\",\"store\":\"42\",\"uniqueDeviceID\":\"9030B7C5-1DA3-4D74-9D73-12C236CA4532\",\"appID\":\"15773\",\"deviceID\":\"\",\"region\":\"\",\"adsQueueEventStats\":[{\"adID\":\"0\",\"sceneID\":\"2\",\"request\":\"2\",\"impression\":\"1\",\"click\":\"0\"}],\"gssdkVersion\":\"5.1.3\",\"sdkVersionID\":\"2151\",\"sdkVersion\":\"2151\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request); string URL = "http://sheeda.consoliads.com/sheeda/admin/api/startNewAdSession_2";

            //SendJsonRequest.SendJsonRequestMainMethod(jSonbody, URL);
            
            
        }
    }
}