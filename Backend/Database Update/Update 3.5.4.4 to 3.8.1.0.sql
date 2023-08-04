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



CREATE TABLE dbo.Tmp_Supplier
	(
	SuppID varchar(25) NOT NULL,
	SuppName nvarchar(50) NULL,
	SuppCell varchar(50) NULL,
	SuppFax varchar(50) NULL,
	SuppPhone varchar(50) NULL,
	SuppEmail varchar(50) NULL,
	IsDeleted bit NULL
	)  ON [PRIMARY]
GO
IF EXISTS(SELECT * FROM dbo.Supplier)
	 EXEC('INSERT INTO dbo.Tmp_Supplier (SuppID, SuppName, SuppCell, SuppFax, SuppPhone, SuppEmail, IsDeleted)
		SELECT SuppID, CONVERT(nvarchar(50), SuppName), SuppCell, SuppFax, SuppPhone, SuppEmail, IsDeleted FROM dbo.Supplier WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.Supplier
GO
EXECUTE sp_rename N'dbo.Tmp_Supplier', N'Supplier', 'OBJECT' 
GO
ALTER TABLE dbo.Supplier ADD CONSTRAINT
	PK_Supplier PRIMARY KEY CLUSTERED 
	(
	SuppID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO



CREATE TABLE dbo.Tmp_Brand
	(
	AstBrandID int NOT NULL,
	AstBrandName nvarchar(100) NULL,
	IsDeleted bit NULL
	)  ON [PRIMARY]
GO
IF EXISTS(SELECT * FROM dbo.Brand)
	 EXEC('INSERT INTO dbo.Tmp_Brand (AstBrandID, AstBrandName, IsDeleted)
		SELECT AstBrandID, CONVERT(nvarchar(100), AstBrandName), IsDeleted FROM dbo.Brand WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.Brand
GO
EXECUTE sp_rename N'dbo.Tmp_Brand', N'Brand', 'OBJECT' 
GO
ALTER TABLE dbo.Brand ADD CONSTRAINT
	PK_Brand PRIMARY KEY CLUSTERED 
	(
	AstBrandID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

CREATE TABLE dbo.Tmp_Insurer
	(
	InsCode int NOT NULL,
	InsName nvarchar(50) NULL,
	IsDeleted bit NULL
	)  ON [PRIMARY]
GO
IF EXISTS(SELECT * FROM dbo.Insurer)
	 EXEC('INSERT INTO dbo.Tmp_Insurer (InsCode, InsName, IsDeleted)
		SELECT InsCode, CONVERT(nvarchar(50), InsName), IsDeleted FROM dbo.Insurer WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.Insurer
GO
EXECUTE sp_rename N'dbo.Tmp_Insurer', N'Insurer', 'OBJECT' 
GO
ALTER TABLE dbo.Insurer ADD CONSTRAINT
	PK_Insurer PRIMARY KEY CLUSTERED 
	(
	InsCode
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

update [DB Version] set version = 'V3.8.1.0',lastUpdate = getdate()
Go

