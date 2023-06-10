CREATE TYPE [dbo].[Account_Coupes_List_Type] AS TABLE
(
	[Code] NVARCHAR(25) ,
	[Account_ID] INT ,
	[Debit] FLOAT ,
	[Credit] FLOAT ,
	[Insert_Date] DATETIME ,
	[Source_Type] TINYINT ,
	[Source_ID] INT ,
	[Notes] NVARCHAR (2000) ,
	[Move_Name] tinyint ,
	[User_ID] int 
)
