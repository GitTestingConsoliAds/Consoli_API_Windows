//-----------------------------------------------------------------------
// <copyright company="ConsoliAds">
//     Copyright (c) Monetization_Automation. All rights reserved.
// </copyright>
// <author>Ahmad Khisal - ConsoliAds Pakistan/author>
//-----------------------------------------------------------------------

namespace Monetization_Automation.Utils
{
    /// <summary>
    /// Developed a DataCollection class which holds all CSV or Excel Objects in Collection.
    /// </summary>
    public class DataCollection
    {
        /// <summary>
        /// Row number for each item from csv in list.
        /// </summary>
        public int RowNumber { get; set; }

        /// <summary>
        /// Column Name for each item from csv in list.
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Column value for each item from csv in list.
        /// </summary>
        public string ColumnValue { get; set; }
    }
}
