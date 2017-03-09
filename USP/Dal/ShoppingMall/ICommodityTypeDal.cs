using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Dal.ShoppingMall
{
    public interface ICommodityTypeDal
    {
        bool IsExisName(long id, string name);
        List<CommodityType> GetAll();
        //List<UP_CommodityType_Result> GetAll(int? pageIndex, int? pageSize, string whereStr, string strOrder, string strOrderType);
        CommodityType GetModelById(long ID);
        ProcResult Enable(long id, long currentOperator);
        ProcResult Edit(CommodityType model, long currentOperator);
        ProcResult Cancel(long id, long currentOperator);

        ProcResult Auditor(CommodityType model, long auditor);
        ProcResult Add(CommodityType model);
        ProcResult Delete(long id);

      
    }
}