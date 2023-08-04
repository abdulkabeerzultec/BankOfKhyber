alter table "AppUsers"
   drop constraint FK_APPUSERS_REFERENCE_ROLES;

alter table "AssetDetails"
   drop constraint FK_ASSETDET_FK_ASSETD_ASSETS;

alter table "AssetDetails"
   drop constraint FK_ASSETDET_FK_ASSETD_COMPANIE;

alter table "AssetDetails"
   drop constraint FK_ASSETDET_FK_ASSETD_CUSTODIA;

alter table "AssetDetails"
   drop constraint FK_ASSETDET_FK_ASSETD_LOCATION;

alter table "Assets"
   drop constraint FK_ASSETS_FK_ASSETS_CATEGORY;

alter table "AstBooks"
   drop constraint FK_ASTBOOKS_FK_ASTBOO_DEPRECIA;

alter table "Ast_Cust_history"
   drop constraint FK_AST_CUST_FK_AST_CU_ASSETDET;

alter table "Ast_Cust_history"
   drop constraint FK_AST_CUST_FK_AST_CU_CUSTODIA;

alter table "Ast_Cust_history"
   drop constraint FK_AST_CUST_FK_AST_CU_CUSTODIA;

alter table "Ast_History"
   drop constraint FK_AST_HIST_FK_AST_HI_ASSETDET;

alter table "Ast_History"
   drop constraint FK_AST_HIST_FK_AST_HI_LOCATION;

alter table "Ast_History"
   drop constraint FK_AST_HIST_FK_AST_HI_LOCATION;

alter table "BookHistory"
   drop constraint FK_BOOKHIST_FK_BOOKHI_ASSETDET;

alter table "BookHistory"
   drop constraint FK_BOOKHIST_FK_BOOKHI_DEPRECIA;

alter table "Books"
   drop constraint FK_BOOKS_FK_BOOKS__COMPANIE;

alter table "Books"
   drop constraint FK_BOOKS_FK_BOOKS__DEPRECIA;

alter table "Custodian"
   drop constraint FK_CUSTODIA_FK_CUSTOD_DEPARTME;

alter table "Custodian"
   drop constraint FK_CUSTODIA_FK_CUSTOD_DESIGNAT;

alter table "DepPolicy"
   drop constraint FK_DEPPOLIC_FK_DEPPOL_CATEGORY;

alter table "DepPolicy"
   drop constraint FK_DEPPOLIC_FK_DEPPOL_DEPRECIA;

alter table "PODetails"
   drop constraint FK_PODETAIL_FK_PODETA_ASSETS;

alter table "PODetails"
   drop constraint FK_PODETAIL_FK_PODETA_PURCHASE;

alter table "PurchaseOrder"
   drop constraint FK_PURCHASE_FK_PURCHA_COSTCENT;

alter table "PurchaseOrder"
   drop constraint FK_PURCHASE_FK_PURCHA_SUPPLIER;

drop table AUDITSTATUS cascade constraints;

drop table "Address" cascade constraints;

drop table "AppUsers" cascade constraints;

drop table "AssetCoding" cascade constraints;

drop table "AssetDetails" cascade constraints;

drop table "Assets" cascade constraints;

drop table "AstBooks" cascade constraints;

drop table "AstBooks_temp" cascade constraints;

drop table "Ast_Cust_history" cascade constraints;

drop table "Ast_History" cascade constraints;

drop table "Ast_INV_Schedule" cascade constraints;

drop table "BarCode_Struct" cascade constraints;

drop table "BookHistory" cascade constraints;

drop table "BookHistory_temp" cascade constraints;

drop table "Books" cascade constraints;

drop table "Brand" cascade constraints;

drop table "Category" cascade constraints;

drop table "CompGroups" cascade constraints;

drop table "CompLvl" cascade constraints;

drop table "Companies" cascade constraints;

drop table "CompanyInfo" cascade constraints;

drop table "CostCenter" cascade constraints;

drop table "Custodian" cascade constraints;

drop table "DB Version" cascade constraints;

drop table "DepLogs" cascade constraints;

drop table "DepPolicy" cascade constraints;

drop table "DepPolicy_History" cascade constraints;

drop table "Department" cascade constraints;

drop table "Depreciation_Method" cascade constraints;

drop table "Designation" cascade constraints;

drop table "Devices" cascade constraints;

drop table "Disposal_Method" cascade constraints;

drop table "GLCodes" cascade constraints;

drop table "Insurer" cascade constraints;

drop table "Location" cascade constraints;

drop table "NonBarCodedTemp" cascade constraints;

drop table "OrgHier" cascade constraints;

drop table "PODetails" cascade constraints;

drop table "PurchaseOrder" cascade constraints;

drop table "Report_AssetCategory" cascade constraints;

drop table "Report_AssetSubCategory" cascade constraints;

drop table "Report_CompanyAssets" cascade constraints;

drop table "Roles" cascade constraints;

drop table "Supplier" cascade constraints;

drop table "SysConfig" cascade constraints;

drop table "Units" cascade constraints;

drop sequence "S_BookHistory";

drop sequence "S_CompanyInfo";

drop sequence "S_DepLogs";

create sequence "S_BookHistory";

create sequence "S_CompanyInfo";

create sequence "S_DepLogs";

create table AUDITSTATUS  (
   "StatusID"           NUMBER(4),
   "StatusDesc"         VARCHAR2(50)
);

create table "Address"  (
   "AddressID"          NUMBER(37)                      not null,
   "AddressDesc"        VARCHAR2(200),
   "IsDeleted"          NUMBER(1),
   constraint PK_ADDRESS primary key ("AddressID")
);

create table "AppUsers"  (
   "LoginName"          VARCHAR2(50)                    not null,
   "UserName"           VARCHAR2(50),
   "Password"           VARCHAR2(50),
   "IsDeleted"          NUMBER(1),
   "UserAccess"         NUMBER(5),
   "RoleID"             NUMBER(12),
   constraint PK_APPUSERS primary key ("LoginName")
);

create table "AssetCoding"  (
   "CompanyID"          NUMBER(12)                      not null,
   "StartSerial"        NUMBER(37),
   "EndSerial"          NUMBER(37),
   "Status"             NUMBER(1),
   constraint PK_ASSETCODING primary key ()
);

create table "AssetDetails"  (
   "AstID"              VARCHAR2(25)                    not null,
   "DispCode"           NUMBER(4),
   "ItemCode"           VARCHAR2(25)                    not null,
   "SuppID"             VARCHAR(25),
   "POCode"             NUMBER(37),
   "InvNumber"          VARCHAR2(25),
   "CustodianID"        VARCHAR2(25)                    not null,
   "BaseCost"           FLOAT,
   "Tax"                FLOAT,
   "SrvDate"            DATE,
   "PurDate"            DATE,
   "Disposed"           NUMBER(1),
   "DispDate"           DATE,
   "InvSchCode"         NUMBER(37),
   "BookID"             VARCHAR2(50),
   "InsID"              NUMBER(37),
   "LocID"              VARCHAR2(255)                   not null,
   "InvStatus"          NUMBER(12),
   "IsDeleted"          NUMBER(1),
   "IsSold"             NUMBER(1),
   "Sel_Date"           DATE,
   "Sel_Price"          FLOAT,
   "SoldTo"             VARCHAR2(50),
   "RefNo"              VARCHAR2(50),
   "AstNum"             NUMBER(37),
   "AstBrandId"         NUMBER(12)                      not null,
   "AstDesc"            VARCHAR2(200),
   "AstModel"           VARCHAR2(50),
   "CompanyID"          NUMBER(12)                      not null,
   "TransRemarks"       VARCHAR2(200),
   "LabelCount"         NUMBER(12),
   "OldAssetID"         VARCHAR2(50),
   "Discount"           NUMBER(37),
   "NoPiece"            NUMBER(12),
   "BarCode"            VARCHAR2(25),
   "SerailNo"           VARCHAR2(20),
   "RefCode"            VARCHAR2(50),
   "Plate"              VARCHAR2(50),
   "Poerp"              VARCHAR2(50),
   "Capex"              VARCHAR2(50),
   "Grn"                VARCHAR2(50),
   "GLCode"             NUMBER(12),
   "PONumber"           NVARCHAR2(50),
   constraint PK_ASSETDETAILS primary key ("AstID")
);

create table "Assets"  (
   "ItemCode"           VARCHAR2(25)                    not null,
   "AstBrandID"         NUMBER(12),
   "AstCatID"           VARCHAR2(25)                    not null,
   "POItmID"            VARCHAR2(25),
   "AstDesc"            VARCHAR2(100),
   "AstModel"           VARCHAR2(50),
   "AstQty"             NUMBER(37),
   "IsDeleted"          NUMBER(1),
   constraint PK_ASSETS primary key ("ItemCode")
);

create table "AstBooks"  (
   "BookID"             VARCHAR2(50)                    not null,
   "AstID"              VARCHAR2(25),
   "DepCode"            NUMBER(12),
   "SalvageValue"       FLOAT,
   "SalvageYear"        NUMBER(12),
   "LastBV"             FLOAT,
   "CurrentBV"          FLOAT,
   "BVUpdate"           DATE,
   "IsDelete"           NUMBER(1),
   "SalvageMonth"       NUMBER(12),
   constraint PK_ASTBOOKS primary key ()
);

create table "AstBooks_temp"  (
   "BookID"             VARCHAR2(50)                    not null,
   "AstID"              VARCHAR2(25),
   "DepCode"            NUMBER(12),
   "SalvageValue"       FLOAT,
   "SalvageYear"        NUMBER(12),
   "LastBV"             FLOAT,
   "CurrentBV"          FLOAT,
   "BVUpdate"           DATE,
   "IsDelete"           NUMBER(1),
   "SalvageMonth"       NUMBER(12),
   constraint PK_ASTBOOKS_TEMP primary key ()
);

create table "Ast_Cust_history"  (
   "HistoryID"          NUMBER(37)                      not null,
   "AstId"              VARCHAR2(50)                    not null,
   "ToCustodian"        VARCHAR2(25)                    not null,
   "Fromcustodian"      VARCHAR2(25)                    not null,
   "HisDate"            DATE,
   "IsDeleted"          NUMBER(1),
   constraint PK_AST_CUST_HISTORY primary key ("HistoryID")
);

create table "Ast_History"  (
   "HistoryID"          NUMBER(37)                      not null,
   "InvSchCode"         NUMBER(37),
   "AstID"              VARCHAR2(25)                    not null,
   "Fr_loc"             VARCHAR2(255),
   "To_Loc"             VARCHAR2(255)                   not null,
   "HisDate"            DATE,
   "Status"             INTEGER,
   "IsDeleted"          NUMBER(1),
   constraint PK_AST_HISTORY primary key ("HistoryID")
);

create table "Ast_INV_Schedule"  (
   "InvSchCode"         NUMBER(37)                      not null,
   "InvDesc"            VARCHAR2(100),
   "InvStartDate"       DATE,
   "InvEndDate"         DATE,
   "IsDeleted"          NUMBER(1),
   "Closed"             NUMBER(1),
   constraint PK_AST_INV_SCHEDULE primary key ("InvSchCode")
);

create table "BarCode_Struct"  (
   "BarStructID"        NUMBER(37)                      not null,
   "BarStructDesc"      VARCHAR2(25),
   "BarStructLength"    NUMBER(37),
   "BarStructPreFix"    VARCHAR2(10),
   "ValueSep"           VARCHAR2(5),
   "BarCode"            VARCHAR2(25),
   "IsDeleted"          NUMBER(1),
   constraint PK_BARCODE_STRUCT primary key ("BarStructID")
);

create table "BookHistory"  (
   "DepHistID"          NUMBER(37)                      not null,
   "BookID"             VARCHAR2(50),
   "DepCode"            NUMBER(12),
   "DepValue"           FLOAT,
   "AccDepValue"        FLOAT,
   "DepDate"            DATE,
   "CurrentBV"          FLOAT,
   "IsDeleted"          NUMBER(1),
   "AssetId"            VARCHAR2(100),
   "SalvageYear"        NUMBER(12),
   "SalvageMonth"       NUMBER(12),
   constraint PK_BOOKHISTORY primary key ("DepHistID")
);

create table "BookHistory_temp"  (
   "DepHistID"          NUMBER(37)                      not null,
   "BookID"             VARCHAR2(50),
   "DepCode"            NUMBER(12),
   "DepValue"           FLOAT,
   "AccDepValue"        FLOAT,
   "DepDate"            DATE,
   "CurrentBV"          FLOAT,
   "IsDeleted"          NUMBER(1),
   "AssetId"            VARCHAR2(100),
   "SalvageYear"        NUMBER(12),
   "SalvageMonth"       NUMBER(12),
   constraint PK_BOOKHISTORY_TEMP primary key ("DepHistID")
);

create table "Books"  (
   "BookID"             NUMBER(12)                      not null,
   "DepCode"            NUMBER(12),
   "IsDeleted"          NUMBER(1),
   "Description"        VARCHAR2(200),
   "IsDefault"          NUMBER(1),
   "CompanyID"          NUMBER(12)                      not null,
   constraint PK_BOOKS primary key ("BookID")
);

create table "Brand"  (
   "AstBrandID"         NUMBER(12)                      not null,
   "AstBrandName"       VARCHAR2(100),
   "IsDeleted"          NUMBER(1),
   constraint PK_BRAND primary key ("AstBrandID")
);

create table "Category"  (
   "AstCatID"           VARCHAR2(25)                    not null,
   "AstCatDesc"         VARCHAR2(100),
   "IsDeleted"          NUMBER(1),
   ID1                  NUMBER(37)                      not null,
   "Code"               VARCHAR2(50),
   constraint PK_CATEGORY primary key ("AstCatID")
);

create table "CompGroups"  (
   "GrpID"              VARCHAR(25)                     not null,
   "GrpDesc"            VARCHAR2(100),
   "LvlID"              NUMBER(37),
   "isDeleted"          NUMBER(1),
   constraint PK_COMPGROUPS primary key ("GrpID")
);

create table "CompLvl"  (
   "LvlID"              NUMBER(37)                      not null,
   "LvlDesc"            VARCHAR2(200),
   "isDeleted"          NUMBER(1),
   constraint PK_COMPLVL primary key ("LvlID")
);

create table "Companies"  (
   "CompanyId"          NUMBER(12)                      not null,
   "CompanyCode"        VARCHAR2(50),
   "CompanyName"        VARCHAR2(50),
   "BarStructID"        INT,
   "IsDeleted"          NUMBER(1),
   constraint PK_COMPANIES primary key ("CompanyId")
);

create table "CompanyInfo"  (
   ID                   NUMBER(5)                       not null,
   "Name"               VARCHAR2(50),
   "Address"            VARCHAR2(200),
   "State"              VARCHAR2(50),
   "PCode"              VARCHAR2(50),
   "City"               VARCHAR2(50),
   "Country"            VARCHAR2(50),
   "Phone"              VARCHAR2(50),
   "Fax"                VARCHAR2(50),
   "Email"              VARCHAR2(50),
   "Image"              VARCHAR2(200),
   constraint PK_COMPANYINFO primary key (ID)
);

create table "CostCenter"  (
   "CostID"             VARCHAR2(50)                    not null,
   "CostNumber"         VARCHAR2(50),
   "CostName"           VARCHAR2(50),
   "IsDeleted"          NUMBER(1),
   constraint PK_COSTCENTER primary key ("CostID")
);

create table "Custodian"  (
   "CustodianID"        VARCHAR2(25)                    not null,
   "CustodianName"      VARCHAR2(100),
   "DesignationID"      NUMBER(12)                      not null,
   "CustodianPhone"     VARCHAR2(50),
   "CustodianEmail"     VARCHAR2(50),
   "CustodianFax"       VARCHAR2(50),
   "CustodianCell"      VARCHAR2(50),
   "CustodianAddress"   VARCHAR2(100),
   "IsDeleted"          NUMBER(1),
   "DeptId"             VARCHAR(100)                    not null,
   constraint PK_CUSTODIAN primary key ("CustodianID")
);

create table "DB Version"  (
   "Version"            VARCHAR2(50),
   "LastUpdate"         DATE
);

create table "DepLogs"  (
   "DepLogID"           NUMBER(12)                      not null,
   "updDate"            DATE,
   "DepPrdtype"         NUMBER(12),
   "totDepValue"        FLOAT,
   "totAstCount"        FLOAT,
   "totAstValue"        FLOAT,
   "UpdMonth"           NUMBER(12),
   "UpdYear"            NUMBER(12),
   "BookID"             NUMBER(12),
   constraint PK_DEPLOGS primary key ("DepLogID")
);

create table "DepPolicy"  (
   "CatDepID"           VARCHAR2(50)                    not null,
   "AstCatID"           VARCHAR2(25),
   "DepCode"            NUMBER(12),
   "SalvageValue"       FLOAT,
   "SalvageYear"        NUMBER(12),
   "SalvageMonth"       NUMBER(12),
   "SalvagePercent"     NUMBER(12),
   constraint PK_DEPPOLICY primary key ("CatDepID")
);

create table "DepPolicy_History"  (
   "BookID"             VARCHAR2(50)                    not null,
   "AstID"              VARCHAR2(25),
   "DepCode"            NUMBER(12),
   "SalvageValue"       FLOAT,
   "SalvageYear"        NUMBER(12),
   "LastBV"             FLOAT,
   "CurrentBV"          FLOAT,
   "BVUpdate"           DATE,
   "IsDelete"           NUMBER(1),
   "SalvageMonth"       NUMBER(12),
   "SalvagePercent"     NUMBER(12),
   constraint PK_DEPPOLICY_HISTORY primary key ()
);

create table "Department"  (
   "DeptId"             NUMBER(37)                      not null,
   "DeptName"           VARCHAR2(200),
   "IsDeleted"          NUMBER(1),
   constraint PK_DEPARTMENT primary key ("DeptId")
);

create table "Depreciation_Method"  (
   "DepCode"            NUMBER(12)                      not null,
   "DepDesc"            NVARCHAR2(50),
   "IsDeleted"          NUMBER(1)                       not null,
   constraint PK_DEPRECIATION_METHOD primary key ("DepCode")
);

create table "Designation"  (
   "DesignationID"      NUMBER(12)                      not null,
   "Description"        VARCHAR2(50),
   "IsDeleted"          NUMBER(1),
   constraint PK_DESIGNATION primary key ("DesignationID")
);

create table "Devices"  (
   "DeviceID"           NUMBER(12)                      not null,
   "DeviceDesc"         VARCHAR2(50),
   "ComType"            NUMBER(12),
   "DeviceIP"           VARCHAR2(50),
   "Status"             NUMBER(1),
   "IsDeleted"          NUMBER(1),
   constraint PK_DEVICES primary key ("DeviceID")
);

create table "Disposal_Method"  (
   "DispCode"           NUMBER(12)                      not null,
   "DispDesc"           VARCHAR2(50),
   "IsDeleted"          NUMBER(1),
   constraint PK_DISPOSAL_METHOD primary key ("DispCode")
);

create table "GLCodes"  (
   "GLCode"             NUMBER(12)                      not null,
   "GLDesc"             varchar2(50),
   "isDeleted"          number(1),
   constraint PK_GLCODES primary key ("GLCode")
);

create table "Insurer"  (
   "InsCode"            NUMBER(12)                      not null,
   "InsName"            VARCHAR2(25),
   "IsDeleted"          NUMBER(1),
   constraint PK_INSURER primary key ("InsCode")
);

create table "Location"  (
   "LocID"              VARCHAR2(255)                   not null,
   "LocDesc"            VARCHAR2(100),
   "IsDeleted"          NUMBER(1),
   ID1                  NUMBER(37)                      not null,
   "Code"               VARCHAR2(50),
   constraint PK_LOCATION primary key ("LocID")
);

create table "NonBarCodedTemp"  (
   "NonBCode"           NVARCHAR2(50)                   not null,
   "DeviceID"           NUMBER(12)                      not null,
   "LocID"              NVARCHAR2(255),
   "AstCatID"           NVARCHAR2(15),
   "AstDesc"            NVARCHAR2(255),
   constraint PK_NONBARCODEDTEMP primary key ("NonBCode")
);

create table "OrgHier"  (
   "HierCode"           VARCHAR2(25)                    not null,
   "GrpID"              VARCHAR2(25),
   "isWareHouse"        NUMBER(12),
   "isDeleted"          NUMBER(1),
   constraint PK_ORGHIER primary key ("HierCode")
);

create table "PODetails"  (
   "POItmID"            NUMBER(37)                      not null,
   "POCode"             NUMBER(37)                      not null,
   "ItemCode"           VARCHAR2(25)                    not null,
   "POItmDesc"          VARCHAR2(200),
   "POItmBaseCost"      FLOAT,
   "AddCharges"         FLOAT,
   "POItmQty"           NUMBER(37),
   "IsDeleted"          NUMBER(1),
   "IsTrans"            NUMBER(1),
   "Unit"               NUMBER(12),
   constraint PK_PODETAILS primary key ("POItmID")
);

create table "PurchaseOrder"  (
   "POCode"             NUMBER(37)                      not null,
   "SuppID"             VARCHAR2(25),
   "PODate"             DATE,
   "Quotation"          VARCHAR2(25),
   "Amount"             FLOAT,
   "AddCharges"         FLOAT,
   "ModeDelivery"       VARCHAR2(200),
   "Payterm"            VARCHAR2(200),
   "Remarks"            VARCHAR2(200),
   "Approvedby"         VARCHAR2(50),
   "Preparedby"         VARCHAR2(50),
   "POStatus"           NUMBER(12),
   "IsDeleted"          NUMBER(1),
   "IsTrans"            NUMBER(1),
   "ReferenceNo"        VARCHAR2(25),
   "CostID"             VARCHAR2(50),
   "RequestedBy"        VARCHAR2(25),
   "Discount"           NUMBER(37),
   "TermnCon"           VARCHAR2(200),
   constraint PK_PURCHASEORDER primary key ("POCode")
);

create table "Report_AssetCategory"  (
   "MainCat"            VARCHAR2(50),
   "Invest_OB"          float,
   "Invest_Add"         FLOAT,
   "Invest_Ded"         FLOAT,
   "Invest_CB"          FLOAT,
   "Depr_OB"            FLOAT,
   "Depr_Add"           FLOAT,
   "Depr_Ded"           FLOAT,
   "Depr_CB"            FLOAT,
   "NetValue"           FLOAT
);

create table "Report_AssetSubCategory"  (
   "MainCat"            VARCHAR2(50),
   "SubCat"             VARCHAR2(50),
   "Invest_OB"          float,
   "Invest_Add"         FLOAT,
   "Invest_Ded"         FLOAT,
   "Invest_CB"          FLOAT,
   "Depr_OB"            FLOAT,
   "Depr_Add"           FLOAT,
   "Depr_Ded"           FLOAT,
   "Depr_CB"            FLOAT,
   "NetValue"           FLOAT
);

create table "Report_CompanyAssets"  (
   "AstNum"             VARCHAR(25)                     not null,
   "AstID"              VARCHAR(25),
   "PurDate"            DATE,
   "OracleRef"          VARCHAR(25),
   "CompRef"            VARCHAR(25),
   "AstCat1"            VARCHAR(25),
   "AstCat2"            VARCHAR(25),
   "AstCat3"            VARCHAR(25),
   CC1                  VARCHAR(25),
   CC2                  VARCHAR(25),
   CC3                  VARCHAR(25),
   "CustodianID"        VARCHAR(25),
   "CustodianName"      VARCHAR(25),
   "Cost"               FLOAT,
   constraint PK_REPORT_COMPANYASSETS primary key ()
);

create table "Roles"  (
   "RoleID"             Number(12)                      not null,
   "Description"        varchar(50),
   "AppUser"            Number(7),
   "CompanyInfo"        Number(7),
   "Location"           Number(7),
   "DepPolicy"          Number(7),
   "Brands"             Number(7),
   "Designation"        Number(7),
   "Department"         Number(7),
   "Custodian"          Number(7),
   "Insurer"            Number(7),
   "DisposalMethod"     Number(7),
   "Supplier"           Number(7),
   "DepreciationMethod" Number(7),
   "InvSch"             Number(7),
   "AssetsBooks"        Number(7),
   "AddressBook"        Number(7),
   "AssetsCat"          Number(7),
   "CostCenter"         Number(7),
   "AssetItems"         Number(7),
   "Company"            Number(7),
   PO                   Number(7),
   "POApproval"         Number(7),
   "POTrans"            Number(7),
   "DeviceConfig"       Number(7),
   "SysConfig"          Number(7),
   "MasterReports"      Number(7),
   "RptCompany"         Number(7),
   "RptAnonym"          Number(7),
   "RptDepreciation"    Number(7),
   "RepExDepreciation"  Number(7),
   "rptBrand"           Number(7),
   "rptDesignation"     Number(7),
   "rptDepartment"      Number(7),
   "rptCustodian"       Number(7),
   "rptInsurer"         Number(7),
   "rptDispMethod"      Number(7),
   "rptSupplier"        Number(7),
   "rptDeprec"          Number(7),
   "rptInvsch"          Number(7),
   "rptAstBook"         Number(7),
   "rptAddress"         Number(7),
   "rptCC"              Number(7),
   "rptAssetItems"      Number(7),
   "DataSend"           Number(7),
   "DataRecieve"        Number(7),
   "AstAdmin"           Number(7),
   "AstDetail"          Number(7),
   "AstTrans"           Number(7),
   "AstSrc"             Number(7),
   "InterComTrans"      Number(7),
   "Anonym"             Number(7),
   "Roles"              Number(7),
   "IsDeleted"          Number(1),
   "AstDetails"         Number(7),
   "AstTaging"          Number(7),
   "AssetsLedger"       Number(7),
   "DisposedAssets"     Number(7),
   "SoldAssets"         Number(7),
   "DamagedAssets"      Number(7),
   "InterCoTrans"       Number(7),
   "Units"              Number(7),
   "DepMan"             Number(7),
   "BarStuct"           Number(7),
   "BarCodePolicy"      Number(7),
   "AssetCoding"        Number(7),
   "CompLvl"            Number(7),
   "CompGroup"          Number(7),
   "GroupHier"          Number(7),
   "AuditStatusReport"  Number(7),
   constraint PK_ROLES primary key ("RoleID")
);

create table "Supplier"  (
   "SuppID"             VARCHAR2(25)                    not null,
   "SuppName"           VARCHAR2(100),
   "SuppCell"           VARCHAR2(50),
   "SuppFax"            VARCHAR2(50),
   "SuppPhone"          VARCHAR2(50),
   "SuppEmail"          VARCHAR2(50),
   "IsDeleted"          NUMBER(1),
   constraint PK_SUPPLIER primary key ("SuppID")
);

create table "SysConfig"  (
   ID                   Number(12),
   "Depr_Period"        varchar2(50),
   "FinancialYearStart" varchar2(50),
   "FinyrStartDate"     DATE
);

create table "Units"  (
   "UnitID"             NUMBER(12)                      not null,
   "UnitDesc"           varchar2(50),
   "IsDeleted"          NUMBER(1),
   constraint PK_UNITS primary key ("UnitID")
);

alter table "AppUsers"
   add constraint FK_APPUSERS_REFERENCE_ROLES foreign key ("RoleID")
      references "Roles" ("RoleID");

alter table "AssetDetails"
   add constraint FK_ASSETDET_FK_ASSETD_ASSETS foreign key ("ItemCode")
      references "Assets" ("ItemCode");

alter table "AssetDetails"
   add constraint FK_ASSETDET_FK_ASSETD_COMPANIE foreign key ("CompanyID")
      references "Companies" ("CompanyId");

alter table "AssetDetails"
   add constraint FK_ASSETDET_FK_ASSETD_CUSTODIA foreign key ("CustodianID")
      references "Custodian" ("CustodianID");

alter table "AssetDetails"
   add constraint FK_ASSETDET_FK_ASSETD_LOCATION foreign key ("LocID")
      references "Location" ("LocID");

alter table "Assets"
   add constraint FK_ASSETS_FK_ASSETS_CATEGORY foreign key ("AstCatID")
      references "Category" ("AstCatID");

alter table "AstBooks"
   add constraint FK_ASTBOOKS_FK_ASTBOO_DEPRECIA foreign key ("DepCode")
      references "Depreciation_Method" ("DepCode");

alter table "Ast_Cust_history"
   add constraint FK_AST_CUST_FK_AST_CU_ASSETDET foreign key ("AstId")
      references "AssetDetails" ("AstID");

alter table "Ast_Cust_history"
   add constraint FK_AST_CUST_FK_AST_CU_CUSTODIA foreign key ("Fromcustodian")
      references "Custodian" ("CustodianID");

alter table "Ast_Cust_history"
   add constraint FK_AST_CUST_FK_AST_CU_CUSTODIA foreign key ("ToCustodian")
      references "Custodian" ("CustodianID");

alter table "Ast_History"
   add constraint FK_AST_HIST_FK_AST_HI_ASSETDET foreign key ("AstID")
      references "AssetDetails" ("AstID");

alter table "Ast_History"
   add constraint FK_AST_HIST_FK_AST_HI_LOCATION foreign key ("Fr_loc")
      references "Location" ("LocID");

alter table "Ast_History"
   add constraint FK_AST_HIST_FK_AST_HI_LOCATION foreign key ("To_Loc")
      references "Location" ("LocID");

alter table "BookHistory"
   add constraint FK_BOOKHIST_FK_BOOKHI_ASSETDET foreign key ("AssetId")
      references "AssetDetails" ("AstID");

alter table "BookHistory"
   add constraint FK_BOOKHIST_FK_BOOKHI_DEPRECIA foreign key ("DepCode")
      references "Depreciation_Method" ("DepCode");

alter table "Books"
   add constraint FK_BOOKS_FK_BOOKS__COMPANIE foreign key ("CompanyID")
      references "Companies" ("CompanyId");

alter table "Books"
   add constraint FK_BOOKS_FK_BOOKS__DEPRECIA foreign key ("DepCode")
      references "Depreciation_Method" ("DepCode");

alter table "Custodian"
   add constraint FK_CUSTODIA_FK_CUSTOD_DEPARTME foreign key ("DeptId")
      references "Department" ("DeptId");

alter table "Custodian"
   add constraint FK_CUSTODIA_FK_CUSTOD_DESIGNAT foreign key ("DesignationID")
      references "Designation" ("DesignationID");

alter table "DepPolicy"
   add constraint FK_DEPPOLIC_FK_DEPPOL_CATEGORY foreign key ("AstCatID")
      references "Category" ("AstCatID");

alter table "DepPolicy"
   add constraint FK_DEPPOLIC_FK_DEPPOL_DEPRECIA foreign key ("DepCode")
      references "Depreciation_Method" ("DepCode");

alter table "PODetails"
   add constraint FK_PODETAIL_FK_PODETA_ASSETS foreign key ("ItemCode")
      references "Assets" ("ItemCode");

alter table "PODetails"
   add constraint FK_PODETAIL_FK_PODETA_PURCHASE foreign key ("POCode")
      references "PurchaseOrder" ("POCode");

alter table "PurchaseOrder"
   add constraint FK_PURCHASE_FK_PURCHA_COSTCENT foreign key ("CostID")
      references "CostCenter" ("CostID");

alter table "PurchaseOrder"
   add constraint FK_PURCHASE_FK_PURCHA_SUPPLIER foreign key ("SuppID")
      references "Supplier" ("SuppID");

