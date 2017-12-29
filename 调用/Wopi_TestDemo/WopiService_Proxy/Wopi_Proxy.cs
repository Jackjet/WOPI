using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WopiService_Proxy.Wopi_WebService;


namespace WopiService_Proxy
{
    public class Wopi_Proxy
    {
        static Wopi_ServiceSoapClient Client = new Wopi_ServiceSoapClient();

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="documentData">文件内容</param>
        /// <param name="callBack">回调函数</param>
        public static void Upload_File(string fileName, byte[] documentData, Action<bool> callBack)
        {
            new Thread(() =>
            {
                bool isSuccessed = Client.UploadFile(fileName, documentData);

                if (isSuccessed)
                {
                    callBack(true);
                }
                else
                {
                    callBack(false);
                }

            }) { IsBackground = true }.Start();
        }

        /// <summary>
        /// 获取文件有效链接
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="callback">回调函数</param>
        public static void GetLink(string fileName, Action<string> callback)
        {
            new Thread(() =>
            {
                string link = Client.Get_Link(fileName);

                callback(link);

            }) { IsBackground = true }.Start();
        }


        /// <summary>
        /// 上传文件并获取有效链接
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="documentData">文件字节流</param>
        /// <param name="callback">回调函数</param>
        public static void UploadFile_GetLink(string fileName, byte[] documentData,  Action<string> callback)
        {
            new Thread(() =>
            {
                string link = Client.UploadFile_GetLink(fileName, documentData);

                callback(link);

            }) { IsBackground = true }.Start();
        }


        public static void Delete_File(string fileName, Action<bool> callback)
        {
            new Thread(() =>
            {
                bool isSuccessed = Client.Delete_File(fileName);

                callback(isSuccessed);

            }) { IsBackground = true }.Start();
        }
    }
}
