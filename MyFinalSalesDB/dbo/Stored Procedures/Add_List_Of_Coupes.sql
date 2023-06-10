Create PROCEDURE [Add_List_Of_Coupes]
	@coupes Account_Coupes_List_Type readonly
AS
		INSERT INTO [dbo].[Coupes_Of_Account]
           ([Code]
           ,[Account_ID]
           ,[Debit]
           ,[Credit]
           ,[Insert_Date]
           ,[Source_Type]
           ,[Source_ID]
           ,[Notes]
           ,[Move_Name]
           ,[User_ID])
	 SELECT 
			[Code]
           ,[Account_ID]
           ,[Debit]
           ,[Credit]
           ,[Insert_Date]
           ,[Source_Type]
           ,[Source_ID]
           ,[Notes]
           ,[Move_Name]
           ,[User_ID]
		   FROM @coupes
RETURN 0
