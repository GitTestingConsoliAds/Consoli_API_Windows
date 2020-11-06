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
    public class SendJsonRequest
    {

        public SendJsonRequest()
        {

        }

        public static void SendJsonRequestSessionTaken(string jSonRequest, string URL, RestRequest restRequest, int loop)
        {
            Thread.Sleep(1000);
            RestClient restClient = new RestClient(URL);
            IRestResponse response = restClient.Execute(restRequest);

            //save network stats API response call session token
            if (URL.Equals("http://sheeda.consoliads.com/sheeda/admin/analytics/saveNetworkStats"))
            {
                ValidateResponseJsonSaveNetworkStats.ValidateResponseJsonSaveNetworkStatsSessionTokenMainMethod(response, URL, loop, jSonRequest);
            }

            //Save new Ad session/ camapign log summary API Json Response Call
            if (URL.Equals("http://sheeda.consoliads.com/sheeda/admin/api/startNewAdSession_2") || URL.Equals("http://sheeda.consoliads.com/sheeda/admin/api_v1/startNewAdSession_2"))
            {
                ValidateResponseJsonCampaignLogSummaryAdSessionAPI.ValidateResponseJsonCampaignLogSummaryAPIASessionTokenMainMethod(response, URL, loop,jSonRequest);
            }

        }
    


        public static void SendJsonRequestMainMethod(string jSonRequest, string URL, RestRequest restRequest, int loop)
        {
            Thread.Sleep(1000);
            RestClient restClient = new RestClient(URL);
            IRestResponse response = restClient.Execute(restRequest);
            // Record Click from Device API Response Call
            if (URL.Equals("http://sheeda.consoliads.com/sheeda/admin/api_v1/recordClicksFromDevice") || URL.Equals("http://sheeda.consoliads.com/sheeda/admin/api/recordClicksFromDevice"))
            {
                ValidateResponseJsonRecordClickFromDevice.ValidateResponseJsonRecordCickFromDevice(response, URL, loop);
            }


            //Record Broad Card Install Validate Json Response Call
            if (URL.Equals("http://sheeda.consoliads.com/sheeda/admin/api/recordBroadCastInstall"))
                 {
                ValidateResponseJsonRecordBraodCardInstall.ValidateResponseJsonValidateResponseJsonRecordBraodCardInstall(response, URL, loop);
                 }

            // Sync User Device Json Response call
            if (URL.Equals("http://sheeda.consoliads.com/sheeda/admin/analytics/syncUserDevice"))
            {
                ValidateResponseJsonSyncUserDevice.ValidateResponseJsonSyncUserDeviceMainMethod(response, URL, loop);
            }

            //Save new Ad session/ camapign log summary API Json Response Call
            if (URL.Equals("http://sheeda.consoliads.com/sheeda/admin/api/startNewAdSession_2") || URL.Equals("http://sheeda.consoliads.com/sheeda/admin/api_v1/startNewAdSession_2"))
            {
                ValidateResponseJsonCampaignLogSummaryAdSessionAPI.ValidateResponseJsonCampaignLogSummaryAPIASessionMainMethod(response, URL, loop);
            }

            //Sync App Native/Unity API Json Response Call
            if (URL.Equals("http://sheeda.consoliads.com/sheeda/admin/json/syncApp"))
            {
                ValidateResponseJsonSyncAppUnityNative.ValidateResponseJsonSyncAppUnityNativeMainMethod(response, URL, loop);
            }

            //save network stats API response call
            if (URL.Equals("http://sheeda.consoliads.com/sheeda/admin/analytics/saveNetworkStats"))
            {
                ValidateResponseJsonSaveNetworkStats.ValidateResponseJsonSaveNetworkStatsMainMethod(response, URL, loop);    
            }
        }
    }
}

