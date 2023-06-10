CREATE TYPE [dbo].[Add_Users_Screen_Access_Type] 
AS TABLE
(
    ID INT ,
	Screen_Role_ID INT ,
	Screen_ID INT ,
	Can_Show BIT ,
	Can_Open BIT ,
	Can_Add BIT ,
	Can_Edit BIT ,
	Can_Delete BIT ,
	Can_Print BIT 
)
