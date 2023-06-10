CREATE PROCEDURE [dbo].[Update_Product_Units]
	@productUnits Add_Product_Unit_Type READONLY
AS
	UPDATE p 
   SET p.[Pro_ID] = pu.Pro_ID
      ,p.[Unit_ID] = pu.Unit_ID
      ,p.[Factor] = pu.Factor
      ,p.[Buy_Price] = pu.Buy_Price
      ,p.[Sell_Price] = pu.Sell_Price
      ,p.[Sell_Discount] = pu.Sell_Discount
      ,p.[BarCode] = pu.BarCode
	  FROM @productUnits pu
	  JOIN 	 [dbo].[Product_Unit] p
	  ON pu.ID = p.ID
RETURN 0
