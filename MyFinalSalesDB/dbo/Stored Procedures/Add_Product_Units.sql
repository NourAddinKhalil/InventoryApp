CREATE PROCEDURE [dbo].[Add_Product_Units]
	@productUnits Add_Product_Unit_Type READONLY
AS
	INSERT INTO [dbo].[Product_Unit]
           (
		    [Pro_ID]
           ,[Unit_ID]
           ,[Factor]
           ,[Buy_Price]
           ,[Sell_Price]
           ,[Sell_Discount]
           ,[BarCode]
		   )
     SELECT 
		    [Pro_ID]
           ,[Unit_ID]
           ,[Factor]
           ,[Buy_Price]
           ,[Sell_Price]
           ,[Sell_Discount]
           ,[BarCode]
		   FROM @productUnits;
RETURN 0
