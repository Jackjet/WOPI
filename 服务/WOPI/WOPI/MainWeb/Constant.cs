using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace MainWeb
{
    public static class Constant
    {
        /// <summary>
        /// 数据存储目录
        /// </summary>
        public static string App_Data = WebConfigurationManager.AppSettings["App_Data"];

        /// <summary>
        /// DiscoveryXml文件
        /// </summary>
        public static string appDiscoveryXml = WebConfigurationManager.AppSettings["appDiscoveryXml"];


        /// <summary>
        /// WopiServer
        /// </summary>
        public static string appWopiServer = WebConfigurationManager.AppSettings["appWopiServer"];

         /// <summary>
        /// appSampleLink
        /// </summary>
        public static string appSampleLink = WebConfigurationManager.AppSettings["appSampleLink"];
     

          /// <summary>
        /// appHmacKey
        /// </summary>
        public static string appHmacKey = WebConfigurationManager.AppSettings["appHmacKey"];

        
    }
}