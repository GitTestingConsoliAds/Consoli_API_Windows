﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
using System.Reflection;

namespace Monetization_Automation.Test
{
    [TestClass]
    public class SyncAppUnityAPIInputGenerator
    {

        public SyncAppUnityAPIInputGenerator()
        {
              //  string localFileName = null;
                try
                {
                   /* var OS = Environment.OSVersion;
                    string remoteUri = ("https://consoliads-file.s3-us-west-2.amazonaws.com/QATeam/APIExcelInputGenerator.xlsx");
                    // Create a new WebClient instance.
                    WebClient myWebClient = new WebClient();
                    // Download the Web resource and save it into the current filesystem folder.
                    if (OS.ToString().Contains("Unix"))
                    {
                        localFileName = @"/Volumes/DataDisk/Ahmadkhisal/Consoliads/Automation/Automation_Data/APIExcelInputGenerator.xlsx";
                        if (File.Exists(@"/Volumes/DataDisk/Ahmadkhisal/Consoliads/Automation/Automation_Data/APIExcelInputGenerator.xlsx"))
                        {
                            File.Delete(@"/Volumes/DataDisk/Ahmadkhisal/Consoliads/Automation/Automation_Data/APIExcelInputGenerator.xlsx");
                        }
                    }
                    else
                    {
                        localFileName = @"C:/APIExcelInputGenerator.xlsx";
                        if (File.Exists(@"C:/APIExcelInputGenerator.xlsx"))
                        {
                            File.Delete(@"C:/APIExcelInputGenerator.xlsx");
                        }
                    }

                    myWebClient.DownloadFile(remoteUri, localFileName);
                    myWebClient.Dispose();*/
                    if (ExcelUtil._dataCollection.Count == 0)
                {
                    // var OS = Environment.OSVersion;
                    var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
                    if (directoryInfo != null)
                    {
                        if (directoryInfo.Parent != null)
                        {
                            ExcelUtil.PopulateInCollection(@"/Volumes/DataDisk/Ahmadkhisal/Consoliads/Automation/Automation_Data/APIExcels/APIExcelCodeGenerator.xlsx", Monetization_Automation.TestingAPI.Properties.Settings.Default.Environment);
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
            public void SyncAppunityMainMethodCodeGenerator()
            {
                int dataSetIndex = 7;
                DataCollectionAPIKeys.URLSDK = Utils.ExcelUtil.ReadData(6, "URL");
                Extension.OpenDBConnection();
                QueryArrayAllAPI.queryArrayAllAPI[0] = Utils.ExcelUtil.ReadData(5, "Query") + " " + Utils.ExcelUtil.ReadData(1, "Limit");
            QueryArrayAllAPI.queryArrayAllAPI[1] = Utils.ExcelUtil.ReadData(5, "adID");
                QueryArrayAllAPI.queryArrayAllAPI[2] = Utils.ExcelUtil.ReadData(5, "failOverAdID");
                QueryArrayAllAPI.queryArrayAllAPI[3] = Utils.ExcelUtil.ReadData(5, "failOverAdIDBanner");
                QueryArrayAllAPI.queryArrayAllAPI[4] = Utils.ExcelUtil.ReadData(5, "failOverAdIDNative");
                QueryArrayAllAPI.queryArrayAllAPI[5] = Utils.ExcelUtil.ReadData(5, "failOverAdIDRewardedVideo");
                QueryArrayAllAPI.queryArrayAllAPI[6] = Utils.ExcelUtil.ReadData(5, "sdkVersion") + " " + Utils.ExcelUtil.ReadData(1, "Limit");
                Extension.ExecuteQueryAPICodeGenerator(SqlConnectionInstance.SqlConnection, dataSetIndex);
                CreateJsonRequestInputGenerator.CreateJson(DataSetPropertyFill.dataSetPropertyFill, "SyncAppUnity");
            }
        
    }
}


