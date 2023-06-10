CREATE PROCEDURE [dbo].[Update_Invoice]
	 @type TINYINT,
	 @code nvarchar(50),
	 @perType TINYINT,
	 @perID INT,
	 @date DATETIME,
	 @delDate DATETIME,
	 @strID INT,
	 @notes nvarchar(200),
	 @isposted Bit,
	 @postDate DATETIME,
	 @tot FLOAT,
     @disv Float,
	 @disp Float,
	 @taxv Float,
	 @taxp Float,
	 @expen Float,
	 @net Float,
	 @paid Float,
	 @drawer Int,
	 @remain Float,
	 @shipAdd nvarchar(100),
	 @invBack Int,
	 @userID INT,
	 @id INT
AS
	UPDATE [dbo].[Invoice]
   SET 
       [Type] =@type
      ,[Code] = @code
      ,[Person_Type] = @perType
      ,[Person_ID] = @perID
      ,[Date] = @date
      ,[Delivalry_Date] = @delDate
      ,[Store_ID] = @strID
      ,[Notes] = @notes
      ,[Is_Posted_To_Store] = @isposted
      ,[Post_Date] = @postDate
      ,[Total] = @tot
      ,[Discount_Val] = @disv
      ,[Discount_Perec] = @disp
      ,[Tax_Val] = @taxv
      ,[Tax_Perce] = @taxp
      ,[Expences] = @expen
      ,[Net] = @net
      ,[Paid] = @paid
      ,[Drawer_ID] = @drawer
      ,[Remaining] = @remain
      ,[Shipping_Address] = @shipAdd
      ,[Invoice_Back_ID] = @invBack
      ,[User_ID] = @userID
 WHERE ID = @id
RETURN 0
