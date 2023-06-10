CREATE PROCEDURE [dbo].[Update_List_Of_Opening_Accounts]
		@coupes Account_Coupes_List_Type READONLY , @note NVARCHAR(200)
AS
	UPDATE ca
   SET ca.[Code] = c.Code
      ,ca.[Account_ID] = c.Account_ID
      ,ca.[Debit] = c.Debit
      ,ca.[Credit] = c.Credit
      ,ca.[Insert_Date] = c.Insert_Date
      ,ca.[Source_Type] = c.Source_Type
      ,ca.[Source_ID] = c.Source_ID
      ,ca.[Notes] = c.Notes
      ,ca.[Move_Name] = c.Move_Name
      ,ca.[User_ID] = c.User_ID
  FROM @coupes c
	  JOIN [dbo].[Coupes_Of_Account] ca 
	  ON c.Account_ID = ca.Account_ID
	  AND c.Move_Name = ca.Move_Name
	  AND c.Notes = @note
RETURN 0
