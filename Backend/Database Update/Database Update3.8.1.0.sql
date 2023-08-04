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

