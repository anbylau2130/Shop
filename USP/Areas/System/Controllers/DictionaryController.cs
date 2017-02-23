using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using USP.Attributes;

namespace USP.Areas.System.Controllers
{
    public class DictionaryController : Controller
    {
        [MenuItem(Parent = "系统设置", Name = "字典管理", Icon = "icon-sitemap")]
        // GET: System/SysDictionary
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string actionName)
        {
            return OtherAction(actionName);
        }

        private ActionResult OtherAction(string ac)
        {
            switch (ac)
            {
                case "DictTree":
                       
                default:
                    return Content("");
            }
        }
    }
}