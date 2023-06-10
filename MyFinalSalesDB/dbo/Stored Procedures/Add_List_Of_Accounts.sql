CREATE PROCEDURE [dbo].[Add_List_Of_Accounts]
	@Acc_List [Account_List_Type] READONLY
AS
INSERT INTO [dbo].[Accounts]
           ([Name]
           ,[Code]
           ,[Parent_ID])
           SELECT 
		    [Name]
           ,[Code]
           ,[Parent_ID]
		   FROM @Acc_List
RETURN 0
