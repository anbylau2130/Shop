using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll.ShoppingMall
{
   public interface IShopCommodityTypeBll
    {
         AjaxResult Active(int id, long @operator);
        bool Add(ShopCommodityType model);
        AjaxResult Cancel(int id, long @operator);
        bool Edit(ShopCommodityType model, long @operator);
        List<ShopCommodityType> GetAll();
        List<SelectOption> GetShopCommodityTypeList(long id);
        ShopCommodityType GetModelById(long id);
        AjaxResult IsExisName(int id, string name);
    }
}
