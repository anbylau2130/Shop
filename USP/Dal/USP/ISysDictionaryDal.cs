using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Dal
{
    public interface ISysDictionaryDal
    {
        bool IsExisName(long id, string name);
        List<SysDictionary> GetAll();
       // List<UP_SysDictionary_Result> GetAll(int? pageIndex, int? pageSize, string whereStr, string strOrder, string strOrderType);
        SysDictionary GetModelById(long ID);
        ProcResult Enable(long id, long currentOperator);
        ProcResult Edit(SysDictionary model, long currentOperator);
        ProcResult Cancel(long id, long currentOperator);

        ProcResult Auditor(SysDictionary model, long auditor);
        ProcResult Add(SysDictionary model);
        ProcResult Delete(long id);
        List<SysDictionary> GetSubTreeNodesByName(string nodeName);
    }
}