
using MainWeb;
using MainWeb.Helpers;
using MainWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.ModelBinding;
using System.Web.Services;


namespace Wopi_Service
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。
    // [System.Web.Script.Services.ScriptService]
    public class WopiHelper : System.Web.Services.WebService
    {

        [WebMethod]
        public string Get_Link(string name)
        {
            Link link = GetLink(name);

            return link.Url;
        }

        [WebMethod]
        public bool UploadFile(string fileNmae, byte[] documentData)
        {
            bool isSuccessed = false;

            try
            {
                string file = HostingEnvironment.MapPath(Constant.App_Data + "/" + fileNmae);
                using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.ReadWrite))
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
    }

}