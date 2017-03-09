using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Dal.ShoppingMall
{
    public interface ICommodityDal
    {
        bool IsExisName(long id, string name);
        List<Commodity> GetAll();
        List<UP_ShowCommodity_Result> GetAll(int? pageIndex, int? pageSize, string whereStr, string strOrder, string strOrderType);
        Commodity GetModelById(long ID);
        ProcResult Enable(long id, long currentOperator);
        ProcResult Edit(Commodity model, long currentOperator);
        ProcResult Cancel(long id, long currentOperator);

        ProcResult Auditor(Commodity model, long auditor);
        ProcResult Add(Commodity model);
    }
}
