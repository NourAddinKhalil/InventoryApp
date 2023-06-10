Create PROCEDURE [Add_List_Of_Pro_Movement]
	@proMove [Pro_Movement_List_Type] Readonly
AS
	Insert Into dbo.[Pro_Store_Movement]
				(
					 [Product_ID]
					,[Insert_Date] 
					,[Source_ID] 
					,[Source_Type] 
					,[Notes] 
					,[Import_Qty] 
					,[Export_Qty] 
					,[Store_ID] 
					,[Cost_Value]
					,[User_ID]
				)
				Select 
					 [Product_ID]
					,[Insert_Date] 
					,[Source_ID] 
					,[Source_Type] 
					,[Notes] 
					,[Import_Qty] 
					,[Export_Qty] 
					,[Store_ID] 
					,[Cost_Value] 
					,[User_ID]
					From @proMove
RETURN 0
