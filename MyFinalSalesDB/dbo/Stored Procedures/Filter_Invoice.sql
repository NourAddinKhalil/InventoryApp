CREATE PROCEDURE [dbo].[Filter_Invoice]
	@id int = 0,
	@fromDate Date  = '2010-01-01',
	@toDate Date  = GetDate,
	@invType int = 0,
	@perID int = 0,
	@perType int = 0,
	@strID int = 0,
	@drID int = 0,
	@usID int = 0
AS
   IF @id != 0
		BEGIN
			SELECT * FROM [dbo].[MaxInvoiceView]
			WHERE [ID] = @id AND [Date] >= @fromDate AND [Date] <= @toDate
		END
   Else IF @invType != 0 And @perID != 0 And @perType != 0 And @strID != 0 And @drID != 0 And @usID != 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE [Type] = @invType
	           And [Person_ID] = @perID 
	           And [Person_Type] = @perType
	           And [Store_ID] = @strID
	           And [Drawer_ID] = @drID
	           And [User_ID] = @usID
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
   Else IF @invType != 0 And @perID = 0 And @perType != 0 And @strID != 0 And @drID != 0 And @usID != 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE [Type] = @invType
	           And [Person_Type] = @perType
	           And [Store_ID] = @strID
	           And [Drawer_ID] = @drID
	           And [User_ID] = @usID
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
   Else IF @invType != 0 And @perID != 0 And @perType != 0 And @strID = 0 And @drID != 0 And @usID != 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE [Type] = @invType
	           And [Person_ID] = @perID 
	           And [Person_Type] = @perType
	           And [Drawer_ID] = @drID
	           And [User_ID] = @usID
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
   Else IF @invType != 0 And @perID != 0 And @perType != 0 And @strID != 0 And @drID = 0 And @usID != 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE [Type] = @invType
	           And [Person_ID] = @perID 
	           And [Person_Type] = @perType
	           And [Store_ID] = @strID
	           And [User_ID] = @usID
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
   Else IF @invType != 0 And @perID != 0 And @perType != 0 And @strID != 0 And @drID != 0 And @usID = 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE [Type] = @invType
	           And [Person_ID] = @perID 
	           And [Person_Type] = @perType
	           And [Store_ID] = @strID
	           And [Drawer_ID] = @drID
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
   Else IF @invType != 0 And @perID = 0 And @perType = 0 And @strID != 0 And @drID != 0 And @usID != 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE [Type] = @invType
	           And [Store_ID] = @strID
	           And [Drawer_ID] = @drID
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
   Else IF @invType != 0 And @perID = 0 And @perType = 0 And @strID = 0 And @drID = 0 And @usID = 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE [Type] = @invType
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
		-----------------------------------------------------------------
   Else IF @invType = 0 And @perID != 0 And @perType != 0 And @strID != 0 And @drID != 0 And @usID != 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE  [Person_Type] = @perType
	           And [Store_ID] = @strID
	           And [Drawer_ID] = @drID
	           And [User_ID] = @usID
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
   Else IF @invType!= 0 And @perID != 0 And @perType != 0 And @strID = 0 And @drID = 0 And @usID = 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE [Person_Type] = @perType
	           And [Person_ID] = @perID
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
  -----------------------------------------------------------------------
  Else IF @invType = 0 And @perID = 0 And @perType != 0 And @strID != 0 And @drID != 0 And @usID != 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE [Person_Type] = @perType 
	           And [Store_ID] = @strID
	           And [Drawer_ID] = @drID
	           And [User_ID] = @usID
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
   Else IF @invType != 0 And @perID = 0 And @perType != 0 And @strID = 0 And @drID != 0 And @usID != 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE [Type] = @invType
	           And [Person_Type] = @perType
	           And [Drawer_ID] = @drID
	           And [User_ID] = @usID
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
   Else IF @invType != 0 And @perID = 0 And @perType != 0 And @strID != 0 And @drID = 0 And @usID != 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE [Type] = @invType
	           And [Person_Type] = @perType
	           And [Store_ID] = @strID
	           And [User_ID] = @usID
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
   Else IF @invType != 0 And @perID = 0 And @perType != 0 And @strID != 0 And @drID != 0 And @usID = 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE [Type] = @invType
	           And [Person_Type] = @perType
	           And [Store_ID] = @strID
	           And [Drawer_ID] = @drID
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
   Else IF @invType = 0 And @perID = 0 And @perType != 0 And @strID = 0 And @drID = 0 And @usID = 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE [Person_Type] = @perType
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
	-----------------------------------------------------------------------
   Else IF @invType = 0 And @perID != 0 And @perType != 0 And @strID != 0 And @drID != 0 And @usID != 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE [Person_Type] = @perType
			   And [Person_ID] = @perID
	           And [Store_ID] = @strID
	           And [Drawer_ID] = @drID
	           And [User_ID] = @usID
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
   Else IF @invType != 0 And @perID = 0 And @perType != 0 And @strID != 0 And @drID != 0 And @usID != 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE [Type] = @invType
	           And [Person_Type] = @perType
			   And [Store_ID] = @strID
	           And [Drawer_ID] = @drID
	           And [User_ID] = @usID
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
   Else IF @invType != 0 And @perID = 0 And @perType = 0 And @strID != 0 And @drID != 0 And @usID != 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE [Type] = @invType
	           And [Store_ID] = @strID
			   And [Drawer_ID] = @drID
	           And [User_ID] = @usID
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
   Else IF @invType != 0 And @perID != 0 And @perType != 0 And @strID != 0 And @drID = 0 And @usID != 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE [Type] = @invType
	           And [Person_Type] = @perType
			   And [Person_ID] = @perID
	           And [Store_ID] = @strID
			   And [User_ID] = @usID
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
   Else IF @invType = 0 And @perID != 0 And @perType != 0 And @strID != 0 And @drID != 0 And @usID = 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE [Person_Type] = @perType
			 And [Person_Type] = @perType
			   And [Person_ID] = @perID
	           And [Store_ID] = @strID
			   And [Drawer_ID] = @drID
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
   Else IF @invType = 0 And @perID = 0 And @perType = 0 And @strID != 0 And @drID = 0 And @usID = 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE [Store_ID] = @strID
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
	-----------------------------------------------------------------------
   Else IF @invType != 0 And @perID != 0 And @perType != 0 And @strID = 0 And @drID != 0 And @usID != 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE [Type] = @invType
	           And [Person_Type] = @perType
			   And [Person_ID] = @perID
	           And [Drawer_ID] = @drID
			   And [User_ID] = @usID
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
   Else IF @invType != 0 And @perID != 0 And @perType != 0 And @strID != 0 And @drID != 0 And @usID = 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE [Type] = @invType
			   And [Person_Type] = @perType
			   And [Person_Type] = @perType
			   And [Person_ID] = @perID
	           And [Store_ID] = @strID
			   And [Drawer_ID] = @drID
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
   Else IF @invType = 0 And @perID = 0 And @perType = 0 And @strID = 0 And @drID != 0 And @usID = 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE [Drawer_ID] = @drID
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
	-----------------------------------------------------------------------
   Else IF @invType != 0 And @perID = 0 And @perType != 0 And @strID != 0 And @drID != 0 And @usID != 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE [Type] = @invType
	           And [Person_Type] = @perType
			   And [Store_ID] = @strID
	           And [Drawer_ID] = @drID
			   And [User_ID] = @usID
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
   Else IF @invType != 0 And @perID != 0 And @perType != 0 And @strID = 0 And @drID != 0 And @usID != 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE [Type] = @invType
			   And [Person_Type] = @perType
			   And [Person_Type] = @perType
			   And [Person_ID] = @perID
	           And [User_ID] = @usID
			   And [Drawer_ID] = @drID
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
   Else IF @invType != 0 And @perID != 0 And @perType != 0 And @strID != 0 And @drID = 0 And @usID != 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE [Type] = @invType
			   And [Person_Type] = @perType
			   And [Person_Type] = @perType
			   And [Person_ID] = @perID
	           And [User_ID] = @usID
			   And [Store_ID] = @strID
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
   Else IF @invType = 0 And @perID = 0 And @perType = 0 And @strID = 0 And @drID = 0 And @usID != 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
	        WHERE [User_ID] = @usID
			   And [Date] >= @fromDate And [Date] <= @toDate
		END
		-----------------------------------------------------------------------
   Else IF @invType = 0 And @perID = 0 And @perType = 0 And @strID = 0 And @drID = 0 And @usID = 0
		BEGIN 
	       	SELECT * FROM [dbo].[MaxInvoiceView]
			   WHERE [Date] >= @fromDate And [Date] <= @toDate
		END
RETURN 0
