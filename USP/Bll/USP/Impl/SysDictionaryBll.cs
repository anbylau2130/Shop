using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Dal;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll.Impl
{

    public class SysDictionaryBll : ISysDictionaryBll
    {

        ISysDictionaryDal dal;

        public SysDictionaryBll(ISysDictionaryDal dal)
        {
            this.dal = dal;

        }
        public AjaxResult Active(int id, long @operator)
        {
            AjaxResult result = new AjaxResult();
            var procResult = dal.Enable(id, @operator);
            result.flag = procResult.IsSuccess;
            if (procResult.IsSuccess)
            {
                result.message = "激活成功！";
            }
            else
            {
                result.message = procResult.ProcMsg;
            }

            return result;
        }

        public AjaxResult Add(SysDictionary model)
        {
            AjaxResult result = new AjaxResult();
            var procResult = dal.Add(model);
            result.flag = procResult.IsSuccess;
            if (result.flag)
            {
                result.message = "新增成功！";
            }
            else
            {
                result.message = procResult.ProcMsg;
            }
            return result;

        }

        public AjaxResult Cancel(int id, long @operator)
        {
            AjaxResult result = new AjaxResult();
            var procResult = dal.Cancel(id, @operator);
            result.flag = procResult.IsSuccess;
            if (result.flag)
            {
                result.message = "注销成功！";
            }
            else
            {
                result.message = procResult.ProcMsg;
            }
            return result;
        }

        public AjaxResult Edit(SysDictionary model, long @operator)
        {
            AjaxResult result = new AjaxResult();
            var procResult = dal.Edit(model, @operator);
            result.flag = procResult.IsSuccess;
            if (result.flag)
            {
                result.message = "修改成功！";
            }
            else
            {
                result.message = procResult.ProcMsg;
            }
            return result;
        }

        public List<SysDictionary> GetAll()
        {
            return dal.GetAll();
        }

        public List<SelectOption> GetSysDictionaryList(long id)
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

        public SysDictionary GetModelById(long id)
        {
            return dal.GetModelById(id);
        }

        public AjaxResult IsExisName(long id, string name)
        {
            AjaxResult result = new AjaxResult();
            if (dal.IsExisName(id, name))
            {
                result.flag = true;
                result.message = "已经存在该名称！";
            }
            else
            {
                result.flag = false;
                result.message = "";
            }
            return result;
        }

        public List<TreeNode> GetDictTree()
        {
            List<TreeNode> resultTree = new List<TreeNode>();
            List<SysDictionary> dicList = dal.GetAll();

            var parentList = dicList.Where(x => (x.Parent == 0 || x.Parent == null) && x.ID == 0).ToList();

            foreach (var pdic in parentList)
            {
                TreeNode tn = new TreeNode();
                tn.id = pdic.ID;
                tn.text = pdic.Name;
                tn.attributes = new
                {
                    Type = pdic.Type,
                    Creator = pdic.Creator,
                    CreateTime = pdic.CreateTime,
                    Auditor = pdic.Auditor,
                    AuditTime = pdic.AuditTime,
                    Canceler = pdic.Canceler,
                    CancelTime = pdic.CancelTime,
                    Remark = pdic.Remark,
                    Reserve = pdic.Reserve
                };
                tn.children.AddRange(GetDictTree(pdic.ID, dicList));
                resultTree.Add(tn);
            }
            return resultTree;
        }
        public List<TreeNode> GetDictTree(long? id, List<SysDictionary> allDict)
        {
            List<TreeNode> result = new List<TreeNode>();

            var List = allDict.Where(x => x.Parent == id && x.ID != 0);
            foreach (var item in List)
            {
                TreeNode tn = new TreeNode();
                tn.id = item.ID;
                if (item.Canceler != null || item.CancelTime != null)
                {
                    tn.text = "(禁用)" + item.Name;
                }
                else
                {
                    tn.text = item.Name;
                }
                tn.attributes = new
                {
                    Type = item.Type,
                    Creator = item.Creator,
                    CreateTime = item.CreateTime,
                    Auditor = item.Auditor,
                    AuditTime = item.AuditTime,
                    Canceler = item.Canceler,
                    CancelTime = item.CancelTime,
                    Remark = item.Remark,
                    Reserve = item.Reserve
                };
                var temp = allDict.Where(x => x.Parent == item.ID && x.ID != 0).ToList();
                if (temp.Count() > 0)
                {
                    tn.children.AddRange(GetDictTree(item.ID, allDict));
                }
                result.Add(tn);
            }
            return result;
        }

        public AjaxResult Delete(long id)
        {
            AjaxResult result = new AjaxResult();
            var temp = dal.Delete(id);
            if (temp.IsSuccess)
            {
                result.flag = true;
                result.message = "删除成功！";
            }
            else
            {
                result.flag = false;
                result.message = temp.ProcMsg;
            }
            return result;
        }

        public List<SysDictionary> GetSubTreeNodesByName(string nodeName)
        {
            return dal.GetSubTreeNodesByName(nodeName);
        }


        //public List<UP_ShowSysDictionary_Result> GetAll(int? pageIndex, int? pageSize, string whereStr, string strOrder, string strOrderType)
        //{
        //    return dal.GetAll(pageIndex, pageSize, whereStr, strOrder, strOrderType);
        //}

    }
}