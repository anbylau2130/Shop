﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll.USP
{
    public interface ISysDictionaryBll
    {
        AjaxResult Active(int id, long @operator);
        AjaxResult Add(SysDictionary model);
        AjaxResult Cancel(int id, long @operator);
        AjaxResult Edit(SysDictionary model, long @operator);
        List<SysDictionary> GetAll();
        List<SelectOption> GetSysDictionaryList(long id);
        SysDictionary GetModelById(long id);
        AjaxResult IsExisName(long id, string name);
        //List<UP_ShowSysDictionary_Result> GetAll(int? pageIndex, int? pageSize, string whereStr, string strOrder, string strOrderType);
    }
}