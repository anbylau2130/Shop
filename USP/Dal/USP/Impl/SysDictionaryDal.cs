using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Context;
using USP.Models.Entity;
using USP.Models.POCO;
using USP.Utility;

namespace USP.Dal.Impl
{
    public class SysDictionaryDal : ISysDictionaryDal
    {
        USPEntities db = new USPEntities();
        public ProcResult Add(SysDictionary model)
        {
            ProcResult result = new ProcResult();
            try
            {
                db.SysDictionary.Add(model);
                result.IsSuccess = db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                result.ProcMsg = ex.InnerException.Message;
                LogUtil.Exception("ExceptionLogger", ex);
            }
            return result;
        }

        public ProcResult Auditor(SysDictionary model, long auditor)
        {
            ProcResult result = new ProcResult();

            try
            {
                var entity = GetModelById(model.ID);
                if (entity != null)
                {
                    entity.Auditor = auditor;
                    entity.AuditTime = DateTime.Now;
                    entity.Creator = auditor;
                }
                db.Entry<SysDictionary>((SysDictionary)entity).State = System.Data.Entity.EntityState.Modified;
                result.IsSuccess = db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                result.ProcMsg = ex.InnerException.Message;
                LogUtil.Exception("ExceptionLogger", ex);
            }
            return result;
        }


        public ProcResult Cancel(long id, long currentOperator)
        {
            ProcResult result = new ProcResult();
            try
            {
                var entity = GetModelById(id);
                if (entity != null)
                {
                    entity.Canceler = currentOperator;
                    entity.CancelTime = DateTime.Now;
                }
                db.Entry<SysDictionary>((SysDictionary)entity).State = System.Data.Entity.EntityState.Modified;
                result.IsSuccess = db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                result.ProcMsg = ex.InnerException.Message;
                LogUtil.Exception("ExceptionLogger", ex);

            }
            return result;
        }

        public ProcResult Edit(SysDictionary model, long currentOperator)
        {
            ProcResult result = new ProcResult();
            try
            {
                var entity = GetModelById(model.ID);
                if (entity != null)
                {
                    entity.Name = model.Name;
                    entity.Type = model.Type;
                    entity.Reserve = model.Reserve;
                    entity.Auditor = model.Auditor;
                    entity.AuditTime = model.AuditTime;
                    entity.Canceler = model.Canceler;
                    entity.CancelTime = model.CancelTime;
                    entity.Remark = model.Remark;
                    entity.Creator = currentOperator;
                    entity.CreateTime = DateTime.Now;
                }
                db.Entry<SysDictionary>((SysDictionary)entity).State = System.Data.Entity.EntityState.Modified;
                result.IsSuccess = db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                result.ProcMsg = ex.InnerException.Message;
                LogUtil.Exception("ExceptionLogger", ex);
            }
            return result;
        }

        public ProcResult Enable(long id, long currentOperator)
        {
            ProcResult result = new ProcResult();
            try
            {
                var entity = GetModelById(id);
                if (entity != null)
                {
                    entity.Canceler = null;
                    entity.CancelTime = null;
                    entity.Auditor = currentOperator;
                    entity.AuditTime = DateTime.Now;
                }
                db.Entry<SysDictionary>((SysDictionary)entity).State = System.Data.Entity.EntityState.Modified;
                result.IsSuccess = db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                result.ProcMsg = ex.InnerException.Message;
                LogUtil.Exception("ExceptionLogger", ex);
            }
            return result;
        }

        public SysDictionary GetModelById(long ID)
        {
            return db.SysDictionary.FirstOrDefault(x => x.ID == ID);
        }

        //public List<UP_ShowSysDictionary_Result> GetAll(int? pageIndex, int? pageSize, string whereStr, string strOrder, string strOrderType)
        //{
        //    try
        //    {
        //        return (from i in db.UP_ShowSysDictionary(pageIndex, pageSize, whereStr, strOrder, strOrderType).ToList()
        //                select i).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtil.Exception("ExceptionLogger", ex);
        //        return new List<UP_ShowSysDictionary_Result>();
        //    }
        //}




        public List<SysDictionary> GetAll()
        {
            return db.SysDictionary.ToList();
            //return db.SysDictionary.Where(x => x.Canceler == null).ToList();
        }

        public bool IsExisName(long id, string name)
        {
            return db.SysDictionary.Where(x => x.ID != id && x.Name == name).Count() > 0 ? true : false;
        }

        public ProcResult Delete(long id)
        {
            ProcResult result = new ProcResult();
            try
            {
                var entity = GetModelById(id);
                db.SysDictionary.Remove(entity);
                result.IsSuccess = db.SaveChanges() > 0;
                
            }
            catch (Exception ex)
            {
                result.ProcMsg = ex.InnerException.Message;
                LogUtil.Exception("ExceptionLogger", ex);
            }
            return result;
        }

        public List<SysDictionary> GetSubTreeNodesByName(string nodeName)
        {
            var pnode= db.SysDictionary.Where(x => x.Name == nodeName).First();
            if (pnode == null) {
                return new List<SysDictionary>();
            }
            return db.SysDictionary.Where(x => x.Parent == pnode.ID).ToList();
        }
    }


}