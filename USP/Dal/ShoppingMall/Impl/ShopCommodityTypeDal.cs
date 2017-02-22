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
        public bool Add(ShopCommodityType model)
        {
            int result;
            try
            {
                db.ShopCommodityType.Add(model);
                result = db.SaveChanges();
            }
            catch (Exception ex)
            {
                result = 0;
                LogUtil.Exception("ExceptionLogger", ex);
            }
            return result > 0 ? true : false;
        }

        public int Auditor(ShopCommodityType ShopCommodityType, long auditor)
        {
            int result;
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
                result = db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                result = -1;
            }
            return result;
        }


        public int Cancel(long id, long currentOperator)
        {
            int result;
            try
            {
                var entity = GetModelById(id);
                if (entity != null)
                {
                    entity.Canceler = currentOperator;
                    entity.CancelTime = DateTime.Now;
                }
                db.Entry<ShopCommodityType>((ShopCommodityType)entity).State = System.Data.Entity.EntityState.Modified;
                result = db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                result = 0;
            }
            return result;
        }

        public bool Edit(ShopCommodityType model, long currentOperator)
        {
            int result;
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
                result = db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                result = 0;
            }
            return result > 0 ? true : false;
        }

        public int Enable(long id, long currentOperator)
        {
            int result;
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
                result = db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                result = 0;
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
