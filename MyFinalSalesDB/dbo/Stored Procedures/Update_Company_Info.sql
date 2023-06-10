CREATE PROCEDURE [dbo].[Update_Company_Info]
	@id INT,
	@name NVARCHAR(70),
	@phone NVARCHAR(70),
	@mobile NVARCHAR (50),
    @address NVARCHAR (50),
	@logo Image
AS
	Update Company_Info 
	SET dbo.Company_Info.Name = @name,
	    dbo.Company_Info.Phone = @phone,
		dbo.Company_Info.Mobile = @mobile,
		dbo.Company_Info.Address = @address,
		dbo.Company_Info.Logo = @logo
		WHERE dbo.Company_Info.ID = @id;
RETURN 0
