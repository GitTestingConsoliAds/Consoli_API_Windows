//-----------------------------------------------------------------------
// <copyright company="ConsoliAds">
//     Copyright (c) Monetization_Automation. All rights reserved.
// </copyright>
// <author>Ahmad Khisal - ConsoliAds Pakistan/author>
//-----------------------------------------------------------------------

namespace Monetization_Automation.Utils
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Text.RegularExpressions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.IE;
    using OpenQA.Selenium.Remote;
    using OpenQA.Selenium.Appium.Windows;
    using System.Net;
    using RestSharp;
    using System.Web.UI.WebControls;
    using System.Windows.Forms;
    using System.Threading;
    using System.IO.Compression;
    using System.Text;
    using Google.Apis.Services;
    using MySql.Data.MySqlClient;

    public static class Extension
    {

        public static void HighLighterMethod(IWebDriver driver , IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].setAttribute('style', 'background: yellow; border: 2px solid red;');", element);
        }

        public static void CaptureScreenShot(string fileName)
        {
            System.DateTime currentDate = DateTime.Now;
            string dateFormat = @"MMddyyyy";
            string timeFormat = @"hhmmss";
            string directoryPath = @"D:\AutoVal\Monetization_AutomationScreenshots" + currentDate.ToString(dateFormat);
            Directory.CreateDirectory(directoryPath);
            var pathToSave = System.IO.Path.Combine(directoryPath, " - " + DateTime.Now.ToString(timeFormat) + fileName + ".jpg");
            //Screenshot screenShot = ((ITakesScreenshot)Driver).GetScreenshot();
            //screenShot.SaveAsFile(pathToSave);
        }

        public static void ExecuteCronJob(string URL)
        {
            try
            {
                RestClient restClient = new RestClient(URL);
                RestRequest restRequest = new RestRequest(Method.POST);
                IRestResponse response = restClient.Execute(restRequest);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Debug.WriteLine("Cronjob did not execute successfully");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        public static void OpenDBConnection()
        {
            MySqlConnection mySqlconnection;
            string connetionString = "server=aurora-shoeda-2020aug21.cvpiqdvbgbus.us-west-2.rds.amazonaws.com;database=db_consoliads; uid =sheeda_user; pwd =consoli123;";
            mySqlconnection = new MySqlConnection(connetionString);
            try
            {
                mySqlconnection.Open();
                SqlConnectionInstance.SqlConnection = mySqlconnection;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public static string ExecuteQuery(MySqlConnection connection, string sqlQuery)
        {
            string firstrowvalue = null;
            try
            {
                DataSet dataSet = new DataSet();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(sqlQuery, connection);
                mySqlDataAdapter.Fill(dataSet);
               // dataSet.WriteXml(@"H:/Automation/Automation_Data/APIExcels/testDataSet.csv");
                firstrowvalue = dataSet.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);

            }
            return firstrowvalue;
        }

        public static void ExecuteQueryAPICodeGenerator(MySqlConnection connection, int dataSetIndex)
        {
            for (int i = 1; i <= dataSetIndex; i++)
            {
                try
                {
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(QueryArrayAllAPI.queryArrayAllAPI[i-1], connection);
                    DataSetPropertyFill.dataSetPropertyFill[i - 1] = new DataSet();
                    try
                    {
                        mySqlDataAdapter.Fill(DataSetPropertyFill.dataSetPropertyFill[i-1]);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                  //  DataSetPropertyFill.dataSetPropertyFill[i-1].WriteXml(@"/Volumes/DataDisk/APIExcelCodeGenerator" + i + ".xml");
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }

        public static void AddEntry(string fileName, byte[] fileContent, ZipArchive archive)
        {
            var entry = archive.CreateEntry(fileName);
            using (var stream = entry.Open())
                stream.Write(fileContent, 0, fileContent.Length);
        }

        public static Byte[] GZipJsonString(string jsonBody)
        {
            string inputStr = jsonBody;
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputStr);
            using (var outputStream = new MemoryStream())
            {
                using (var gZipStream = new System.IO.Compression.GZipStream(outputStream, System.IO.Compression.CompressionMode.Compress))
                    gZipStream.Write(inputBytes, 0, inputBytes.Length);
                return inputBytes;
            }
        }

        public static void  CreateZipFile(string jsonBody, string path)
        {
            var zipContent = new MemoryStream();
            var archive = new ZipArchive(zipContent, ZipArchiveMode.Create);
            //Transform string into byte[]  
            byte[] byteArray = new byte[jsonBody.Length];
            int indexBA = 0;
            foreach (char itemString in jsonBody.ToCharArray())
            {
                byteArray[indexBA++] = (byte)itemString;
            }
            Extension.AddEntry("file1.json", byteArray, archive);
            archive.Dispose();
            File.WriteAllBytes(path, zipContent.ToArray());
        }

        public static int randomNumberGenerator(int startvalue, int endvalue)
        {
            Random random = new Random();
            int randomValue = random.Next(startvalue, endvalue);
            return randomValue;
        }

        public static void CreateLogFile(int serialNumber, string jsonRequest, string jsonResponse, string Status,string Name, string URL)
        {
            string dateFormat = @"yyyyMMdd";
            string logFileName = DateTime.Now.ToString(dateFormat);
            string fileName = @"C:\Windows\System32\config\systemprofile\AppData\Local\Jenkins\.jenkins\workspace\Consoli_API_Windows\AutomationAPIExecutionResultsFiles\" + logFileName + Name + ".htm";
            //Create the HTML file.
            if (!File.Exists(fileName))
            {
                //Table start.
                string html = "<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:arial'>";

                //Adding HeaderRow.
                html += "<tr>";
                html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>#</th>";
                html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>TransactionTime</th>";
                html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>JsonRequest</th>";
                html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>jsonResponse</th>";
                html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Status</th>";
                html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>URL</th>";
                html += "</tr>";

                //Adding DataRow.
                html += "<tr>";
                html += "<td style='width:1000px;border: 1px solid #ccc'>" + serialNumber + "</td>";
                html += "<td style='width:2000px;border: 1px solid #ccc'>" + TransactionTimeAPICall.duration + "</td>";
                html += "<td style='width:1000px;border: 1px solid #ccc'>" + jsonRequest + "</td>";
                html += "<td style='width:1000px;border: 1px solid #ccc'>" + jsonResponse + "</td>";
                html += "<td style='width:2000px;border: 1px solid #ccc'>" + Status + "</td>";
                html += "<td style='width:2000px;border: 1px solid #ccc'>" + URL + "</td>";
                html += "</tr>";

                //Table end.
                html += "</table>";
                File.WriteAllText(@"C:\Windows\System32\config\systemprofile\AppData\Local\Jenkins\.jenkins\workspace\Consoli_API_Windows\AutomationAPIExecutionResultsFiles\" + logFileName +  Name + ".htm", html);
               // Console.ReadLine();
            }
            else
            {
                string html = "<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:arial'>";
                //Adding DataRow.
                html += "<tr>";
                html += "<td style='width:1000px;border: 1px solid #ccc'>" + serialNumber + "</td>";
                html += "<td style='width:2000px;border: 1px solid #ccc'>" + TransactionTimeAPICall.duration + "</td>";
                html += "<td style='width:1000px;border: 1px solid #ccc'>" + jsonRequest + "</td>";
                html += "<td style='width:1000px;border: 1px solid #ccc'>" + jsonResponse + "</td>";
                html += "<td style='width:2000px;border: 1px solid #ccc'>" + Status + "</td>";
                html += "<td style='width:2000px;border: 1px solid #ccc'>" + URL + "</td>";
                html += "</tr>";
                //Table end.
                html += "</table>";
                File.AppendAllText(@"C:\Windows\System32\config\systemprofile\AppData\Local\Jenkins\.jenkins\workspace\Consoli_API_Windows\AutomationAPIExecutionResultsFiles\" + logFileName + Name + ".htm", html);
                //Console.ReadLine();
            }

        }

    

    public enum PropertyType
        {
            Id,
            Name,
            ControlName,
            FriendlyName,
            Text,
            ClassName
        }
    }
}
