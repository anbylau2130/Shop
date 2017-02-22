using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Dal.ShoppingMall
{
    public interface IShopCommodityTypeDal
    {
         bool IsExisName(int id, string name);
         List<ShopCommodityType> GetAll();
        List<UP_ShowShopCommodityType_Result> GetAll(int? pageIndex, int? pageSize, string whereStr, string strOrder, string strOrderType);
        ShopCommodityType GetModelById(long ID);
        ProcResult Enable(long id, long currentOperator);
        ProcResult Edit(ShopCommodityType model, long currentOperator);
        ProcResult Cancel(long id, long currentOperator);

        ProcResult Auditor(ShopCommodityType ShopCommodityType, long auditor);
        ProcResult Add(ShopCommodityType model);
    }
}
