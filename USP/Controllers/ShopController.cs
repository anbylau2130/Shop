using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using USP.Bll;
using USP.Bll.ShoppingMall;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Controllers
{
    public class ShopController : Controller
    {

        ICommodityBll comodityBll;
        ICommodityTypeBll comodityTypeBll;
        ISysDictionaryBll dictionaryBll;
        public ShopController(ICommodityBll comodityBll, ICommodityTypeBll comodityTypeBll, ISysDictionaryBll dictionaryBll)
        {
            this.comodityBll = comodityBll;
            this.comodityTypeBll = comodityTypeBll;
            this.dictionaryBll = dictionaryBll;
        }
        // GET: Shop
        public ActionResult Index()
        {
            ViewBag.CommodityType = comodityTypeBll.GetAll().Where(x => x.Parent == 0 && x.ID != 0).ToList();
            ViewBag.LeftMenus = dictionaryBll.GetSubTreeNodesByName("促销类型");
            return View();
        }


        public ActionResult GetCommodities()
        {
            int pageIndex = -1;    //页索引
            int pageSize = -1;     //页大小
            long commodityType = -1;//商品分类
            long promotionType =-1;//促销类型
            StringBuilder whereFilter = new StringBuilder();
            if (!int.TryParse(Request["pageIndex"].ToString(), out pageIndex) || pageIndex <0 ||
                !int.TryParse(Request["pageSize"].ToString(), out pageSize) || pageSize < 0 )
            {
                return Json(new AjaxResult()
                {
                     flag=false,
                     message="参数非法"
                });
            }
            if (Request["commodityType"].ToString() != string.Empty)
            {
                if ((long.TryParse(Request["commodityType"].ToString(), out commodityType) && commodityType >= 0)
               || Request["commodityType"].ToString() == string.Empty)
                {
                    whereFilter.Append(" and c.Type=" + commodityType);
                }
            }
            if (Request["promotionType"].ToString() != string.Empty)
            {
                if ((long.TryParse(Request["promotionType"].ToString(), out promotionType) && promotionType >= 0)
                || Request["promotionType"].ToString() == string.Empty)
                {
                    whereFilter.Append(" and c.PlatformName=" + promotionType);
                }
            }
            
            var result = comodityBll.GetAll(pageIndex, pageSize, whereFilter.ToString(), " CreateTime "," DESC ");
            return Json(result.rows);
        }

       
    }
}