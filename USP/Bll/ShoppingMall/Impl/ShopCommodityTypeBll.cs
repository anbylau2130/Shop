using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Dal.ShoppingMall;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll.ShoppingMall.Impl
{
    public class ShopCommodityTypeBll : IShopCommodityTypeBll
    {

        IShopCommodityTypeDal dal;

        public ShopCommodityTypeBll(IShopCommodityTypeDal dal)
        {
            this.dal = dal;
           
        }
        public AjaxResult Active(int id, long @operator)
        {
             AjaxResult result = new AjaxResult();

            if (dal.Enable(id, @operator) > 0)
            {
                result.flag = true;
                result.message = "激活成功！";
            }
            else
            {
                result.flag = false;
                result.message = "激活失败！";
            }
            return result;
        }

        public bool Add(ShopCommodityType model)
        {
            return dal.Add(model);
        }

        public AjaxResult Cancel(int id, long @operator)
        {
            AjaxResult result = new AjaxResult();

            if (dal.Cancel(id, @operator) > 0)
            {
                result.flag = true;
                result.message = "注销成功！";
            }
            else
            {
                result.flag = false;
                result.message = "注销失败！";
            }
            return result;
        }

        public bool Edit(ShopCommodityType model,long @operator)
        {
            return dal.Edit(model, @operator);
        }

        public List<ShopCommodityType> GetAll()
        {
            return dal.GetAll();
        }

        public List<SelectOption> GetShopCommodityTypeList(long id)
        {
            var entity = dal.GetAll();
            List<SelectOption> list = new List<SelectOption>();
            foreach (var v in entity)
            {
                var temp = new SelectOption()
                {
                    id = v.ID.ToString(),
                    text = v.Name,
                    selected = v.ID == id
                };
                list.Add(temp);
            }
            return list;
        }

        public ShopCommodityType GetModelById(long id)
        {
            return dal.GetModelById(id);
        }
       
        public AjaxResult IsExisName(int id, string name)
        {
            AjaxResult result = new AjaxResult();
            if (dal.IsExisName(id, name))
            {
                result.flag = true;
                result.message = "已经存在商品类型名称！";
            }
            else
            {
                result.flag = false;
                result.message = "";
            }
            return result;
        }
    }
}
