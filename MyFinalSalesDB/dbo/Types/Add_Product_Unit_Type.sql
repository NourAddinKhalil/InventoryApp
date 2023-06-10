CREATE TYPE [dbo].[Add_Product_Unit_Type] AS TABLE
(
    [ID] INT ,
	[Pro_ID] INT NOT NULL,
	[Unit_ID] INT NOT NULL,
	[Factor] FlOAT NOT NULL,
	[Buy_Price] FlOAT NOT NULL,
	[Sell_Price] FlOAT NOT NULL,
	[Sell_Discount] FlOAT NOT NULL,
	[BarCode] NVARCHAR (25) NOT NULL
)
