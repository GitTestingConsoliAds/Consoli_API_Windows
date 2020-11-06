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
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace Monetization_Automation.Test
{
    [TestClass]
    public class CronJobs
    {
        public CronJobs()
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
                            ExcelUtil.PopulateInCollection(@"/Volumes/DataDisk/Ahmad khisal/Consoliads/Automation_Data/CronJobs.xlsx", Monetization_Automation.TestingAPI.Properties.Settings.Default.Environment);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Excel file is not loaded successfully" + " " + e.Message);
            }
        }


       // [TestMethod]
        public void CronJob_updateCampaignsDailyStatus()
        {
            string httpCall = (ExcelUtil.ReadData(1, "HttpCall"));
            string dFoler = (ExcelUtil.ReadData(1, "DeploymentFolder"));
            string fName = (ExcelUtil.ReadData(1, "FileName"));
            string CJName = (ExcelUtil.ReadData(1, "CronJobName"));
            string saltKey = (ExcelUtil.ReadData(1, "SaltKey"));
            string cronJobURL = httpCall + "/" + dFoler + "/" + fName + "/" + CJName + "/" + saltKey;
            Extension.OpenDBConnection();


            //Execute Update queries to create test data
            string query = ExcelUtil.ReadData(1, "UpdateQueriesCreateTestData");
            Extension.ExecuteQuery(SqlConnectionInstance.SqlConnection, ExcelUtil.ReadData(2, "UpdateQueriesCreateTestData"));
            Extension.ExecuteQuery(SqlConnectionInstance.SqlConnection, ExcelUtil.ReadData(2, "UpdateQueriesCreateTestData"));
            Extension.ExecuteQuery(SqlConnectionInstance.SqlConnection, ExcelUtil.ReadData(3, "UpdateQueriesCreateTestData"));

            //Queries before crone job execution
            var value1Before = Extension.ExecuteQuery(SqlConnectionInstance.SqlConnection, ExcelUtil.ReadData(1, "QueriesBeforeCronJob"));
            var value2Before = Extension.ExecuteQuery(SqlConnectionInstance.SqlConnection, ExcelUtil.ReadData(2, "QueriesBeforeCronJob"));
            var value3Before = Extension.ExecuteQuery(SqlConnectionInstance.SqlConnection, ExcelUtil.ReadData(3, "QueriesBeforeCronJob"));

            //Execute cron job
            Extension.ExecuteCronJob(cronJobURL);
            Thread.Sleep(15000);

            //Queries after cron job execution
            var value1After = Extension.ExecuteQuery(SqlConnectionInstance.SqlConnection, ExcelUtil.ReadData(1, "QueriesAfterCronJob"));
            var value2After = Extension.ExecuteQuery(SqlConnectionInstance.SqlConnection, ExcelUtil.ReadData(2, "QueriesAfterCronJob"));
            var value3After = Extension.ExecuteQuery(SqlConnectionInstance.SqlConnection, ExcelUtil.ReadData(3, "QueriesAfterCronJob"));
            Thread.Sleep(200);
            SqlConnectionInstance.SqlConnection.Close();

            if (value1After != value1Before)
            {
                Debug.WriteLine("Update Campaign Daily Status got failed on Limited by Daily Budget");
            }

            if (value2After != value2Before)
            {
                Debug.WriteLine("Update Campaign Daily Status got failed on Limited by Daily Impression");
            }

            if (value3Before != value3After)
            {
                Debug.WriteLine("Update Campaign Daily Status got failed on end date");
            }
        }

        //[TestMethod]
        public void CronJob_CreateReportingForDownloads()
        {
            String date = DateTime.Now.ToString("yyyy-MM-dd");
            string httpCall = (ExcelUtil.ReadData(4, "HttpCall"));
            string dFoler = (ExcelUtil.ReadData(4, "DeploymentFolder"));
            string fName = (ExcelUtil.ReadData(4, "FileName"));
            string CJName = (ExcelUtil.ReadData(4, "CronJobName"));
            string saltKey = (ExcelUtil.ReadData(4, "SaltKey"));
            string companyID = (ExcelUtil.ReadData(4, "CompanyID"));
            string ExecutingFromCronJob = (ExcelUtil.ReadData(4, "ExecutingFromCronJob"));
            string cronJobURL = httpCall + "/" + dFoler + "/" + fName + "/" + CJName + "/" + saltKey + "/" + companyID + "/" + date + "/" + ExecutingFromCronJob;
            Extension.OpenDBConnection();


            //Queries before crone job execution
            string query = ExcelUtil.ReadData(4, "QueriesBeforeCronJob");
            var value1Before = Extension.ExecuteQuery(SqlConnectionInstance.SqlConnection, query);
            //Execute cron job
            Extension.ExecuteCronJob(cronJobURL);
            Thread.Sleep(15000);


            //Queries after cron job execution
            string query1 = ExcelUtil.ReadData(4, "QueriesAfterCronJob");
            var value1After = Extension.ExecuteQuery(SqlConnectionInstance.SqlConnection, query1);
            Thread.Sleep(2000);
            if (value1After != value1Before)
            {
                Debug.WriteLine("Create Reporting for downloads cornjob Failed to insert data in reporting table");
            }

            SqlConnectionInstance.SqlConnection.Close();
            Thread.Sleep(2000);
        }

       // [TestMethod]
        public void CronJob_createReportingForCampaignLogSummary()
        {
            string httpCall = (ExcelUtil.ReadData(5, "HttpCall"));
            string dFoler = (ExcelUtil.ReadData(5, "DeploymentFolder"));
            string fName = (ExcelUtil.ReadData(5, "FileName"));
            string CJName = (ExcelUtil.ReadData(5, "CronJobName"));
            string saltKey = (ExcelUtil.ReadData(5, "SaltKey"));
            string companyID = (ExcelUtil.ReadData(5, "CompanyID"));
            string dateTimeValue = (ExcelUtil.ReadData(5, "DateTimeValue"));
            string cronJobURL = httpCall + "/" + dFoler + "/" + fName + "/" + CJName + "/" + saltKey + "/" + companyID + "/" + dateTimeValue;
            Extension.OpenDBConnection();


            //Queries before crone job execution
            string query = ExcelUtil.ReadData(5, "QueriesBeforeCronJob");
            var value1Before = Extension.ExecuteQuery(SqlConnectionInstance.SqlConnection, query);
            //Execute cron job
            Extension.ExecuteCronJob(cronJobURL);
            Thread.Sleep(15000);

            //Queries after cron job execution
            var value1After = Extension.ExecuteQuery(SqlConnectionInstance.SqlConnection, ExcelUtil.ReadData(5, "QueriesAfterCronJob"));
            Thread.Sleep(2000);
            SqlConnectionInstance.SqlConnection.Close();
            if (value1After != value1Before)
            {
                Debug.WriteLine("Create Reporting for Campaign Log Summary cornjob Failed to insert data in reporting table");
            }

        }

       // [TestMethod]
        public void CronJob_createReportingForConsolidatedMediationData()
        {
            string httpCall = (ExcelUtil.ReadData(6, "HttpCall"));
            string dFoler = (ExcelUtil.ReadData(6, "DeploymentFolder"));
            string fName = (ExcelUtil.ReadData(6, "FileName"));
            string CJName = (ExcelUtil.ReadData(6, "CronJobName"));
            string saltKey = (ExcelUtil.ReadData(6, "SaltKey"));
            string companyID = (ExcelUtil.ReadData(6, "CompanyID"));
            string dateTimeValue = (ExcelUtil.ReadData(6, "DateTimeValue"));
            string cronJobURL = httpCall + "/" + dFoler + "/" + fName + "/" + CJName + "/" + saltKey + "/" + companyID + "/" + dateTimeValue;
            Extension.OpenDBConnection();


            //Queries before crone job execution
            string query = ExcelUtil.ReadData(6, "QueriesBeforeCronJob");
            var value1Before = Extension.ExecuteQuery(SqlConnectionInstance.SqlConnection, query);
            //Execute cron job
            Extension.ExecuteCronJob(cronJobURL);
            Thread.Sleep(15000);

            //Queries after cron job execution
            var value1After = Extension.ExecuteQuery(SqlConnectionInstance.SqlConnection, ExcelUtil.ReadData(6, "QueriesAfterCronJob"));
            Thread.Sleep(2000);
            SqlConnectionInstance.SqlConnection.Close();
            if (value1After != value1Before)
            {
                Debug.WriteLine("create Reporting For Consolidated Mediation Data cornjob Failed to insert data in reporting table");
            }

        }

     //   [TestMethod]
        public void CronJob_createMasterReportingCampaigns()
        {
            string httpCall = (ExcelUtil.ReadData(7, "HttpCall"));
            string dFoler = (ExcelUtil.ReadData(7, "DeploymentFolder"));
            string fName = (ExcelUtil.ReadData(7, "FileName"));
            string CJName = (ExcelUtil.ReadData(7, "CronJobName"));
            string saltKey = (ExcelUtil.ReadData(7, "SaltKey"));
            string Fromdate = (ExcelUtil.ReadData(7, "Fromdate"));
            string Todate = (ExcelUtil.ReadData(7, "Todate"));
            string cronJobURL = httpCall + "/" + dFoler + "/" + fName + "/" + CJName + "/" + saltKey + "/" + Fromdate + "/" + Todate;
            Extension.OpenDBConnection();


            //Queries before crone job execution
            string query = ExcelUtil.ReadData(7, "QueriesBeforeCronJob");
            var value1Before = Extension.ExecuteQuery(SqlConnectionInstance.SqlConnection, query);
            //Execute cron job
            Extension.ExecuteCronJob(cronJobURL);
            Thread.Sleep(15000);

            //Queries after cron job execution
            var value1After = Extension.ExecuteQuery(SqlConnectionInstance.SqlConnection, ExcelUtil.ReadData(7, "QueriesAfterCronJob"));
            Thread.Sleep(2000);
            SqlConnectionInstance.SqlConnection.Close();
            if (value1After != value1Before)
            {
                Debug.WriteLine("create Reporting For master reporting campaign Data cornjob Failed to insert data in master table");
            }

        }

       // [TestMethod]
        public void CronJob_createMasterReportingDownloads()
        {
            string httpCall = (ExcelUtil.ReadData(8, "HttpCall"));
            string dFoler = (ExcelUtil.ReadData(8, "DeploymentFolder"));
            string fName = (ExcelUtil.ReadData(8, "FileName"));
            string CJName = (ExcelUtil.ReadData(8, "CronJobName"));
            string saltKey = (ExcelUtil.ReadData(8, "SaltKey"));
            string Fromdate = (ExcelUtil.ReadData(8, "Fromdate"));
            string Todate = (ExcelUtil.ReadData(8, "Todate"));
            string cronJobURL = httpCall + "/" + dFoler + "/" + fName + "/" + CJName + "/" + saltKey + "/" + Fromdate + "/" + Todate;
            Extension.OpenDBConnection();


            //Queries before crone job execution
            string query = ExcelUtil.ReadData(8, "QueriesBeforeCronJob");
            var value1Before = Extension.ExecuteQuery(SqlConnectionInstance.SqlConnection, query);
            //Execute cron job
            Extension.ExecuteCronJob(cronJobURL);
            Thread.Sleep(15000);

            //Queries after cron job execution
            var value1After = Extension.ExecuteQuery(SqlConnectionInstance.SqlConnection, ExcelUtil.ReadData(8, "QueriesAfterCronJob"));
            Thread.Sleep(2000);
            SqlConnectionInstance.SqlConnection.Close();
            if (value1After != value1Before)
            {
                Debug.WriteLine("create Reporting For master reporting downloads Data cornjob Failed to insert data in master table");
            }

        }

       // [TestMethod]
        public void CronJob_createMasterReportingMediation()
        {
            string httpCall = (ExcelUtil.ReadData(9, "HttpCall"));
            string dFoler = (ExcelUtil.ReadData(9, "DeploymentFolder"));
            string fName = (ExcelUtil.ReadData(9, "FileName"));
            string CJName = (ExcelUtil.ReadData(9, "CronJobName"));
            string saltKey = (ExcelUtil.ReadData(9, "SaltKey"));
            string Fromdate = (ExcelUtil.ReadData(9, "Fromdate"));
            string Todate = (ExcelUtil.ReadData(9, "Todate"));
            string cronJobURL = httpCall + "/" + dFoler + "/" + fName + "/" + CJName + "/" + saltKey + "/" + Fromdate + "/" + Todate;
            Extension.OpenDBConnection();


            //Queries before crone job execution
            string query = ExcelUtil.ReadData(9, "QueriesBeforeCronJob");
            var value1Before = Extension.ExecuteQuery(SqlConnectionInstance.SqlConnection, query);
            //Execute cron job
            Extension.ExecuteCronJob(cronJobURL);
            Thread.Sleep(15000);

            //Queries after cron job execution
            var value1After = Extension.ExecuteQuery(SqlConnectionInstance.SqlConnection, ExcelUtil.ReadData(9, "QueriesAfterCronJob"));
            Thread.Sleep(2000);
            SqlConnectionInstance.SqlConnection.Close();
            if (value1After != value1Before)
            {
                Debug.WriteLine("create Reporting For master reporting Mediation Data cornjob Failed to insert data in master table");
            }

        }
    }
}