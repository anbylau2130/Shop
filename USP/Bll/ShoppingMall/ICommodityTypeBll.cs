using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll.ShoppingMall
{
    public interface ICommodityTypeBll
    {
        AjaxResult Active(int id, long @operator);
        AjaxResult Add(CommodityType model);
        AjaxResult Cancel(int id, long @operator);
        AjaxResult Edit(CommodityType model, long @operator);
        List<CommodityType> GetAll();
        List<SelectOption> GetCommodityTypeList(long id);
        CommodityType GetModelById(long id);
        AjaxResult IsExisName(long id, string name);
        List<TreeNode> GetTree();
        AjaxResult Delete(long iD);
        //List<UP_ShowCommodityType_Result> GetAll(int? pageIndex, int? pageSize, string whereStr, string strOrder, string strOrderType);
    }
}