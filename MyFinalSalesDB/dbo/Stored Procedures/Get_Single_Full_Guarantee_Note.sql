CREATE PROCEDURE [dbo].[Get_Single_Full_Guarantee_Note]
 @guaraID [dbo].[IDs_List_Type] READONLY
AS

SELECT * FROM dbo.[FullGuaranteedView]
  WHERE [ID] IN(
                      SELECT [ID] FROM @guaraID
               )
RETURN 0
