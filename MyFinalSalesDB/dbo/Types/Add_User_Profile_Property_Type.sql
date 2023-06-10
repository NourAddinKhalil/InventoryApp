CREATE TYPE [dbo].[Add_User_Profile_Property_Type] AS TABLE
(
	Profile_ID INT NOT NULL,
	Prope_Name NVARCHAR(500) NOT NULL,
	Prope_Value VARBINARY(MAX) NOT Null
)
