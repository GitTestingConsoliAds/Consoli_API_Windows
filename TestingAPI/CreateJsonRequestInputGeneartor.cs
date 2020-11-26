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
using System.Data;
using System.Text;
using Ionic.Zip;
using Ionic.Zlib;
using System.IO.Compression;

namespace Monetization_Automation.Test
{
    public class CreateJsonRequestInputGenerator
    {
        public static List<DataCollectionAPIKeys> _dataCollectionAPIKeys = new List<DataCollectionAPIKeys>();

        public CreateJsonRequestInputGenerator()
        {

        }

        private static void AddEntry(string fileName, byte[] fileContent, ZipArchive archive)
        {
            var entry = archive.CreateEntry(fileName);
            using (var stream = entry.Open())
            stream.Write(fileContent, 0, fileContent.Length);

        }

        public static void CreateJson(DataSet[] dataSet, string apiName)
        {
            int counter = 0;
            string jsonBody = null;
            DataCollectionAPIKeys item = new DataCollectionAPIKeys();
            if (apiName.Equals("CampaignLogSummary"))
            {
                SessionToken.sessionToken = "vhRDZd5MK+4lIH8X/mNh9q3AWhAF6oQpsHzkSChxcELpqKx6kQxk3RdbBSxO0WdoUKHfmD2btxDbDaOk1jW1lp611h8JJ+FxRc8NQutzk0X7xTjJHvfIKjdMGZMp8Lax";
                foreach (var row in dataSet[0].Tables[0].Rows)
                {
                    string appKey = (dataSet[0].Tables[0].Rows[counter][0].ToString());
                    string campaignID = (dataSet[0].Tables[0].Rows[counter][1].ToString());
                    string packageID = (dataSet[0].Tables[0].Rows[counter][2].ToString());
                    string sdkVersion = (dataSet[0].Tables[0].Rows[counter][3].ToString());
                    item.deviceID = Extension.randomNumberGenerator(1, 10000);
                    item.sceneID = Extension.randomNumberGenerator(1, 26);
                    jsonBody = "{" + "\"appKey\"" + ":" + "\"" + appKey + "\"," + "\"deviceID\"" + ":" + "\"" + item.deviceID + "\"" + "," +
                        "\"stats\"" + ":" + "[{" + "\"campaignID\"" + ":" + campaignID + "," + "\"packageID\"" + ":" + "\"" + packageID + "\"" + "," + "\"sceneStats\"" + ":" +
                        "[{" + "\"sceneID\"" + ":" + "\"" + item.sceneID + "\"," + "\"eventStats\"" + ":" + "\"" + Extension.randomNumberGenerator(301, 400) + "," +
                        Extension.randomNumberGenerator(201, 300) + "," + Extension.randomNumberGenerator(101, 200) + "," + Extension.randomNumberGenerator(1, 100) + "," +
                        Extension.randomNumberGenerator(1, 100) + "," + Extension.randomNumberGenerator(0, 1) + "," + Extension.randomNumberGenerator(0, 1) + "\"" + "}]}]," +
                        "\"sdkVersion\"" + ":" + "\"" + sdkVersion + "\"," + "\"sessionToken\"" + ":" + "\"" + SessionToken.sessionToken + "\"" + "}";
                    jsonBody = jsonBody.Trim();
                    jsonBody = jsonBody.TrimEnd();
                    jsonBody = jsonBody.TrimStart();
                    Byte[] inputBytes = Extension.GZipJsonString(jsonBody);
                    int sdkVersion1 = Convert.ToInt32(sdkVersion);
                    //request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "text/plain");
                    request.AddHeader("Cookie", "PHPSESSID=lvvrnj3qmat8gnrvdqqupalqb3; ci_session=a%3A5%3A%7Bs%3A10%3A%22session_id%22%3Bs%3A32%3A%22697c0bd027499da7d9cbef8882dd2251%22%3Bs%3A10%3A%22ip_address%22%3Bs%3A14%3A%22182.180.164.20%22%3Bs%3A10%3A%22user_agent%22%3Bs%3A21%3A%22PostmanRuntime%2F7.25.0%22%3Bs%3A13%3A%22last_activity%22%3Bi%3A1595916909%3Bs%3A9%3A%22user_data%22%3Bs%3A0%3A%22%22%3B%7Dd525c055394d6d7f26655c212dc08ce83f625ec3");
                    request.AddParameter("text/plain", inputBytes, ParameterType.RequestBody);
                    if (sdkVersion1 >= 22)
                    {
                        DataCollectionAPIKeys.URLSDK = "http://52.13.174.6/" + DataCollectionAPIKeys.branchName + "/admin/api/startNewAdSession_2";
                    }
                     else if (sdkVersion1 <= 21)
                    {
                        DataCollectionAPIKeys.URLSDK = "http://52.13.174.6/" + DataCollectionAPIKeys.branchName + "/admin/api_v1/startNewAdSession_2";
                    }
                    SendJsonRequest.SendJsonRequestMainMethod(jsonBody, DataCollectionAPIKeys.URLSDK, request, counter, apiName);
                    counter = counter + 1;
                }
            }

            if (apiName.Equals("SyncAppNative"))
            {
                int variable = 0;
                foreach (var row in dataSet[0].Tables[0].Rows)
                {
                    string package = (dataSet[0].Tables[0].Rows[counter][0].ToString());
                    string osID = (dataSet[0].Tables[0].Rows[counter][1].ToString());
                    string companyID = (dataSet[0].Tables[0].Rows[counter][2].ToString());
                    string userSignature = (dataSet[0].Tables[0].Rows[counter][3].ToString());
                    string sdkID = (dataSet[1].Tables[0].Rows[variable][0].ToString());
                    string ver = (dataSet[1].Tables[0].Rows[variable][1].ToString());
                    string adID = (dataSet[2].Tables[0].Rows[variable][0].ToString());
                    string failOverAdIDBanner = (dataSet[3].Tables[0].Rows[variable][0].ToString());
                    string failOverAdIDNative = (dataSet[4].Tables[0].Rows[variable][0].ToString());
                    string failOverAdIDRewardedVideo = (dataSet[5].Tables[0].Rows[variable][0].ToString());
                    int isAdavailable = 1;
                    var iconid = 59;
                    jsonBody = "{" + "\"package\"" + ":" + "\"" + package + "\"," + "\"store\"" + ":" + "\"" + osID + "\"," +
                        "\"sdkVersionID\"" + ":" + "\"" + sdkID + "\"" + "," + "\"nativeApp\"" + ":" + "\"" + 1 + "\"," + "\"sdkVersion\"" +
                        ":" + "\"" + sdkID + "\"," + "\"appAdnetwork\"" + ":" + "[{" + "\"adID\"" + ":" + "\"" + adID + "\"," +
                        "\"isAdavailable\"" + ":" + "\"" + isAdavailable + "\"" + "}," + "{" + "\"adID\"" + ":" + "\"" +
                        failOverAdIDBanner + "\"," + "\"isAdavailable\"" + ":" + "\"" + isAdavailable + "\"" + "}," + "{" + "\"adID\"" + ":"
                        + "\"" + failOverAdIDNative + "\"," + "\"isAdavailable\"" + ":" + "\"" + isAdavailable + "\"" + "}," +
                        "{" + "\"adID\"" + ":" + "\"" + failOverAdIDRewardedVideo + "\"," + "\"isAdavailable\"" + ":" + "\"" + isAdavailable + "\"" + "},"
                        + "{" + "\"adID\"" + ":" + "\"" + iconid + "\"," + "\"isAdavailable\"" + ":" + "\"" + isAdavailable + "\"" + "}],"
                        + "\"userSignature\"" + ":" + "\"" + userSignature + "\"," + "\"gssdkVersion\"" + ":" + "\"" + ver + "\"" + "}";
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    //request.AddHeader("Content-Encoding", "gzip");
                    //request.AddHeader("Accept-Encoding", "gzip");
                    request.AddHeader("Cookie", "PHPSESSID=kio0no2hop8uoojregmr5hbut2; ci_session=a%3A5%3A%7Bs%3A10%3A%22session_id%22%3Bs%3A32%3A%224157e02b65e9b33c77372f98636e5f57%22%3Bs%3A10%3A%22ip_address%22%3Bs%3A14%3A%22182.180.164.20%22%3Bs%3A10%3A%22user_agent%22%3Bs%3A21%3A%22PostmanRuntime%2F7.25.0%22%3Bs%3A13%3A%22last_activity%22%3Bi%3A1595588385%3Bs%3A9%3A%22user_data%22%3Bs%3A0%3A%22%22%3B%7Dc65147d8fb27fc5ad681ca3f2110857e0287a65c");
                    //Byte[] inputBytes = Extension.GZipJsonString(jsonBody);
                    request.AddParameter("appJson", jsonBody);
                    SendJsonRequest.SendJsonRequestMainMethod(jsonBody, DataCollectionAPIKeys.URLSDK, request, counter, apiName);
                    counter = counter + 1;
                    variable = variable + 1;
                    if (variable == 9)
                    {
                        variable = 0;
                    }
                }
            }


            if (apiName.Equals("StartNewAdSession"))
            {
                string[] product, osVersion, model, telco_operator, brand, architecture, resolution, ram;
                var Rand = 0;
                product = new string[9] { "j7xeltexx", "mido", "STK-L21M", "aosp_arm", "potter", "iPhone", "LUA-U22", "BKK-LX2", "a7y18ltedx" };
                osVersion = new string[9] { "9.0", "8.0", "7.0", "6.0", "5.0", "4.4", "4.3", "4.2", "4.1" };
                model = new string[9] { "GT-Nnnn0", "SM-Nnn0", "GT-Snnn0", "GT-Snnn2", "xiaomi Mi 4s", "E5673", "E3372", "iphone 3GS", "iphone 6" };
                telco_operator = new string[9] { "", "Azercell", "Oi", "Maroc", "Telecom", "airtel", "Metfone", " Ooredoo", "DIGICEL" };
                brand = new string[8] { "HUAWEI", "Xiaomi", "HONOR", "vivo", "samsung", "realme", "iPhone", "OPPO" };
                architecture = new string[9] { "", "0", "aarch64", "armv8l", "armv7l,", "aarch81", "aarch81", "armv64", "armv90" };
                resolution = new string[9] { "1280x720", "1196x720", "1436x720", "1344x720", "1024x552", "1387x720", "1080 x 1920", "1440 x 2960", "480 x 853" };
                ram = new string[9] { "953,45 MB", "3.51 GB ", "1.77 GB", "1.68 GB", "3.68 GB", "2.75 GB", "1,37 GB", "3.69 GB", "" };
                var internet_connectivity = Extension.randomNumberGenerator(0, 1);
                if (internet_connectivity == 0)
                {
                }
                else
                {
                }

                var cpu = Extension.randomNumberGenerator(0, 1);
                if (cpu == 0)
                {
                }
                else
                {
                }

                var orientation = Extension.randomNumberGenerator(0, 1);
                if (orientation == 0)
                {
                }
                else
                {
                }

                foreach (var row in dataSet[0].Tables[0].Rows)
                {
                    string appKey = (dataSet[0].Tables[0].Rows[counter][0].ToString());
                    int sdkID = Convert.ToInt32(dataSet[0].Tables[0].Rows[counter][1]);
                    jsonBody = "{" + "\"appKey\"" + ":" + "\"" + appKey + "\"," + "\"osVersion\"" + ":" + "\"" + osVersion[Rand]
                        + "\"," + "\"orientation\"" + ":" + "\"" + orientation + "\"," + "\"is_mac\"" + ":" + "\"" +
                        Extension.randomNumberGenerator(0, 1) + "\"," + "\"model\"" + ":" + "\"" + model[Rand] + "\"," +
                        "\"deviceID\"" + ":" + "\"" + Extension.randomNumberGenerator(0, 1000) + "\"," + "\"telco_operator\""
                        + ":" + "\"" + telco_operator[Rand] + "\"," + "\"cpu\"" + ":" + "\"" + cpu + "\"," + "\"brand\"" + ":" +
                        "\"" + brand[Rand] + "\"," + "\"userConsent\"" + ":" + "\"" + Extension.randomNumberGenerator(0, 1) +
                        "\"," + "\"internet_connectivity\"" + ":" + "\"" + internet_connectivity + "\"," + "\"height\"" + ":" +
                        "\"" + Extension.randomNumberGenerator(100, 500) + "\"," + "\"architecture\"" + ":" + "\"" +
                        architecture[Rand] + "\"," + "\"width\"" + ":" + "\"" + Extension.randomNumberGenerator(100, 500) + "\","
                        + "\"resolution\"" + ":" + "\"" + resolution[Rand] + "\"," + "\"ram\"" + ":" + "\"" + ram[Rand] + "\","
                        + "\"sdkVersion\"" + ":" + "\"" + sdkID + "\"," + "\"product\"" + ":" + "\"" + product[Rand] + "\","
                        + "\"devMode\"" + ":" + "\"" + Extension.randomNumberGenerator(0, 1) + "\"" + "}";
                    jsonBody = jsonBody.Trim();
                    jsonBody = jsonBody.TrimEnd();
                    jsonBody = jsonBody.TrimStart();
                    Byte[] inputBytes = Extension.GZipJsonString(jsonBody);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Cookie", "PHPSESSID=lvvrnj3qmat8gnrvdqqupalqb3; ci_session=a%3A5%3A%7Bs%3A10%3A%22session_id%22%3Bs%3A32%3A%22078c6b0e112ea610a7f8ea62d27b7149%22%3Bs%3A10%3A%22ip_address%22%3Bs%3A14%3A%22182.180.164.20%22%3Bs%3A10%3A%22user_agent%22%3Bs%3A21%3A%22PostmanRuntime%2F7.25.0%22%3Bs%3A13%3A%22last_activity%22%3Bi%3A1595848468%3Bs%3A9%3A%22user_data%22%3Bs%3A0%3A%22%22%3B%7D1fb222d14774fde04c86b33836e3d5248615be5b");

                    request.AddParameter("application/json", inputBytes, ParameterType.RequestBody);
                    if (sdkID >= 22)
                    {
                        DataCollectionAPIKeys.URLSDK = "http://52.13.174.6/" + DataCollectionAPIKeys.branchName + "/admin/api/startNewAdSession_2";
                    }
                    else if(sdkID <= 21)
                    {
                        DataCollectionAPIKeys.URLSDK = "http://52.13.174.6/" + DataCollectionAPIKeys.branchName + "/admin/api_v1/startNewAdSession_2";
                    }
                    SendJsonRequest.SendJsonRequestMainMethod(jsonBody, DataCollectionAPIKeys.URLSDK, request, counter, apiName);
                    counter = counter + 1;
                    Rand = Rand + 1;
                    if (Rand == 8)
                    {
                        Rand = 0;
                    }
                }
            }

                if (apiName.Equals("RecordClicksFromDevice"))
                {
                    
                    foreach (var row in dataSet[0].Tables[0].Rows)
                    {
                        string campaignID = (dataSet[0].Tables[0].Rows[counter][0].ToString());
                        string appID = (dataSet[0].Tables[0].Rows[counter][1].ToString());
                        int sdkID = Convert.ToInt32((dataSet[0].Tables[0].Rows[counter][2]));
                        string companyID = (dataSet[0].Tables[0].Rows[counter][3].ToString());
                        string osID = (dataSet[0].Tables[0].Rows[counter][4].ToString());
                        string appKey = (dataSet[1].Tables[0].Rows[counter][0].ToString());
                        string appID1 = (dataSet[1].Tables[0].Rows[counter][1].ToString());
                        string advertising_id = Extension.randomNumberGenerator(1, 10000).ToString();
                        //  item.sceneID = Extension.randomNumberGenerator(1, 26);
                        string ClickID = appID1 + "_" + companyID + "_" + advertising_id;
                        jsonBody = "{" + "\"appKey\"" + ":" + "\"" + appKey + "\"," + "\" appToPromoteID\"" + ":" + "\"" + appID + "\"" + "," + "\"sceneID\"" + ":" + "\"" + Extension.randomNumberGenerator(1, 26) + "\"," + "\"campaignID\"" + ":" + "\"" + campaignID + "\"," + "\"advertising_id\"" + ":" + "\"" + advertising_id + "\"," + "\"osID \"" + ":" + "\"" + osID + "\"," + "\"sha1_advertising_id\"" + ":" + "\"" + advertising_id + "\"," + "\"clickID\"" + ":" + "\"" + ClickID + "\"," + "\"sdkVersion\"" + ":" + "\"" + sdkID + "\"," + "\"sessionToken\"" + ":" + "\"" + ExcelUtil.ReadData(1, "sessionToken") + "\"" + "}";
                    jsonBody = jsonBody.Trim();
                    jsonBody = jsonBody.TrimEnd();
                    jsonBody = jsonBody.TrimStart();
                    Byte[] inputBytes = Extension.GZipJsonString(jsonBody);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Cookie", "PHPSESSID=ukk3jhiein0p3fddg824jilep2; ci_session=a%3A5%3A%7Bs%3A10%3A%22session_id%22%3Bs%3A32%3A%2274d3c988d669499a01b875c1bca1abc2%22%3Bs%3A10%3A%22ip_address%22%3Bs%3A14%3A%22182.180.164.20%22%3Bs%3A10%3A%22user_agent%22%3Bs%3A21%3A%22PostmanRuntime%2F7.25.0%22%3Bs%3A13%3A%22last_activity%22%3Bi%3A1591103816%3Bs%3A9%3A%22user_data%22%3Bs%3A0%3A%22%22%3B%7Df63f392ff4facd0290cdfd0e4f42de96acb73f70");
                    request.AddParameter("application/json", inputBytes, ParameterType.RequestBody);

                    if (sdkID >= 22)
                    {
                        DataCollectionAPIKeys.URLSDK = "http://52.13.174.6/" + DataCollectionAPIKeys.branchName + "/admin/api/recordClicksFromDevice";
                    }
                    else if (sdkID <= 21)
                    {
                        DataCollectionAPIKeys.URLSDK = "http://52.13.174.6/" + DataCollectionAPIKeys.branchName + "/admin/api_v1/recordClicksFromDevice";
                    }
                    SendJsonRequest.SendJsonRequestMainMethod(jsonBody, DataCollectionAPIKeys.URLSDK, request, counter,apiName);
                    counter = counter + 1;
                    }
                }

            if (apiName.Equals("SyncAppUnity"))
            {
                int variable = 0;
                foreach (var row in dataSet[0].Tables[0].Rows)
                {
                    string package = (dataSet[0].Tables[0].Rows[counter][0].ToString());
                    string title = (dataSet[0].Tables[0].Rows[counter][1].ToString());
                    string companyID = (dataSet[0].Tables[0].Rows[counter][2].ToString());
                    string osID = (dataSet[0].Tables[0].Rows[counter][3].ToString());
                    string userSignature = (dataSet[0].Tables[0].Rows[counter][4].ToString());
                    string adID = (dataSet[1].Tables[0].Rows[variable][0].ToString());
                    string failOverAdID = (dataSet[2].Tables[0].Rows[variable][0].ToString());
                    string failOverAdIDBanner = (dataSet[3].Tables[0].Rows[variable][0].ToString());
                    string failOverAdIDNative = (dataSet[4].Tables[0].Rows[variable][0].ToString());
                    string failOverAdIDRewardedVideo = (dataSet[5].Tables[0].Rows[variable][0].ToString());
                    string sdkID = (dataSet[6].Tables[0].Rows[variable][0].ToString());
                    string ver = (dataSet[6].Tables[0].Rows[variable][1].ToString());
                    //string failOverAdIDBanner = (dataSet[2].Tables[0].Rows[counter][0].ToString());
                    // string[] adsQueueType = new string[] { "priority", "roundrobin" };
                    var iconid = 59;
                    item.totalSequences = 1;
                    var isAdavailable = 1;
                    var adsQueueType = Extension.randomNumberGenerator(0, 1);
                    string adsqueue = null;
                    bool isfirst = true;
                    bool skipRewarded = true;
                    bool bannerEnable = true;
                    bool nativeEnable = true;
                    bool enable = true;
                    if (adsQueueType == 0)
                    {
                        adsqueue = "priority";
                    }
                    else
                    {
                        adsqueue = "roundrobin";
                    }
                    var isFirstSkip = Extension.randomNumberGenerator(0, 1);
                    if (isFirstSkip == 0)
                    {
                        isfirst = false;
                    }
                    if (isFirstSkip == 1)
                    {
                        isfirst = true;
                    }
                    var isFirstSkipRewardedVideo = Extension.randomNumberGenerator(0, 1);
                    if (isFirstSkipRewardedVideo == 0)
                    {
                        skipRewarded = false;
                    }
                    if (isFirstSkip == 1)
                    {
                        skipRewarded = true;
                    }
                    var bannerEnabled = Extension.randomNumberGenerator(0, 1);
                    if (bannerEnabled == 0)
                    {
                        bannerEnable = false;
                    }
                    if (isFirstSkip == 1)
                    {
                        bannerEnable = true;
                    }
                    var nativeEnabled = Extension.randomNumberGenerator(0, 1);
                    if (nativeEnabled == 0)
                    {
                        nativeEnable = false;
                    }
                    if (isFirstSkip == 1)
                    {
                        nativeEnable = true;
                    }
                    var enabled = Extension.randomNumberGenerator(0, 1);
                    if (enabled == 0)
                    {
                        enable = false;
                    }
                    if (isFirstSkip == 1)
                    {
                        enable = true;
                    }
                    jsonBody = "{" + "\"package\"" + ":" + "\"" + package + "\"," + "\"title\"" + ":" + "\"" + title + "\"," +
                        "\"gssdkVersion\"" + ":" + "\"" + ver + "\"" + "," + "\"sdkVersionID\"" + ":" + "\"" + sdkID + "\"" + ","
                        + "\"sdkVersion\"" + ":" + "\"" + sdkID + "\"" + "," + "\"userSignature\"" + ":" + "\"" + userSignature + "\""
                        + "," + "\"totalSequences\"" + ":" + "\"" + item.totalSequences + "\"" + "," + "\"adsQueueType\"" + ":" + "\""
                        + adsqueue + "\"" + "," + "\"gpRateUsURL\"" + ":" + "\"" + "\"," + "\"isHideAds\"" + ":" + "\"" +
                        Extension.randomNumberGenerator(0, 1) + "\"" + "," + "\"childDirected\"" + ":" + "\"" +
                        Extension.randomNumberGenerator(1, 1) + "\"" + "," + "\"mediationLog\"" + ":" + "\"" + Extension.randomNumberGenerator(0, 1) + "\"" + "," + "\"store\"" + ":" + "\"" + osID + "\"" + ","
                        + "\"sequences\"" + ":" + "[{" + "\"seqTitleID\"" + ":" + "\"" + Extension.randomNumberGenerator(1, 26)
                        + "\"" + "," + "\"isFirstSkip\"" + ":" + "\"" + isfirst + "\"" + "," + "\"failOverAdID\"" + ":" + "\"" +
                        failOverAdID + "\"" + "," + "\" isFirstSkipRewardedVideo\"" + ":" + "\"" + skipRewarded + "\"" + "," + "\" " +
                        "bannerEnabled\"" + ":" + "\"" + bannerEnable + "\"" + "," + "\"bannerPosition\"" + ":" + "\"" +
                        Extension.randomNumberGenerator(1, 6) + "\"" + "," + "\"failOverAdIDBanner\"" + ":" + "\"" +
                        failOverAdIDBanner + "\"" + "," + "\"failOverAdIDNative\"" + ":" + "\"" + failOverAdIDNative + "\"" + ","
                        + "\"failOverAdIDRewardedVideo\"" + ":" + "\"" + failOverAdIDRewardedVideo + "\"" + "," + "\"nativeEnabled\""
                        + ":" + "\"" + nativeEnable + "\"" + "," + "\"nativeWidth\"" + ":" + "\"" + Extension.randomNumberGenerator(100, 300)
                        + "\"" + "," + "\"nativeHeight\"" + ":" + "\"" + Extension.randomNumberGenerator(100, 300) + "\"" + ","
                        + "\"interstitialAndVideo\"" + ":" + "[{" + "\"adID\"" + ":" + "\"" + failOverAdID + "\"" + "," + "\"adOrder\""
                        + ":" + "\"" + Extension.randomNumberGenerator(1, 10) + "\"" + "}]," + "\"rewardedVideo\"" + ":" + "[{" + "\"adID\"" + ":"
                        + "\"" + failOverAdIDRewardedVideo + "\"" + "," + "\"adOrder\"" + ":" + "\"" + Extension.randomNumberGenerator(1, 10) + "\"" + "}],"
                        + "\"native\"" + ":" + "[{" + "\"adID\"" + ":" + "\"" + failOverAdIDNative + "\"" + "," + "\"adOrder\"" + ":"
                        + "\"" + Extension.randomNumberGenerator(1, 10) + "\"" + "}]," + "\"banner\"" + ":" + "[{" + "\"adID\"" + ":"
                        + "\"" + failOverAdIDBanner + "\"" + "," + "\"adOrder\"" + ":" + "\"" + Extension.randomNumberGenerator(1, 10)
                        + "\"" + "}]," + "\"icon\"" + ":" + "{" + "\"enabled\"" + ":" + "\"" + enable + "\"" + "," + "\"adID\"" + ":" + "\""
                        + iconid + "\"" + "," + "\"size\"" + ":" + "\"" + Extension.randomNumberGenerator(1, 3) + "\"" + "}}],"
                        + "\"appAdnetwork\"" + ":" + "[{" + "\"adID\"" + ":" + "\"" + failOverAdID + "\"" + "," + "\"isAdavailable\"" + ":"
                        + "\"" + isAdavailable + "\"" + "}," + "{" + "\"adID\"" + ":" + "\"" + failOverAdIDBanner + "\"" + "," + "\"isAdavailable\""
                        + ":" + "\"" + isAdavailable + "\"" + "}," + "{" + "\"adID\"" + ":" + "\"" + failOverAdIDNative + "\"" + "," + "\"isAdavailable\""
                        + ":" + "\"" + isAdavailable + "\"" + "}," + "{" + "\"adID\"" + ":" + "\"" + failOverAdIDRewardedVideo + "\"" + ","
                        + "\"isAdavailable\"" + ":" + "\"" + isAdavailable + "\"" + "}," + "{" + "\"adID\"" + ":" + "\"" + iconid + "\"" + ","
                        + "\"isAdavailable\"" + ":" + "\"" + isAdavailable + "\"" + "}]}";
                    //Byte[] inputBytes = Extension.GZipJsonString(jsonBody);
                    jsonBody = jsonBody.Trim();
                    jsonBody = jsonBody.TrimEnd();
                    jsonBody = jsonBody.TrimStart();
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddHeader("Cookie", "PHPSESSID=36v04t3lr80mu2vafteha92ir5; ci_session=a%3A5%3A%7Bs%3A10%3A%22session_id%22%3Bs%3A32%3A%2269304cc027e019004a45ebb080b5f2b8%22%3Bs%3A10%3A%22ip_address%22%3Bs%3A14%3A%22182.180.164.20%22%3Bs%3A10%3A%22user_agent%22%3Bs%3A21%3A%22PostmanRuntime%2F7.25.0%22%3Bs%3A13%3A%22last_activity%22%3Bi%3A1591874862%3Bs%3A9%3A%22user_data%22%3Bs%3A0%3A%22%22%3B%7D61bbe05f1cb8ab900ec2b5dfd7a84788382126de");
                    request.AddParameter("appJson", jsonBody);
                    DataCollectionAPIKeys.URLSDK = ExcelUtil.ReadData(5, "URL");
                    SendJsonRequest.SendJsonRequestMainMethod(jsonBody, ExcelUtil.ReadData(5, "URL"), request, counter, apiName);
                    counter = counter + 1;
                    variable = variable + 1;
                    if (variable == 9)
                    {
                        variable = 0;
                    }
                }
            }



            if (apiName.Equals("SaveNetworkStats"))
                {
                int variable = 0;
                //devmode code
                SessionToken.sessionToken = ExcelUtil.ReadData(3, "sessionToken");
                    foreach (var row in dataSet[0].Tables[0].Rows)
                    {
                        string package = (dataSet[0].Tables[0].Rows[counter][0].ToString());
                        string osID = (dataSet[0].Tables[0].Rows[counter][1].ToString());
                        string appID = (dataSet[0].Tables[0].Rows[counter][2].ToString());
                        string companyID = (dataSet[0].Tables[0].Rows[counter][3].ToString());
                        string sdkID = (dataSet[0].Tables[0].Rows[counter][4].ToString());
                        string ver = (dataSet[0].Tables[0].Rows[counter][5].ToString());
                        string adID = (dataSet[1].Tables[0].Rows[counter][0].ToString());
                        item.uniqueDeviceID = Extension.randomNumberGenerator(1, 10000);
                        item.sceneID = Extension.randomNumberGenerator(1, 26);

                        jsonBody = "{" + "\"package\"" + ":" + "\"" + package + "\"," + "\"store\"" + ":" + "\"" + osID + "\"," + "\"uniqueDeviceID\"" + ":" + "\"" + item.uniqueDeviceID + "\"" + "," + "\"appID\"" + ":" + "\"" + "\"," + "\"deviceID\"" + ":" + "\"" + "\"," + "\"region\"" + ":" + "\"" + "\"," + "\"adsQueueEventStats\"" + ":" + "[{" + "\"adID\"" + ":" + "\"" + adID + "\"" + "," + "\"sceneID\"" + ":" + "\"" + item.sceneID + "\"" + "," + "\"request\"" + ":" + "\"" + Extension.randomNumberGenerator(1, 1000) + "\"" + "," + "\"impression\"" + ":" + "\"" + Extension.randomNumberGenerator(1, 1000) + "\"" + "," + "\"click\"" + ":" + "\"" + Extension.randomNumberGenerator(1, 1000) + "\"" + "}]," + "\"gssdkVersion\"" + ":" + "\"" + ver + "\"," + "\"sdkVersionID\"" + ":" + "\"" + sdkID + "\"," + "\"sdkVersion\"" + ":" + "\"" + sdkID + "\"," + "\"sessionToken\"" + ":" + "\"" + SessionToken.sessionToken + "\"" + "}";
                       // Byte[] inputBytes = Extension.GZipJsonString(jsonBody);
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                        request.AddHeader("Cookie", "PHPSESSID=6jbdoaunm5fct788b2pvkl14q0; ci_session=a%3A5%3A%7Bs%3A10%3A%22session_id%22%3Bs%3A32%3A%22fd4a87e7ea735684cc81fc8e6ca28ec9%22%3Bs%3A10%3A%22ip_address%22%3Bs%3A14%3A%22182.180.164.20%22%3Bs%3A10%3A%22user_agent%22%3Bs%3A21%3A%22PostmanRuntime%2F7.25.0%22%3Bs%3A13%3A%22last_activity%22%3Bi%3A1596801919%3Bs%3A9%3A%22user_data%22%3Bs%3A0%3A%22%22%3B%7Dba4e237c16bb0df0c9181d078a464cc72add908b");
                        request.AddParameter("appJson", jsonBody);
                        DataCollectionAPIKeys.URLSDK = ExcelUtil.ReadData(3, "URL");
                        jsonBody = jsonBody.Trim();
                        jsonBody = jsonBody.TrimEnd();
                        jsonBody = jsonBody.TrimStart();
                        SendJsonRequest.SendJsonRequestMainMethod(jsonBody, DataCollectionAPIKeys.URLSDK, request, counter,apiName);
                        counter = counter + 1;
                        variable = variable + 1;
                        if (variable == 9)
                        {
                            variable = 0;
                        }
                    }
                }

            if (apiName.Equals("SyncUserDevice"))
            {
                foreach (var row in dataSet[0].Tables[0].Rows)
                {
                    string package = (dataSet[0].Tables[0].Rows[counter][0].ToString());
                    string appID = (dataSet[0].Tables[0].Rows[counter][1].ToString());
                    string osID = (dataSet[0].Tables[0].Rows[counter][2].ToString());
                    string companyID = (dataSet[0].Tables[0].Rows[counter][3].ToString());
                    string sdkID = (dataSet[0].Tables[0].Rows[counter][4].ToString());
                    string ver = (dataSet[0].Tables[0].Rows[counter][5].ToString());
                    string adID = (dataSet[1].Tables[0].Rows[counter][0].ToString());
                    item.uniqueDeviceID = Extension.randomNumberGenerator(1, 10000);
                    item.sceneID = Extension.randomNumberGenerator(1, 26);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddHeader("Cookie", "PHPSESSID=ukk3jhiein0p3fddg824jilep2; ci_session=a%3A5%3A%7Bs%3A10%3A%22session_id%22%3Bs%3A32%3A%2274d3c988d669499a01b875c1bca1abc2%22%3Bs%3A10%3A%22ip_address%22%3Bs%3A14%3A%22182.180.164.20%22%3Bs%3A10%3A%22user_agent%22%3Bs%3A21%3A%22PostmanRuntime%2F7.25.0%22%3Bs%3A13%3A%22last_activity%22%3Bi%3A1591103816%3Bs%3A9%3A%22user_data%22%3Bs%3A0%3A%22%22%3B%7Df63f392ff4facd0290cdfd0e4f42de96acb73f70");
                    jsonBody = "{" + "\"package\"" + ":" + "\"" + package + "\"," + "\"store\"" + ":" + "\"" + osID + "\"," + "\"uniqueDeviceID\"" + ":" + "\"" + item.uniqueDeviceID + "\"" + "," + "\"appID\"" + ":" + "\"" + "\"," + "\"deviceID\"" + ":" + "\"" + "\"," + "\"region\"" + ":" + "\"" + "\"," + "\"adsQueueEventStats \"" + ":" + "[{" + "\"adID\"" + ":" + "\"" + adID + "\"" + "," + "\"sceneID\"" + ":" + "\"" + item.sceneID + "\"" + "," + "\" request\"" + ":" + "\"" + Extension.randomNumberGenerator(1, 1000) + "\"" + "," + "\" impression\"" + ":" + "\"" + Extension.randomNumberGenerator(1, 1000) + "\"" + "," + "\" click\"" + ":" + "\"" + Extension.randomNumberGenerator(1, 1000) + "\"" + "}]," + "\"gssdkVersion\"" + ":" + "\"" + ver + "\"," + "\"sdkVersionID\"" + ":" + "\"" + sdkID + "\"," + "\"sdkVersion\"" + ":" + "\"" + sdkID + "\"," + "\"devMode\"" + ":" + "\"" + Extension.randomNumberGenerator(0,1) + "\"" + "}";
                    //Byte[] inputBytes = Extension.GZipJsonString(jsonBody);
                    request.AddParameter("appJson", jsonBody);
                    DataCollectionAPIKeys.URLSDK = ExcelUtil.ReadData(2, "URL");
                    SendJsonRequest.SendJsonRequestMainMethod(jsonBody, DataCollectionAPIKeys.URLSDK, request, counter,apiName);
                    counter = counter + 1;
                }
            }

        }
    }
}

