using MainWeb.Helpers;
using MainWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Services;

namespace MainWeb
{
    /// <summary>
    /// Wopi_Service 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class Wopi_Service : System.Web.Services.WebService
    {
        #region 获取链接

        [WebMethod]
        public string Get_Link(string fileNmae)
        {
            Link link = GetLink(fileNmae);

            return link.Url;
        }

        #endregion

        #region 上传文件

        [WebMethod]
        public bool UploadFile(string fileNmae, byte[] documentData)
        {
            bool isSuccessed = false;

            try
            {
              
                string file = HostingEnvironment.MapPath(Constant.App_Data + "/" + fileNmae);
                using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    fs.Write(documentData, 0, documentData.Length);
                    fs.Flush();
                }
                isSuccessed = true;
            }
            catch (Exception)
            {
                isSuccessed = false;

            }
            return isSuccessed;
        }

        #endregion

        #region 上传文件获取链接

        [WebMethod]
        public string UploadFile_GetLink(string fileNmae, byte[] documentData)
        {
            string linkUri = string.Empty;
            try
            {
                string extenSion = Path.GetExtension(fileNmae);
               
                string file = HostingEnvironment.MapPath(Constant.App_Data + "/" + fileNmae);
                using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    fs.Write(documentData, 0, documentData.Length);
                    fs.Flush();
                }
                Link link = GetLink(fileNmae);

                linkUri = link.Url;
                if (extenSion.ToLower() == ".pdf")
                {
                   linkUri= linkUri.Replace("WOPISrc=", "PdfMode=1&WOPISrc=");
                }

            }
            catch (Exception)
            {

            }
            return linkUri;
        }

        #endregion

        #region 删除文件

        [WebMethod]
        public bool Delete_File(string fileNmae)
        {
            bool isSuccessed = false;
            try
            {
                string file = HostingEnvironment.MapPath(Constant.App_Data + "/" + fileNmae);
                if (File.Exists(file))
                {
                    File.Delete(file);
                    isSuccessed = true;
                }
            }
            catch (Exception)
            {
            }
            return isSuccessed;
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// Provides a link that can be used to Open a document in the relative viewer
        /// from the Office Web Apps server
        /// </summary>
        /// <param name="fileRequest">indicates the request type</param>
        /// <returns>A link usable for HREF</returns>
        public Link GetLink(string name)
        {

            WopiAppHelper wopiHelper = new WopiAppHelper(HostingEnvironment.MapPath(Constant.appDiscoveryXml));

            var result = wopiHelper.GetDocumentLink(Constant.appWopiServer + name);

            var rv = new Link
            {
                Url = result
            };
            return rv;
        }

        #endregion
    }

}
