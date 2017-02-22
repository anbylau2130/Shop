using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Context;
using USP.Models.Entity;
using USP.Models.POCO;
using USP.Utility;

namespace USP.Dal.ShoppingMall.Impl
{
    public class ShopCommodityTypeDal : IShopCommodityTypeDal
    {
        USPEntities db = new USPEntities();
        public ProcResult Add(ShopCommodityType model)
        {
            ProcResult result=new ProcResult();
            try
            {
                db.ShopCommodityType.Add(model);
                result.IsSuccess = db.SaveChanges()>0;
            }
            catch (Exception ex)
            {
                result.ProcMsg = ex.InnerException.Message;
                LogUtil.Exception("ExceptionLogger", ex);
            }
            return result ;
        }

        public ProcResult Auditor(ShopCommodityType ShopCommodityType, long auditor)
        {
            ProcResult result = new ProcResult();
           
            try
            {
                var entity = GetModelById(ShopCommodityType.ID);
                if (entity != null)
                {
                    entity.Auditor = auditor;
                    entity.AuditTime = DateTime.Now;
                    entity.Creator = auditor;
                }
                db.Entry<ShopCommodityType>((ShopCommodityType)entity).State = System.Data.Entity.EntityState.Modified;
                result.IsSuccess = db.SaveChanges()>0;
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
                db.Entry<ShopCommodityType>((ShopCommodityType)entity).State = System.Data.Entity.EntityState.Modified;
                result.IsSuccess = db.SaveChanges()>0;
            }
            catch (Exception ex)
            {
                result.ProcMsg = ex.InnerException.Message;
                LogUtil.Exception("ExceptionLogger", ex);
               
            }
            return result;
        }

        public ProcResult Edit(ShopCommodityType model, long currentOperator)
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
                db.Entry<ShopCommodityType>((ShopCommodityType)entity).State = System.Data.Entity.EntityState.Modified;
                result.IsSuccess = db.SaveChanges()>0;
            }
            catch (Exception ex)
            {
                result.ProcMsg = ex.InnerException.Message;
                LogUtil.Exception("ExceptionLogger", ex);
            }
            return result ;
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
                    entity.Creator = currentOperator;
                    entity.CreateTime = DateTime.Now;
                }
                db.Entry<ShopCommodityType>((ShopCommodityType)entity).State = System.Data.Entity.EntityState.Modified;
                result.IsSuccess = db.SaveChanges()>0;
            }
            catch (Exception ex)
            {
                result.ProcMsg = ex.InnerException.Message;
                LogUtil.Exception("ExceptionLogger", ex);
            }
            return result;
        }

        public ShopCommodityType GetModelById(long ID)
        {
            return db.ShopCommodityType.FirstOrDefault(x => x.ID == ID);
        }

        public List<UP_ShowShopCommodityType_Result> GetAll(int? pageIndex, int? pageSize, string whereStr, string strOrder, string strOrderType)
        {
            try
            {
                return (from i in db.UP_ShowShopCommodityType(pageIndex, pageSize, whereStr, strOrder, strOrderType).ToList()
                        select i).ToList();
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return new List<UP_ShowShopCommodityType_Result>();
            }
        }




        public List<ShopCommodityType> GetAll()
        {
            return db.ShopCommodityType.Where(x => x.Canceler == null).ToList();
        }

        public bool IsExisName(int id, string name)
        {
            return db.ShopCommodityType.Where(x => x.ID != id && x.Name == name).Count() > 0 ? true : false;
        }

    }
}
