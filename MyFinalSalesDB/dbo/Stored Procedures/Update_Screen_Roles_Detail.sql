CREATE PROCEDURE [dbo].[Update_Screen_Roles_Detail]
	@Detial Add_Users_Screen_Access_Type readonly
AS
	UPDATE sr
   SET sr.[Screen_Role_ID] = d.Screen_Role_ID
      ,sr.[Screen_ID] = d.Screen_ID
      ,sr.[Can_Show] = d.Can_Show
      ,sr.[Can_Open] = d.Can_Open
      ,sr.[Can_Add] = d.Can_Add
      ,sr.[Can_Edit] = d.Can_Edit
      ,sr.[Can_Delete] = d.Can_Delete
      ,sr.[Can_Print] = d.Can_Print
	  FROM @Detial d
	  JOIN [dbo].[Screen_Roles_Detail] sr
	  ON sr.ID = d.ID
RETURN 0
