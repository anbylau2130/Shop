using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll.ShoppingMall
{
    public interface ICommodityBll
    {
        AjaxResult Active(int id, long @operator);
        AjaxResult Add(Commodity model);
        AjaxResult Cancel(int id, long @operator);
        AjaxResult Edit(Commodity model, long @operator);
        //List<TreeNode> GetTree();
        List<Commodity> GetAll();
        List<SelectOption> GetCommodityList(long id);
        Commodity GetModelById(long id);
        AjaxResult IsExisName(long id, string name);
        DataGrid<UP_ShowCommodity_Result> GetAll(int? pageIndex, int? pageSize, string whereStr, string strOrder, string strOrderType);
    }
}
