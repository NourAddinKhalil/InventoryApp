CREATE FUNCTION [dbo].[Get_The_Sum_Of_The_Already_Paid_Invoices]
(
	@invoiceID int
)
RETURNS INT
AS
BEGIN
     Declare @theSum float;
     Set @theSum = ISNULL((Select SUM([Amount]) From [FinalSalesDB].[dbo].[Guaranteed_Notes] 
     Where [Invoice_ID] = @invoiceID) ,0);

	RETURN @theSum
END
