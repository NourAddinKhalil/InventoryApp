CREATE TYPE [dbo].[Opening_Destructor_Details_List] AS TABLE
(
	[Open_Dest_ID] INT NOT NULL,
	[Product_ID] INT NOT NULL,
	[Unit_ID] INT NOT NULL,
	[Qty] [float] NOT NULL,
	[Price] [float] NOT NULL,
	[Total_Price] [float] NOT NULL
)
