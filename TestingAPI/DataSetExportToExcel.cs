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
using Microsoft.Office.Interop.Excel;

namespace Monetization_Automation.Test
{

    public class DataSetExportToExcel
    {
        public DataSetExportToExcel()
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
                            ExcelUtil.PopulateInCollection(@"/Volumes/DataDisk/Ahmad khisal/Consoliads/Automation_Data/APIExcels/SyncAppNative.xlsx", Monetization_Automation.TestingAPI.Properties.Settings.Default.Environment);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Excel file is not loaded successfully" + " " + e.Message);
            }
        }

        public static void ExportToExcel(string fileName, DataSet dataSet, string sheetName)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = null;
            Microsoft.Office.Interop.Excel.Workbook excelWorkBook = null;
            try
            {
                excelApp = new Microsoft.Office.Interop.Excel.Application();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            //Create an Excel workbook instance and open it from the predefined location
            excelWorkBook = excelApp.Workbooks.Open(fileName);
            foreach (System.Data.DataTable table in dataSet.Tables)
            {
                //Add a new worksheet to workbook with the Datatable name
                Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet = (Worksheet)excelWorkBook.Sheets.Add();
                excelWorkSheet.Name = table.TableName;
                for (int i = 1; i < table.Columns.Count + 1; i++)
                {
                    excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
                }
                for (int j = 0; j < table.Rows.Count; j++)
                {
                    for (int k = 0; k < table.Columns.Count; k++)
                    {
                        excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                    }
                }
            }
            excelWorkBook.Save();
            excelWorkBook.Close();
            excelApp.Quit();
        }
    }
}