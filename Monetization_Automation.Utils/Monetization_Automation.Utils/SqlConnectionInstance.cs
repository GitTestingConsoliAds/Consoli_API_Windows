//-----------------------------------------------------------------------
// <copyright company="ConsoliAds">
//     Copyright (c) Monetization_Automation. All rights reserved.
// </copyright>
// <author>Ahmad Khisal - ConsoliAds Pakistan/author>
//-----------------------------------------------------------------------

using MySql.Data.MySqlClient;

namespace Monetization_Automation.Utils
{
    /// <summary>
    /// Developed a DataCollection class which holds all CSV or Excel Objects in Collection.
    /// </summary>
    public class SqlConnectionInstance
    {
        //string to get the session 
        public static MySqlConnection SqlConnection { get; set; }
    }
}
