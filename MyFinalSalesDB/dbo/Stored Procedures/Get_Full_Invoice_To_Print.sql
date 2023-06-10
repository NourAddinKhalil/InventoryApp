CREATE PROCEDURE [dbo].[Get_Full_Invoice_To_Print]
	@invIDs [dbo].[IDs_List_Type] READONLY
AS

SELECT * FROM  dbo.[FullInvoiceView]
  WHERE [ID] IN (
                        SELECT ID FROM @invIDs
                     )
RETURN 0
