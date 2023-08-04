--update 3.4.1.2 KPMG Changes 1
USE [ZulAssetsBE]
GO

/****** Object:  Table [dbo].[OfflineMachines]    Script Date: 10/09/2010 10:01:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OfflineMachines](
	[MachineID] [int] NOT NULL,
	[MachineDesc] [nvarchar](50) NOT NULL,
	[ServerName] [nvarchar](50) NULL,
	[DatabaseName] [nvarchar](50) NULL,[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Port] [nvarchar](50) NULL,
	[LastAssetNumber] [bigint] NULL,
	[StartSerial] [bigint] NULL,
	[EndSerial] [bigint] NULL,
	[CompanyID] [int] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_RemoteMachines] PRIMARY KEY CLUSTERED 
(
	[MachineID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



UPDATE [ZulAssetsBE].[dbo].[DB Version]
   SET [Version] = 'V3.4.2.0'
      ,[LastUpdate] = getdate()


GO
ALTER TABLE dbo.Companies ADD
	LastAssetNumber bigint NULL
GO
ALTER TABLE dbo.Companies ADD CONSTRAINT
	DF_Companies_LastAssetNumber DEFAULT 0 FOR LastAssetNumber
GO

update dbo.Companies set LastAssetNumber = (select max(astnum) from assetdetails where assetdetails.CompanyID = Companies.CompanyId)

GO
ALTER TABLE dbo.SysSettings ADD
	IsOfflineMachine bit NULL
GO

ALTER TABLE dbo.SysSettings ADD CONSTRAINT
	DF_SysSettings_IsOfflineMachine DEFAULT 0 FOR IsOfflineMachine
GO
	
update dbo.SysSettings set IsOfflineMachine = 0

GO

--update 3.4.2.4 KPMG Changes 2:

UPDATE [ZulAssetsBE].[dbo].[DB Version]
   SET [Version] = 'V3.4.2.4'
      ,[LastUpdate] = getdate()
GO

ALTER TABLE dbo.Location ADD
	LocationFullPath varchar(MAX) NULL
GO

ALTER TABLE dbo.Category ADD
	CatFullPath varchar(MAX) NULL
GO

	  
--update 3.4.3.0 CMA Changes:
Use ZulAssetsBETemp
GO
CREATE TABLE dbo.AssetsTempReceiving
	(
	DeviceID int NOT NULL,
	AstID nvarchar(50) NOT NULL,
	AstDesc nvarchar(255) NULL,
	AstCatID nvarchar(255) NULL,
	LocID nvarchar(255) NULL,
	Status int NULL,
	Pieces int NULL,
	BarCode varchar(50) NULL
	)  ON [PRIMARY]
GO

ALTER TABLE dbo.NonBarCodedTemp ADD
	Model nvarchar(100) NULL,
	Serial nvarchar(100) NULL,
	TransDate datetime  NULL
GO

use ZulAssetsBE
GO
UPDATE [ZulAssetsBE].[dbo].[DB Version]
   SET [Version] = 'V3.4.3.0'
      ,[LastUpdate] = getdate()
	  
GO

BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Category
	(
	AstCatID varchar(25) NOT NULL,
	AstCatDesc nvarchar(100) COLLATE Arabic_CI_AS NULL,
	IsDeleted bit NULL,
	ID1 bigint NOT NULL,
	Code nvarchar(50) COLLATE Arabic_CI_AS NULL,
	CompCode nvarchar(255) COLLATE Arabic_CI_AS NULL,
	catLevel int NULL,
	CatFullPath nvarchar(MAX) COLLATE Arabic_CI_AS NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
IF EXISTS(SELECT * FROM dbo.Category)
	 EXEC('INSERT INTO dbo.Tmp_Category (AstCatID, AstCatDesc, IsDeleted, ID1, Code, CompCode, catLevel, CatFullPath)
		SELECT AstCatID, CONVERT(nvarchar(100), AstCatDesc), IsDeleted, ID1, Code, CompCode, catLevel, CONVERT(nvarchar(MAX), CatFullPath) FROM dbo.Category WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.Category
GO
EXECUTE sp_rename N'dbo.Tmp_Category', N'Category', 'OBJECT' 
GO
ALTER TABLE dbo.Category ADD CONSTRAINT
	PK_Category PRIMARY KEY CLUSTERED 
	(
	AstCatID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE TABLE dbo.Tmp_Location
	(
	LocID varchar(255) NOT NULL,
	LocDesc nvarchar(100) COLLATE Arabic_CI_AS NULL,
	IsDeleted bit NULL,
	ID1 bigint NOT NULL,
	Code nvarchar(50) COLLATE Arabic_CI_AS NULL,
	CompCode nvarchar(255) COLLATE Arabic_CI_AS NULL,
	LocLevel int NULL,
	LocationFullPath nvarchar(MAX) COLLATE Arabic_CI_AS NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
IF EXISTS(SELECT * FROM dbo.Location)
	 EXEC('INSERT INTO dbo.Tmp_Location (LocID, LocDesc, IsDeleted, ID1, Code, CompCode, LocLevel, LocationFullPath)
		SELECT CONVERT(nvarchar(255), LocID), CONVERT(nvarchar(100), LocDesc), IsDeleted, ID1, CONVERT(nvarchar(50), Code), CONVERT(nvarchar(255), CompCode), LocLevel, CONVERT(nvarchar(MAX), LocationFullPath) FROM dbo.Location WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.Location
GO
EXECUTE sp_rename N'dbo.Tmp_Location', N'Location', 'OBJECT' 
GO
ALTER TABLE dbo.Location ADD CONSTRAINT
	PK_Location PRIMARY KEY CLUSTERED 
	(
	LocID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE TABLE dbo.Tmp_Assets
	(
	ItemCode varchar(25) NOT NULL,
	AstBrandID int NULL,
	AstCatID varchar(25) NULL,
	POItmID varchar(25) NULL,
	AstDesc nvarchar(100) COLLATE Arabic_CI_AS NULL,
	AstModel varchar(50) NULL,
	AstQty bigint NULL,
	IsDeleted bit NULL,
	ItmImage image NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
IF EXISTS(SELECT * FROM dbo.Assets)
	 EXEC('INSERT INTO dbo.Tmp_Assets (ItemCode, AstBrandID, AstCatID, POItmID, AstDesc, AstModel, AstQty, IsDeleted, ItmImage)
		SELECT ItemCode, AstBrandID, AstCatID, POItmID, CONVERT(nvarchar(100), AstDesc), AstModel, AstQty, IsDeleted, ItmImage FROM dbo.Assets WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.Assets
GO
EXECUTE sp_rename N'dbo.Tmp_Assets', N'Assets', 'OBJECT' 
GO
ALTER TABLE dbo.Assets ADD CONSTRAINT
	PK_Assets PRIMARY KEY CLUSTERED 
	(
	ItemCode
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

COMMIT
GO

CREATE TABLE dbo.Tmp_AssetDetails
	(
	AstID varchar(25) NOT NULL,
	DispCode int NULL,
	ItemCode varchar(25) NULL,
	SuppID varchar(25) NULL,
	POCode bigint NULL,
	InvNumber varchar(25) NULL,
	CustodianID varchar(25) NULL,
	BaseCost float(53) NULL,
	Tax float(53) NULL,
	SrvDate datetime NULL,
	PurDate datetime NULL,
	Disposed bit NULL,
	DispDate datetime NULL,
	InvSchCode bigint NULL,
	BookID varchar(50) NULL,
	InsID int NULL,
	LocID varchar(255) NULL,
	InvStatus int NULL,
	IsDeleted bit NULL,
	IsSold bit NULL,
	Sel_Date datetime NULL,
	Sel_Price float(53) NULL,
	SoldTo varchar(50) NULL,
	RefNo varchar(50) NULL,
	AstNum bigint NOT NULL,
	AstBrandId int NULL,
	AstDesc nvarchar(200) COLLATE Arabic_CI_AS NULL,
	AstModel nvarchar(50) NULL,
	CompanyID int NULL,
	TransRemarks varchar(200) NULL,
	LabelCount int NULL,
	OldAssetID varchar(50) NULL,
	Discount bigint NULL,
	NoPiece int NULL,
	BarCode varchar(50) NULL,
	SerailNo nvarchar(50) NULL,
	RefCode varchar(50) NULL,
	Plate varchar(50) NULL,
	Poerp varchar(50) NULL,
	Capex varchar(50) NULL,
	Grn varchar(50) NULL,
	GLCode bigint NULL,
	PONumber nvarchar(50) NULL,
	AstDesc2 nvarchar(200) COLLATE Arabic_CI_AS NULL,
	AstImage image NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
IF EXISTS(SELECT * FROM dbo.AssetDetails)
	 EXEC('INSERT INTO dbo.Tmp_AssetDetails (AstID, DispCode, ItemCode, SuppID, POCode, InvNumber, CustodianID, BaseCost, Tax, SrvDate, PurDate, Disposed, DispDate, InvSchCode, BookID, InsID, LocID, InvStatus, IsDeleted, IsSold, Sel_Date, Sel_Price, SoldTo, RefNo, AstNum, AstBrandId, AstDesc, AstModel, CompanyID, TransRemarks, LabelCount, OldAssetID, Discount, NoPiece, BarCode, SerailNo, RefCode, Plate, Poerp, Capex, Grn, GLCode, PONumber, AstDesc2, AstImage)
		SELECT AstID, DispCode, ItemCode, SuppID, POCode, InvNumber, CustodianID, BaseCost, Tax, SrvDate, PurDate, Disposed, DispDate, InvSchCode, BookID, InsID, LocID, InvStatus, IsDeleted, IsSold, Sel_Date, Sel_Price, SoldTo, RefNo, AstNum, AstBrandId, AstDesc, CONVERT(nvarchar(50), AstModel), CompanyID, TransRemarks, LabelCount, OldAssetID, Discount, NoPiece, BarCode, CONVERT(nvarchar(50), SerailNo), RefCode, Plate, Poerp, Capex, Grn, GLCode, PONumber, AstDesc2, AstImage FROM dbo.AssetDetails WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.AssetDetails
GO
EXECUTE sp_rename N'dbo.Tmp_AssetDetails', N'AssetDetails', 'OBJECT' 
GO
ALTER TABLE dbo.AssetDetails ADD CONSTRAINT
	PK_AssetDetails PRIMARY KEY CLUSTERED 
	(
	AstID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO



--update 3.4.4.0 CMA Changes:
insert into AuditStatus values(5,'Anonymous')
GO
insert into AuditStatus values(6,'Lost')
GO

ALTER TABLE dbo.Ast_History ADD
	DeptName nvarchar(MAX) NULL,
	Remarks nvarchar(MAX) NULL
GO
update [DB Version] set version = 'V3.4.4.0',lastUpdate = getdate()


Use ZulAssetsBETemp
CREATE TABLE dbo.DBVersion
	(
	Version varchar(50) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.DBVersion ADD CONSTRAINT
	PK_DBVersion PRIMARY KEY CLUSTERED 
	(
	Version
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.DBVersion ADD
	LastUpdate datetime NULL
GO

insert into DBVersion values('V3.4.4.0',getdate())
update DBVersion set version = 'V3.4.4.0',lastUpdate = getdate()


--update 3.4.5.0 CMA Changes:

update Ast_INV_Schedule set InvDesc = 'System Trans' where InvSchCode = 1

ALTER TABLE dbo.Roles ADD
	CMAExport smallint NULL,
	CMAImport smallint NULL,
	DataProcessing smallint NULL
GO

Update roles set CMAExport = 1, CMAImport= 1 ,DataProcessing = 1 where RoleID = 1
Go
Update roles set CMAExport = 0, CMAImport= 0 ,DataProcessing = 0 where RoleID <> 1
Go

update [DB Version] set version = 'V3.4.5.0',lastUpdate = getdate()


--update 3.4.6.1
Use ZulAssetsBETemp
CREATE TABLE dbo.Tmp_LocationTemp
	(
	LocID nvarchar(255) NULL,
	LocDesc nvarchar(100) NULL,
	IsDeleted bit NULL,
	ID1 bigint NULL,
	LocLevel int NULL
	)  ON [PRIMARY]
GO
IF EXISTS(SELECT * FROM dbo.LocationTemp)
	 EXEC('INSERT INTO dbo.Tmp_LocationTemp (LocID, LocDesc, IsDeleted, ID1, LocLevel)
		SELECT CONVERT(nvarchar(255), LocID), CONVERT(nvarchar(100), LocDesc), IsDeleted, ID1, LocLevel FROM dbo.LocationTemp WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.LocationTemp
GO
EXECUTE sp_rename N'dbo.Tmp_LocationTemp', N'LocationTemp', 'OBJECT' 
GO
update DBVersion set version = 'V3.4.6.1',lastUpdate = getdate()

Use ZulAssetsBE
update [DB Version] set version = 'V3.4.6.1',lastUpdate = getdate()



--Updated in the diagram


--update 3.4.6.3
Use ZulAssetsBETemp
CREATE TABLE dbo.Tmp_LocationTemp
	(
	LocID nvarchar(255) NULL,
	LocDesc nvarchar(100) NULL,
	IsDeleted bit NULL,
	ID1 bigint NULL,
	LocLevel int NULL
	)  ON [PRIMARY]
GO
IF EXISTS(SELECT * FROM dbo.LocationTemp)
	 EXEC('INSERT INTO dbo.Tmp_LocationTemp (LocID, LocDesc, IsDeleted, ID1, LocLevel)
		SELECT CONVERT(nvarchar(255), LocID), CONVERT(nvarchar(100), LocDesc), IsDeleted, ID1, LocLevel FROM dbo.LocationTemp WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.LocationTemp
GO
EXECUTE sp_rename N'dbo.Tmp_LocationTemp', N'LocationTemp', 'OBJECT' 
GO
update DBVersion set version = 'V3.4.6.3',lastUpdate = getdate()

Use ZulAssetsBE

CREATE TABLE [dbo].[SYSPARAMETER](
	[PARAMETERID] [int] NOT NULL,
	[DESCRIPTION] [nvarchar](255) NULL,
	[DESCRIPTIONAR] [nvarchar](255) NULL,
	[NAME] [nvarchar](255) NULL,
	[VALUE] [nvarchar](255) NULL,
	[Type] [nvarchar](50) NULL,
 CONSTRAINT [PK_SYSPARAMETER] PRIMARY KEY NONCLUSTERED 
(
	[PARAMETERID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

insert into [dbo].[SYSPARAMETER] values(1,'Max Location Level',NULL, 'MaxLocationLevel',4,'Decimal')
insert into [dbo].[SYSPARAMETER] values(2,'Max Category Level',NULL, 'MaxCategoryLevel',2,'Decimal')
insert into [dbo].[SYSPARAMETER] values(3,'Asset location min level',NULL, 'Assetlocationminlevel',4,'Decimal')
insert into [dbo].[SYSPARAMETER] values(4,'Asset Category min level',NULL, 'AssetCategoryminlevel',2,'Decimal')

--update 3.4.6.6
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Ast_INV_Schedule
	(
	InvSchCode bigint NOT NULL,
	InvDesc nvarchar(100) NULL,
	InvStartDate datetime NULL,
	InvEndDate datetime NULL,
	IsDeleted bit NULL,
	Closed bit NULL
	)  ON [PRIMARY]
GO
IF EXISTS(SELECT * FROM dbo.Ast_INV_Schedule)
	 EXEC('INSERT INTO dbo.Tmp_Ast_INV_Schedule (InvSchCode, InvDesc, InvStartDate, InvEndDate, IsDeleted, Closed)
		SELECT InvSchCode, CONVERT(nvarchar(100), InvDesc), InvStartDate, InvEndDate, IsDeleted, Closed FROM dbo.Ast_INV_Schedule WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.Ast_INV_Schedule
GO
EXECUTE sp_rename N'dbo.Tmp_Ast_INV_Schedule', N'Ast_INV_Schedule', 'OBJECT' 
GO
ALTER TABLE dbo.Ast_INV_Schedule ADD CONSTRAINT
	PK_Ast_INV_Schedule PRIMARY KEY CLUSTERED 
	(
	InvSchCode
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
GO
update [DB Version] set version = 'V3.4.6.6',lastUpdate = getdate()

--update 3.4.7.4 ABB Modfication.

GO
ALTER TABLE dbo.AssetDetails ADD
	CapitalizationDate datetime NULL,
	BussinessArea nvarchar(50) NULL,
	InventoryNumber nvarchar(50) NULL,
	CostCenterID varchar(50) NULL,
	InStockAsset bit NULL,
	EvaluationGroup1 nvarchar(50) NULL,
	EvaluationGroup2 nvarchar(50) NULL,
	EvaluationGroup3 nvarchar(50) NULL,
	EvaluationGroup4 nvarchar(50) NULL,
	CreatedBY nvarchar(50) NULL,
	IsDataChanged bit NULL
GO
update [DB Version] set version = 'V3.4.7.4',lastUpdate = getdate()



--update V3.4.7.4  ABB Modfication.
Use ZulAssetsBETemp
ALTER TABLE dbo.AssetsTemp ADD
	CustodianID varchar(25) NULL,
	RefNo varchar(50) NULL,
	SerailNo nvarchar(50) NULL,
	InventoryNumber nvarchar(50) NULL,
	InStockAsset bit NULL,
	IsDataChanged bit NULL,
	CostCenter varchar(50) NULL
GO

ALTER TABLE dbo.AssetsTempReceiving ADD
	CustodianID varchar(25) NULL,
	RefNo varchar(50) NULL,
	SerailNo nvarchar(50) NULL,
	InventoryNumber nvarchar(50) NULL,
	CostCenter varchar(50) NULL,
	InStockAsset bit NULL,
	IsDataChanged bit NULL
GO
ALTER TABLE dbo.NonBarCodedTemp ADD
	RefNo varchar(50) NULL
GO
update DBVersion set version = 'V3.4.7.4',lastUpdate = getdate()


--update V3.5.0.0 ABB Modfication.
Use ZulAssetsBETemp
CREATE TABLE [dbo].[GR](
	[GRID] [int] NOT NULL,
	[DeviceID] [int] NOT NULL,
	[PONo] [nvarchar](100) NOT NULL,
	[DeliveryNo] [nvarchar](100) NOT NULL,
	[PostingDate] [datetime] NULL
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[GRItems](
	[GRItemID] [int] NOT NULL,
	[GRID] [int] NOT NULL,
	[DeviceID] [int] NOT NULL,
	[ManPartNo] [nvarchar](100) NOT NULL,
	[SAPMatCode] [nvarchar](100) NOT NULL,
	[LineItemNo] [int] NOT NULL,
	[SeqNo] [int] NOT NULL,
	[ProductSerialNo] [nvarchar](100) NOT NULL,
	[Status] [int] NOT NULL
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[GI](
	[GIID] [int] NOT NULL,
	[DeviceID] [int] NOT NULL,
	[IssueDate] [datetime] NOT NULL,
	[InvProposalNo] [nvarchar](100) NULL,
	[EmpNo] [nvarchar](100) NOT NULL,
	[EmpName] [nvarchar](100) NOT NULL,
	[PORequisitionNo] [nvarchar](100) NULL,
	[IsInvestmentProposal] [bit] NULL,
	[IsPurchaseRequisition] [bit] NULL
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[GIItems](
	[GIItemID] [int] NOT NULL,
	[GIID] [int] NOT NULL,
	[DeviceID] [int] NOT NULL,
	[LineItemNo] [int] NOT NULL,
	[ManPartNo] [nvarchar](100) NOT NULL,
	[SAPMatCode] [nvarchar](100) NOT NULL,
	[SerialNo] [nvarchar](100) NOT NULL,
	[AssetNo] [nvarchar](100) NULL,
	[CostCenter] [nvarchar](100) NULL,
	[BusinessArea] [nvarchar](100) NULL,
	[GLAC] [nvarchar](100) NULL
) ON [PRIMARY]

GO
update DBVersion set version = 'V3.5.0.0',lastUpdate = getdate()

--update V3.5.0.2 ABB Modfication.
ALTER TABLE dbo.GIItems ADD
	ID bigint NOT NULL IDENTITY (1, 1),
	ExportedFlag bit NULL
GO
ALTER TABLE dbo.GIItems ADD CONSTRAINT
	DF_GIItems_ExportedFlag DEFAULT ((0)) FOR ExportedFlag
GO
ALTER TABLE dbo.GIItems ADD CONSTRAINT
	PK_GIItems PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.GRItems ADD
	ID bigint NOT NULL IDENTITY (1, 1),
	ExportedFlag bit NULL
GO
ALTER TABLE dbo.GRItems ADD CONSTRAINT
	DF_GRItems_ExportedFlag DEFAULT ((0)) FOR ExportedFlag
GO
ALTER TABLE dbo.GRItems ADD CONSTRAINT
	PK_GRItems PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
update GIItems set ExportedFlag =0
GO
update GRItems set ExportedFlag =0
GO
update DBVersion set version = 'V3.5.0.2',lastUpdate = getdate()
GO

--update V3.5.0.4 ABB Modfication.
ALTER TABLE dbo.NonBarCodedTemp ADD
	Remarks nvarchar(MAX) NULL
GO
update DBVersion set version = 'V3.5.0.4',lastUpdate = getdate()
GO
use ZulAssetsBE
update [DB Version] set version = 'V3.5.0.4',lastUpdate = getdate()

--update V3.5.0.6 ABB Modfication.
use ZulAssetsBE
ALTER TABLE dbo.AssetDetails ADD
	LastInventoryDate datetime NULL
GO
ALTER TABLE dbo.AssetDetails ADD
	LastEditDate datetime NULL,
	CreationDate datetime NULL,
	LastEditBY nvarchar(50) NULL
GO
ALTER TABLE dbo.AssetDetails ADD CONSTRAINT
	DF_AssetDetails_LastEditDate DEFAULT (getdate()) FOR LastEditDate
GO
ALTER TABLE dbo.AssetDetails ADD CONSTRAINT
	DF_AssetDetails_CreationDate DEFAULT (getdate()) FOR CreationDate
GO
ALTER TABLE dbo.AssetDetails ADD
	CustomFld1 nvarchar(255) NULL,
	CustomFld2 nvarchar(255) NULL,
	CustomFld3 nvarchar(255) NULL,
	CustomFld4 nvarchar(255) NULL,
	CustomFld5 nvarchar(255) NULL
GO
ALTER TABLE dbo.AssetDetails ADD CONSTRAINT
	DF_AssetDetails_EmpCustomFld1 DEFAULT ('') FOR CustomFld1
GO
ALTER TABLE dbo.AssetDetails ADD CONSTRAINT
	DF_AssetDetails_EmpCustomFld2 DEFAULT ('') FOR CustomFld2
GO
ALTER TABLE dbo.AssetDetails ADD CONSTRAINT
	DF_AssetDetails_EmpCustomFld3 DEFAULT ('') FOR CustomFld3
GO
ALTER TABLE dbo.AssetDetails ADD CONSTRAINT
	DF_AssetDetails_EmpCustomFld4 DEFAULT ('') FOR CustomFld4
GO
ALTER TABLE dbo.AssetDetails ADD CONSTRAINT
	DF_AssetDetails_EmpCustomFld5 DEFAULT ('') FOR CustomFld5
GO
ALTER TABLE dbo.Ast_INV_Schedule ADD
	SchType int NULL
GO
update dbo.Ast_INV_Schedule set SchType=0
Go
update [DB Version] set version = 'V3.5.0.6',lastUpdate = getdate()
Go
Use ZulAssetsBETemp
update DBVersion set version = 'V3.5.0.6',lastUpdate = getdate()
GO



--update V3.5.0.8 ABB Modfication.
use ZulAssetsBE
Go
drop TABLE AssetsTemp
go
drop TABLE AssetTransferTemp
go
drop TABLE AstBooks_temp
go
drop TABLE BookHistory_temp
go
drop TABLE CategoryTemp
go
drop TABLE NonBarCodedTemp
go
drop TABLE SysConfig
go
update [DB Version] set version = 'V3.5.0.8',lastUpdate = getdate()
Go
Use ZulAssetsBETemp
Go
CREATE TABLE [dbo].[CompanyTemp](
	[CompanyId] [int] NOT NULL,
	[CompanyCode] [varchar](50) NULL,
	[CompanyName] [varchar](50) NULL,
	[BarStructID] [int] NULL,
	[HierCode] [varchar](50) NULL,
	[LastAssetNumber] [bigint] NULL,
 CONSTRAINT [PK_CompanyTemp] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CompanyTemp] ADD  CONSTRAINT [DF_CompanyTemp_LastAssetNumber]  DEFAULT ((0)) FOR [LastAssetNumber]
GO

ALTER TABLE dbo.AssetsTemp ADD
	InventoryDate datetime NULL,
	ID bigint NOT NULL IDENTITY (1, 1)
GO
ALTER TABLE dbo.AssetsTemp ADD CONSTRAINT
	PK_AssetsTemp PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

ALTER TABLE dbo.AssetsTempReceiving ADD
	InventoryDate datetime NULL,
	ID bigint NOT NULL IDENTITY (1, 1)
GO
ALTER TABLE dbo.AssetsTempReceiving ADD CONSTRAINT
	PK_AssetsTempReceiving PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE TABLE [dbo].[CustodianTemp](
	[CustodianID] [varchar](25) NOT NULL,
	[CustodianName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_CustodianTemp] PRIMARY KEY CLUSTERED 
(
	[CustodianID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE dbo.NonBarCodedTemp ADD
	CompanyCode varchar(50) NULL,
	InvSchID bigint NULL
Go
ALTER TABLE dbo.NonBarCodedTemp ADD
	ID bigint NOT NULL IDENTITY (1, 1)
GO
ALTER TABLE dbo.NonBarCodedTemp ADD CONSTRAINT
	PK_NonBarCodedTemp PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

update DBVersion set version = 'V3.5.0.8',lastUpdate = getdate()
GO



--update V3.5.1.1 ABB Modfication.
use ZulAssetsBE

CREATE TABLE [dbo].[AssetDetailsLog](
	[AseetLogID] [bigint] IDENTITY(1,1) NOT NULL,
	[AstID] [varchar](25) NOT NULL,
	[DispCode] [int] NULL,
	[ItemCode] [varchar](25) NULL,
	[SuppID] [varchar](25) NULL,
	[POCode] [bigint] NULL,
	[InvNumber] [varchar](25) NULL,
	[CustodianID] [varchar](25) NULL,
	[BaseCost] [float] NULL,
	[Tax] [float] NULL,
	[SrvDate] [datetime] NULL,
	[PurDate] [datetime] NULL,
	[Disposed] [bit] NULL,
	[DispDate] [datetime] NULL,
	[InvSchCode] [bigint] NULL,
	[BookID] [varchar](50) NULL,
	[InsID] [int] NULL,
	[LocID] [varchar](255) NULL,
	[InvStatus] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsSold] [bit] NULL,
	[Sel_Date] [datetime] NULL,
	[Sel_Price] [float] NULL,
	[SoldTo] [varchar](50) NULL,
	[RefNo] [varchar](50) NULL,
	[AstNum] [bigint] NOT NULL,
	[AstBrandId] [int] NULL,
	[AstDesc] [nvarchar](200) NULL,
	[AstModel] [nvarchar](50) NULL,
	[CompanyID] [int] NULL,
	[TransRemarks] [varchar](200) NULL,
	[LabelCount] [int] NULL,
	[OldAssetID] [varchar](50) NULL,
	[Discount] [bigint] NULL,
	[NoPiece] [int] NULL,
	[BarCode] [varchar](50) NULL,
	[SerailNo] [nvarchar](50) NULL,
	[RefCode] [varchar](50) NULL,
	[Plate] [varchar](50) NULL,
	[Poerp] [varchar](50) NULL,
	[Capex] [varchar](50) NULL,
	[Grn] [varchar](50) NULL,
	[GLCode] [bigint] NULL,
	[PONumber] [nvarchar](50) NULL,
	[AstDesc2] [nvarchar](200) NULL,
	[AstImage] [image] NULL,
	[CapitalizationDate] [datetime] NULL,
	[BussinessArea] [nvarchar](50) NULL,
	[InventoryNumber] [nvarchar](50) NULL,
	[CostCenterID] [varchar](50) NULL,
	[InStockAsset] [bit] NULL,
	[EvaluationGroup1] [nvarchar](50) NULL,
	[EvaluationGroup2] [nvarchar](50) NULL,
	[EvaluationGroup3] [nvarchar](50) NULL,
	[EvaluationGroup4] [nvarchar](50) NULL,
	[CreatedBY] [nvarchar](50) NULL,
	[IsDataChanged] [bit] NULL,
	[LastInventoryDate] [datetime] NULL,
	[CreationDate] [datetime] NULL,
	[LastEditDate] [datetime] NULL,
	[LastEditBY] [nvarchar](50) NULL,
	[CustomFld1] [nvarchar](255) NULL,
	[CustomFld2] [nvarchar](255) NULL,
	[CustomFld3] [nvarchar](255) NULL,
	[CustomFld4] [nvarchar](255) NULL,
	[CustomFld5] [nvarchar](255) NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionDate] [datetime] NULL,
 CONSTRAINT [PK_AssetDetailsLog_1] PRIMARY KEY CLUSTERED 
(
	[AseetLogID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

USE [ZulAssetsBE]
GO
Create TRIGGER [dbo].[AssetLogUpdate]
   ON  [dbo].[AssetDetails]
   For Update
AS 
BEGIN
	SET NOCOUNT ON;
		Insert into AssetDetailsLog 
	([AstID]
      ,[DispCode]
      ,[ItemCode]
      ,[SuppID]
      ,[POCode]
      ,[InvNumber]
      ,[CustodianID]
      ,[BaseCost]
      ,[Tax]
      ,[SrvDate]
      ,[PurDate]
      ,[Disposed]
      ,[DispDate]
      ,[InvSchCode]
      ,[BookID]
      ,[InsID]
      ,[LocID]
      ,[InvStatus]
      ,[IsDeleted]
      ,[IsSold]
      ,[Sel_Date]
      ,[Sel_Price]
      ,[SoldTo]
      ,[RefNo]
      ,[AstNum]
      ,[AstBrandId]
      ,[AstDesc]
      ,[AstModel]
      ,[CompanyID]
      ,[TransRemarks]
      ,[LabelCount]
      ,[OldAssetID]
      ,[Discount]
      ,[NoPiece]
      ,[BarCode]
      ,[SerailNo]
      ,[RefCode]
      ,[Plate]
      ,[Poerp]
      ,[Capex]
      ,[Grn]
      ,[GLCode]
      ,[PONumber]
      ,[AstDesc2]
      ,[CapitalizationDate]
      ,[BussinessArea]
      ,[InventoryNumber]
      ,[CostCenterID]
      ,[InStockAsset]
      ,[EvaluationGroup1]
      ,[EvaluationGroup2]
      ,[EvaluationGroup3]
      ,[EvaluationGroup4]
      ,[CreatedBY]
      ,[IsDataChanged]
      ,[LastInventoryDate]
      ,[LastEditDate]
      ,[CreationDate]
      ,[LastEditBY]
      ,[CustomFld1]
      ,[CustomFld2]
      ,[CustomFld3]
      ,[CustomFld4]
      ,[CustomFld5]
      ,ActionType
      ,ActionDate)  
	SELECT 
	   deleted.[AstID]
      ,deleted.[DispCode]
      ,deleted.[ItemCode]
      ,deleted.[SuppID]
      ,deleted.[POCode]
      ,deleted.[InvNumber]
      ,deleted.[CustodianID]
      ,deleted.[BaseCost]
      ,deleted.[Tax]
      ,deleted.[SrvDate]
      ,deleted.[PurDate]
      ,deleted.[Disposed]
      ,deleted.[DispDate]
      ,deleted.[InvSchCode]
      ,deleted.[BookID]
      ,deleted.[InsID]
      ,deleted.[LocID]
      ,deleted.[InvStatus]
      ,deleted.[IsDeleted]
      ,deleted.[IsSold]
      ,deleted.[Sel_Date]
      ,deleted.[Sel_Price]
      ,deleted.[SoldTo]
      ,deleted.[RefNo]
      ,deleted.[AstNum]
      ,deleted.[AstBrandId]
      ,deleted.[AstDesc]
      ,deleted.[AstModel]
      ,deleted.[CompanyID]
      ,deleted.[TransRemarks]
      ,deleted.[LabelCount]
      ,deleted.[OldAssetID]
      ,deleted.[Discount]
      ,deleted.[NoPiece]
      ,deleted.[BarCode]
      ,deleted.[SerailNo]
      ,deleted.[RefCode]
      ,deleted.[Plate]
      ,deleted.[Poerp]
      ,deleted.[Capex]
      ,deleted.[Grn]
      ,deleted.[GLCode]
      ,deleted.[PONumber]
      ,deleted.[AstDesc2]
      ,deleted.[CapitalizationDate]
      ,deleted.[BussinessArea]
      ,deleted.[InventoryNumber]
      ,deleted.[CostCenterID]
      ,deleted.[InStockAsset]
      ,deleted.[EvaluationGroup1]
      ,deleted.[EvaluationGroup2]
      ,deleted.[EvaluationGroup3]
      ,deleted.[EvaluationGroup4]
      ,deleted.[CreatedBY]
      ,deleted.[IsDataChanged]
      ,deleted.[LastInventoryDate]
      ,inserted.[LastEditDate]
      ,deleted.[CreationDate]
      ,inserted.[LastEditBY]
      ,deleted.[CustomFld1]
      ,deleted.[CustomFld2]
      ,deleted.[CustomFld3]
      ,deleted.[CustomFld4]
      ,deleted.[CustomFld5]
       ,'Updated'
      ,GetDate()
  FROM deleted  inner join inserted on deleted.AstID = Deleted.AstID
  
END

GO

USE [ZulAssetsBE]
GO
CREATE TRIGGER [dbo].[AssetLogInsert] 
   ON  [dbo].[AssetDetails]
   For INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Insert into AssetDetailsLog 
	([AstID]
      ,[DispCode]
      ,[ItemCode]
      ,[SuppID]
      ,[POCode]
      ,[InvNumber]
      ,[CustodianID]
      ,[BaseCost]
      ,[Tax]
      ,[SrvDate]
      ,[PurDate]
      ,[Disposed]
      ,[DispDate]
      ,[InvSchCode]
      ,[BookID]
      ,[InsID]
      ,[LocID]
      ,[InvStatus]
      ,[IsDeleted]
      ,[IsSold]
      ,[Sel_Date]
      ,[Sel_Price]
      ,[SoldTo]
      ,[RefNo]
      ,[AstNum]
      ,[AstBrandId]
      ,[AstDesc]
      ,[AstModel]
      ,[CompanyID]
      ,[TransRemarks]
      ,[LabelCount]
      ,[OldAssetID]
      ,[Discount]
      ,[NoPiece]
      ,[BarCode]
      ,[SerailNo]
      ,[RefCode]
      ,[Plate]
      ,[Poerp]
      ,[Capex]
      ,[Grn]
      ,[GLCode]
      ,[PONumber]
      ,[AstDesc2]
      ,[CapitalizationDate]
      ,[BussinessArea]
      ,[InventoryNumber]
      ,[CostCenterID]
      ,[InStockAsset]
      ,[EvaluationGroup1]
      ,[EvaluationGroup2]
      ,[EvaluationGroup3]
      ,[EvaluationGroup4]
      ,[CreatedBY]
      ,[IsDataChanged]
      ,[LastInventoryDate]
      ,[LastEditDate]
      ,[CreationDate]
      ,[LastEditBY]
      ,[CustomFld1]
      ,[CustomFld2]
      ,[CustomFld3]
      ,[CustomFld4]
      ,[CustomFld5]
      ,ActionType
      ,ActionDate) 
      	SELECT 
	[AstID]
      ,[DispCode]
      ,[ItemCode]
      ,[SuppID]
      ,[POCode]
      ,[InvNumber]
      ,[CustodianID]
      ,[BaseCost]
      ,[Tax]
      ,[SrvDate]
      ,[PurDate]
      ,[Disposed]
      ,[DispDate]
      ,[InvSchCode]
      ,[BookID]
      ,[InsID]
      ,[LocID]
      ,[InvStatus]
      ,[IsDeleted]
      ,[IsSold]
      ,[Sel_Date]
      ,[Sel_Price]
      ,[SoldTo]
      ,[RefNo]
      ,[AstNum]
      ,[AstBrandId]
      ,[AstDesc]
      ,[AstModel]
      ,[CompanyID]
      ,[TransRemarks]
      ,[LabelCount]
      ,[OldAssetID]
      ,[Discount]
      ,[NoPiece]
      ,[BarCode]
      ,[SerailNo]
      ,[RefCode]
      ,[Plate]
      ,[Poerp]
      ,[Capex]
      ,[Grn]
      ,[GLCode]
      ,[PONumber]
      ,[AstDesc2]
      ,[CapitalizationDate]
      ,[BussinessArea]
      ,[InventoryNumber]
      ,[CostCenterID]
      ,[InStockAsset]
      ,[EvaluationGroup1]
      ,[EvaluationGroup2]
      ,[EvaluationGroup3]
      ,[EvaluationGroup4]
      ,[CreatedBY]
      ,[IsDataChanged]
      ,[LastInventoryDate]
      ,[LastEditDate]
      ,[CreationDate]
      ,[LastEditBY]
      ,[CustomFld1]
      ,[CustomFld2]
      ,[CustomFld3]
      ,[CustomFld4]
      ,[CustomFld5]
      ,'Inserted'
      ,GetDate()
  FROM Inserted 
END

GO


USE [ZulAssetsBE]
GO
CREATE TRIGGER [dbo].[AssetLogDelete]
   ON  [dbo].[AssetDetails]
   For Delete
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Insert into AssetDetailsLog 
	([AstID]
      ,[DispCode]
      ,[ItemCode]
      ,[SuppID]
      ,[POCode]
      ,[InvNumber]
      ,[CustodianID]
      ,[BaseCost]
      ,[Tax]
      ,[SrvDate]
      ,[PurDate]
      ,[Disposed]
      ,[DispDate]
      ,[InvSchCode]
      ,[BookID]
      ,[InsID]
      ,[LocID]
      ,[InvStatus]
      ,[IsDeleted]
      ,[IsSold]
      ,[Sel_Date]
      ,[Sel_Price]
      ,[SoldTo]
      ,[RefNo]
      ,[AstNum]
      ,[AstBrandId]
      ,[AstDesc]
      ,[AstModel]
      ,[CompanyID]
      ,[TransRemarks]
      ,[LabelCount]
      ,[OldAssetID]
      ,[Discount]
      ,[NoPiece]
      ,[BarCode]
      ,[SerailNo]
      ,[RefCode]
      ,[Plate]
      ,[Poerp]
      ,[Capex]
      ,[Grn]
      ,[GLCode]
      ,[PONumber]
      ,[AstDesc2]
      ,[CapitalizationDate]
      ,[BussinessArea]
      ,[InventoryNumber]
      ,[CostCenterID]
      ,[InStockAsset]
      ,[EvaluationGroup1]
      ,[EvaluationGroup2]
      ,[EvaluationGroup3]
      ,[EvaluationGroup4]
      ,[CreatedBY]
      ,[IsDataChanged]
      ,[LastInventoryDate]
      ,[LastEditDate]
      ,[CreationDate]
      ,[LastEditBY]
      ,[CustomFld1]
      ,[CustomFld2]
      ,[CustomFld3]
      ,[CustomFld4]
      ,[CustomFld5]
       ,ActionType
      ,ActionDate)  
	SELECT 
	[AstID]
      ,[DispCode]
      ,[ItemCode]
      ,[SuppID]
      ,[POCode]
      ,[InvNumber]
      ,[CustodianID]
      ,[BaseCost]
      ,[Tax]
      ,[SrvDate]
      ,[PurDate]
      ,[Disposed]
      ,[DispDate]
      ,[InvSchCode]
      ,[BookID]
      ,[InsID]
      ,[LocID]
      ,[InvStatus]
      ,[IsDeleted]
      ,[IsSold]
      ,[Sel_Date]
      ,[Sel_Price]
      ,[SoldTo]
      ,[RefNo]
      ,[AstNum]
      ,[AstBrandId]
      ,[AstDesc]
      ,[AstModel]
      ,[CompanyID]
      ,[TransRemarks]
      ,[LabelCount]
      ,[OldAssetID]
      ,[Discount]
      ,[NoPiece]
      ,[BarCode]
      ,[SerailNo]
      ,[RefCode]
      ,[Plate]
      ,[Poerp]
      ,[Capex]
      ,[Grn]
      ,[GLCode]
      ,[PONumber]
      ,[AstDesc2]
      ,[CapitalizationDate]
      ,[BussinessArea]
      ,[InventoryNumber]
      ,[CostCenterID]
      ,[InStockAsset]
      ,[EvaluationGroup1]
      ,[EvaluationGroup2]
      ,[EvaluationGroup3]
      ,[EvaluationGroup4]
      ,[CreatedBY]
      ,[IsDataChanged]
      ,[LastInventoryDate]
      ,[LastEditDate]
      ,[CreationDate]
      ,[LastEditBY]
      ,[CustomFld1]
      ,[CustomFld2]
      ,[CustomFld3]
      ,[CustomFld4]
      ,[CustomFld5]
      ,'Deleted'
      ,Getdate()
  FROM deleted  
END

GO
--update V3.5.1.3 ABB Modfication.

update [DB Version] set version = 'V3.5.1.3',lastUpdate = getdate()
Go

USE [ZulAssetsBETemp]
GO

ALTER TABLE dbo.AppUsersTemp ADD
	IsAllowAssetAudit bit NULL,
	IsAllowInventory bit NULL
GO

update [DBVersion] set version = 'V3.5.1.3',lastUpdate = getdate()
Go


--update V3.5.1.4 ABB Modfication.
USE [ZulAssetsBETemp]
GO

ALTER TABLE dbo.GIItems ADD
	GUID uniqueidentifier NOT NULL CONSTRAINT DF_GIItems_GUID DEFAULT newid(),
	GIGUID uniqueidentifier NULL
GO
ALTER TABLE dbo.GIItems
	DROP CONSTRAINT PK_GIItems
GO
ALTER TABLE dbo.GIItems ADD CONSTRAINT
	PK_GIItems_1 PRIMARY KEY CLUSTERED 
	(
	GUID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.GIItems
	DROP COLUMN ID
GO



GO
ALTER TABLE dbo.GI ADD
	GUID uniqueidentifier NOT NULL CONSTRAINT DF_GI_GUID DEFAULT newid()
GO
ALTER TABLE dbo.GI ADD CONSTRAINT
	PK_GI PRIMARY KEY CLUSTERED 
	(
	GUID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO


ALTER TABLE dbo.GRItems ADD
	GUID uniqueidentifier NOT NULL CONSTRAINT DF_GRItems_GUID DEFAULT (newid()),
	GRGUID uniqueidentifier NULL
GO
ALTER TABLE dbo.GRItems
	DROP CONSTRAINT PK_GRItems
GO
ALTER TABLE dbo.GRItems ADD CONSTRAINT
	PK_GRItems_1 PRIMARY KEY CLUSTERED 
	(
	GUID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.GRItems
	DROP COLUMN ID
GO


ALTER TABLE dbo.GR ADD
	GUID uniqueidentifier NOT NULL CONSTRAINT DF_GR_GUID DEFAULT NewID()
GO
ALTER TABLE dbo.GR ADD CONSTRAINT
	PK_GR PRIMARY KEY CLUSTERED 
	(
	GUID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO



ALTER TABLE dbo.AssetsTempReceiving ADD
	LastEditDate datetime NULL,
	LastEditBY nvarchar(50) NULL
GO
ALTER TABLE dbo.AssetsTempReceiving ADD CONSTRAINT
	DF_AssetsTempReceiving_LastEditDate DEFAULT (getdate()) FOR LastEditDate
GO
update [DBVersion] set version = 'V3.5.1.4',lastUpdate = getdate()
Go

USE [ZulAssetsBE]
GO
update [DB Version] set version = 'V3.5.1.3',lastUpdate = getdate()
Go


--ABB Update 3.5.1.12

USE [ZulAssetsBETemp]

ALTER TABLE dbo.GR ADD
	LastEditDate datetime NULL,
	CreationDate datetime NULL,
	CreatedBy uniqueidentifier NULL,
	LastEditBY uniqueidentifier NULL
GO
ALTER TABLE dbo.GR ADD CONSTRAINT
	DF_GR_LastEditDate DEFAULT (getdate()) FOR LastEditDate
GO
ALTER TABLE dbo.GR ADD CONSTRAINT
	DF_GR_CreationDate DEFAULT (getdate()) FOR CreationDate
GO
ALTER TABLE dbo.GR ADD CONSTRAINT
	DF_GR_CreatedBy DEFAULT (0x00) FOR CreatedBy
GO
ALTER TABLE dbo.GR ADD CONSTRAINT
	DF_GR_LastEditBY DEFAULT (0x00) FOR LastEditBY
GO


--GRItems Table
ALTER TABLE dbo.GRItems ADD
	LastEditDate datetime NULL,
	CreationDate datetime NULL,
	CreatedBy uniqueidentifier NULL,
	LastEditBY uniqueidentifier NULL
GO
ALTER TABLE dbo.GRItems ADD CONSTRAINT
	DF_GRItems_LastEditDate DEFAULT (getdate()) FOR LastEditDate
GO
ALTER TABLE dbo.GRItems ADD CONSTRAINT
	DF_GRItems_CreationDate DEFAULT (getdate()) FOR CreationDate
GO
ALTER TABLE dbo.GRItems ADD CONSTRAINT
	DF_GRItems_CreatedBy DEFAULT (0x00) FOR CreatedBy
GO
ALTER TABLE dbo.GRItems ADD CONSTRAINT
	DF_GRItems_LastEditBY DEFAULT (0x00) FOR LastEditBY
GO


ALTER TABLE dbo.GI ADD
	LastEditDate datetime NULL,
	CreationDate datetime NULL,
	CreatedBy uniqueidentifier NULL,
	LastEditBY uniqueidentifier NULL
GO
ALTER TABLE dbo.GI ADD CONSTRAINT
	DF_GI_LastEditDate DEFAULT (getdate()) FOR LastEditDate
GO
ALTER TABLE dbo.GI ADD CONSTRAINT
	DF_GI_CreationDate DEFAULT (getdate()) FOR CreationDate
GO
ALTER TABLE dbo.GI ADD CONSTRAINT
	DF_GI_CreatedBy DEFAULT (0x00) FOR CreatedBy
GO
ALTER TABLE dbo.GI ADD CONSTRAINT
	DF_GI_LastEditBY DEFAULT (0x00) FOR LastEditBY
GO



ALTER TABLE dbo.GIItems ADD
	LastEditDate datetime NULL,
	CreationDate datetime NULL,
	CreatedBy uniqueidentifier NULL,
	LastEditBY uniqueidentifier NULL
GO
ALTER TABLE dbo.GIItems ADD CONSTRAINT
	DF_GIItems_LastEditDate DEFAULT (getdate()) FOR LastEditDate
GO
ALTER TABLE dbo.GIItems ADD CONSTRAINT
	DF_GIItems_CreationDate DEFAULT (getdate()) FOR CreationDate
GO
ALTER TABLE dbo.GIItems ADD CONSTRAINT
	DF_GIItems_CreatedBy DEFAULT (0x00) FOR CreatedBy
GO
ALTER TABLE dbo.GIItems ADD CONSTRAINT
	DF_GIItems_LastEditBY DEFAULT (0x00) FOR LastEditBY
GO

update GI set CreationDate = IssueDate,LastEditDate = IssueDate,CreatedBy = '00000000-0000-0000-0000-000000000000' ,LastEditBY='00000000-0000-0000-0000-000000000000'
GO
update GR set CreationDate = PostingDate, LastEditDate = PostingDate,CreatedBy = '00000000-0000-0000-0000-000000000000' ,LastEditBY='00000000-0000-0000-0000-000000000000'
GO
update GIItems Set CreationDate = Getdate(),LastEditDate = Getdate(),CreatedBy = '00000000-0000-0000-0000-000000000000' ,LastEditBY='00000000-0000-0000-0000-000000000000'
GO
update GRItems Set CreationDate = Getdate(),LastEditDate = Getdate(),CreatedBy = '00000000-0000-0000-0000-000000000000' ,LastEditBY='00000000-0000-0000-0000-000000000000'



--update2

CREATE TABLE [dbo].[SAPDocuments](
	[GUID] [uniqueidentifier] NOT NULL,
	[DocNo] [nvarchar](50) NULL,
	[DocDate] [datetime] NULL,
	[DocType] [nvarchar](50) NULL,
	[Remarks] [nvarchar](255) NULL,
	[ExpectedReturnDate] [datetime] NULL,
	[WarrantyClaimDocGUID] [uniqueidentifier] NULL,
	[PONo] [nvarchar](100) NULL,
	[DeliveryNo] [nvarchar](100) NULL,
	[InvProposalNo] [nvarchar](100) NULL,
	[EmpNo] [nvarchar](100) NULL,
	[EmpName] [nvarchar](100) NULL,
	[PORequisitionNo] [nvarchar](100) NULL,
	[LastEditDate] [datetime] NULL,
	[CreationDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastEditBY] [uniqueidentifier] NULL,
 CONSTRAINT [PK_SAPWarrantyClaim] PRIMARY KEY CLUSTERED 
(
	[GUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SAPDocuments] ADD  CONSTRAINT [DF_SAPWarrantyClaim_GUID]  DEFAULT (newid()) FOR [GUID]
GO

ALTER TABLE [dbo].[SAPDocuments] ADD  CONSTRAINT [DF_SAPWarrantyClaim_LastEditDate]  DEFAULT (getdate()) FOR [LastEditDate]
GO

ALTER TABLE [dbo].[SAPDocuments] ADD  CONSTRAINT [DF_SAPWarrantyClaim_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO

ALTER TABLE [dbo].[SAPDocuments] ADD  CONSTRAINT [DF_SAPWarrantyClaim_CreatedBy]  DEFAULT (0x00) FOR [CreatedBy]
GO

ALTER TABLE [dbo].[SAPDocuments] ADD  CONSTRAINT [DF_SAPWarrantyClaim_LastEditBY]  DEFAULT (0x00) FOR [LastEditBY]
GO



CREATE TABLE [dbo].[SAPItems](
	[GUID] [uniqueidentifier] NOT NULL,
	[SAPPartNo] [nvarchar](50) NULL,
	[Description] [nvarchar](255) NULL,
	[Warranty] [int] NULL,
	[UpdateAssetSN] [bit] NULL,
	[CheckUniqueSNOnReceiving] [bit] NULL,
	[NewWarranty] [int] NULL,
	[NewUpdateAssetSN] [bit] NULL,
	[NewCheckUniqueSNOnReceiving] [bit] NULL,
	[Status] [nvarchar](50) NULL,
	[LastEditDate] [datetime] NULL,
	[CreationDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastEditBY] [uniqueidentifier] NULL,
 CONSTRAINT [PK_SAPItems_1] PRIMARY KEY CLUSTERED 
(
	[GUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SAPItems] ADD  CONSTRAINT [DF_SAPItems_GUID]  DEFAULT (newid()) FOR [GUID]
GO

ALTER TABLE [dbo].[SAPItems] ADD  CONSTRAINT [DF_SAPItems_LastEditDate]  DEFAULT (getdate()) FOR [LastEditDate]
GO

ALTER TABLE [dbo].[SAPItems] ADD  CONSTRAINT [DF_SAPItems_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO

ALTER TABLE [dbo].[SAPItems] ADD  CONSTRAINT [DF_SAPItems_CreatedBy]  DEFAULT (0x00) FOR [CreatedBy]
GO

ALTER TABLE [dbo].[SAPItems] ADD  CONSTRAINT [DF_SAPItems_LastEditBY]  DEFAULT (0x00) FOR [LastEditBY]
GO



CREATE TABLE [dbo].[SAPItemSerials](
	[GUID] [uniqueidentifier] NOT NULL,
	[SAPPartNo] [nvarchar](255) NULL,
	[SerialNo] [nvarchar](255) NULL,
	[ManufacturePartNo] [nvarchar](255) NULL,
	[LastStatus] [nvarchar](50) NULL,
	[Warranty] [int] NULL,
	[WarrantyStartDate] [datetime] NULL,
	[OrgSerialGUID] [uniqueidentifier] NULL,
	[LastEditDate] [datetime] NULL,
	[CreationDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastEditBY] [uniqueidentifier] NULL,
 CONSTRAINT [PK_SAPItemsHistory] PRIMARY KEY CLUSTERED 
(
	[GUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SAPItemSerials] ADD  CONSTRAINT [DF_SAPItemsHistory_GUID]  DEFAULT (newid()) FOR [GUID]
GO

ALTER TABLE [dbo].[SAPItemSerials] ADD  CONSTRAINT [DF_SAPItemSerials_OrgSerialGUID]  DEFAULT (0x00) FOR [OrgSerialGUID]
GO

ALTER TABLE [dbo].[SAPItemSerials] ADD  CONSTRAINT [DF_SAPItemsHistory_LastEditDate]  DEFAULT (getdate()) FOR [LastEditDate]
GO

ALTER TABLE [dbo].[SAPItemSerials] ADD  CONSTRAINT [DF_SAPItemsHistory_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO

ALTER TABLE [dbo].[SAPItemSerials] ADD  CONSTRAINT [DF_SAPItemsHistory_CreatedBy]  DEFAULT (0x00) FOR [CreatedBy]
GO

ALTER TABLE [dbo].[SAPItemSerials] ADD  CONSTRAINT [DF_SAPItemsHistory_LastEditBY]  DEFAULT (0x00) FOR [LastEditBY]
GO

CREATE TABLE [dbo].[SAPItemSerialsTrans](
	[GUID] [uniqueidentifier] NOT NULL,
	[ItemSerialGUID] [uniqueidentifier] NULL,
	[DocGUID] [uniqueidentifier] NULL,
	[TransName] [nvarchar](50) NULL,
	[MovementType] [nvarchar](50) NULL,
	[SAPMaterialDocNo] [nvarchar](100) NULL,
	[SAPMaterialDocLineNo] [nvarchar](100) NULL,
	[PONo] [nvarchar](100) NULL,
	[POLineNo] [nvarchar](100) NULL,
	[LineItemNo] [int] NULL,
	[SeqNo] [int] NULL,
	[AssetNo] [nvarchar](100) NULL,
	[CostCenter] [nvarchar](100) NULL,
	[BusinessArea] [nvarchar](100) NULL,
	[GLAC] [nvarchar](100) NULL,
	[ReasonOfFault] [nvarchar](max) NULL,
	[ExportedFlag] [bit] NULL,
	[LastEditDate] [datetime] NULL,
	[CreationDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastEditBY] [uniqueidentifier] NULL,
 CONSTRAINT [PK_SAPItemSerialsTrans] PRIMARY KEY CLUSTERED 
(
	[GUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SAPItemSerialsTrans] ADD  CONSTRAINT [DF_SAPItemSerialsTrans_GUID]  DEFAULT (newid()) FOR [GUID]
GO

ALTER TABLE [dbo].[SAPItemSerialsTrans] ADD  CONSTRAINT [DF_Table_1_SerialGUID]  DEFAULT (0x00) FOR [ItemSerialGUID]
GO

ALTER TABLE [dbo].[SAPItemSerialsTrans] ADD  CONSTRAINT [DF_Table_1_DocGUID]  DEFAULT (0x00) FOR [DocGUID]
GO

ALTER TABLE [dbo].[SAPItemSerialsTrans] ADD  CONSTRAINT [DF_SAPItemSerialsTrans_ExportedFlag]  DEFAULT ((0)) FOR [ExportedFlag]
GO

ALTER TABLE [dbo].[SAPItemSerialsTrans] ADD  CONSTRAINT [DF_SAPItemSerialsTrans_LastEditDate]  DEFAULT (getdate()) FOR [LastEditDate]
GO

ALTER TABLE [dbo].[SAPItemSerialsTrans] ADD  CONSTRAINT [DF_SAPItemSerialsTrans_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO

ALTER TABLE [dbo].[SAPItemSerialsTrans] ADD  CONSTRAINT [DF_SAPItemSerialsTrans_CreatedBy]  DEFAULT (0x00) FOR [CreatedBy]
GO

ALTER TABLE [dbo].[SAPItemSerialsTrans] ADD  CONSTRAINT [DF_SAPItemSerialsTrans_LastEditBY]  DEFAULT (0x00) FOR [LastEditBY]
GO

update [DBVersion] set version = 'V3.5.1.8',lastUpdate = getdate()
Go

USE [ZulAssetsBE]
GO
update [DB Version] set version = 'V3.5.1.8',lastUpdate = getdate()
Go


USE [ZulAssetsBETemp]

--update3
alter table dbo.SAPItemSerialsTrans
add 
InvProposalNo	nvarchar(100) null,
EmpNo	nvarchar(100)	null,
PORequisitionNo	nvarchar(100) null
GO
--update4
alter table dbo.SAPItemSerialsTrans
add 
OrgItemSerialGUID	uniqueidentifier null,
OrgItemSerialTransGUID	uniqueidentifier null
GO
--update5
ALTER TABLE dbo.SAPItemSerials ADD
	LastDocGUID nvarchar(50) NULL,
	AssetNo nvarchar(100) NULL,
	ReceivedDate datetime NULL,
	IssuedDate datetime NULL,
	CostCenter nvarchar(100) NULL CONSTRAINT DF_SAPItemSerials_CostCenter DEFAULT '',
	CustodianID varchar(25) NULL CONSTRAINT DF_SAPItemSerials_CustodianID DEFAULT ''
GO
ALTER TABLE dbo.SAPItemSerials ADD CONSTRAINT
	DF_SAPItemSerials_LastDocNo DEFAULT (0x00) FOR LastDocGUID
GO
ALTER TABLE dbo.SAPItemSerials ADD CONSTRAINT
	DF_SAPItemSerials_AssetNo DEFAULT '' FOR AssetNo
GO
ALTER TABLE dbo.SAPItemSerials ADD CONSTRAINT
	DF_SAPItemSerials_ReceivedDate DEFAULT getdate() FOR ReceivedDate
GO
ALTER TABLE dbo.SAPItemSerials ADD CONSTRAINT
	DF_SAPItemSerials_IssuedDate DEFAULT getdate() FOR IssuedDate
GO

ALTER TABLE dbo.SAPItemSerialsTrans ADD
	Plant nvarchar(100) NULL,
	Location nvarchar(100) NULL
GO


USE [ZulAssetsBE]
GO
ALTER TRIGGER [dbo].[AssetLogUpdate]
   ON  [dbo].[AssetDetails]
   For Update
AS 
BEGIN
	SET NOCOUNT ON;
		Insert into AssetDetailsLog 
	([AstID]
      ,[DispCode]
      ,[ItemCode]
      ,[SuppID]
      ,[POCode]
      ,[InvNumber]
      ,[CustodianID]
      ,[BaseCost]
      ,[Tax]
      ,[SrvDate]
      ,[PurDate]
      ,[Disposed]
      ,[DispDate]
      ,[InvSchCode]
      ,[BookID]
      ,[InsID]
      ,[LocID]
      ,[InvStatus]
      ,[IsDeleted]
      ,[IsSold]
      ,[Sel_Date]
      ,[Sel_Price]
      ,[SoldTo]
      ,[RefNo]
      ,[AstNum]
      ,[AstBrandId]
      ,[AstDesc]
      ,[AstModel]
      ,[CompanyID]
      ,[TransRemarks]
      ,[LabelCount]
      ,[OldAssetID]
      ,[Discount]
      ,[NoPiece]
      ,[BarCode]
      ,[SerailNo]
      ,[RefCode]
      ,[Plate]
      ,[Poerp]
      ,[Capex]
      ,[Grn]
      ,[GLCode]
      ,[PONumber]
      ,[AstDesc2]
      ,[CapitalizationDate]
      ,[BussinessArea]
      ,[InventoryNumber]
      ,[CostCenterID]
      ,[InStockAsset]
      ,[EvaluationGroup1]
      ,[EvaluationGroup2]
      ,[EvaluationGroup3]
      ,[EvaluationGroup4]
      ,[CreatedBY]
      ,[IsDataChanged]
      ,[LastInventoryDate]
      ,[LastEditDate]
      ,[CreationDate]
      ,[LastEditBY]
      ,[CustomFld1]
      ,[CustomFld2]
      ,[CustomFld3]
      ,[CustomFld4]
      ,[CustomFld5]
      ,ActionType
      ,ActionDate)  
	SELECT 
	   inserted.[AstID]
      ,inserted.[DispCode]
      ,inserted.[ItemCode]
      ,inserted.[SuppID]
      ,inserted.[POCode]
      ,inserted.[InvNumber]
      ,inserted.[CustodianID]
      ,inserted.[BaseCost]
      ,inserted.[Tax]
      ,inserted.[SrvDate]
      ,inserted.[PurDate]
      ,inserted.[Disposed]
      ,inserted.[DispDate]
      ,inserted.[InvSchCode]
      ,inserted.[BookID]
      ,inserted.[InsID]
      ,inserted.[LocID]
      ,inserted.[InvStatus]
      ,inserted.[IsDeleted]
      ,inserted.[IsSold]
      ,inserted.[Sel_Date]
      ,inserted.[Sel_Price]
      ,inserted.[SoldTo]
      ,inserted.[RefNo]
      ,inserted.[AstNum]
      ,inserted.[AstBrandId]
      ,inserted.[AstDesc]
      ,inserted.[AstModel]
      ,inserted.[CompanyID]
      ,inserted.[TransRemarks]
      ,inserted.[LabelCount]
      ,inserted.[OldAssetID]
      ,inserted.[Discount]
      ,inserted.[NoPiece]
      ,inserted.[BarCode]
      ,inserted.[SerailNo]
      ,inserted.[RefCode]
      ,inserted.[Plate]
      ,inserted.[Poerp]
      ,inserted.[Capex]
      ,inserted.[Grn]
      ,inserted.[GLCode]
      ,inserted.[PONumber]
      ,inserted.[AstDesc2]
      ,inserted.[CapitalizationDate]
      ,inserted.[BussinessArea]
      ,inserted.[InventoryNumber]
      ,inserted.[CostCenterID]
      ,inserted.[InStockAsset]
      ,inserted.[EvaluationGroup1]
      ,inserted.[EvaluationGroup2]
      ,inserted.[EvaluationGroup3]
      ,inserted.[EvaluationGroup4]
      ,inserted.[CreatedBY]
      ,inserted.[IsDataChanged]
      ,inserted.[LastInventoryDate]
      ,inserted.[LastEditDate]
      ,inserted.[CreationDate]
      ,inserted.[LastEditBY]
      ,inserted.[CustomFld1]
      ,inserted.[CustomFld2]
      ,inserted.[CustomFld3]
      ,inserted.[CustomFld4]
      ,inserted.[CustomFld5]
       ,'Updated'
      ,GetDate()
  FROM inserted 
  
END

GO


--FairMont Update 3.5.2.0
CREATE TABLE [dbo].[CustomFields](
	[ID] [int] NOT NULL,
	[FormName] [nvarchar](50) NOT NULL,
	[ControlName] [nvarchar](50) NOT NULL,
	[EngCaption] [nvarchar](50) NULL,
	[ArabicCaption] [nvarchar](50) NULL,
	[UrduCaption] [nvarchar](50) NULL,
	[Example] [nvarchar](50) NULL,
 CONSTRAINT [PK_CustomFields] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CustomFields] ADD  CONSTRAINT [DF_CustomFields_FormName]  DEFAULT ('') FOR [FormName]
GO

ALTER TABLE [dbo].[CustomFields] ADD  CONSTRAINT [DF_CustomFields_ControlName]  DEFAULT ('') FOR [ControlName]
GO

ALTER TABLE [dbo].[CustomFields] ADD  CONSTRAINT [DF_CustomFields_EngCaption]  DEFAULT ('') FOR [EngCaption]
GO

ALTER TABLE [dbo].[CustomFields] ADD  CONSTRAINT [DF_CustomFields_ArabicCaption]  DEFAULT ('') FOR [ArabicCaption]
GO

ALTER TABLE [dbo].[CustomFields] ADD  CONSTRAINT [DF_CustomFields_UrduCaption]  DEFAULT ('') FOR [UrduCaption]
GO

ALTER TABLE [dbo].[CustomFields] ADD  CONSTRAINT [DF_CustomFields_Example]  DEFAULT ('') FOR [Example]
GO




--Nahdi 3.5.2.1
USE [ZulAssetsBE]

alter table custodian
alter column CustodianName Nvarchar(100)
GO

ALTER TABLE dbo.Ast_History ADD
	NoPiece int NULL
GO



USE [ZulAssetsBETemp]
update [DBVersion] set version = 'V3.5.4.0',lastUpdate = getdate()
Go
USE [ZulAssetsBE]
GO
update [DB Version] set version = 'V3.5.4.0',lastUpdate = getdate()
Go	



--Fairmont Update 
ALTER TABLE dbo.Assets ADD
	Warranty int NULL
GO
ALTER TABLE dbo.AssetDetails ADD
	Warranty int NULL
GO

update assets set warranty =0
GO
update AssetDetails set warranty =0
GO

USE [ZulAssetsBETemp]
update [DBVersion] set version = 'V3.5.4.1',lastUpdate = getdate()
Go
USE [ZulAssetsBE]
GO
update [DB Version] set version = 'V3.5.4.1',lastUpdate = getdate()
Go

--Fix the roles for some menu items.

ALTER TABLE Roles ADD
	OfflineMachine smallint NULL,
	BackendInventory smallint NULL,
	Custom1 smallint NULL,
	Custom2 smallint NULL,
	Custom3 smallint NULL,
	Custom4 smallint NULL,
	Custom5 smallint NULL,
	Custom6 smallint NULL,
	Custom7 smallint NULL,
	Custom8 smallint NULL,
	Custom9 smallint NULL,
	Custom10 smallint NULL,
	Custom11 smallint NULL,
	Custom12 smallint NULL,
	Custom13 smallint NULL,
	Custom14 smallint NULL,
	Custom15 smallint NULL
GO
update Roles set  OfflineMachine = 0,BackendInventory =0,Custom1 =0,Custom2 =0,Custom3 =0,Custom4 =0,Custom5 =0,Custom6 =0,Custom7 =0,Custom8 =0,Custom9 =0,Custom10 =0,Custom11 =0,Custom12 =0,Custom13 =0,Custom14 =0,Custom15 = 0 where roleid <> 1
GO
update Roles set  OfflineMachine = 1,BackendInventory =1,Custom1 =1,Custom2 =1,Custom3 =1,Custom4 =1,Custom5 =1,Custom6 =1,Custom7 =1,Custom8 =1,Custom9 =1,Custom10 =1,Custom11 =1,Custom12 =1,Custom13 =1,Custom14 =1,Custom15 = 1 where roleid  = 1
GO

USE [ZulAssetsBETemp]
update [DBVersion] set version = 'V3.5.4.2',lastUpdate = getdate()
Go
USE [ZulAssetsBE]
GO
update [DB Version] set version = 'V3.5.4.2',lastUpdate = getdate()
Go

---Abunayyan Update....
USE [ZulAssetsBE]
GO
ALTER TABLE dbo.CostCenter ADD
	CompanyId int NULL
GO
update [DB Version] set version = 'V3.5.4.3',lastUpdate = getdate()
Go


USE [ZulAssetsBETemp]
Go
CREATE TABLE [dbo].[CostCenterTemp](
	[CostID] [varchar](50) NOT NULL,
	[CostNumber] [varchar](50) NULL,
	[CostName] [varchar](50) NULL,
	[CompanyId] [int] NULL,
 CONSTRAINT [PK_CostCenterTemp] PRIMARY KEY CLUSTERED 
(
	[CostID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE dbo.CustodianTemp ADD
	DeptId varchar(50) NULL
GO

update [DBVersion] set version = 'V3.5.4.3',lastUpdate = getdate()
Go


USE [ZulAssetsBE]
GO

ALTER TABLE dbo.Location ADD
	CompanyID int NULL
GO

update [DB Version] set version = 'V3.5.4.4',lastUpdate = getdate()
Go


---------------------------------------------------------------------
--V3.5.5.0-----------------------------------------------------------
---------------------------------------------------------------------
ALTER TABLE dbo.AssetDetails ADD
	StatusID int NULL
GO


CREATE TABLE [dbo].[AssetStatus](
	[ID] [int] NOT NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_AssetStatus] PRIMARY KEY CLUSTERED
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

insert into AssetStatus values(1,N'In Use')
insert into AssetStatus values(2,N'Live')
insert into AssetStatus values(3,N'In Stock')
insert into AssetStatus values(4,N'Off')
insert into AssetStatus values(5,N'In Vendor Stocks')
insert into AssetStatus values(6,N'In Storage')
insert into AssetStatus values(7,N'Loaned Out')
insert into AssetStatus values(8,N'Out For Repair')
insert into AssetStatus values(9,N'N/A')


update assetdetails set StatusID = 1


/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
CREATE TABLE dbo.Tmp_ReportsFiles
	(
	ReportName varchar(100) NOT NULL,
	ReportData ntext NULL,
	Query varchar(2000) NULL,
	Type bit NULL,
	IsDeleted bit NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
IF EXISTS(SELECT * FROM dbo.ReportsFiles)
	 EXEC('INSERT INTO dbo.Tmp_ReportsFiles (ReportName, ReportData, Query, Type, IsDeleted)
		SELECT ReportName, CONVERT(ntext, ReportData), Query, Type, IsDeleted FROM dbo.ReportsFiles WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.ReportsFiles
GO
EXECUTE sp_rename N'dbo.Tmp_ReportsFiles', N'ReportsFiles', 'OBJECT' 
GO
ALTER TABLE dbo.ReportsFiles ADD CONSTRAINT
	PK_ReportsFiles PRIMARY KEY CLUSTERED 
	(
	ReportName
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

ALTER TABLE Category
ALTER COLUMN AstCatDesc nvarchar(200)
COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
ALTER TABLE Category
ALTER COLUMN CompCode nvarchar(255)
COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
ALTER TABLE Category
ALTER COLUMN CatFullPath nvarchar(max)
COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
ALTER TABLE Category
ALTER COLUMN Code nvarchar(100)
COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
ALTER TABLE Location
ALTER COLUMN LocDesc nvarchar(200)
COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
ALTER TABLE Location
ALTER COLUMN Code nvarchar(100)
COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
ALTER TABLE Location
ALTER COLUMN CompCode nvarchar(255)
COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
ALTER TABLE Location
ALTER COLUMN LocationFullPath nvarchar(max)
COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO

ALTER TABLE Assets
ALTER COLUMN AstDesc nvarchar(200)
COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
ALTER TABLE AssetDetails
ALTER COLUMN AstDesc nvarchar(200)
COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO
ALTER TABLE AssetDetails
ALTER COLUMN AstDesc2 nvarchar(200)
COLLATE SQL_Latin1_General_CP1_CI_AS NULL
GO

update [DB Version] set version = 'V3.5.5.0',lastUpdate = getdate()
Go




--Update the database for version 3.5.8.2 to 3.5.9.0
USE [ZulAssetsBE]
GO

CREATE TABLE [dbo].[AssetWarranty](
	[ID] [bigint] NOT NULL,
	[AstID] [varchar](25) NOT NULL,
	[WarrantyStart] [datetime] NOT NULL,
	[WarrantyPeriodMonth] [int] NOT NULL,
	[AlarmBeforeDays] [int] NOT NULL,
	[AlarmActivated] [bit] NOT NULL,
 CONSTRAINT [PK_AssetWarranty] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[StockTransfer](
	[GUID] [uniqueidentifier] NOT NULL,
	[FromWarehouseGUID] [uniqueidentifier] NULL,
	[ToWarehouseGUID] [uniqueidentifier] NULL,
	[CustodianID] [varchar](25) NULL,
	[POCode] [bigint] NULL,
	[Code] [nvarchar](50) NULL,
	[Notes] [nvarchar](max) NULL,
	[TransDate] [datetime] NULL,
	[Status] [nvarchar](50) NULL,
	[LastEditDate] [datetime] NULL,
	[CreationDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastEditBY] [uniqueidentifier] NULL,
	[TransferType] [nvarchar](50) NULL,
	[InvoiceNumber] [nvarchar](255) NULL,
	[SuppID] [varchar](25) NULL,
	[TotalDiscount] [float] NULL,
	[FreightCharge] [float] NULL,
	[AddCharges] [float] NULL,
	[TotalAmount] [float] NULL,
	[CurrencyGUID] [uniqueidentifier] NULL,
	[BatchID] [nvarchar](255) NULL,
	[AWBNumber] [nvarchar](255) NULL,
 CONSTRAINT [PK_StockTransfer] PRIMARY KEY CLUSTERED 
(
	[GUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[StockTransferItems](
	[GUID] [uniqueidentifier] NOT NULL,
	[ItemCode] [varchar](25) NULL,
	[StockTransferGUID] [uniqueidentifier] NULL,
	[QTY] [float] NULL,
	[Notes] [nvarchar](max) NULL,
	[LastEditDate] [datetime] NULL,
	[CreationDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastEditBY] [uniqueidentifier] NULL,
	[Price] [float] NULL,
	[Discount] [float] NULL,
	[SupplierRef] [nvarchar](255) NULL,
	[FromLocID] [varchar](255) NULL,
	[ToLocID] [varchar](255) NULL,
	[LotNumber] [nvarchar](255) NULL,
	[SerialNumber] [nvarchar](255) NULL,
	[ExpireDate] [datetime] NULL,
	[ProductionDate] [datetime] NULL,
	[WarrantyExpireDate] [datetime] NULL,
	[Number] [int] NULL,
	[CustodianID] [varchar](25) NULL,
 CONSTRAINT [PK_StockTransferItems] PRIMARY KEY CLUSTERED 
(
	[GUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[StockTransferItems]  WITH CHECK ADD  CONSTRAINT [FK_StockTransferItems_StockTransfer] FOREIGN KEY([StockTransferGUID])
REFERENCES [dbo].[StockTransfer] ([GUID])
GO

ALTER TABLE [dbo].[StockTransferItems] CHECK CONSTRAINT [FK_StockTransferItems_StockTransfer]
GO



ALTER TABLE dbo.SysSettings ADD
	ShowAlarmOnStartup bit NULL,
	AlarmBeforeDays int NULL
GO


update SysSettings set ShowAlarmOnStartup = 0
update SysSettings set AlarmBeforeDays = 0


update [DB Version] set version = 'V3.5.9.0',lastUpdate = getdate()
Go

USE [ZulAssetsBETemp]
Go
update [DBVersion] set version = 'V3.5.9.0',lastUpdate = getdate()
Go



USE [ZulAssetsBE]
GO
--Second update for SABB:

ALTER TABLE dbo.StockTransfer ADD
	FromLocID varchar(255) NULL,
	ToLocID varchar(255) NULL,
	AssetStatusID int NULL
GO

ALTER TABLE dbo.StockTransfer
	DROP COLUMN FromWarehouseGUID, ToWarehouseGUID
GO

ALTER TABLE dbo.StockTransferItems ADD
	AssetStatusID int NULL
GO

GO
ALTER TABLE dbo.AssetStatus ADD
	IsReturnStatus bit NULL
GO

update AssetStatus set IsReturnStatus = 0


insert into AssetStatus values(20,N'Trade-In',1)
insert into AssetStatus values(21,N'Do Cancel',1)
insert into AssetStatus values(22,N'Replacement',1)
insert into AssetStatus values(23,N'DOA',1)
insert into AssetStatus values(24,N'Credit Note',1)
insert into AssetStatus values(25,N'Repair',1)


update [DB Version] set version = 'V3.5.9.2',lastUpdate = getdate()
Go


CREATE TABLE [dbo].[StockTransferItemsDetails](
	[GUID] [uniqueidentifier] NOT NULL,
	[STItemGUID] [uniqueidentifier] NULL,
	[LotNumber] [nvarchar](255) NULL,
	[SerialNumber] [nvarchar](255) NULL,
	[ExpireDate] [datetime] NULL,
	[ProductionDate] [datetime] NULL,
	[WarrantyExpireDate] [datetime] NULL,
	[QTY] [float] NULL,
	[Number] [int] NULL,
	[LastEditDate] [datetime] NULL,
	[CreationDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastEditBY] [uniqueidentifier] NULL,
 CONSTRAINT [PK_StockTransferItemDetails] PRIMARY KEY CLUSTERED 
(
	[GUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE dbo.StockTransferItemsDetails ADD CONSTRAINT
	FK_StockTransferItemsDetails_StockTransferItems FOREIGN KEY
	(
	STItemGUID
	) REFERENCES dbo.StockTransferItems
	(
	GUID
	) ON UPDATE  CASCADE 
	 ON DELETE  CASCADE 
	
GO


CREATE TABLE [dbo].[DocumentNumbers](
	GUID [uniqueidentifier] NOT NULL,
	[DocTypeEngName] [nvarchar](255) NULL,
	[DocTypeName] [nvarchar](255) NULL,
	[SerialNumber] [bigint] NULL,
	[DigitsCount] [int] NULL,
	[Prefix] [nvarchar](50) NULL,
	[Suffix] [nvarchar](50) NULL,
	[LastEditDate] [datetime] NULL,
	[CreationDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[LastEditBY] [uniqueidentifier] NULL,
 CONSTRAINT [PK_DocumentNumbers] PRIMARY KEY CLUSTERED 
(
	[GUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]





INSERT [dbo].[DocumentNumbers] ([GUID], [DocTypeEngName], [DocTypeName], [SerialNumber], [DigitsCount], [Prefix], [Suffix], [LastEditDate], [CreationDate], [CreatedBy], [LastEditBY]) VALUES (N'fd53d7fa-8ca7-4d2d-9075-423ab34db12f', N'StockIssuance', N'', 0, 5, N'SI-', N'', CAST(0x0000A30F00A05FFF AS DateTime), CAST(0x0000A30F00A05FFF AS DateTime), N'9e515667-ab30-48ab-a0ad-79d1a6cda36c', N'9e515667-ab30-48ab-a0ad-79d1a6cda36c')
INSERT [dbo].[DocumentNumbers] ([GUID], [DocTypeEngName], [DocTypeName], [SerialNumber], [DigitsCount], [Prefix], [Suffix], [LastEditDate], [CreationDate], [CreatedBy], [LastEditBY]) VALUES (N'2167ea11-2946-41f7-bd01-5cf95bdc9a6e', N'CustodianReturn', N'', 0, 5, N'CR-', N'', CAST(0x0000A31B0095882D AS DateTime), CAST(0x0000A31B0095882D AS DateTime), N'9e515667-ab30-48ab-a0ad-79d1a6cda36c', N'9e515667-ab30-48ab-a0ad-79d1a6cda36c')
INSERT [dbo].[DocumentNumbers] ([GUID], [DocTypeEngName], [DocTypeName], [SerialNumber], [DigitsCount], [Prefix], [Suffix], [LastEditDate], [CreationDate], [CreatedBy], [LastEditBY]) VALUES (N'8dddc46a-ca6b-4abf-b66d-6c233c4a16ff', N'DirectReceiving', N'', 0, 5, N'GRN-', N'', CAST(0x0000A31B00F82BEC AS DateTime), CAST(0x0000A30F00A05FFF AS DateTime), N'9e515667-ab30-48ab-a0ad-79d1a6cda36c', N'9e515667-ab30-48ab-a0ad-79d1a6cda36c')
INSERT [dbo].[DocumentNumbers] ([GUID], [DocTypeEngName], [DocTypeName], [SerialNumber], [DigitsCount], [Prefix], [Suffix], [LastEditDate], [CreationDate], [CreatedBy], [LastEditBY]) VALUES (N'1d0c79e7-5381-450e-8191-a82d451bed03', N'DOReceiving', N'', 0, 5, N'DOGRN-', N'', CAST(0x0000A30E009DF0D3 AS DateTime), CAST(0x0000A30D00F6CACE AS DateTime), N'9e515667-ab30-48ab-a0ad-79d1a6cda36c', N'9e515667-ab30-48ab-a0ad-79d1a6cda36c')
INSERT [dbo].[DocumentNumbers] ([GUID], [DocTypeEngName], [DocTypeName], [SerialNumber], [DigitsCount], [Prefix], [Suffix], [LastEditDate], [CreationDate], [CreatedBy], [LastEditBY]) VALUES (N'ce41949b-7718-4a8c-8443-b05fd9b11c11', N'ItemReturn', N'', 0, 5, N'SR-', N'', CAST(0x0000A30F00A06000 AS DateTime), CAST(0x0000A30F00A06000 AS DateTime), N'9e515667-ab30-48ab-a0ad-79d1a6cda36c', N'9e515667-ab30-48ab-a0ad-79d1a6cda36c')

update [DB Version] set version = 'V3.6.0.0',lastUpdate = getdate()
Go

--Temp Updated up to here

ALTER TABLE dbo.PurchaseOrder 
alter column ReferenceNo varchar(250) null
GO

update CustomFields set EngCaption = 'Old DO#' where ID=1
update CustomFields set EngCaption = 'Old DO QTY' where ID=6


ALTER TABLE dbo.StockTransferItemsDetails ADD
	AstID varchar(25) NULL
GO

ALTER TABLE dbo.StockTransferItems ADD
	AstID varchar(25) NULL
GO

update [DB Version] set version = 'V3.7.0.0',lastUpdate = getdate()
Go


ALTER TABLE dbo.StockTransferItemsDetails ADD
	OldSerialNumber varchar(255)  NULL
GO

update [DB Version] set version = 'V3.7.0.1',lastUpdate = getdate()
Go

--Update V3.8.0.0 for Sanofi
USE [ZulAssetsBE]

GO

ALTER TABLE Custodian ADD
	CustodianCode nvarchar(255) NULL

GO

Update Custodian set CustodianCode = ''

GO


ALTER TABLE AssetDetails ADD
	DisposalComments nvarchar(MAX) NULL
	
GO	

Update AssetDetails set DisposalComments = ''

GO

ALTER TABLE DepPolicy ADD
	IsSalvageValuePercent bit NULL

GO

Update DepPolicy set IsSalvageValuePercent = 0

GO

ALTER TABLE AstBooks ADD
	SalvageValuePercent float(53) NULL
GO

update AstBooks set SalvageValuePercent = round((SalvageValue/(basecost + tax)) * 100,2) from AstBooks inner join AssetDetails on AssetDetails.AstID = AstBooks.AstID
where  basecost + tax> 0

GO

update AstBooks set SalvageValuePercent = 0 from AstBooks inner join AssetDetails on AssetDetails.AstID = AstBooks.AstID
where  basecost + tax <= 0

GO

update [DB Version] set version = 'V3.8.0.0',lastUpdate = getdate()
Go

