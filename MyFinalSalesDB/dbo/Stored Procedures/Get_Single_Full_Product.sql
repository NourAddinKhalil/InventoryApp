CREATE PROCEDURE [dbo].[Get_Single_Full_Product]
	@proID int 
AS
	SELECT  * FROM dbo.[FullProductView]
     Where [ID] = @proID
RETURN 0
