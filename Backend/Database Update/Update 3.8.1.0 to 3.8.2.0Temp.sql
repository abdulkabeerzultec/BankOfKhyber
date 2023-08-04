ALTER TABLE dbo.AssetsTemp ADD
	CustomFld1 nvarchar(255) NULL,
	CustomFld2 nvarchar(255) NULL,
	CustomFld3 nvarchar(255) NULL,
	CustomFld4 nvarchar(255) NULL,
	CustomFld5 nvarchar(255) NULL
GO
ALTER TABLE dbo.AssetsTemp ADD CONSTRAINT
	DF_AssetsTemp_CustomFld1 DEFAULT ('') FOR CustomFld1
GO
ALTER TABLE dbo.AssetsTemp ADD CONSTRAINT
	DF_AssetsTemp_CustomFld2 DEFAULT ('') FOR CustomFld2
GO
ALTER TABLE dbo.AssetsTemp ADD CONSTRAINT
	DF_AssetsTemp_CustomFld3 DEFAULT ('') FOR CustomFld3
GO
ALTER TABLE dbo.AssetsTemp ADD CONSTRAINT
	DF_AssetsTemp_CustomFld4 DEFAULT ('') FOR CustomFld4
GO
ALTER TABLE dbo.AssetsTemp ADD CONSTRAINT
	DF_AssetsTemp_CustomFld5 DEFAULT ('') FOR CustomFld5
GO
update DBVersion set version = 'V3.8.2.0',lastUpdate = getdate()
