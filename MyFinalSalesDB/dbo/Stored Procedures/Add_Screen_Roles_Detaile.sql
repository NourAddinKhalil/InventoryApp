CREATE PROCEDURE [dbo].[Add_Screen_Roles_Detaile] 
(
	@Detial Add_Users_Screen_Access_Type readonly
)
AS
	INSERT INTO [dbo].[Screen_Roles_Detail]
          (
			[Screen_Role_ID]
           ,[Screen_ID]
           ,[Can_Show]
           ,[Can_Open]
           ,[Can_Add]
           ,[Can_Edit]
           ,[Can_Delete]
           ,[Can_Print]
		  )
    SELECT 
	        [Screen_Role_ID] 
           ,[Screen_ID]
		   ,[Can_Show]
		   ,[Can_Open]
		   ,[Can_Add]
		   ,[Can_Edit]
		   ,[Can_Delete]
		   ,[Can_Print]
		   FROM @Detial;
RETURN 0