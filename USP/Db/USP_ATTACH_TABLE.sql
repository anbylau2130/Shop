
/*
	商品类型类型
*/
CREATE  TABLE  ShopCommodityType
(
	ID bigint not null identity(0,1),--唯一标识
	Name varchar(100) not null,--名字
	Reserve varchar(250) null,--保留
	Remark varchar(250) null,--备注
	Creator bigint not null,--创建人
	CreateTime datetime not null default(getdate()),--创建时间
	Auditor bigint,--审核人
	AuditTime datetime,--审核时间
	Canceler bigint,--注销人
	CancelTime datetime,--注销时间
	CONSTRAINT PK_ShopCommodityType_ID primary key(ID),
	CONSTRAINT UK_ShopCommodityType_Name unique(Name)
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
CONSTRAINT UK_SysDictionary_Name unique(Name),
constraint FK_SysDictionary_Parent foreign key(Parent) references SysDictionary(ID)
)