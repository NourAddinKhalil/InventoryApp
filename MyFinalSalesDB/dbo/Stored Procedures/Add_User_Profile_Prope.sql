CREATE PROCEDURE [dbo].[Add_User_Profile_Prope]
	@prop Add_User_Profile_Property_Type READONLY
AS
INSERT INTO [dbo].[User_Profile_Property]
           ([Profile_ID]
           ,[Prope_Name]
           ,[Prope_Value])
		   SELECT 
		    [Profile_ID]
		   ,[Prope_Name]
		   ,[Prope_Value]
		   FROM @prop
RETURN 0
