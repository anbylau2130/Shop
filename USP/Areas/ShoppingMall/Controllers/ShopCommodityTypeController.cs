using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class ShopCommodityTypeController : SysPrivilegeController
    {
        IShopCommodityTypeBll shopCommodityTypeBll;
        public ShopCommodityTypeController(IShopCommodityTypeBll CommodityType)
        {
            this.shopCommodityTypeBll = CommodityType;
        }
        [MenuItem(Parent = "商城管理", Name = "商品类型管理", Icon = "glyphicon glyphicon-info-sign")]
        // GET: Supplier/Order
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
                case "indexdatagrid":
                    return IndexGetDataGrid();//列表页数据
                default:
                    return Content("");
            }
        }
        private ActionResult IndexGetDataGrid()
        {
            string wherestr = string.Empty;

            int page;
            if (!int.TryParse(Request["page"], out page))
            {
                page = 1;
            }
            int rows;
            if (!int.TryParse(Request["rows"], out rows))
            {
                rows = 10;
            }
            string type = Request["type"];
            string name = Request["name"];
            if (!string.IsNullOrEmpty(type))
            {
                wherestr += "  and [" + type + "] like '%" + name + "%' ";
            }
            var result = shopCommodityTypeBll.GetAll(page, rows, wherestr.ToString(), "", "");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [Privilege(Menu = "商品类型管理", Name = "新增")]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(ShopCommodityType model)
        {
            var currentUser = HttpContext.Session[Constants.USER_KEY] as USP.Models.POCO.User;
            
            if (shopCommodityTypeBll.IsExisName(model.ID, model.Name).flag)
            {
                TempData["returnMsgType"] = "error";
                TempData["returnMsg"] = "已存在相同的类型！";
                return View(model);
            }

            if (ModelState.IsValid)
            {
                model.CreateTime = DateTime.Now;
                model.Creator = currentUser.SysOperator.ID;

                AjaxResult result = shopCommodityTypeBll.Add(model);

                if (result.flag)
                {
                    TempData["returnMsgType"] = "success";
                    TempData["returnMsg"] = "添加成功";
                    return View("Index");
                }
                else
                {
                    TempData["returnMsgType"] = "error";
                    TempData["returnMsg"] = result.message;
                }
               
            }

            return View(model);
        }
        [Privilege(Menu = "商品类型管理", Name = "修改")]
        public ActionResult Edit(string id)
        {
            long idParse;
            ShopCommodityType model = new ShopCommodityType();
            if (long.TryParse(id, out idParse))
            {
                model = shopCommodityTypeBll.GetModelById(idParse);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(ShopCommodityType model)
        {
            var currentUser = HttpContext.Session[Constants.USER_KEY] as USP.Models.POCO.User;
            model.Creator = currentUser.SysOperator.ID;
            model.CreateTime = DateTime.Now;
            model.Auditor = null;
            model.AuditTime = null;
            if (shopCommodityTypeBll.IsExisName(model.ID, model.Name).flag)
            {
                TempData["returnMsgType"] = "error";
                TempData["returnMsg"] = "已存在相同的类型！";
                return View(model);
            }
            if (ModelState.IsValid)
            {
                var result = shopCommodityTypeBll.Edit(model, currentUser.SysOperator.ID);
                TempData["isSuccess"] = result.flag.ToString();
                if (result.flag)
                {
                    TempData["MessageInfo"] = "完善信息成功!";
                    return View("Index");
                }
                else
                {
                    TempData["MessageInfo"] = result.message;
                }
            }else
            {
                ModelState.AddModelError("Name", "完善信息失败!");
            }
            return View(model);
        }




        [Privilege(Menu = "商品类型管理", Name = "注销")]
        [HttpPost]
        public ActionResult Cancel(int id)
        {
            return Json(shopCommodityTypeBll.Cancel(id, ((User)HttpContext.Session[Common.Constants.USER_KEY]).SysOperator.ID));
        }

        [Privilege(Menu = "商品类型管理", Name = "激活")]
        [HttpPost]
        public ActionResult Active(int id)
        {
            return Json(shopCommodityTypeBll.Active(id, ((User)HttpContext.Session[Common.Constants.USER_KEY]).SysOperator.ID));
        }


    }

}
