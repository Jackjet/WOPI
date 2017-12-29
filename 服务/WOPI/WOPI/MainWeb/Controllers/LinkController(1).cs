using MainWeb.Helpers;
using MainWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;

namespace MainWeb.Controllers
{

    [Obsolete("use Api/Link controller instead", false)]
    public class LinkController : Controller
    {
        //
        // GET: /Link/

      
        public ActionResult GetLink(FileRequest fileRequest)
        {
            if (ModelState.IsValid)
            {
               
                WopiAppHelper wopiHelper = new WopiAppHelper(HostingEnvironment.MapPath(Constant.appDiscoveryXml));

                var result = wopiHelper.GetDocumentLink(fileRequest.name);
                
                var rv = new Link
                {
                    Url = result
                };
                return Json(rv, JsonRequestBehavior.AllowGet);
            }

            throw new ApplicationException("Invalid ModelState");
        }

    }


}
