using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Context;
using USP.Models.Entity;
using USP.Models.POCO;
using USP.Utility;

namespace USP.Dal.ShoppingMall.Impl
{
    public class CommodityTypeDal : ICommodityTypeDal
    {
        USPEntities db = new USPEntities();
        public ProcResult Add(CommodityType model)
        {
            ProcResult result = new ProcResult();
            try
            {
                db.CommodityType.Add(model);
                result.IsSuccess = db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                result.ProcMsg = ex.InnerException.Message;
                LogUtil.Exception("ExceptionLogger", ex);
            }
            return result;
        }

        public ProcResult Auditor(CommodityType model, long auditor)
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
                db.Entry<CommodityType>((CommodityType)entity).State = System.Data.Entity.EntityState.Modified;
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
                db.Entry<CommodityType>((CommodityType)entity).State = System.Data.Entity.EntityState.Modified;
                result.IsSuccess = db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                result.ProcMsg = ex.InnerException.Message;
                LogUtil.Exception("ExceptionLogger", ex);

            }
            return result;
        }

        public ProcResult Edit(CommodityType model, long currentOperator)
        {
            ProcResult result = new ProcResult();
            try
            {
                var entity = GetModelById(model.ID);
                if (entity != null)
                {
                    entity.Name = model.Name;
                    entity.Remark = model.Remark;
                    entity.Creator = currentOperator;
                    entity.CreateTime = DateTime.Now;
                }
                db.Entry<CommodityType>((CommodityType)entity).State = System.Data.Entity.EntityState.Modified;
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
                db.Entry<CommodityType>((CommodityType)entity).State = System.Data.Entity.EntityState.Modified;
                result.IsSuccess = db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                result.ProcMsg = ex.InnerException.Message;
                LogUtil.Exception("ExceptionLogger", ex);
            }
            return result;
        }

        public CommodityType GetModelById(long ID)
        {
            return db.CommodityType.FirstOrDefault(x => x.ID == ID);
        }

        //public List<UP_ShowCommodityType_Result> GetAll(int? pageIndex, int? pageSize, string whereStr, string strOrder, string strOrderType)
        //{
        //    try
        //    {
        //        return (from i in db.UP_ShowCommodityType(pageIndex, pageSize, whereStr, strOrder, strOrderType).ToList()
        //                select i).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtil.Exception("ExceptionLogger", ex);
        //        return new List<UP_ShowCommodityType_Result>();
        //    }
        //}




        public List<CommodityType> GetAll()
        {
            return db.CommodityType.ToList();
            //return db.CommodityType.Where(x => x.Canceler == null).ToList();
        }

        public bool IsExisName(long id, string name)
        {
            return db.CommodityType.Where(x => x.ID != id && x.Name == name).Count() > 0 ? true : false;
        }

        public ProcResult Delete(long id)
        {
            ProcResult result = new ProcResult();
            try
            {
                var entity = GetModelById(id);
                db.CommodityType.Remove(entity);
                result.IsSuccess = db.SaveChanges() > 0;

            }
            catch (Exception ex)
            {
                result.ProcMsg = ex.InnerException.Message;
                LogUtil.Exception("ExceptionLogger", ex);
            }
            return result;
        }


    }



}