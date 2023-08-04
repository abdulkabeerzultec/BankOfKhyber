ALTER TABLE Assets ADD
	Warranty NUMBER(7) NULL

ALTER TABLE AssetDetails ADD
	Warranty NUMBER(7) NULL

update assets set warranty =0

update AssetDetails set warranty =0



--Fix the roles for some menu items.

ALTER TABLE Roles ADD
	OfflineMachine NUMBER(7) NULL,
	BackendInventory NUMBER(7) NULL,
	Custom1 NUMBER(7) NULL,
	Custom2 NUMBER(7) NULL,
	Custom3 NUMBER(7) NULL,
	Custom4 NUMBER(7) NULL,
	Custom5 NUMBER(7) NULL,
	Custom6 NUMBER(7) NULL,
	Custom7 NUMBER(7) NULL,
	Custom8 NUMBER(7) NULL,
	Custom9 NUMBER(7) NULL,
	Custom10 NUMBER(7) NULL,
	Custom11 NUMBER(7) NULL,
	Custom12 NUMBER(7) NULL,
	Custom13 NUMBER(7) NULL,
	Custom14 NUMBER(7) NULL,
	Custom15 NUMBER(7) NULL

	update Roles set  OfflineMachine = 0,BackendInventory =0,Custom1 =0,Custom2 =0,Custom3 =0,Custom4 =0,Custom5 =0,Custom6 =0,Custom7 =0,Custom8 =0,Custom9 =0,Custom10 =0,Custom11 =0,Custom12 =0,Custom13 =0,Custom14 =0,Custom15 = 0 where roleid <> 1

	update Roles set  OfflineMachine = 1,BackendInventory =1,Custom1 =1,Custom2 =1,Custom3 =1,Custom4 =1,Custom5 =1,Custom6 =1,Custom7 =1,Custom8 =1,Custom9 =1,Custom10 =1,Custom11 =1,Custom12 =1,Custom13 =1,Custom14 =1,Custom15 = 1 where roleid  = 1
	commit;
	