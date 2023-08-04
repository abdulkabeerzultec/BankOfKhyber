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
