using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Dal.ShoppingMall;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll.ShoppingMall.Impl
{
    public class CommodityTypeBll : ICommodityTypeBll
    {
        ICommodityTypeDal dal;
        public CommodityTypeBll(ICommodityTypeDal dal)
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

        public AjaxResult Add(CommodityType model)
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

        public AjaxResult Edit(CommodityType model, long @operator)
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

        public List<CommodityType> GetAll()
        {
            return dal.GetAll();
        }

        public List<SelectOption> GetCommodityTypeList(long id)
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

        public CommodityType GetModelById(long id)
        {
            return dal.GetModelById(id);
        }

        public List<TreeNode> GetTree()
        {
            List<TreeNode> resultTree = new List<TreeNode>();
            List<CommodityType> commodityTypeList = dal.GetAll();

            var parentList = commodityTypeList.Where(x => (x.Parent == 0 || x.Parent == null) && x.ID==0).ToList();

            foreach (var model in parentList)
            {
                TreeNode tn = new TreeNode();
                tn.id = model.ID;
                tn.text = model.Name;
                tn.attributes = new
                {
                     Group = model.Group,
                    Creator = model.Creator,
                    CreateTime = model.CreateTime,
                    Auditor = model.Auditor,
                    AuditTime = model.AuditTime,
                    Canceler = model.Canceler,
                    CancelTime = model.CancelTime,
                    Remark = model.Remark,
                    Reserve = model.Reserve
                };
                tn.children.AddRange(GetTree(model.ID, commodityTypeList));
                resultTree.Add(tn);
            }
            return resultTree;
        }
        public List<TreeNode> GetTree(long? id, List<CommodityType> allDict)
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
                    Group = item.Group,
                    Creator = item.Creator,
                    CreateTime = item.CreateTime,
                    Auditor = item.Auditor,
                    AuditTime = item.AuditTime,
                    Canceler = item.Canceler,
                    CancelTime = item.CancelTime,
                    Remark = item.Remark,
                    Reserve = item.Reserve
                };
                var temp = allDict.Where(x => x.Parent == item.ID &&x.ID!=0).ToList();
                if (temp.Count() > 0)
                {
                    tn.children.AddRange(GetTree(item.ID, allDict));
                }
                result.Add(tn);
            }
            return result;
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

        //public List<UP_ShowCommodityType_Result> GetAll(int? pageIndex, int? pageSize, string whereStr, string strOrder, string strOrderType)
        //{
        //    return dal.GetAll(pageIndex, pageSize, whereStr, strOrder, strOrderType);
        //}

    }
}