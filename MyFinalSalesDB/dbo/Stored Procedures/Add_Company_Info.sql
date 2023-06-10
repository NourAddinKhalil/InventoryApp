CREATE PROCEDURE [dbo].[Add_Company_Info]
	@name NVARCHAR(70),
	@phone NVARCHAR(70),
	@mobile NVARCHAR (50),
    @address NVARCHAR (50),
	@logo Image
AS
	INSERT INTO Company_Info 
	values(@name,@phone,@mobile,@address,@logo);
RETURN 0
