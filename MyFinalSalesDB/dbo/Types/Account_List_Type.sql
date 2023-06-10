CREATE TYPE [dbo].[Account_List_Type] AS TABLE
(
	[ID] [int] ,
	[Name] nvarchar(70) ,
	[Code] nvarchar(25) ,
	[Parent_ID] int 
)
