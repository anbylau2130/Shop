using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using USP.Attributes;
using USP.Bll;
using USP.Bll.ShoppingMall;
using USP.Common;
using USP.Context;
using USP.Controllers;
using USP.Models.Entity;
using USP.Models.POCO;
using USP.Utility;

namespace USP.Areas.ShoppingMall.Controllers
{
    public class ComodityController : SysPrivilegeController
    {
        ISysDictionaryBll dictionaryBll;
        ICommodityBll commodityBll;
        ICommodityTypeBll comodityTypeBll;
        public ComodityController(ICommodityBll comodityBll, ICommodityTypeBll comodityTypeBll, ISysDictionaryBll dictionaryBll)
        {
            this.comodityTypeBll = comodityTypeBll;
            this.commodityBll = comodityBll;
            this.dictionaryBll = dictionaryBll;
        }
        [MenuItem(Parent = "商城管理", Name = "商品信息", Icon = "glyphicon glyphicon-info-sign")]
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
            var currentUser = HttpContext.Session[Constants.USER_KEY] as User;
            var result = commodityBll.GetAll(page, rows, wherestr, "", "");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [Privilege(Menu = "商品信息", Name = "新增")]
        public ActionResult Add()
        {
            var currentUser = HttpContext.Session[Constants.USER_KEY] as USP.Models.POCO.User;
            Commodity model = new Commodity();
            model.Creator = currentUser.SysOperator.ID;
            model.CreateTime = DateTime.Now;
            model.CouponBeginTime = DateTime.Now;
            model.CouponEndTime = DateTime.Now;
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(Commodity model)
        {
            var currentUser = HttpContext.Session[Constants.USER_KEY] as USP.Models.POCO.User;
            if (ModelState.IsValid)
            {
                AjaxResult result = commodityBll.Add(model);
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


        [Privilege(Menu = "商品信息", Name = "修改")]
        public ActionResult Edit(string id)
        {
            long idParse;
            Commodity model = new Commodity();
            if (long.TryParse(id, out idParse))
            {
                model = commodityBll.GetModelById(idParse);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Commodity model)
        {
            var currentUser = HttpContext.Session[Constants.USER_KEY] as USP.Models.POCO.User;
            model.Creator = currentUser.SysOperator.ID;
            model.CreateTime = DateTime.Now;
            model.Auditor = null;
            model.AuditTime = null;
            AjaxResult result = new AjaxResult();
            if (ModelState.IsValid)
            {
                result = commodityBll.Edit(model, currentUser.SysOperator.ID);
                if (result.flag)
                {
                    TempData["isSuccess"] = "true";
                    TempData["MessageInfo"] = "完善信息成功!";
                    return View("Index");
                }
            }
            TempData["isSuccess"] = result.flag;
            TempData["MessageInfo"] = result.message;
            return View(model);
        }


        [Privilege(Menu = "商品信息", Name = "注销")]
        [HttpPost]
        public ActionResult Cancel(int id)
        {
            return Json(commodityBll.Cancel(id, ((User)HttpContext.Session[Common.Constants.USER_KEY]).SysOperator.ID));
        }

        [Privilege(Menu = "商品信息", Name = "激活")]
        [HttpPost]
        public ActionResult Active(int id)
        {
            return Json(commodityBll.Active(id, ((User)HttpContext.Session[Common.Constants.USER_KEY]).SysOperator.ID));
        }

        [Privilege(Menu = "商品信息", Name = "导入Excel")]
        [HttpPost]
        public ActionResult ExcelImport()
        {
            HttpPostedFileBase fileUpload = Request.Files[0];
            ControllerContext.HttpContext.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
            ControllerContext.HttpContext.Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
            ControllerContext.HttpContext.Response.Charset = "UTF-8";


            AjaxResult result = new AjaxResult();
            var fileName = Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
            if (UploadUtil.UploadFile(fileUpload, Server.MapPath("~/Upload/"), fileName))
            {
                DataTable dt = NPOIHelper.Import(Server.MapPath("~/Upload/") + fileName);
                //初始化创建人，创建时间列,促销类型，优惠券条件
                var creatorCol = new DataColumn("Creator", typeof(long));
                var createTimeCol = new DataColumn("CreateTime", typeof(DateTime));
                var PromotionTypeCol = new DataColumn("PromotionType", typeof(long));
                var CouponConditionCol=new DataColumn("CouponCondition", typeof(decimal));
                var CouponPriceCol = new DataColumn("CouponPrice", typeof(decimal));
                creatorCol.DefaultValue = 0;
                createTimeCol.DefaultValue = DateTime.Now;
                CouponPriceCol.DefaultValue = 0;
                CouponConditionCol.DefaultValue = 0;
                PromotionTypeCol.DefaultValue = 0;

                dt.Columns.Add(creatorCol);
                dt.Columns.Add(createTimeCol);
                dt.Columns.Add(CouponPriceCol);
                dt.Columns.Add(CouponConditionCol);
                dt.Columns.Add(PromotionTypeCol);
  

                //处理 平台 和类目
                var plates = dictionaryBll.GetSubTreeNodesByName("平台类型");
                var types = comodityTypeBll.GetAll();
                var dicPromotionType= dictionaryBll.GetSubTreeNodesByName("促销类型");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        var ptlx = plates.Where(x => x.Name == dt.Rows[i]["平台类型"].ToString()).ToList();
                        if (ptlx != null)
                        {
                            dt.Rows[i]["平台类型"] = ptlx.First().ID;
                        }
                        //获取一级类目
                        var splm = types.Where(x => x.Name == dt.Rows[i]["商品一级类目"].ToString()).ToList(); //父类型
                        if (splm != null&& splm.Count>0)
                        {
                            dt.Rows[i]["商品一级类目"] = splm.First().Parent;
                        }
                        else
                        {
                            dt.Rows[i]["商品一级类目"] = 13; //其他
                        }
                        //获取优惠券金额
                        var couponPrice = string.Empty;
                        if (dt.Rows[i]["优惠券面额"].ToString().Contains("元无条件券"))
                        {
                            couponPrice = dt.Rows[i]["优惠券面额"].ToString().TrimEnd("元无条件券".ToCharArray());
                            dt.Rows[i]["CouponPrice"] = decimal.Parse(couponPrice);
                            dt.Rows[i]["CouponCondition"] = 0;
                        }
                        else
                        {
                            var  priceArray = dt.Rows[i]["优惠券面额"].ToString().TrimStart('满').TrimEnd('元').Split(new string[] { "元减" }, StringSplitOptions.None).ToList();
                            couponPrice = priceArray[1].ToString();
                            dt.Rows[i]["CouponPrice"] = decimal.Parse(couponPrice);
                            dt.Rows[i]["CouponCondition"] = decimal.Parse(priceArray[0].ToString());
                        }
                        var truePrice = decimal.Parse(dt.Rows[i]["商品价格(单位：元)"].ToString()) - decimal.Parse(dt.Rows[i]["CouponPrice"].ToString());
                        if (truePrice <= 10)
                        {
                            dt.Rows[i]["PromotionType"] = dicPromotionType.Where(x => x.Name == "9块9包邮").First().ID;
                        }
                        if(truePrice <= 20 && truePrice > 10)
                        {
                            dt.Rows[i]["PromotionType"] = dicPromotionType.Where(x => x.Name == "19块9包邮").First().ID;
                        }
                        if (truePrice <= 30 && truePrice > 20)
                        {
                            dt.Rows[i]["PromotionType"] = dicPromotionType.Where(x => x.Name == "29块9包邮").First().ID;
                        }
                        if (truePrice <= 40 && truePrice > 30)
                        {
                            dt.Rows[i]["PromotionType"] = dicPromotionType.Where(x => x.Name == "39块9包邮").First().ID;
                        }
                        if (truePrice <= 50 && truePrice >40)
                        {
                            dt.Rows[i]["PromotionType"] = dicPromotionType.Where(x => x.Name == "49块9包邮").First().ID;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogUtil.Exception("error", ex);

                    }
                }
              

                using (var bulkCopy = new SqlBulkCopy(new USPEntities().Database.Connection.ConnectionString))
                {
                    bulkCopy.BatchSize = dt.Rows.Count;
                    bulkCopy.DestinationTableName = "Commodity";
                    bulkCopy.ColumnMappings.Add("商品id", "Code");
                    bulkCopy.ColumnMappings.Add("商品名称", "Name");
                    bulkCopy.ColumnMappings.Add("商品主图", "Picture");
                    bulkCopy.ColumnMappings.Add("商品详情页链接地址", "DetailLink");
                    bulkCopy.ColumnMappings.Add("商品一级类目", "Type");
                    bulkCopy.ColumnMappings.Add("淘宝客链接", "TBKLink");
                    bulkCopy.ColumnMappings.Add("商品价格(单位：元)", "Price");
                    bulkCopy.ColumnMappings.Add("商品月销量", "MonthOrder");
                    bulkCopy.ColumnMappings.Add("收入比率(%)", "IncomeRate");
                    bulkCopy.ColumnMappings.Add("佣金", "Commission");
                    bulkCopy.ColumnMappings.Add("卖家旺旺", "SellerWangWangName");
                    bulkCopy.ColumnMappings.Add("卖家id", "SellerId");
                    bulkCopy.ColumnMappings.Add("店铺名称", "ShopName");
                    bulkCopy.ColumnMappings.Add("平台类型", "PlatformName");
                    bulkCopy.ColumnMappings.Add("优惠券id", "CouponId");
                    bulkCopy.ColumnMappings.Add("优惠券总量", "CouponCount");
                    bulkCopy.ColumnMappings.Add("优惠券剩余量", "CouponLeft");
                    bulkCopy.ColumnMappings.Add("优惠券面额", "CouponDenomination");
                    bulkCopy.ColumnMappings.Add("优惠券开始时间", "CouponBeginTime");
                    bulkCopy.ColumnMappings.Add("优惠券结束时间", "CouponEndTime");
                    bulkCopy.ColumnMappings.Add("优惠券链接", "CouponLink");
                    bulkCopy.ColumnMappings.Add("商品优惠券推广链接", "PromotionLink");

                    bulkCopy.ColumnMappings.Add("CouponPrice", "CouponPrice");
                    bulkCopy.ColumnMappings.Add("PromotionType", "PromotionType");
                    bulkCopy.ColumnMappings.Add("CouponCondition", "CouponCondition");
                    bulkCopy.ColumnMappings.Add("Creator", "Creator");
                    bulkCopy.ColumnMappings.Add("CreateTime", "CreateTime");
                    try
                    {
                        bulkCopy.WriteToServer(dt);
                        result.flag = true;
                        result.message = "导入成功!";
                    }
                    catch (Exception ex)
                    {
                        LogUtil.Exception("error", ex);
                        result.flag = false;
                        result.message = ex.Message;
                    }
                }
            }
            return Json(result);
        }
        public ActionResult ExcelImport(string id)
        {
            return View();
        }


    }
}
