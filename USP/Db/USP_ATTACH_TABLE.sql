drop table CommodityType
create table CommodityType(

ID bigint not null identity(0,1),--唯一标识
Name [varchar](255)  not NULL,
Parent bigint  default(0),
[Group] varchar(50) null,--类型
Reserve varchar(250) null,--保留
Remark varchar(250) null,--备注

Creator bigint not null,--创建人
CreateTime datetime not null default(getdate()),--创建时间
Auditor bigint,--审核人
AuditTime datetime,--审核时间
Canceler bigint,--注销人
CancelTime datetime,--注销时间
CONSTRAINT PK_CommodityType_ID primary key(ID),
--CONSTRAINT UK_CommodityType_Name unique(Name),
--constraint FK_CommodityType_Parent foreign key(Parent) references CommodityType(ID)
)

create table Commodity(
ID bigint not null identity(0,1),--唯一标识
Code VARCHAR(255) NOT NULL, --商品id
Name varchar(255)  not NULL, --商品名称
Picture VARCHAR(400) NOT NULL,--商品主图
DetailLink VARCHAR(400) NOT NULL,-- 详细页链接
[Type] BIGINT NOT NULL, --商品一级类目
TBKLink VARCHAR(400) NOT NULL, --淘宝客链接
Price DECIMAL(36,2) NOT NULL ,--商品价格
MonthOrder BIGINT NOT NULL ,--月销量
IncomeRate DECIMAL(18,2) NOT NULL, --收入比率
Commission DECIMAL(18,2) NOT NULL, --佣金
SellerId VARCHAR(400) NOT NULL, --卖家ID
SellerWangWangName VARCHAR(255) NOT NULL , --卖家旺旺
ShopName VARCHAR(255) NOT NULL, --店铺名称
PlatformName VARCHAR(32) NOT NULL,--平台名称
CouponId VARCHAR(100) NOT NULL, --优惠券Id
CouponCount BIGINT NOT null, --优惠券总数
CouponLeft  BIGINT NOT NULL, --优惠券剩余数量
CouponDenomination VARCHAR(100) NOT NULL,--优惠券面额
CouponBeginTime DATETIME NOT NULL, --优惠券开始日期
CouponEndTime DATETIME NOT NULL, --优惠券结束日期
CouponLink VARCHAR(400) NOT NULL,--优惠券链接
PromotionLink VARCHAR(400) NOT NULL, --推广链接
Reserve varchar(250) null,--保留
Remark varchar(250) null,--备注
Creator bigint not null,--创建人
CreateTime datetime not null default(getdate()),--创建时间
Auditor bigint,--审核人
AuditTime datetime,--审核时间
Canceler bigint,--注销人
CancelTime datetime,--注销时间
CONSTRAINT PK_Commodity_ID primary key(ID),

)

CREATE TABLE [dbo].[Area](
	[ID] [bigint] NOT NULL,
	[Code] [varchar](20) NULL,
	[Parent] [varchar](20) NULL,
	[Name] [varchar](100) NULL,
	[Type] [int] NOT NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[SysBank](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Number] [bigint] NULL,
	[Url] [varchar](100) NULL,
	[Type] [int] NOT NULL,
	[Name] [varchar](50) NULL,
	[ShortName] [varchar](20) NULL,
	[NiceName] [varchar](20) NULL,
	[Status] [int] NULL,
	[Reserve] [varchar](250) NULL,
	[Remark] [varchar](250) NULL,
	[Creator] [bigint] NULL,
	[CreateTime] [datetime] NULL,
	[Auditor] [bigint] NULL,
	[AuditTime] [datetime] NULL,
	[Canceler] [bigint] NULL,
	[CancelTime] [datetime] NULL
) ON [PRIMARY]

GO

--drop table SysDictionary
create table SysDictionary(
ID bigint not null identity(0,1),--唯一标识
Name [varchar](255)  not NULL,
Parent bigint  default(0),
Reserve varchar(250) null,--保留
Remark varchar(250) null,--备注
Creator bigint not null,--创建人
Type varchar(50) null,--类型
CreateTime datetime not null default(getdate()),--创建时间
Auditor bigint,--审核人
AuditTime datetime,--审核时间
Canceler bigint,--注销人
CancelTime datetime,--注销时间
CONSTRAINT PK_SysDictionary_ID primary key(ID),
--CONSTRAINT UK_SysDictionary_Name unique(Name),
--constraint FK_SysDictionary_Parent foreign key(Parent) references SysDictionary(ID)
)