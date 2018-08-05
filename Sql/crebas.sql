

use [master]
/*==============================================================*/
/* DateBase: HTPS                                                */
/*==============================================================*/
CREATE DATABASE [HTPS] ON  PRIMARY 
( NAME = N'HTPS', FILENAME = N'E:\DATABASE\HTPS.mdf' , SIZE = 7168KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'HTPS_log', FILENAME = N'E:\DATABASE\HTPS_log.ldf' , SIZE = 11264KB , MAXSIZE = 4194304KB , FILEGROWTH = 10240KB )
 COLLATE Chinese_PRC_CI_AS

EXEC dbo.sp_dbcmptlevel @dbname=N'HTPS', @new_cmptlevel=90

go--这个go不能省


use [HTPS]


/*==============================================================*/
/* Table: User                                              */
/*==============================================================*/
create table [dbo].[User] (
   [Id]              int                  identity(1, 1),
   [Name]            nvarchar(20)         not null,
   [Phone]           varchar(20)          not null default(''),
   [Email]           varchar(100)         not null default(''),
   [Password]        varchar(50)          not null default('f14029217ff5e7a50cdc7e70f686cf29'),
   [Role]            int                  not null,
   [Signature]       nvarchar(200)        not null default(''),
   [IsSignNeeded]    bit                  not null default(1),
   [IsEnable]        bit                  not null default(1),
   [Creater]         int                  null,
   [CreatedDate]     datetime             not null constraint DF_User_CreatedDate default getdate(),
   [Modifier]        int                  null,
   [ModifiedDate]    datetime             not null constraint DF_User_ModifiedDate default getdate()
)
on "PRIMARY"

alter table [User]
    add constraint PK_User_Id primary key ([Id])
    on "PRIMARY"


/*==============================================================*/
/* Table: Role                                           */
/*==============================================================*/
create table [dbo].[Role] (
   [Id]              int                  identity(1, 1),
   [Name]            nvarchar(20)         not null,
   [Powers]          varchar(20)          null,
   [Description]     nvarchar(200)        not null default(''),
   [Creater]         int                  null,
   [CreatedDate]     datetime             not null constraint DF_Role_CreatedDate default getdate(),
   [Modifier]        int                  null,
   [ModifiedDate]    datetime             not null constraint DF_Role_ModifiedDate default getdate()
)
on "PRIMARY"

alter table [Role]
    add constraint PK_Role_Id primary key ([Id])
    on "PRIMARY"

/*==============================================================*/
/* Table: Power                                           */
/*==============================================================*/
create table [dbo].[Power] (
   [Id]              int                  identity(1, 1),
   [Name]            nvarchar(20)         not null,
   [Description]     nvarchar(200)        not null default('')
)
on "PRIMARY"

alter table [Power]
    add constraint PK_Power_Id primary key ([Id])
    on "PRIMARY"


/*==============================================================*/
/* Table: [Piece]                                          */
/*==============================================================*/
create table [dbo].[Piece] (
   [Id]                 int                  identity(1, 1),
   [Name]               nvarchar(20)         not null,
   [Number]             varchar(20)          not null,
   [Order]              varchar(20)          not null default(''),
   [Count]              int                  not null default(0),
   [PrintedCount]       int                  not null default(0),
   [IsPrinted]          bit                  not null default(0),
   [Ots]                nvarchar(20)         not null default(''),
   [DelegateNumber]     varchar(20)          not null default(''),
   [AccessoryFactory]   nvarchar(20)         not null default(''),
   [VehicleType]        nvarchar(20)         not null default(''),
   [TestContent]        nvarchar(20)         not null default(''),
   [SendPerson]         nvarchar(20)         not null default(''),
   [ChargePerson]       nvarchar(20)         not null default(''),
   [SendDate]           datetime             null,
   [Place]              nvarchar(20)         not null default(''),
   [IsEnable]           bit                  not null default(1),
   [Description]        nvarchar(200)        not null default(''),
   [Creater]            int                  not null,
   [CreatedDate]        datetime             not null constraint DF_Piece_CreatedDate default getdate(),
   [Modifier]           int                  not null,
   [ModifiedDate]       datetime             not null constraint DF_Piece_ModifiedDate default getdate()
)
on "PRIMARY"

alter table [Piece]
    add constraint PK_Piece_Id primary key ([Id])
    on "PRIMARY"


/*==============================================================*/
/* Table: [Department]                                          */
/*==============================================================*/
create table [dbo].[Department] (
   [Id]              int                  identity(1, 1),
   [Name]            nvarchar(20)         not null,
   [Description]     nvarchar(200)        not null default(''),
   [Creater]         int                  not null,
   [CreatedDate]     datetime             not null constraint DF_Department_CreatedDate default getdate(),
   [Modifier]        int                  not null,
   [ModifiedDate]    datetime             not null constraint DF_Department_ModifiedDate default getdate()
)
on "PRIMARY"

alter table [Department]
    add constraint PK_Department_Id primary key ([Id])
    on "PRIMARY"


/*==============================================================*/
/* Table: [Project]                                          */
/*==============================================================*/
create table [dbo].[Project] (
   [Id]              int                  identity(1, 1),
   [Name]            nvarchar(20)         not null,
   [IsEnable]        bit                  not null default(1),
   [Description]     nvarchar(200)        not null default(''),
   [Creater]         int                  not null,
   [CreatedDate]     datetime             not null constraint DF_Project_CreatedDate default getdate(),
   [Modifier]        int                  not null,
   [ModifiedDate]    datetime             not null constraint DF_Project_ModifiedDate default getdate()
)
on "PRIMARY"

alter table [Project]
    add constraint PK_Project_Id primary key ([Id])
    on "PRIMARY"


/*==============================================================*/
/* Table: [Oil]                                          */
/*==============================================================*/
create table [dbo].[Oil] (
   [Id]              int                  identity(1, 1),
   [Name]            nvarchar(20)         not null,
   [YellowRate]      float                not null default(0),
   [RedRate]         float                not null default(0),
   [Description]     nvarchar(200)        not null default(''),
   [Creater]         int                  not null,
   [CreatedDate]     datetime             not null constraint DF_Oil_CreatedDate default getdate(),
   [Modifier]        int                  not null,
   [ModifiedDate]    datetime             not null constraint DF_Oil_ModifiedDate default getdate()
)
on "PRIMARY"

alter table [Oil]
    add constraint PK_Oil_Id primary key ([Id])
    on "PRIMARY"

execute sp_addextendedproperty 'MS_Description', 
   '黄色油量比率最大值，当rate大于该值时黄色高亮提醒',
   'user', 'dbo', 'table', 'Oil', 'column', 'YellowRate'

execute sp_addextendedproperty 'MS_Description', 
   '红色油量比率最大值，当rate大于该值时红色高亮提醒',
   'user', 'dbo', 'table', 'Oil', 'column', 'RedRate'



/*==============================================================*/
/* Table: [Car]                                       */
/*==============================================================*/
create table [dbo].[Car] (
   [Id]              int                  identity(1, 1),
   [Number]          varchar(20)          not null,
   [Vin]             varchar(100)         not null,
   [Model]           nvarchar(20)         not null default(''),
   [IsArchived]      bit                  not null default(0),
   [InstanceCount]   int                  not null default(0),
   [BillCount]       int                  not null default(0),
   [PreviousOil]     int                  null,
   [LastOil]         int                  null,
   [LastVolume]      float                not null default(0),
   [LastMileage]     float                not null default(0),
   [LastRate]        float                not null default(0),
   [Description]     nvarchar(200)        not null,
   [Creater]         int                  not null,
   [CreatedDate]     datetime             not null constraint DF_Car_CreatedDate default getdate(),
   [Modifier]        int                  not null,
   [ModifiedDate]    datetime             not null constraint DF_Car_ModifiedDate default getdate()
)
on "PRIMARY"

alter table [Car]
    add constraint PK_Car_Id primary key ([Id])
    on "PRIMARY"

execute sp_addextendedproperty 'MS_Description', 
   'last_oil的上一次oil，用于计算车辆最后一次油量超标颜色值',
   'user', 'dbo', 'table', 'Car', 'column', 'PreviousOil'

execute sp_addextendedproperty 'MS_Description', 
   '最后一次加油油品，用于快速计算油量超标颜色值',
   'user', 'dbo', 'table', 'Car', 'column', 'LastOil'

execute sp_addextendedproperty 'MS_Description', 
   '最后一次加油量，用于快速计算出最新bill的rate',
   'user', 'dbo', 'table', 'Car', 'column', 'LastVolume'

execute sp_addextendedproperty 'MS_Description', 
   '最后一次里程数，用于快速计算出最新bill的rate',
   'user', 'dbo', 'table', 'Car', 'column', 'LastMileage'

execute sp_addextendedproperty 'MS_Description', 
   '车辆最后一次加油油耗值',
   'user', 'dbo', 'table', 'Car', 'column', 'LastRate'


/*==============================================================*/
/* Table: [Instance]                                        */
/*==============================================================*/
create table [dbo].[Instance] (
   [Id]                 int                  identity(1, 1),
   [Car]                int                  not null,
   [Project]            int                  not null,
   [Department]         int                  not null,
   [UserName]           nvarchar(20)         not null,
   [Oils]               varchar(20)          not null,
   [Goal]               nvarchar(200)        not null,
   [StartDate]          datetime             not null,
   [EndDate]            datetime             not null,
   [IsReleased]         bit                  not null default(0),
   [IsPending]          bit                  not null default(0),
   [IsArchived]         bit                  not null default(0),
   [IsEnable]           bit                  not null default(1),
   [BillCount]          int                  not null default(0),
   [Creater]            int                  not null,
   [CreatedDate]        datetime             not null constraint DF_Instance_CreatedDate default getdate(),
   [Modifier]           int                  not null,
   [ModifiedDate]       datetime             not null constraint DF_Instance_ModifiedDate default getdate()
)
on "PRIMARY"

alter table [Instance]
    add constraint PK_Instance_Id primary key ([Id])
    on "PRIMARY"

execute sp_addextendedproperty 'MS_Description', 
   '允许加的油品，多个油品用逗号分隔',
   'user', 'dbo', 'table', 'Instance', 'column', 'Oils'

execute sp_addextendedproperty 'MS_Description', 
   '是否已发布，默认为false，一旦成功发布，就永远为true，加油时判断是否可以加油时，该字段只是检测字段之一，只有is_released与is_enable同时为ture时才可以加油',
   'user', 'dbo', 'table', 'Instance', 'column', 'IsReleased'

execute sp_addextendedproperty 'MS_Description', 
   '是否未审核，即在审核人的工作台出现',
   'user', 'dbo', 'table', 'Instance', 'column', 'IsPending'


/*==============================================================*/
/* Table: [Trace]                                       */
/*==============================================================*/
create table [dbo].[Trace] (
   [Id]              int                  identity(1, 1),
   [Car]             int                  not null,
   [Instance]        int                  not null,
   [PreviousTrace]   int                  null,
   [Status]          varchar(20)          not null,
   [IsFinished]      bit                  not null default(0),
   [IsArchived]      bit                  not null default(0),
   [Project]         int                  not null,
   [Department]      int                  not null,
   [UserName]        nvarchar(20)         not null,
   [Oils]            varchar(20)          not null,
   [Goal]            nvarchar(200)        not null,
   [StartDate]       datetime             not null,
   [EndDate]         datetime             not null,
   [StartInfo]       nvarchar(200)        not null default(''),
   [EndInfo]         nvarchar(200)        not null default(''),
   [Creater]         int                  not null,
   [CreatedDate]     datetime             not null constraint DF_Trace_CreatedDate default getdate(),
   [Modifier]        int                  not null,
   [ModifiedDate]    datetime             not null constraint DF_Trace_ModifiedDate default getdate()
)
on "PRIMARY"

alter table [Trace]
    add constraint PK_Trace_Id primary key ([Id])
    on "PRIMARY"

execute sp_addextendedproperty 'MS_Description', 
   'recaptured-已取回，modified-已修改，pending-待审核，released-已审核，rejected-已驳回',
   'user', 'dbo', 'table', 'Trace', 'column', 'Status'

execute sp_addextendedproperty 'MS_Description', 
   '允许加油油品，多个油品用逗号分隔',
   'user', 'dbo', 'table', 'Trace', 'column', 'Oils'

execute sp_addextendedproperty 'MS_Description', 
   '发给审核人的消息',
   'user', 'dbo', 'table', 'Trace', 'column', 'StartInfo'

execute sp_addextendedproperty 'MS_Description', 
   '发给申请人的消息',
   'user', 'dbo', 'table', 'Trace', 'column', 'EndInfo'


/*==============================================================*/
/* Table: [Bill]                                       */
/*==============================================================*/
create table [dbo].[Bill] (
   [Id]              int                  identity(1, 1),
   [Car]             int                  not null,
   [Instance]        int                  null,
   [Project]         int                  not null,
   [Department]      int                  not null,
   [Oil]             int                  not null,
   [PreviousOil]     int                  null,
   [Volume]          float                not null,
   [Mileage]         float                not null,
   [DriverName]      nvarchar(10)         not null,
   [Signature]       int                  null,
   [Rate]            float                not null default(0),
   [Oiler]           int                  not null,
   [Time]            datetime             not null,
   [IsLost]          bit                  not null default(0),
   [IsPrinted]       bit                  not null default(0),
   [Creater]         int                  not null,
   [CreatedDate]     datetime             not null constraint DF_Bill_CreatedDate default getdate(),
   [Modifier]        int                  not null,
   [ModifiedDate]    datetime             not null constraint DF_Bill_ModifiedDate default getdate()
)
on "PRIMARY"

alter table [Bill]
    add constraint PK_Bill_Id primary key ([Id])
    on "PRIMARY"

execute sp_addextendedproperty 'MS_Description', 
   '上一次加油油品，用于提取red_rate及yellow_rate高亮提醒，=car.last_oil',
   'user', 'dbo', 'table', 'Bill', 'column', 'PreviousOil'

execute sp_addextendedproperty 'MS_Description', 
   '默认值为0，表示第一次加油，同一车辆上一次加油单中加油量与两次里程数差值的比率，=car.last_volume/(mileage-car.last_mileage)',
   'user', 'dbo', 'table', 'Bill', 'column', 'Rate'

execute sp_addextendedproperty 'MS_Description', 
   '真实创建时间，扫码时自动填入当前时间，手动补录则需要手动输入',
   'user', 'dbo', 'table', 'Bill', 'column', 'Time'

execute sp_addextendedproperty 'MS_Description', 
   '是否为补录的，默认为False',
   'user', 'dbo', 'table', 'Bill', 'column', 'IsLost'


/*==============================================================*/
/* Table: [Signature]                                       */
/*==============================================================*/
create table [dbo].[Signature] (
   [Id]              int                  identity(1, 1),
   [Name]            nvarchar(10)         not null,
   [Sign]            image                not null,
   [Creater]         int                  not null,
   [CreatedDate]     datetime             not null constraint DF_Signature_CreatedDate default getdate()
)
on "PRIMARY"

alter table [Signature]
    add constraint PK_Signature_Id primary key ([Id])
    on "PRIMARY"


/*==============================================================*/
/* Table: [Preference]                                       */
/*==============================================================*/
create table [dbo].[Preference] (
   [Id]              int                  identity(1, 1),
   [ShortcutHour]    int                  not null,
   [FinishHour]      int                  not null default(48),
   [Creater]         int                  not null,
   [CreatedDate]     datetime             not null constraint DF_Preference_CreatedDate default getdate(),
   [Modifier]        int                  not null,
   [ModifiedDate]    datetime             not null constraint DF_Preference_ModifiedDate default getdate()
)
on "PRIMARY"

alter table [Preference]
    add constraint PK_Preference_Id primary key ([Id])
    on "PRIMARY"


/*==============================================================*/
/* Table: [ErrorLog]                                        */
/*==============================================================*/
create table [dbo].[ErrorLog] (
   [Id]              int                  identity(1, 1),
   [User]            int                  not null,
   [UserName]        nvarchar(20)         not null,
   [TargetIds]       varchar(100)         not null,
   [CodeTag]         varchar(50)          not null,
   [LogName]         nvarchar(50)         not null,
   [LogContent]      nvarchar(500)        not null default(''),
   [ErrorMsg]        nvarchar(500)        not null default(''),
   [IpAddr]          varchar(50)          not null default(''),
   [UrlPath]         varchar(50)          not null default(''),
   [CreatedDate]     datetime             not null constraint DF_ErrorLog_CreatedDate default getdate()
)
on "PRIMARY"

alter table [ErrorLog]
    add constraint PK_ErrorLog_Id primary key ([Id])
    on "PRIMARY"

/*==============================================================*/
/* Table: [EventLog]                                        */
/*==============================================================*/
create table [dbo].[EventLog] (
   [Id]              int                  identity(1, 1),
   [User]            int                  not null,
   [UserName]        nvarchar(20)         not null,
   [TargetIds]       varchar(100)         not null,
   [CodeTag]         varchar(50)          not null,
   [LogName]         nvarchar(50)         not null,
   [LogContent]      nvarchar(500)        not null default(''),
   [IpAddr]          varchar(50)          not null default(''),
   [UrlPath]         varchar(50)          not null default(''),
   [IsSpecial]       bit                  not null default(0),
   [CreatedDate]     datetime             not null constraint DF_EventLog_CreatedDate default getdate()
)
on "PRIMARY"

alter table [EventLog]
    add constraint PK_EventLog_Id primary key ([Id])
    on "PRIMARY"

/*==============================================================*/
/* Table: [Deleted]                                        */
/*==============================================================*/
create table [dbo].[Deleted] (
   [Id]              int                  identity(1, 1),
   [Model]           varchar(50)          not null,
   [TargetIds]       varchar(1000)        not null,
   [CreatedDate]     datetime             not null constraint DF_Deleted_CreatedDate default getdate()
)
on "PRIMARY"

alter table [Deleted]
    add constraint PK_Deleted_Id primary key ([Id])
    on "PRIMARY"

/*==============================================================*/
/* 创建表外键关联                                            */
/*==============================================================*/
alter table [User]
    add constraint FK_User_Role foreign key (Role)
    references [Role] ([Id])

alter table [User]
    add constraint FK_User_Creater foreign key ([Creater])
    references [User] ([Id])

alter table [User]
    add constraint FK_User_Modifier foreign key ([Modifier])
    references [User] ([Id])


alter table [Role]
    add constraint FK_Role_Creater foreign key ([Creater])
    references [User] ([Id])
    
alter table [Role]
    add constraint FK_Role_Modifier foreign key ([Modifier])
    references [User] ([Id])


alter table [Piece]
    add constraint FK_Piece_Creater foreign key ([Creater])
    references [User] ([Id])
    
alter table [Piece]
    add constraint FK_Piece_Modifier foreign key ([Modifier])
    references [User] ([Id])


alter table [Project]
    add constraint FK_Project_Creater foreign key ([Creater])
    references [User] ([Id])
    
alter table [Project]
    add constraint FK_Project_Modifier foreign key ([Modifier])
    references [User] ([Id])


alter table [Department]
    add constraint FK_Department_Creater foreign key ([Creater])
    references [User] ([Id])
    
alter table [Department]
    add constraint FK_Department_Modifier foreign key ([Modifier])
    references [User] ([Id])


alter table [Oil]
    add constraint FK_Oil_Creater foreign key ([Creater])
    references [User] ([Id])
    
alter table [Oil]
    add constraint FK_Oil_Modifier foreign key ([Modifier])
    references [User] ([Id])


alter table [Car]
    add constraint FK_Car_PreviousOil foreign key ([PreviousOil])
    references [Oil] ([Id])

alter table [Car]
    add constraint FK_Car_LastOil foreign key ([LastOil])
    references [Oil] ([Id])

alter table [Car]
    add constraint FK_Car_Creater foreign key ([Creater])
    references [User] ([Id])
    
alter table [Car]
    add constraint FK_Car_Modifier foreign key ([Modifier])
    references [User] ([Id])


alter table [Instance]
    add constraint FK_Instance_Car foreign key ([Car])
    references [Car] ([Id])

alter table [Instance]
    add constraint FK_Instance_Project foreign key ([Project])
    references [Project] ([Id])

alter table [Instance]
    add constraint FK_Instance_Department foreign key ([Department])
    references [Department] ([Id])

alter table [Instance]
    add constraint FK_Instance_Creater foreign key ([Creater])
    references [User] ([Id])
    
alter table [Instance]
    add constraint FK_Instance_Modifier foreign key ([Modifier])
    references [User] ([Id])


alter table [Trace]
    add constraint FK_Trace_Car foreign key ([Car])
    references [Car] ([Id])

alter table [Trace]
    add constraint FK_Trace_Instance foreign key ([Instance])
    references [Instance] ([Id])

alter table [Trace]
    add constraint FK_Trace_PreviousTrace foreign key ([PreviousTrace])
    references [Trace] ([Id])

alter table [Trace]
    add constraint FK_Trace_Project foreign key ([Project])
    references [Project] ([Id])

alter table [Trace]
    add constraint FK_Trace_Department foreign key ([Department])
    references [Department] ([Id])

alter table [Trace]
    add constraint FK_Trace_Creater foreign key ([Creater])
    references [User] ([Id])
    
alter table [Trace]
    add constraint FK_Trace_Modifier foreign key ([Modifier])
    references [User] ([Id])


alter table [Bill]
    add constraint FK_Bill_Car foreign key ([Car])
    references [Car] ([Id])

alter table [Bill]
    add constraint FK_Bill_Instance foreign key ([Instance])
    references [Instance] ([Id])

alter table [Bill]
    add constraint FK_Bill_Project foreign key ([Project])
    references [Project] ([Id])

alter table [Bill]
    add constraint FK_Bill_Department foreign key ([Department])
    references [Department] ([Id])

alter table [Bill]
    add constraint FK_Bill_Oil foreign key ([Oil])
    references [Oil] ([Id])

alter table [Bill]
    add constraint FK_Bill_PreviousOil foreign key ([PreviousOil])
    references [Oil] ([Id])
    
alter table [Bill]
    add constraint FK_Bill_Signature foreign key ([Signature])
    references [Signature] ([Id])

alter table [Bill]
    add constraint FK_Bill_Oiler foreign key ([Oiler])
    references [User] ([Id])

alter table [Bill]
    add constraint FK_Bill_Creater foreign key ([Creater])
    references [User] ([Id])

alter table [Bill]
    add constraint FK_Bill_Modifier foreign key ([Modifier])
    references [User] ([Id])


alter table [Signature]
    add constraint FK_Signature_Creater foreign key ([Creater])
    references [User] ([Id])


alter table [Preference]
    add constraint FK_Preference_Creater foreign key ([Creater])
    references [User] ([Id])

alter table [Preference]
    add constraint FK_Preference_Modifier foreign key ([Modifier])
    references [User] ([Id])


/*==============================================================*/
/* 创建必要的存储过程及函数                                     */
/*==============================================================*/
USE [HTPS]
GO
/****** 对象:  StoredProcedure [dbo].[proc_TryDeleteWithFK]    脚本日期: 08/24/2017 08:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_TryDeleteWithFK]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



--实现执行带外键约束的删除语句时返回是否因为外键约束不能删除，删除成功返回1，因为外键约束错误不能删除返回0，其他错误返回-1
--注意该存储过程已经带有回滚程序
CREATE PROC [dbo].[proc_TryDeleteWithFK] 
  @model varchar(50),
  @ids varchar(1000)
AS
--SET NOCOUNT ON
BEGIN TRY
  BEGIN TRAN
    exec(''delete from ['' + @model + ''] where Id in('' + @ids + '')'')
    exec(''insert into [Deleted]([Model],TargetIds) values('''''' + @model + '''''','''''' + @ids + '''''')'')
END TRY
BEGIN CATCH 
  goto err
END CATCH

goto succ

err:
  rollback tran;
    IF ERROR_NUMBER() = 547 -- 如果是外键约束错误
    return 0
  else
    return -1
succ:
  commit tran
  return 1





' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_RoleInsert]    脚本日期: 08/24/2017 08:33:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_RoleInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




CREATE PROCEDURE [dbo].[proc_RoleInsert] 
  @Name nvarchar(20),
  @Powers varchar(100),
  @Description nvarchar(200),
  @Creater int,
  @CreatedDate datetime,
  @Modifier int,
  @ModifiedDate datetime
AS
begin
  /* SET NOCOUNT ON */ 
  insert into [Role] ([Name],[Powers],Description,Creater,CreatedDate,Modifier,ModifiedDate) 
  values(@Name,@Powers,@Description,@Creater,@CreatedDate,@Modifier,@ModifiedDate)
  return @@IDENTITY
end













' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_StartupSelect]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_StartupSelect]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'






CREATE PROCEDURE [dbo].[proc_StartupSelect] 
AS
begin
  select * from [User]
  select * from [Role]
  select * from [Power]
  select * from [Project]
  select * from [Department]
  select * from [Oil]
  select * from [Preference]
  select * from [Car] where IsArchived = 0
  select * from [Instance] where IsArchived = 0
  select * from [Trace] where IsArchived = 0
  select top 20 * from [Bill] order by Id desc
  select * from [Piece] where IsPrinted = 0
  --select top 5 * from [Signature] order by Id desc
end




set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON



' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_RoleByIdUpdate]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_RoleByIdUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



create PROCEDURE [dbo].[proc_RoleByIdUpdate] 
  @Id int,
  @Name nvarchar(20),
  @Powers varchar(100),
  @Description nvarchar(200),
  @Modifier int,
  @ModifiedDate datetime
AS
begin
  Update [Role] set [Name]=@Name,
    [Powers]=@Powers,
    Description=@Description,
    Modifier=@Modifier,
    ModifiedDate=@ModifiedDate 
  where Id=@Id
end











' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_ChangesetSelect]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_ChangesetSelect]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'






CREATE PROCEDURE [dbo].[proc_ChangesetSelect] 
  @SyncToken datetime
AS
begin
  select * from [User] where ModifiedDate >= @SyncToken
  select * from [Role] where ModifiedDate >= @SyncToken
  select * from [Project] where ModifiedDate >= @SyncToken
  select * from [Department] where ModifiedDate >= @SyncToken
  select * from [Oil] where ModifiedDate >= @SyncToken
  select * from [Preference] where ModifiedDate >= @SyncToken
  select * from [Car] where ModifiedDate >= @SyncToken
  select * from [Instance] where ModifiedDate >= @SyncToken
  select * from [Trace] where ModifiedDate >= @SyncToken
  select * from [Bill] where ModifiedDate >= @SyncToken
  --select * from [Signature] where CreatedDate >= @SyncToken
  select * from [Piece] where ModifiedDate >= @SyncToken
  select * from [Deleted] where CreatedDate >= @SyncToken
end
































' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_PieceByIdUpdate]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_PieceByIdUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'





CREATE PROCEDURE [dbo].[proc_PieceByIdUpdate] 
  @Id int,
  @Name nvarchar(20),
  @Number varchar(20),
  @Order varchar(20),
  @Count int,
  @PrintedCount int,
  @IsPrinted bit,
  @Ots nvarchar(20),
  @DelegateNumber varchar(20),
  @AccessoryFactory nvarchar(20),
  @VehicleType nvarchar(20),
  @TestContent nvarchar(20),
  @SendPerson nvarchar(20),
  @ChargePerson nvarchar(20),
  @SendDate datetime = null,
  @Place nvarchar(20),
  @IsEnable bit,
  @Description nvarchar(200),
  @Modifier int,
  @ModifiedDate datetime
AS
begin
  Update [Piece] set [Name]=@Name,
  [Number]=@Number,
  [Order]=@Order,
  [Count]=@Count,
  [PrintedCount]=@PrintedCount,
  [IsPrinted]=@IsPrinted,
  [Ots]=@Ots,
  [DelegateNumber]=@DelegateNumber,
  [AccessoryFactory]=@AccessoryFactory,
  [VehicleType]=@VehicleType,
  [TestContent]=@TestContent,
  [SendPerson]=@SendPerson,
  [ChargePerson]=@ChargePerson,
  [SendDate]=@SendDate,
  [Place]=@Place,
    [IsEnable]=@IsEnable,
    Description=@Description,
    Modifier=@Modifier,
    ModifiedDate=@ModifiedDate 
  where Id=@Id
end













' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_PieceInsert]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_PieceInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'






CREATE PROCEDURE [dbo].[proc_PieceInsert] 
  @Name nvarchar(20),
  @Number varchar(20),
  @Order varchar(20),
  @Count int,
  @PrintedCount int,
  @IsPrinted bit,
  @Ots nvarchar(20),
  @DelegateNumber varchar(20),
  @AccessoryFactory nvarchar(20),
  @VehicleType nvarchar(20),
  @TestContent nvarchar(20),
  @SendPerson nvarchar(20),
  @ChargePerson nvarchar(20),
  @SendDate datetime = null,
  @Place nvarchar(20),
  @IsEnable bit,
  @Description nvarchar(200),
  @Creater int,
  @CreatedDate datetime,
  @Modifier int,
  @ModifiedDate datetime

AS
begin
  /* SET NOCOUNT ON */ 
  insert into [Piece] ([Name],[Number],[Order],[Count],[PrintedCount],[IsPrinted],[Ots],
  [DelegateNumber],[AccessoryFactory],[VehicleType],[TestContent],[SendPerson],[ChargePerson],[SendDate],[Place],
  [IsEnable],Description,Creater,CreatedDate,Modifier,ModifiedDate) 
  values(@Name,@Number,@Order,@Count,@PrintedCount,@IsPrinted,@Ots,
  @DelegateNumber,@AccessoryFactory,@VehicleType,@TestContent,@SendPerson,@ChargePerson,@SendDate,@Place,
  @IsEnable,@Description,@Creater,@CreatedDate,@Modifier,@ModifiedDate)
  return @@IDENTITY
end















' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_ModelByIdsDelete]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_ModelByIdsDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
create PROCEDURE [dbo].[proc_ModelByIdsDelete] 
  @model varchar(50),
  @ids varchar(1000)
AS
--SET NOCOUNT ON
DECLARE @return_value int
exec @return_value = proc_TryDeleteWithFK @model,@ids
return @return_value







' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_DepartmentByIdUpdate]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_DepartmentByIdUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




create PROCEDURE [dbo].[proc_DepartmentByIdUpdate] 
  @Id int,
  @Name nvarchar(20),
  @Description nvarchar(200),
  @Modifier int,
  @ModifiedDate datetime
AS
begin
  Update [Department] set [Name]=@Name,
    Description=@Description,
    Modifier=@Modifier,
    ModifiedDate=@ModifiedDate 
  where Id=@Id
end












' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_DepartmentInsert]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_DepartmentInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'






create PROCEDURE [dbo].[proc_DepartmentInsert] 
  @Name nvarchar(20),
  @Description nvarchar(200),
  @Creater int,
  @CreatedDate datetime,
  @Modifier int,
  @ModifiedDate datetime
AS
begin
  /* SET NOCOUNT ON */ 
  insert into [Department] ([Name],Description,Creater,CreatedDate,Modifier,ModifiedDate) 
  values(@Name,@Description,@Creater,@CreatedDate,@Modifier,@ModifiedDate)
  return @@IDENTITY
end















' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_BillsForPrinterSelect]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_BillsForPrinterSelect]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'





CREATE PROCEDURE [dbo].[proc_BillsForPrinterSelect] 
  @LastId int
AS
begin
  if @LastId > 0
  begin
  select Id,(select Number from Car where Car.Id = Bill.Car) as CarNumber,
    (select Vin from Car where Car.Id = Bill.Car) as CarVin,
    (select [Name] from Project where Project.Id = Bill.Project) as ProjectName,
    (select [Name] from Department where Department.Id = Bill.Department) as DepartmentName,
    (select [Name] from Oil where Oil.Id = Bill.Oil) as OilName,
    Volume,Mileage,DriverName,Rate,
    (select [Name] from [User] where [User].Id = Bill.Oiler) as OilerName,
    --(select [Name] from [User] where [User].Id = Bill.Creater) as CreaterName,
    [Time],IsLost,IsPrinted 
  from Bill where Id > @LastId and IsPrinted = 0 order by Id
  end
  else
  begin
    --鎼滅储鏈€杩戣嫢骞插皬鏃跺唴鐨勫姞娌瑰崟
  declare @maxHour int
    set @maxHour = 24
  select Id,(select Number from Car where Car.Id = Bill.Car) as CarNumber,
    (select Vin from Car where Car.Id = Bill.Car) as CarVin,
    (select [Name] from Project where Project.Id = Bill.Project) as ProjectName,
    (select [Name] from Department where Department.Id = Bill.Department) as DepartmentName,
    (select [Name] from Oil where Oil.Id = Bill.Oil) as OilName,
    Volume,Mileage,DriverName,Rate,
    (select [Name] from [User] where [User].Id = Bill.Oiler) as OilerName,
    --(select [Name] from [User] where [User].Id = Bill.Creater) as CreaterName,
    [Time],IsLost,IsPrinted 
  --from Bill where IsPrinted = 0 order by Id
  from Bill where datediff(n,CreatedDate,getdate()) < (@maxHour * 60) and IsPrinted = 0 order by Id
  end
end





















' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_ProjectByIdUpdate]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_ProjectByIdUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




create PROCEDURE [dbo].[proc_ProjectByIdUpdate] 
  @Id int,
  @Name nvarchar(20),
  @IsEnable bit,
  @Description nvarchar(200),
  @Modifier int,
  @ModifiedDate datetime
AS
begin
  Update [Project] set [Name]=@Name,
    [IsEnable]=@IsEnable,
    Description=@Description,
    Modifier=@Modifier,
    ModifiedDate=@ModifiedDate 
  where Id=@Id
end












' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_ProjectInsert]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_ProjectInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'





CREATE PROCEDURE [dbo].[proc_ProjectInsert] 
  @Name nvarchar(20),
  @IsEnable bit,
  @Description nvarchar(200),
  @Creater int,
  @CreatedDate datetime,
  @Modifier int,
  @ModifiedDate datetime
AS
begin
  /* SET NOCOUNT ON */ 
  insert into [Project] ([Name],[IsEnable],Description,Creater,CreatedDate,Modifier,ModifiedDate) 
  values(@Name,@IsEnable,@Description,@Creater,@CreatedDate,@Modifier,@ModifiedDate)
  return @@IDENTITY
end














' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_OilInsert]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_OilInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'






create PROCEDURE [dbo].[proc_OilInsert] 
  @Name nvarchar(20),
  @YellowRate float,
  @RedRate float,
  @Description nvarchar(200),
  @Creater int,
  @CreatedDate datetime,
  @Modifier int,
  @ModifiedDate datetime
AS
begin
  /* SET NOCOUNT ON */ 
  insert into [Oil] ([Name],[YellowRate],[RedRate],Description,Creater,CreatedDate,Modifier,ModifiedDate) 
  values(@Name,@YellowRate,@RedRate,@Description,@Creater,@CreatedDate,@Modifier,@ModifiedDate)
  return @@IDENTITY
end















' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_OilByIdUpdate]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_OilByIdUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




create PROCEDURE [dbo].[proc_OilByIdUpdate] 
  @Id int,
  @YellowRate float,
  @RedRate float,
  @Name nvarchar(20),
  @Description nvarchar(200),
  @Modifier int,
  @ModifiedDate datetime
AS
begin
  Update [Oil] set [Name]=@Name,
    YellowRate=@YellowRate,
    RedRate=@RedRate,
    Description=@Description,
    Modifier=@Modifier,
    ModifiedDate=@ModifiedDate 
  where Id=@Id
end












' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_InstanceEnable]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_InstanceEnable]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'






CREATE PROCEDURE [dbo].[proc_InstanceEnable] 
  @Trace int,
  @Instance int,
  @Car int,
  @Status varchar(20),
  @StartInfo nvarchar(200),
  @Creater int,
  @CreatedDate datetime,
  @OutState int output
AS
SET NOCOUNT ON
begin tran
  set @OutState = 0
  declare @isArchived bit
  declare @isPending bit
  declare @isReleased bit
  declare @isEnable bit
  select @isArchived = IsArchived,@isPending = IsPending,@isReleased = IsReleased,@isEnable = IsEnable from Instance where Id = @Instance
  if(@isArchived = 1)
  begin
    set @OutState = -100--该申请单已被归档，不能启用
    goto err
  end
  if(@isEnable = 1)
  begin
    set @OutState = -200--该申请单已启用，不能重复启用
    goto err
  end

  declare @isFinished bit
  declare @project int
  declare @department int
  declare @userName nvarchar(20)
  declare @oils varchar(20)
  declare @goal nvarchar(200)
  declare @startDate datetime
  declare @endDate datetime
  select @isFinished = IsFinished,
    @project = Project,
    @department = Department,
    @userName = UserName,
    @oils = Oils,
    @goal = Goal,
    @startDate = StartDate,
    @endDate = EndDate from Trace 
  where Id = @Trace
  if(@isFinished = 0)
  begin
    set @OutState = -300--该申请单最后一个履历处于未完成状态，不能禁用
    goto err
  end

  insert into [Trace] ([Car],[Instance],[PreviousTrace],[Status],[IsFinished],[Project],[Department],[UserName],
    [Oils],[Goal],[StartDate],[EndDate],[StartInfo],
    Creater,CreatedDate,Modifier,ModifiedDate) 
  values(@Car,@Instance,@Trace,@Status,1,@project,@department,@userName,
    @oils,@goal,@startDate,@endDate,@StartInfo,
    @Creater,@CreatedDate,@Creater,@CreatedDate)
  if @@error<>0 
  begin
    set @OutState = -1--添加申请单履历失败
    goto err
  end

  update [Instance] set IsEnable = 1,
    Modifier = @Creater,
    ModifiedDate = @CreatedDate
  where Id = @Instance
  if @@error<>0 
  begin
    set @OutState = -2--更新申请单启用状态失败
    goto err
  end

  update [Car] set Modifier=@Creater,
    ModifiedDate=@CreatedDate
  where Id = @Car
  if @@error<>0 
  begin
    set @OutState = -3--更新车辆信息失败
    goto err
  end


  goto succ



err:
  rollback tran;
  return 0
succ:
  commit tran
  set @OutState = 1
  return @Instance
















' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_InstanceCheck]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_InstanceCheck]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




create PROCEDURE [dbo].[proc_InstanceCheck] 
  @Trace int,
  @Instance int,
  @Car int,
  @Status varchar(20),
  @EndInfo nvarchar(200),
  @Creater int,
  @CreatedDate datetime,
  @OutState int output
AS
SET NOCOUNT ON
begin tran
  set @OutState = 0
  declare @isArchived bit
  declare @isPending bit
  declare @isEnable bit
  select @isArchived = IsArchived,@isPending = IsPending,@isEnable = IsEnable from Instance where Id = @Instance
  if(@isArchived = 1)
  begin
    set @OutState = -100--该申请单已被归档，不能审核
    goto err
  end
  if(@isPending = 0)
  begin
    set @OutState = -200--该申请单处于非待审核状态，可能已被其他用户审核或已被申请人取回，不能重复审核
    goto err
  end
  if(@isEnable = 0)
  begin
    set @OutState = -300--该申请单已被禁用，不能审核
    goto err
  end

  declare @isFinished bit
  declare @project int
  declare @department int
  declare @userName nvarchar(20)
  declare @oils varchar(20)
  declare @goal nvarchar(200)
  declare @startDate datetime
  declare @endDate datetime
  select @isFinished = IsFinished,
    @project = Project,
    @department = Department,
    @userName = UserName,
    @oils = Oils,
    @goal = Goal,
    @startDate = StartDate,
    @endDate = EndDate from Trace 
  where Id = @Trace
  if(@isFinished = 1)
  begin
    set @OutState = -400--该申请单已被其他人审核过了，不能重复审核
    goto err
  end

  update [Trace] set IsFinished = 1,
    Status = @Status,
    EndInfo = @EndInfo,
    Modifier = @Creater,
    ModifiedDate = @CreatedDate
  where Id = @Trace
  if @@error<>0 
  begin
    set @OutState = -1--更新申请单履历失败
    goto err
  end

  if(@Status = ''approved'')
  begin
    update [Instance] set IsPending = 0,
      IsReleased = 1,
      Car = @Car,
      Project = @project,
      Department = @department,
      UserName = @userName,
      Oils = @oils,
      Goal = @goal,
      StartDate = @startDate,
      EndDate = @endDate,
      Modifier = @Creater,
      ModifiedDate = @CreatedDate
    where Id = @Instance
    if @@error<>0 
    begin
      set @OutState = -2--更新申请单信息失败
      goto err
    end
  end
  else
  begin
    update [Instance] set IsPending = 0,
      Modifier = @Creater,
      ModifiedDate = @CreatedDate
    where Id = @Instance
    if @@error<>0 
    begin
      set @OutState = -3--更新申请单状态失败
      goto err
    end
  end

  update [Car] set Modifier=@Creater,
    ModifiedDate=@CreatedDate
  where Id = @Car
  if @@error<>0 
  begin
    set @OutState = -4--更新车辆信息失败
    goto err
  end


  goto succ



err:
  rollback tran;
  return 0
succ:
  commit tran
  set @OutState = 1
  return @Instance














' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_InstanceInsert]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_InstanceInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'







CREATE PROCEDURE [dbo].[proc_InstanceInsert] 
  @Car int,
  @Project int,
  @Department int,
  @UserName nvarchar(20),
  @Oils varchar(100),
  @Goal nvarchar(200),
  @StartDate datetime,
  @EndDate datetime,
  @StartInfo nvarchar(200),
  @Creater int,
  @CreatedDate datetime,
  @OutState int output
AS
SET NOCOUNT ON
begin tran
  set @OutState = 0
--  declare @isArchived bit
  declare @instanceCount int
  declare @instance int
  select @instanceCount = InstanceCount from Car where Id = @Car
--  鍥犱负涓€閿紭鍖栧姛鑳藉彲鑳戒細鎶婅溅杈嗙粰褰掓。浜嗭紝鎵€浠ヨ繖閲屼笉鑳藉姞杩欎釜鏄惁褰掓。鐨勯檺鍒跺垽鏂紝鍚屾椂鍚庣画搴旇鍦ㄥ鍔犵敵璇峰崟鍚庡己琛屾妸杞﹁締璁剧疆涓洪潪褰掓。鐘舵€?
--  if(@isArchived = 1)
--  begin
--    set @OutState = -100--璇ヨ溅杈嗗凡褰掓。锛屼笉鑳芥柊寤虹敵璇峰崟
--    goto err
--  end

  if(@StartDate > @EndDate or @EndDate < getdate())
  begin
    set @OutState = -100--璧锋鏃堕棿璁剧疆鏈夎
    goto err
  end

  insert into [Instance] ([Car],[Project],[Department],[UserName],
    [Oils],[Goal],[StartDate],[EndDate],[IsPending],
    Creater,CreatedDate,Modifier,ModifiedDate) 
  values(@Car,@Project,@Department,@UserName,
    @Oils,@Goal,@StartDate,@EndDate,1,
    @Creater,@CreatedDate,@Creater,@CreatedDate)
  if @@error<>0 
  begin
    set @OutState = -1--娣诲姞鐢宠鍗曞け璐?
    goto err
  end

  set @instance = @@IDENTITY

  insert into [Trace] ([Car],[Instance],[Status],[Project],[Department],[UserName],
    [Oils],[Goal],[StartDate],[EndDate],[StartInfo],
    Creater,CreatedDate,Modifier,ModifiedDate) 
  values(@Car,@instance,''pending'',@Project,@Department,@UserName,
  @Oils,@Goal,@StartDate,@EndDate,@StartInfo,
    @Creater,@CreatedDate,@Creater,@CreatedDate)
  if @@error<>0 
  begin
    set @OutState = -2--娣诲姞鐢宠鍗曞饱鍘嗗け璐?
    goto err
  end

  --鍥犱负杞﹁締鍙兘琚叾浠栫敤鎴烽€氳繃涓€閿紭鍖栧姛鑳界粰褰掓。浜嗭紝鎵€浠ヨ繖閲岄渶瑕佸己琛屾妸褰掓。鐘舵€佽缃负鍚?
  set @instanceCount = @instanceCount + 1;
  update [Car] set InstanceCount = @instanceCount,
    IsArchived=0,
    Modifier=@Creater,
    ModifiedDate=@CreatedDate
  where Id = @Car
  if @@error<>0 
  begin
    set @OutState = -3--鏇存柊杞﹁締淇℃伅澶辫触
    goto err
  end


  goto succ



err:
  rollback tran;
  return 0
succ:
  commit tran
  set @OutState = 1
  return @instance

















' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_CarByIdUpdate]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_CarByIdUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




create PROCEDURE [dbo].[proc_CarByIdUpdate] 
  @Id int,
  @Number varchar(20),
  @Vin varchar(100),
  @Model nvarchar(20),
  @Description nvarchar(200),
  @Modifier int,
  @ModifiedDate datetime
AS
begin
  Update [Car] set [Number]=@Number,
    Vin=@Vin,
    Model=@Model,
    Description=@Description,
    Modifier=@Modifier,
    ModifiedDate=@ModifiedDate 
  where Id=@Id
end












' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_CarInsert]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_CarInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




create PROCEDURE [dbo].[proc_CarInsert] 
  @Number varchar(20),
  @Vin varchar(100),
  @Model nvarchar(20),
  @Description nvarchar(200),
  @Creater int,
  @CreatedDate datetime,
  @Modifier int,
  @ModifiedDate datetime
AS
begin
  /* SET NOCOUNT ON */ 
  insert into [Car] ([Number],[Vin],[Model],Description,Creater,CreatedDate,Modifier,ModifiedDate) 
  values(@Number,@Vin,@Model,@Description,@Creater,@CreatedDate,@Modifier,@ModifiedDate)
  return @@IDENTITY
end













' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_CarRestore]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_CarRestore]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




create PROCEDURE [dbo].[proc_CarRestore] 
  @Car int,
  @Creater int,
  @CreatedDate datetime,
  @OutState int output
AS
SET NOCOUNT ON
begin tran
  set @OutState = 0
  declare @isArchived bit
  select @isArchived = IsArchived from Car where Id = @Car
  if(@isArchived = 0)
  begin
    set @OutState = -100--该车辆处于非归档状态，可能已被其他用户还原，不能重复执行还原操作
    goto err
  end

  update [Car] set IsArchived = 0,
    Modifier=@Creater,
    ModifiedDate=@CreatedDate
  where Id = @Car
  if @@error<>0 
  begin
    set @OutState = -1--还原车辆失败
    goto err
  end


  goto succ



err:
  rollback tran;
  return 0
succ:
  commit tran
  set @OutState = 1
  return @Car












' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_BillInsert]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_BillInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'












CREATE PROCEDURE [dbo].[proc_BillInsert] 
  @Car int,
  @Instance int,
  @Project int,
  @Department int,
  @Oil int,
  @Volume float,
  @Mileage float,
  @DriverName nvarchar(20),
  @Signature int = null,
  @Rate float,
  @Oiler int,
  @Time datetime,
  @IsLost bit,
  @Creater int,
  @CreatedDate datetime,
  @OutState int output
AS
SET NOCOUNT ON
begin tran
  set @OutState = 0
  declare @isArchived bit
  declare @isReleased bit
  declare @isEnable bit
  declare @instanceBillCount int
  declare @startDate datetime
  declare @endDate datetime
  select @isArchived = IsArchived,@isReleased = IsReleased,@isEnable = IsEnable,@instanceBillCount = BillCount,@startDate = StartDate,@endDate = EndDate from Instance 
  where Id = @Instance
  if(@isArchived = 1)
  begin
    set @OutState = -100--该车辆下对应加油申请单已结束，不能加油
    goto err
  end
  if(@isReleased = 0)
  begin
    set @OutState = -200--该车辆下对应加油申请单没有通过审核或被审核员中止，不能加油
    goto err
  end
  if(@isEnable = 0)
  begin
    set @OutState = -300--该车辆下对应加油申请单已被暂停，不能加油
    goto err
  end
  if(getdate() not between @startDate and @endDate)
  begin
    set @OutState = -400--该车辆下对应加油申请单可加油时间不在当前时间范围，不能加油
    goto err
  end 


  declare @lastMileage float
  select @lastMileage = LastMileage from Car where Id = @Car
  if(@lastMileage = @Mileage)
  begin
    set @OutState = -500--车辆里程数重复，不能重复提交相同里程数的加油单
    goto err
  end


  declare @carLastOil int
  declare @carBillCount int
  select @carLastOil = LastOil,@carBillCount = BillCount from Car 
  where Id = @Car

  insert into [Bill] ([Car],[Instance],[Project],[Department],[Oil],
    [PreviousOil],[Volume],[Mileage],[DriverName],[Signature],[Rate],[Oiler],[Time],[IsLost],
    Creater,CreatedDate,Modifier,ModifiedDate) 
  values(@Car,@Instance,@Project,@Department,@Oil,
    @carLastOil,@Volume,@Mileage,@DriverName,@Signature,@Rate,@Oiler,@Time,@IsLost,
    @Creater,@CreatedDate,@Creater,@CreatedDate)
  if @@error<>0 
  begin
    set @OutState = -1--添加加油单失败
    goto err
  end

  declare @bill int
  set @bill = @@IDENTITY

  set @instanceBillCount = @instanceBillCount + 1;
  update [Instance] set BillCount = @instanceBillCount,
    Modifier=@Creater,
    ModifiedDate=@CreatedDate
  where Id = @Instance
  if @@error<>0 
  begin
    set @OutState = -2--更新申请单信息失败
    goto err
  end

  set @carBillCount = @carBillCount + 1;
  update [Car] set BillCount = @carBillCount,
    PreviousOil=@carLastOil,
    LastOil=@Oil,
    LastVolume=@Volume,
    LastMileage=@Mileage,
    LastRate=@Rate,
    Modifier=@Creater,
    ModifiedDate=@CreatedDate
  where Id = @Car
  if @@error<>0 
  begin
    set @OutState = -3--更新车辆信息失败
    goto err
  end


  goto succ



err:
  rollback tran;
  return 0
succ:
  commit tran
  set @OutState = 1
  return @bill






















' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_InstanceArchive]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_InstanceArchive]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'





CREATE PROCEDURE [dbo].[proc_InstanceArchive] 
  @Instance int,
  @Car int,
  @Creater int,
  @CreatedDate datetime,
  @OutState int output
AS
SET NOCOUNT ON
begin tran
  set @OutState = 0
  declare @isArchived bit
  select @isArchived = IsArchived from Instance where Id = @Instance
  if(@isArchived = 1)
  begin
    set @OutState = -100--璇ョ敵璇峰崟澶勪簬宸插綊妗ｇ姸鎬侊紝鍙兘宸茶鍏朵粬鐢ㄦ埛褰掓。锛屼笉鑳介噸澶嶅綊妗?
    goto err
  end

  update [Trace] set IsArchived = 1,
    Modifier = @Creater,
    ModifiedDate = @CreatedDate
  where Instance = @Instance and IsArchived = 0
  if @@error<>0 
  begin
    set @OutState = -1--鏇存柊鐢宠鍗曞饱鍘嗗綊妗ｇ姸鎬佸け璐?
    goto err
  end

  update [Instance] set IsArchived = 1,
    Modifier = @Creater,
    ModifiedDate = @CreatedDate
  where Id = @Instance
  if @@error<>0 
  begin
    set @OutState = -2--鏇存柊鐢宠鍗曞綊妗ｇ姸鎬佸け璐?
    goto err
  end

  update [Car] set Modifier=@Creater,
    ModifiedDate=@CreatedDate
  where Id = @Car
  if @@error<>0 
  begin
    set @OutState = -3--鏇存柊杞﹁締淇℃伅澶辫触
    goto err
  end


  goto succ



err:
  rollback tran;
  return 0
succ:
  commit tran
  set @OutState = 1
  return @Instance















' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_TraceInsert]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_TraceInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'






CREATE PROCEDURE [dbo].[proc_TraceInsert] 
  @Car int,
  @Instance int,
  @PreviousTrace int,
  @Status varchar(20),
  @Project int,
  @Department int,
  @UserName nvarchar(20),
  @Oils varchar(100),
  @Goal nvarchar(200),
  @StartDate datetime,
  @EndDate datetime,
  @StartInfo nvarchar(200),
  @Creater int,
  @CreatedDate datetime,
  @OutState int output
AS
SET NOCOUNT ON
begin tran
  set @OutState = 0
  declare @trace int
  declare @isArchived bit
  declare @isPending bit
  select @isArchived = IsArchived,@isPending = IsPending from Instance where Id = @Instance
  if(@isArchived = 1)
  begin
    set @OutState = -100--该申请单已归档，不能修改
    goto err
  end
  if(@isPending = 1)
  begin
    set @OutState = -200--该申请单处于待审核状态，不能修改申请单，如果需要修改，请先取回该申请单
    goto err
  end

  if(@StartDate > @EndDate or @EndDate < getdate())
  begin
    set @OutState = -300--起止时间设置有误
    goto err
  end

  insert into [Trace] ([Car],[Instance],[PreviousTrace],[Status],[Project],[Department],[UserName],
    [Oils],[Goal],[StartDate],[EndDate],[StartInfo],
    Creater,CreatedDate,Modifier,ModifiedDate) 
  values(@Car,@Instance,@PreviousTrace,@Status,@Project,@Department,@UserName,
  @Oils,@Goal,@StartDate,@EndDate,@StartInfo,
    @Creater,@CreatedDate,@Creater,@CreatedDate)
  if @@error<>0 
  begin
    set @OutState = -1--添加申请单履历失败
    goto err
  end

  set @trace = @@IDENTITY

  if(@Status = ''pending'')
  begin
    update [Instance] set IsPending = 1,
      IsEnable = 1,
      Modifier=@Creater,
      ModifiedDate=@CreatedDate
    where Id = @Instance
    if @@error<>0 
    begin
      set @OutState = -2--更新(提交)申请单信息失败
      goto err
    end
  end
  else
  begin
    update [Trace] set IsFinished = 1
    where Id = @trace
    if @@error<>0 
    begin
      set @OutState = -3--更新申请单履历结束状态失败
      goto err
    end
    --这里为了提高响应速度省略了验证是否修改过敏感信息，来check是否真的不需要提交审核
    --如果修改过敏感信息，应该抛出错误信息让用户重新提交申请单
    update [Instance] set Project = @Project,
      Department = @Department,
      UserName = @UserName,
      Oils = @Oils,
      Goal = @Goal,
      StartDate = @StartDate,
      EndDate = @EndDate,
      Modifier=@Creater,
      ModifiedDate=@CreatedDate
    where Id = @Instance
    if @@error<>0 
    begin
      set @OutState = -4--更新申请单信息失败
      goto err
    end
  end

  update [Car] set Modifier=@Creater,
    ModifiedDate=@CreatedDate
  where Id = @Car
  if @@error<>0 
  begin
    set @OutState = -5--更新车辆信息失败
    goto err
  end


  goto succ



err:
  rollback tran;
  return 0
succ:
  commit tran
  set @OutState = 1
  return @trace
















' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_InstanceAbort]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_InstanceAbort]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'








create PROCEDURE [dbo].[proc_InstanceAbort] 
  @Trace int,
  @Instance int,
  @Car int,
  @Status varchar(20),
  @StartInfo nvarchar(200),
  @Creater int,
  @CreatedDate datetime,
  @OutState int output
AS
SET NOCOUNT ON
begin tran
  set @OutState = 0
  declare @isArchived bit
  declare @isPending bit
  declare @isReleased bit
  declare @isEnable bit
  select @isArchived = IsArchived,@isPending = IsPending,@isReleased = IsReleased,@isEnable = IsEnable from Instance where Id = @Instance
  if(@isArchived = 1)
  begin
    set @OutState = -100--该申请单已被归档，不能中止
    goto err
  end
  if(@isPending = 1)
  begin
    set @OutState = -200--该申请单处于待审核状态，不能中止
    goto err
  end
  if(@isReleased = 0)
  begin
    set @OutState = -300--该申请单处于未发布状态，不能中止
    goto err
  end
  if(@isEnable = 0)
  begin
    set @OutState = -400--该申请单已被禁用，不能中止
    goto err
  end

  declare @isFinished bit
  declare @project int
  declare @department int
  declare @userName nvarchar(20)
  declare @oils varchar(20)
  declare @goal nvarchar(200)
  declare @startDate datetime
  declare @endDate datetime
  select @isFinished = IsFinished,
    @project = Project,
    @department = Department,
    @userName = UserName,
    @oils = Oils,
    @goal = Goal,
    @startDate = StartDate,
    @endDate = EndDate from Trace 
  where Id = @Trace
  if(@isFinished = 0)
  begin
    set @OutState = -500--该申请单最后一个履历处于未完成状态，不能中止
    goto err
  end

  insert into [Trace] ([Car],[Instance],[PreviousTrace],[Status],[IsFinished],[Project],[Department],[UserName],
    [Oils],[Goal],[StartDate],[EndDate],[StartInfo],
    Creater,CreatedDate,Modifier,ModifiedDate) 
  values(@Car,@Instance,@Trace,@Status,1,@project,@department,@userName,
    @oils,@goal,@startDate,@endDate,@StartInfo,
    @Creater,@CreatedDate,@Creater,@CreatedDate)
  if @@error<>0 
  begin
    set @OutState = -1--添加申请单履历失败
    goto err
  end

  update [Instance] set IsReleased = 0,
    Modifier = @Creater,
    ModifiedDate = @CreatedDate
  where Id = @Instance
  if @@error<>0 
  begin
    set @OutState = -2--更新申请单中止状态失败
    goto err
  end

  update [Car] set Modifier=@Creater,
    ModifiedDate=@CreatedDate
  where Id = @Car
  if @@error<>0 
  begin
    set @OutState = -3--更新车辆信息失败
    goto err
  end


  goto succ



err:
  rollback tran;
  return 0
succ:
  commit tran
  set @OutState = 1
  return @Instance


















' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_TraceRecapture]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_TraceRecapture]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




create PROCEDURE [dbo].[proc_TraceRecapture] 
  @Trace int,
  @Instance int,
  @Car int,
  @EndInfo nvarchar(200),
  @Creater int,
  @CreatedDate datetime,
  @OutState int output
AS
SET NOCOUNT ON
begin tran
  set @OutState = 0
  declare @isPending bit
  select @isPending = IsPending from Instance where Id = @Instance
  if(@isPending = 0)
  begin
    set @OutState = -100--该申请单处于非待审核状态，可能已被其他用户取回，不能取回申请单
    goto err
  end
  declare @isFinished bit
  select @isFinished = IsFinished from Trace where Id = @Trace
  if(@isFinished = 1)
  begin
    set @OutState = -200--该申请单待审核履历已结束，可能已被其他用户取回，不能取回申请单
    goto err
  end

  update [Trace] set Status = ''recaptured'',
    IsFinished = 1,
    EndInfo = @EndInfo,
    Modifier = @Creater,
    ModifiedDate = @CreatedDate
  where Id = @Trace
  if @@error<>0 
  begin
    set @OutState = -1--更新申请单履历信息失败
    goto err
  end

  update [Instance] set IsPending = 0,
    Modifier = @Creater,
    ModifiedDate = @CreatedDate
  where Id = @Instance
  if @@error<>0 
  begin
    set @OutState = -2--更新申请单信息失败
    goto err
  end

  update [Car] set Modifier=@Creater,
    ModifiedDate=@CreatedDate
  where Id = @Car
  if @@error<>0 
  begin
    set @OutState = -3--更新车辆信息失败
    goto err
  end


  goto succ



err:
  rollback tran;
  return 0
succ:
  commit tran
  set @OutState = 1
  return @Trace














' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_CarArchive]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_CarArchive]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




create PROCEDURE [dbo].[proc_CarArchive] 
  @Car int,
  @Creater int,
  @CreatedDate datetime,
  @OutState int output
AS
SET NOCOUNT ON
begin tran
  set @OutState = 0
  declare @unArchivedCount int
  select @unArchivedCount = count(*) from Instance where Car = @Car and IsArchived = 0
  if(@unArchivedCount <> 0)
  begin
    set @OutState = -100--该车辆下有申请单没有归档，不能归档该车辆
    goto err
  end

  update [Car] set IsArchived = 1,
    Modifier=@Creater,
    ModifiedDate=@CreatedDate
  where Id = @Car
  if @@error<>0 
  begin
    set @OutState = -1--归档车辆失败
    goto err
  end


  goto succ



err:
  rollback tran;
  return 0
succ:
  commit tran
  set @OutState = 1
  return @Car












' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_OneTouchOptimize]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_OneTouchOptimize]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'








CREATE PROCEDURE [dbo].[proc_OneTouchOptimize] 
  @Creater int,
  @CreatedDate datetime,
  @OutAllCarsCount int output,
  @OutArchivedCarsCount int output,
  @OutArchivedInstancesCount int output,
  @OutState int output
AS
SET NOCOUNT ON
begin tran
  set @OutAllCarsCount = 0
  set @OutArchivedCarsCount = 0
  set @OutArchivedInstancesCount = 0
  set @OutState = 0
  
  declare @tempInstanceId int
  declare @tempCarId int
  declare @cursor cursor
  --用游标找出没有归档且已过期的申请单，并循环归档
  set @cursor = cursor for select Id,Car from [Instance] where IsArchived = 0 and EndDate < getDate()
  open @cursor

  fetch next from @cursor into @tempInstanceId,@tempCarId

  while(@@fetch_status = 0)
  begin
    update [Trace] set IsArchived = 1,
      Modifier = @Creater,
      ModifiedDate = @CreatedDate
    where Instance = @tempInstanceId and IsArchived = 0
    if @@error<>0 
    begin
      set @OutState = -1--更新申请单履历归档状态失败
      goto err
    end

    update [Instance] set IsArchived = 1,
      Modifier = @Creater,
      ModifiedDate = @CreatedDate
    where Id = @tempInstanceId
    if @@error<>0 
    begin
      set @OutState = -2--更新申请单归档状态失败
      goto err
    end

    update [Car] set Modifier=@Creater,
      ModifiedDate=@CreatedDate
    where Id = @tempCarId
    if @@error<>0 
    begin
      set @OutState = -3--更新申请单关联车辆信息失败
      goto err
    end

    set @OutArchivedInstancesCount = @OutArchivedInstancesCount + 1

    fetch next from @cursor into @tempInstanceId,@tempCarId
  end

  close @cursor
  deallocate @cursor


  --用游标找出没有归档且未归档申请单个数为0的车辆，并循环归档
  set @cursor = cursor for select Id from [Car] where IsArchived = 0
  open @cursor

  fetch next from @cursor into @tempCarId

  while(@@fetch_status = 0)
  begin
    if 0 = (select count(*) from Instance where IsArchived = 0 and Car = @tempCarId)
    begin
      update [Car] set IsArchived = 1,
        Modifier=@Creater,
        ModifiedDate=@CreatedDate
      where Id = @tempCarId
      if @@error<>0 
      begin
        set @OutState = -11--归档车辆失败
        goto err
      end

      set @OutArchivedCarsCount = @OutArchivedCarsCount + 1
    end

    fetch next from @cursor into @tempCarId
  end

  close @cursor
  deallocate @cursor

  select @OutAllCarsCount = count(*) from Car

  goto succ



err:
  rollback tran;
  return 0
succ:
  commit tran
  set @OutState = 1
  return 1


















' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_InstanceForbid]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_InstanceForbid]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'







CREATE PROCEDURE [dbo].[proc_InstanceForbid] 
  @Trace int,
  @Instance int,
  @Car int,
  @Status varchar(20),
  @StartInfo nvarchar(200),
  @Creater int,
  @CreatedDate datetime,
  @OutState int output
AS
SET NOCOUNT ON
begin tran
  set @OutState = 0
  declare @isArchived bit
  declare @isPending bit
  declare @isReleased bit
  declare @isEnable bit
  select @isArchived = IsArchived,@isPending = IsPending,@isReleased = IsReleased,@isEnable = IsEnable from Instance where Id = @Instance
  if(@isArchived = 1)
  begin
    set @OutState = -100--该申请单已被归档，不能禁用
    goto err
  end
  if(@isPending = 1)
  begin
    set @OutState = -200--该申请单处于待审核状态，不能禁用
    goto err
  end
  if(@isReleased = 0)
  begin
    set @OutState = -300--该申请单处于未发布状态，不能禁用
    goto err
  end
  if(@isEnable = 0)
  begin
    set @OutState = -400--该申请单已被禁用，不能重复禁用
    goto err
  end

  declare @isFinished bit
  declare @project int
  declare @department int
  declare @userName nvarchar(20)
  declare @oils varchar(20)
  declare @goal nvarchar(200)
  declare @startDate datetime
  declare @endDate datetime
  select @isFinished = IsFinished,
    @project = Project,
    @department = Department,
    @userName = UserName,
    @oils = Oils,
    @goal = Goal,
    @startDate = StartDate,
    @endDate = EndDate from Trace 
  where Id = @Trace
  if(@isFinished = 0)
  begin
    set @OutState = -500--该申请单最后一个履历处于未完成状态，不能禁用
    goto err
  end

  insert into [Trace] ([Car],[Instance],[PreviousTrace],[Status],[IsFinished],[Project],[Department],[UserName],
    [Oils],[Goal],[StartDate],[EndDate],[StartInfo],
    Creater,CreatedDate,Modifier,ModifiedDate) 
  values(@Car,@Instance,@Trace,@Status,1,@project,@department,@userName,
    @oils,@goal,@startDate,@endDate,@StartInfo,
    @Creater,@CreatedDate,@Creater,@CreatedDate)
  if @@error<>0 
  begin
    set @OutState = -1--添加申请单履历失败
    goto err
  end

  update [Instance] set IsEnable = 0,
    Modifier = @Creater,
    ModifiedDate = @CreatedDate
  where Id = @Instance
  if @@error<>0 
  begin
    set @OutState = -2--更新申请单禁用状态失败
    goto err
  end

  update [Car] set Modifier=@Creater,
    ModifiedDate=@CreatedDate
  where Id = @Car
  if @@error<>0 
  begin
    set @OutState = -3--更新车辆信息失败
    goto err
  end


  goto succ



err:
  rollback tran;
  return 0
succ:
  commit tran
  set @OutState = 1
  return @Instance

















' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_BillByIdUpdate]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_BillByIdUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




create PROCEDURE [dbo].[proc_BillByIdUpdate] 
  @Id int,
  @Project int,
  @Department int,
  @Oil int,
  @Volume float,
  @Mileage float,
  @DriverName nvarchar(20),
  @Rate float,
  @Time datetime,
  @Modifier int,
  @ModifiedDate datetime
AS
begin
  Update [Bill] set [Project]=@Project,
    [Department]=@Department,
    [Oil]=@Oil,
    [Volume]=@Volume,
    [Mileage]=@Mileage,
    [DriverName]=@DriverName,
    [Rate]=@Rate,
    [Time]=@Time,
    [IsLost]=1,
    Modifier=@Modifier,
    ModifiedDate=@ModifiedDate 
  where Id=@Id
end









' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_SignatureInsert]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_SignatureInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'





create PROCEDURE [dbo].[proc_SignatureInsert] 
  @Name nvarchar(10),
  @Sign image,
  @Creater int,
  @CreatedDate datetime
AS
begin
  /* SET NOCOUNT ON */ 
  insert into [Signature] ([Name],[Sign],Creater,CreatedDate) 
  values(@Name,@Sign,@Creater,@CreatedDate)
  return @@IDENTITY
end














' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_PreferenceByIdUpdate]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_PreferenceByIdUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[proc_PreferenceByIdUpdate] 
  @Id int,
  @ShortcutHour int,
  @FinishHour int,
  @Modifier int,
  @ModifiedDate datetime
AS
begin
  Update [Preference] set 
    [ShortcutHour]=@ShortcutHour,
    [FinishHour]=@FinishHour,
    Modifier=@Modifier,
    ModifiedDate=@ModifiedDate 
  where Id=@Id
end







' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_UserByIdUpdate]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_UserByIdUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[proc_UserByIdUpdate] 
  @Id int,
  @Name nvarchar(20),
  @Phone varchar(20),
  @Email varchar(100),
  @Role int,
  @Signature nvarchar(200),
  @IsSignNeeded bit,
  @IsEnable bit,
  @Modifier int,
  @ModifiedDate datetime
AS
begin
  Update [User] set [Name]=@Name,
    Phone=@Phone,
    Email=@Email,
    [Role]=@Role,
    Signature=@Signature,
    IsSignNeeded=@IsSignNeeded,
    IsEnable=@IsEnable,
    Modifier=@Modifier,
    ModifiedDate=@ModifiedDate 
  where Id=@Id
end


' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_UserInsert]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_UserInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[proc_UserInsert] 
  @Name nvarchar(20),
  @Phone varchar(20),
  @Email varchar(20),
  @Role int,
  @Signature nvarchar(200),
  @IsSignNeeded bit,
  @Creater int,
  @CreatedDate datetime,
  @Modifier int,
  @ModifiedDate datetime
AS
begin
  /* SET NOCOUNT ON */ 
  insert into [User] ([Name],[Phone],Email,[Role],Signature,IsSignNeeded,Creater,CreatedDate,Modifier,ModifiedDate) 
  values(@Name,@Phone,@Email,@Role,@Signature,@IsSignNeeded,@Creater,@CreatedDate,@Modifier,@ModifiedDate)
  return @@IDENTITY
end

' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_AccountpwdUpdate]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_AccountpwdUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'





CREATE PROCEDURE [dbo].[proc_AccountpwdUpdate] 
  @User int,
  @OldPassword varchar(50),
  @NewPassword varchar(50),
  @OutState int output
AS
begin
  declare @count int
  select @count = count(*) from [User] where Id = @User
  set @OutState = 1
  if(@count > 0)
  begin
    select @count = count(*) from [User] where Id = @User and password = @OldPassword
    if(@count > 0)
    begin
      Update [User] set [Password]=@NewPassword where  Id = @User
    end
    else
    begin
      set @OutState = -2--密码错误
    end
  end
  else
  begin
    set @OutState = -1--用户不存在
  end
end










' 
END
GO
/****** 对象:  StoredProcedure [dbo].[proc_UserResetpwd]    脚本日期: 08/24/2017 08:33:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_UserResetpwd]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



create PROCEDURE [dbo].[proc_UserResetpwd] 
  @User int,
  @NewPassword varchar(50)
AS
begin
  Update [User] set [Password]=@NewPassword
  where Id=@User
end











' 
END





/*==============================================================*/
/* 插入必要的基础数据                                            */
/*==============================================================*/
insert into [Role] ([Name],[Powers],[Description]) values ('系统管理员','1,2,3,4','系统管理员作为系统中最高级别角色，有最高级别功能权限。')
insert into [User] ([Role],[Name],[Phone]) values (1,'初始账户','1')
update [Role] set [Creater] = 1,[Modifier] = 1
update [User] set [Creater] = 1,[Modifier] = 1

insert into [Power] ([Name],[Description]) values ('系统设置','设置系统基础资料')
insert into [Power] ([Name],[Description]) values ('试件管理','添加、删除、修改试件信息')
insert into [Power] ([Name],[Description]) values ('试件打印','打印试件二维码')
insert into [Power] ([Name],[Description]) values ('试件扫码','扫描二维码查看试件信息')


insert into [Role] ([Name],[Powers],[Description],[Creater],[CreatedDate],[Modifier],[ModifiedDate]) 
  values ('试件管理员','2,3,4','试件管理员作为系统中试件管理人员，主要负责维护试件信息。',
  1,DATEADD(ss,1,getdate()),1,DATEADD(ss,1,getdate()))
insert into [Role] ([Name],[Powers],[Description],[Creater],[CreatedDate],[Modifier],[ModifiedDate]) 
  values ('试件打印员','3,4','试件打印员可以查看试件信息并打印为二维码。',
  1,DATEADD(ss,2,getdate()),1,DATEADD(ss,2,getdate()))
insert into [Role] ([Name],[Powers],[Description],[Creater],[CreatedDate],[Modifier],[ModifiedDate]) 
  values ('游客','4','游客可以扫描二维码查看试件信息。',
  1,DATEADD(ss,3,getdate()),1,DATEADD(ss,3,getdate()))


insert into [User] ([Role],[Name],[Phone],[Creater],[CreatedDate],[Modifier],[ModifiedDate]) values (2,'试件记录员','2',1,DATEADD(ss,1,getdate()),1,DATEADD(ss,1,getdate()))
insert into [User] ([Role],[Name],[Phone],[Creater],[CreatedDate],[Modifier],[ModifiedDate]) values (3,'打印工','3',1,DATEADD(ss,2,getdate()),1,DATEADD(ss,2,getdate()))
insert into [User] ([Role],[Name],[Phone],[Creater],[CreatedDate],[Modifier],[ModifiedDate]) values (4,'游客','4',1,DATEADD(ss,3,getdate()),1,DATEADD(ss,3,getdate()))


insert into [Piece] ([Name],[Number],[IsEnable],[Description],[Creater],[CreatedDate],[Modifier],[ModifiedDate]) 
  values ('试件1','20180908100',1,'',
  1,DATEADD(ss,1,getdate()),1,DATEADD(ss,1,getdate()))
insert into [Piece] ([Name],[Number],[IsEnable],[Description],[Creater],[CreatedDate],[Modifier],[ModifiedDate]) 
  values ('试件2','20180908101',1,'',
  1,DATEADD(ss,2,getdate()),1,DATEADD(ss,2,getdate()))
insert into [Piece] ([Name],[Number],[IsEnable],[Description],[Creater],[CreatedDate],[Modifier],[ModifiedDate]) 
  values ('试件3','20180908102',1,'',
  1,DATEADD(ss,3,getdate()),1,DATEADD(ss,3,getdate()))


insert into [Project] ([Name],[IsEnable],[Description],[Creater],[CreatedDate],[Modifier],[ModifiedDate]) 
  values ('项目1',1,'',
  1,DATEADD(ss,1,getdate()),1,DATEADD(ss,1,getdate()))
insert into [Project] ([Name],[IsEnable],[Description],[Creater],[CreatedDate],[Modifier],[ModifiedDate]) 
  values ('项目2',1,'',
  1,DATEADD(ss,2,getdate()),1,DATEADD(ss,2,getdate()))
insert into [Project] ([Name],[IsEnable],[Description],[Creater],[CreatedDate],[Modifier],[ModifiedDate]) 
  values ('项目3',1,'',
  1,DATEADD(ss,3,getdate()),1,DATEADD(ss,3,getdate()))


insert into [Department] ([Name],[Description],[Creater],[CreatedDate],[Modifier],[ModifiedDate]) 
  values ('部门1','',
  1,DATEADD(ss,1,getdate()),1,DATEADD(ss,1,getdate()))
insert into [Department] ([Name],[Description],[Creater],[CreatedDate],[Modifier],[ModifiedDate]) 
  values ('部门2','',
  1,DATEADD(ss,2,getdate()),1,DATEADD(ss,2,getdate()))


insert into [Oil] ([Name],[YellowRate],[RedRate],[Description],[Creater],[CreatedDate],[Modifier],[ModifiedDate]) 
  values ('90',0,0,'',
  1,DATEADD(ss,1,getdate()),1,DATEADD(ss,1,getdate()))
insert into [Oil] ([Name],[YellowRate],[RedRate],[Description],[Creater],[CreatedDate],[Modifier],[ModifiedDate]) 
  values ('93',0,0,'',
  1,DATEADD(ss,2,getdate()),1,DATEADD(ss,2,getdate()))
insert into [Oil] ([Name],[YellowRate],[RedRate],[Description],[Creater],[CreatedDate],[Modifier],[ModifiedDate]) 
  values ('97',0,0,'',
  1,DATEADD(ss,3,getdate()),1,DATEADD(ss,3,getdate()))


insert into [Preference] ([ShortcutHour],[Creater],[CreatedDate],[Modifier],[ModifiedDate]) 
  values (8,1,DATEADD(ss,1,getdate()),1,DATEADD(ss,1,getdate()))
