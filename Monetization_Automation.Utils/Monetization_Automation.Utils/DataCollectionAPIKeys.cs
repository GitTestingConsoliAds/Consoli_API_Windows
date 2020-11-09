//-----------------------------------------------------------------------
// <copyright company="ConsoliAds">
//     Copyright (c) Monetization_Automation. All rights reserved.
// </copyright>
// <author>Ahmad Khisal - ConsoliAds Pakistan/author>
//-----------------------------------------------------------------------

namespace Monetization_Automation.Utils
{
    /// <summary>
    /// Developed a DataCollection class which holds all API values in Collection.
    /// </summary>
    public class DataCollectionAPIKeys
    {
        //API KEY VARIABLE DEFINITIONS

        public static string URLSDK { get; set; }
        //public string branchName;

        public static string branchName = "sheeda";
        public int companyID { get; set; }
        public string operatingSystem { get; set; }
        public int sceneID { get; set; }
        public int requests { get; set; }
        public int impressions { get; set; }
        public int click { get; set; }
        public int install { get; set; }
        public int accidentalClick { get; set; }
        public int notReady { get; set; }
        public int completed { get; set; }
        public string sdkVersion { get; set; }
        public int sessionToken { get; set; }

        public int store { get; set; }
        public int uniqueDeviceID { get; set; }
        public int appID { get; set; }
        public int deviceID { get; set; }
        public string regigon { get; set; }
        public int adsQueueEventStats { get; set; }
        public string gsSDKVersion { get; set; }
        public int sdkVersionID { get; set; }


        public int devMode { get; set; }
        public string title { get; set; }
        public string userSignature { get; set; }
        public int totalSequences { get; set; }
        public string adsQueueType { get; set; }
        public string gpRateUsURL { get; set; }
        public string isHideAds { get; set; }
        public int childDirected { get; set; }

        public int sequences { get; set; }
        public int sequenceTitleID { get; set; }
        public bool isFirtSkip { get; set; }
        public int failOverAdID { get; set; }
        public string isFirstSkip { get; set; }
        public string isFirstSkipRewardedVideo { get; set; }
        public string bannerEnabled { get; set; }
        public int bannerSize { get; set; }

        public int bannerPosition { get; set; }
        public int failOverAdIDBanner { get; set; }
        public int failOverAdIDNative { get; set; }
        public int nativeEnabled { get; set; }
        public int nativeWidth { get; set; }
        public int nativeHeight { get; set; }
        public int interstitialAndVideo { get; set; }
        public int adID { get; set; }
        public int adOrder { get; set; }
        public int rewardedVideo { get; set; }
        public int native { get; set; }
        public int nativeApp { get; set; }
        public int banner { get; set; }
        public int icon { get; set; }
        public int enabled { get; set; }
        public int size { get; set; }
        public string appAdnetwork {get; set;}
        public string isAdavailable { get; set; }

        public string product { get; set; }
        public string osVersion { get; set; }
        public string orientation { get; set; }
        public int is_mac { get; set; }
        public string model { get; set; }
        public string telco_operator { get; set; }
        public string cpu { get; set; }
        public string brand { get; set; }
        public string userConsent { get; set; }
        public string internet_connectivity { get; set; }
        public string architecture { get; set; }
        public string resolution { get; set; }
        public string ram { get; set; }
        public string appKey { get; set; }
        public string width { get; set; }
        public string height { get; set; }
        public string gssdkVersion { get; set; }

        public string appToPromoteID { get; set; }
        public string campaignID { get; set; }
        public string advertising_id { get; set; }
        public string osID { get; set; }
        public string sha1_advertising_id { get; set; }
        public string clickID { get; set; }
    }
}
