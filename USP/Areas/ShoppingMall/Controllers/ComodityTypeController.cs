using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using USP.Attributes;
using USP.Bll.ShoppingMall;
using USP.Common;
using USP.Controllers;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Areas.ShoppingMall.Controllers
{
    [Menu(Name = "商城管理", Icon = "icon-building")]
    public class ComodityTypeController : SysPrivilegeController
    {
        ICommodityTypeBll commodityTypeBll;
        public ComodityTypeController(ICommodityTypeBll CommodityTypeBll)
        {
            this.commodityTypeBll = CommodityTypeBll;
        }
        [MenuItem(Parent = "商城管理", Name = "商品类型", Icon = "glyphicon glyphicon-info-sign")]
        // GET: ShoppingMall/ComodityType
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
                case "Tree":
                    return GetTree();
                default:
                    return Content("");
            }
        }

        private ActionResult GetTree()
        {
            return Json(commodityTypeBll.GetTree(), JsonRequestBehavior.AllowGet);
        }


        [Privilege(Menu = "商品类型", Name = "注销")]
        [HttpPost]
        public ActionResult Cancel(int id)
        {
            return Json(commodityTypeBll.Cancel(id, ((User)HttpContext.Session[Common.Constants.USER_KEY]).SysOperator.ID));
        }

        [Privilege(Menu = "商品类型", Name = "激活")]
        [HttpPost]
        public ActionResult Active(int id)
        {
            return Json(commodityTypeBll.Active(id, ((User)HttpContext.Session[Common.Constants.USER_KEY]).SysOperator.ID));
        }

        [Privilege(Menu = "商品类型", Name = "新增")]
        [HttpPost]
        public ActionResult Add(CommodityType model)
        {
            var currentUser = HttpContext.Session[Constants.USER_KEY] as USP.Models.POCO.User;
            model.CreateTime = DateTime.Now;
            model.Creator = currentUser.SysOperator.ID;
            AjaxResult result = new AjaxResult();
            if (ModelState.IsValid)
            {
                result = commodityTypeBll.Add(model);
            }
            return Json(result);
        }
        [Privilege(Menu = "商品类型", Name = "修改")]
        [HttpPost]
        public ActionResult Edit(CommodityType model)
        {
            var currentUser = HttpContext.Session[Constants.USER_KEY] as USP.Models.POCO.User;
            model.Creator = currentUser.SysOperator.ID;
            model.CreateTime = DateTime.Now;
            model.Auditor = null;
            model.AuditTime = null;
            var result = new AjaxResult();
            if (ModelState.IsValid)
            {
                result = commodityTypeBll.Edit(model, currentUser.SysOperator.ID);
            }
            return Json(result);
        }

        [Privilege(Menu = "商品类型", Name = "删除")]
        [HttpPost]
        public ActionResult Del(CommodityType model)
        {
            var currentUser = HttpContext.Session[Constants.USER_KEY] as USP.Models.POCO.User;
            var result = commodityTypeBll.Delete(model.ID);
            return Json(result);
        }
    }
}