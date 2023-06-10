CREATE PROCEDURE [dbo].[Update_User_Profile_Prope]
	@prop Add_User_Profile_Property_Type READONLY
AS
UPDATE up
   SET up.[Profile_ID] = p.Profile_ID 
      ,up.[Prope_Name] = p.Prope_Name 
      ,up.[Prope_Value] =p.Prope_Value
    From @prop p
	JOIN [dbo].[User_Profile_Property] up
	ON p.ID = up.ID
RETURN 0
