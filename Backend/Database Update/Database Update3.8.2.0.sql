drop TRIGGER [dbo].[AssetLogInsert] 

GO

drop TRIGGER [dbo].[AssetLogDelete]

GO

drop TRIGGER [dbo].[AssetLogUpdate] 

GO

--drop table [dbo].[AssetDetailsChangeLogDetail]
--GO

--drop table [dbo].[AssetDetailsChangeLogSummary]
--GO

-- Create the Summary Table
CREATE TABLE [dbo].[AssetDetailsChangeLogSummary](
    [SummaryID] [BIGINT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [ID] varchar(25) NOT NULL,
    [ModifiedByUser] [VARCHAR](50) NULL,
    [ModifiedDate] [smalldatetime] NULL,
    [SeqID] [BIGINT] NULL,
    [LastModifiedDate] [smalldatetime] NULL,
    [ChangeType] CHAR(1) NULL,
        [TableName] VARCHAR(50) NULL
)
;
CREATE NONCLUSTERED INDEX [IX_TableChangeSummary_ID] ON [AssetDetailsChangeLogSummary] ( [ID] ASC )
;
CREATE NONCLUSTERED INDEX [IX_TableChangeSummary_TableDate] ON [AssetDetailsChangeLogSummary] ( [TableName], [ModifiedDate] )
;
CREATE NONCLUSTERED INDEX [IX_TableChangeSummary_DateTable] ON [AssetDetailsChangeLogSummary] ( [ModifiedDate], [TableName] )
;


-- Create Detail Table
CREATE TABLE [dbo].[AssetDetailsChangeLogDetail](
    [DetailID] [BIGINT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [SummaryID] [BIGINT] NOT NULL,
    [ColumnName] [VARCHAR](100) NOT NULL,
    [OldValue] [NVARCHAR](MAX) NULL,
    [NewValue] [NVARCHAR](MAX) NULL
)
;
CREATE NONCLUSTERED INDEX [IX_TableChangeDetail_Summary] ON [dbo].[AssetDetailsChangeLogDetail] ( [SummaryID] ASC )
;
CREATE NONCLUSTERED INDEX [IX_AssetDetailsChangeLogDetail_Column] ON [dbo].[AssetDetailsChangeLogDetail] ( [ColumnName] ASC, [SummaryID] ASC )
;
-- Add a Foreign Key to link the tables
-- and auto-delete the detail when the summary is deleted
ALTER TABLE [dbo].[AssetDetailsChangeLogDetail]  WITH CHECK ADD  CONSTRAINT [FK_AssetDetailsChangeLogDetail_AssetDetailsChangeLogSummary] FOREIGN KEY([SummaryID])
REFERENCES [dbo].[AssetDetailsChangeLogSummary] ([SummaryID])
ON DELETE CASCADE
;
ALTER TABLE [dbo].[AssetDetailsChangeLogDetail] CHECK CONSTRAINT [FK_AssetDetailsChangeLogDetail_AssetDetailsChangeLogSummary]
;

GO

--drop trigger ChangeTrackingTrigger_Table
--GO

CREATE TRIGGER ChangeTrackingTrigger_Table
   ON [dbo].[AssetDetails]  
   AFTER INSERT, DELETE, UPDATE
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;
    -- Get the table name of the current process
    DECLARE @TableName VARCHAR(25);
        SET @TableName = COALESCE((SELECT object_name(parent_id) FROM sys.triggers WHERE object_id = @@PROCID), 'Unknown');
        DECLARE @xmlOld xml;
    DECLARE @xmlNew xml;
    DECLARE @SummaryID INT;
    DECLARE @t TABLE (
            ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
            RowName VARCHAR(100),
            Value1 VARCHAR(100),
            Value2 VARCHAR(100)
            );
    SET @xmlOld =
        (
        SELECT TOP 1 [AstID] ,[DispCode] ,[ItemCode] ,[SuppID] ,[POCode] ,[InvNumber] ,[CustodianID] ,[BaseCost] ,[Tax] ,[SrvDate] ,[PurDate] ,[Disposed] ,[DispDate] ,[InvSchCode] ,[BookID] ,[InsID] ,[LocID] ,[InvStatus] ,[IsDeleted] ,[IsSold] ,[Sel_Date] ,[Sel_Price] ,[SoldTo] ,[RefNo] ,[AstNum] ,[AstBrandId] ,[AstDesc] ,[AstModel] ,[CompanyID] ,[TransRemarks] ,[LabelCount] ,[OldAssetID] ,[Discount] ,[NoPiece] ,[BarCode] ,[SerailNo] ,[RefCode] ,[Plate] ,[Poerp] ,[Capex] ,[Grn] ,[GLCode] ,[PONumber] ,[AstDesc2] ,[CapitalizationDate] ,[BussinessArea] ,[InventoryNumber] ,[CostCenterID] ,[InStockAsset] ,[EvaluationGroup1] ,[EvaluationGroup2] ,[EvaluationGroup3] ,[EvaluationGroup4] ,[CreatedBY] ,[IsDataChanged] ,[LastInventoryDate] ,[LastEditDate] ,[CreationDate] ,[LastEditBY] ,[CustomFld1] ,[CustomFld2] ,[CustomFld3] ,[CustomFld4] ,[CustomFld5] ,[Warranty] ,[StatusID] ,[DisposalComments]
        FROM [deleted] AS [TABLE]
        ORDER BY AstID
        FOR XML AUTO, ELEMENTS
        );
    SET @xmlNew =
        (
        SELECT TOP 1 [AstID] ,[DispCode] ,[ItemCode] ,[SuppID] ,[POCode] ,[InvNumber] ,[CustodianID] ,[BaseCost] ,[Tax] ,[SrvDate] ,[PurDate] ,[Disposed] ,[DispDate] ,[InvSchCode] ,[BookID] ,[InsID] ,[LocID] ,[InvStatus] ,[IsDeleted] ,[IsSold] ,[Sel_Date] ,[Sel_Price] ,[SoldTo] ,[RefNo] ,[AstNum] ,[AstBrandId] ,[AstDesc] ,[AstModel] ,[CompanyID] ,[TransRemarks] ,[LabelCount] ,[OldAssetID] ,[Discount] ,[NoPiece] ,[BarCode] ,[SerailNo] ,[RefCode] ,[Plate] ,[Poerp] ,[Capex] ,[Grn] ,[GLCode] ,[PONumber] ,[AstDesc2] ,[CapitalizationDate] ,[BussinessArea] ,[InventoryNumber] ,[CostCenterID] ,[InStockAsset] ,[EvaluationGroup1] ,[EvaluationGroup2] ,[EvaluationGroup3] ,[EvaluationGroup4] ,[CreatedBY] ,[IsDataChanged] ,[LastInventoryDate] ,[LastEditDate] ,[CreationDate] ,[LastEditBY] ,[CustomFld1] ,[CustomFld2] ,[CustomFld3] ,[CustomFld4] ,[CustomFld5] ,[Warranty] ,[StatusID] ,[DisposalComments]
        FROM [inserted] AS [TABLE]
        ORDER BY AstID
        FOR XML AUTO, ELEMENTS
        );
    -- now join the two XMLs using the ColumName and exclude any that are not different
    WITH
        XML1 AS
        (
            SELECT T.N.value('local-name(.)', 'nvarchar(100)') AS NodeName,
                    T.N.value('.', 'nvarchar(100)') AS VALUE
            FROM @xmlOld.nodes('/TABLE/*') AS T(N)
        ),
        XML2 AS
        (
            SELECT T.N.value('local-name(.)', 'nvarchar(100)') AS NodeName,
                    T.N.value('.', 'nvarchar(100)') AS VALUE
            FROM @xmlNew.nodes('/TABLE/*') AS T(N)
        )
        INSERT INTO @t (RowName, Value1, Value2)
            SELECT COALESCE(XML1.NodeName, XML2.NodeName) AS NodeName,
                   XML1.Value AS Value1,
                   XML2.Value AS Value2
            FROM XML1
              FULL OUTER JOIN XML2
                ON XML1.NodeName = XML2.NodeName
            WHERE COALESCE(XML1.Value, '')  <> COALESCE(XML2.Value, '')
              AND XML1.NodeName NOT IN ('AstImage', 'AstID', 'LastEditDate', 'LastEditBY','IsDataChanged')
			  
    ;
    -- Now create the Summary record
    INSERT INTO AssetDetailsChangeLogSummary (ID, ModifiedByUser, ModifiedDate, SeqID, LastModifiedDate, ChangeType, TableName)
        SELECT COALESCE(I.AstID, D.AstID),
            I.LastEditBY,
            I.LastEditDate,
            I.AstNum,
            D.LastEditDate,
            CASE WHEN (D.AstID IS NULL) THEN 'I' WHEN (I.AstID IS NULL) THEN 'D' ELSE 'U' END,
                        @TableName
        FROM [inserted] I
        FULL OUTER JOIN [deleted] D ON I.AstID = D.AstID
    ;
    -- Now capture the new SummaryID that was generated
    SET @SummaryID = (SELECT SCOPE_IDENTITY());
    -- Now create the detail records
    INSERT INTO AssetDetailsChangeLogDetail (SummaryID, ColumnName, OldValue, NewValue)
        SELECT @SummaryID, T.RowName, T.Value1, T.Value2
        FROM @t T 
    ;
END
update [DB Version] set version = 'V3.8.2.0',lastUpdate = getdate()
Go

