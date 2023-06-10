CREATE PROCEDURE [dbo].[Update_List_Of_Accounts]
		@Acc_List [Account_List_Type] READONLY
AS
UPDATE a
   SET a.[Name] = l.Name
      ,a.[Code] = l.Code
      ,a.[Parent_ID] = l.Parent_ID
	  FROM @Acc_List l
	  JOIN [dbo].Accounts a
	  ON a.ID = l.ID
RETURN 0
