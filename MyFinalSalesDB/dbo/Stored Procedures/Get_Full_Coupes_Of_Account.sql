CREATE PROCEDURE [dbo].[Get_Full_Coupes_Of_Account]
	@accountID [dbo].[IDs_List_Type] READONLY
AS
      SELECT * FROM dbo.[FullCoupesOfAccountView]
        WHERE [Account_ID] IN (
                                    SELECT [ID] FROM @accountID
                                  )
RETURN 0
