using Monetization_Automation;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Data.OleDb;
using System.Diagnostics;
using Excel;
using OfficeOpenXml;
using System.Data.Odbc;


namespace Monetization_Automation.Utils
{
    public class ExcelUtil
    {
        public static List<DataCollection> _dataCollection = new List<DataCollection>();
        public static DataTable ExcelToDataTable(string fileName, string sheetName)
        {
            //Open File and read all content of file as stream.
            FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite);

            //CreateOpenXmlReader via ExcelReaderFactory
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            


            //Set the First Row as Column Name
            excelReader.IsFirstRowAsColumnNames = true;

            //Prepare Data Set object
            DataSet result = excelReader.AsDataSet();

            //Get All The Tables
            DataTableCollection collection = result.Tables;

            //Store all collection values in DataTable
            DataTable resultTable = collection[sheetName];

            //Return Table
            return resultTable;

        }

        public static void PopulateInCollection(string fileName, string sheet)
        {
            DataTable table = ExcelToDataTable(fileName, sheet);
            DataTableCollectionLoop.tableCollection = table;

            //Iterate through the rows and columns of the Table.
            for (int row = 1; row <= table.Rows.Count; row++)
            {
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    DataCollection item = new DataCollection()
                    {
                        RowNumber = row,
                        ColumnName = table.Columns[col].ColumnName,
                        ColumnValue = table.Rows[row - 1][col].ToString()
                    };
                    _dataCollection.Add(item);
                }
            }
        }

        public static string ReadData(int rowNumber, string columnName)
        {
            try
            {
                //Retriving data using LINQ to reduce avoid iterations.
                string data = (from colData in _dataCollection
                               where colData.ColumnName == columnName && colData.RowNumber == rowNumber
                               select colData.ColumnValue).SingleOrDefault();
                Debug.WriteLine("****************** " + data);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void WriteToExcel(string filePath, string sheetName, string value, string columnName)
        {
            try
            {
               string conStr = @"Driver={Microsoft Excel Driver (*.xls, *.xlsx, *.xlsm, *.xlsb)};"  +  @"DBQ=" + filePath + ";ReadOnly = 0;";
             //  String sConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source= " + filePath + ";Extended Properties='Excel 12.0 XML;HDR=YES'";
               OdbcConnection objConn = new System.Data.Odbc.OdbcConnection(conStr);
               objConn.Open();
               OdbcCommand objCmdSelect = new OdbcCommand($"UPDATE [{sheetName}$] SET [" + columnName + "]='" + value + "'", objConn);
               objCmdSelect.ExecuteNonQuery();
               objConn.Close();
            }
                catch (Exception e)
            {
             Debug.WriteLine("System did not enter the file number in excel" + " " + e.Message);
            }
        }
    }
}

