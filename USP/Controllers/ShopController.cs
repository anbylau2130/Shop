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

        public ActionResult Search(string keyWord,int pageIndex,int pageSize)
        {
            if(filterSql(keyWord))
            {
                StringBuilder whereStr = new StringBuilder();
                whereStr.Append(" c.Name like '%").Append(keyWord).Append("%'");
                var result = comodityBll.GetAll(pageIndex, pageSize, whereStr.ToString(), " CreateTime ", " DESC ");
                return Json(result.rows);
            }
            return Json(new object());
        }
      

        public ActionResult GetCommodities()
        {
            int pageIndex = -1;    //页索引
            int pageSize = -1;     //页大小
            long commodityType = -1;//商品分类
            long promotionType =-1;//促销类型
            StringBuilder whereFilter = new StringBuilder();
            var keyWord = Request["keyWord"].ToString();

            var pageIndexStr = Request["pageIndex"].ToString();
            var pageSizeStr = Request["pageSize"].ToString();
            var commodityTypeStr = Request["commodityType"].ToString();
            var promotionTypeStr = Request["promotionType"].ToString();

            if (!int.TryParse(pageIndexStr, out pageIndex) || pageIndex <0 ||
                !int.TryParse(pageSizeStr, out pageSize) || pageSize < 0 )
            {
                return Json(new AjaxResult()
                {
                     flag=false,
                     message="参数非法"
                });
            }
            if (commodityTypeStr != string.Empty)
            {
                if ((long.TryParse(commodityTypeStr, out commodityType) && commodityType >= 0)|| commodityTypeStr == string.Empty)
                {
                    whereFilter.Append(" and c.Type=" + commodityType);
                }
            }
            if (promotionTypeStr != string.Empty)
            {
                if ((long.TryParse(promotionTypeStr, out promotionType) && promotionType >= 0)|| promotionTypeStr == string.Empty)
                {
                    whereFilter.Append(" and c.PromotionType=" + promotionType);
                }
            }
           
            if (filterSql(keyWord) && !string.IsNullOrEmpty( keyWord))
            {
                //var arr= keyWord.ToArray();
                //whereFilter.Append(" and ( ");
                //for (int i=0;i<arr.Length;i++)
                //{
                //    if (i != 0)
                //    {
                //        whereFilter.Append(" or ");
                //    }
                //    whereFilter.Append(" and  c.Name like '%").Append(arr[i]).Append("%' ");
                //}
                //whereFilter.Append(")");
                whereFilter.Append(" and  c.Name like '%").Append(keyWord).Append("%' ");
            }

            var result = comodityBll.GetAll(pageIndex, pageSize, whereFilter.ToString(), " CreateTime "," DESC ");
            return Json(result.rows);
        }
        private bool filterSql(string sSql)
        {
            int srcLen, decLen = 0;
            sSql = sSql.ToLower().Trim();
            srcLen = sSql.Length;
            sSql = sSql.Replace("exec", "");
            sSql = sSql.Replace("delete", "");
            sSql = sSql.Replace("master", "");
            sSql = sSql.Replace("truncate", "");
            sSql = sSql.Replace("declare", "");
            sSql = sSql.Replace("create", "");
            sSql = sSql.Replace("xp_", "no");
            decLen = sSql.Length;
            if (srcLen == decLen)
                return true;
            else
                return false;
        }

    }
}