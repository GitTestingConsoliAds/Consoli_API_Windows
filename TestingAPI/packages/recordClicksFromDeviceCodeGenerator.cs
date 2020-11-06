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
using System.Reflection;

namespace Monetization_Automation.Test
{
    [TestClass]
    public class recordClicksFromDeviceCodeGenerator
    {

        public recordClicksFromDeviceCodeGenerator()
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
                            ExcelUtil.PopulateInCollection(@"H:\Automation_API\Automation_Data\APIExcels\Apirequestautogenerator.xlsx", Monetization_Automation.Test.Properties.Settings.Default.Environment);
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
        public void recordClicksFromDeviceMainMethodCodeGenerator()
        {
            int dataSetIndex = 2;
            //   int count = DataTableCollectionLoop.tableCollection.Rows.Count;
            string URL = Utils.ExcelUtil.ReadData(7, "URL");
            var client = new RestClient(URL);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "PHPSESSID=ukk3jhiein0p3fddg824jilep2; ci_session=a%3A5%3A%7Bs%3A10%3A%22session_id%22%3Bs%3A32%3A%2274d3c988d669499a01b875c1bca1abc2%22%3Bs%3A10%3A%22ip_address%22%3Bs%3A14%3A%22182.180.164.20%22%3Bs%3A10%3A%22user_agent%22%3Bs%3A21%3A%22PostmanRuntime%2F7.25.0%22%3Bs%3A13%3A%22last_activity%22%3Bi%3A1591103816%3Bs%3A9%3A%22user_data%22%3Bs%3A0%3A%22%22%3B%7Df63f392ff4facd0290cdfd0e4f42de96acb73f70");
            Extension.OpenDBConnection();
            QueryArrayAllAPI.queryArrayAllAPI[0] = Utils.ExcelUtil.ReadData(7, "Query");
            QueryArrayAllAPI.queryArrayAllAPI[1] = Utils.ExcelUtil.ReadData(7, "AppKey");
            Extension.ExecuteQueryAPICodeGenerator(SqlConnectionInstance.SqlConnection, dataSetIndex);
            CreateJsonRequestCodeGenerator.CreateJson(DataSetPropertyFill.dataSetPropertyFill, "recordClicksFromDevice", request);
            Thread.Sleep(2000);
        }
    }
}