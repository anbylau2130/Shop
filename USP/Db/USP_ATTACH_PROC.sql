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
