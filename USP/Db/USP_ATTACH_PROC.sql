Create PROC UP_ShowShopCommodityType
@PageIndex int = 1,      
@PageSize int = 10,   
@WhereStr nvarchar(200)='',   
@strOrder varchar(MAX) = '',      
@strOrderType varchar(max) = 'ASC' 
AS
IF 1=0 BEGIN    
 SET FMTONLY OFF    
END    
SELECT * from(      
    select convert(bigint, ROW_NUMBER() OVER(order by ID asc)) as RowNo,      
    convert(bigint, COUNT(0) OVER()) as RowCnt, *      
    FROM(
         SELECT [ID]
      ,[Name]
      ,[Reserve]
      ,[Remark]
      ,[Creator]
      ,[CreateTime]
      ,[Auditor]
      ,[AuditTime]
      ,[Canceler]
      ,[CancelTime]
  FROM [ShopCommodityType]
		) AS a where 1=1   
	) AS temp
WHERE RowNo BETWEEN (@PageIndex-1)*@PageSize+1 AND @PageIndex*@PageSize

go

ALTER PROC [dbo].[UP_ShowCommodity]
@PageIndex int = 1,      
@PageSize int = 10,   
@WhereStr nvarchar(200)='',   
@strOrder varchar(MAX) = '',      
@strOrderType varchar(max) = '' 
AS
IF 1=0 BEGIN    
 SET FMTONLY OFF    
END    
SELECT * from(      
    select convert(bigint, ROW_NUMBER() OVER(order by a.CreateTime desc)) as RowNo,      
    convert(bigint, COUNT(0) OVER()) as RowCnt, *      
    FROM(
		      SELECT  c.[ID]
			  ,[Code]
			  ,c.[Name]
			  ,[Picture]
			  ,[DetailLink]
			  ,tpe.Name as [Type]
			  ,[TBKLink]
			  ,[Price]
			  ,[MonthOrder]
			  ,[IncomeRate]
			  ,[Commission]
			  ,[SellerId]
			  ,[SellerWangWangName]
			  ,[ShopName]
			  , dic.Name as [PlatformName]
			  ,[CouponId]
			  ,[CouponCount]
			  ,[CouponLeft]
			  ,[CouponDenomination]
			  ,[CouponBeginTime]
			  ,[CouponEndTime]
			  ,[CouponLink]
			  ,[PromotionLink]
			  ,c.[Reserve]
			  ,c.[Remark]
			  ,c.[Creator]
			  ,c.[CreateTime]
			  ,c.[Auditor]
			  ,c.[AuditTime]
			  ,c.[Canceler]
			  ,c.[CancelTime]
			  FROM [Commodity] c 
			  LEFT JOIN  SysDictionary dic ON c.PlatformName=dic.ID
			  LEFT JOIN  CommodityType tpe ON c.Type=tpe.ID
		) AS a 
	) AS temp  
WHERE RowNo BETWEEN (@PageIndex-1)*@PageSize+1 AND @PageIndex*@PageSize

go