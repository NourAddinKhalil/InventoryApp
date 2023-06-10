CREATE TABLE [dbo].[Company_Info] (
    [ID]      INT           IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (70) NOT NULL,
    [Phone]   NVARCHAR (50) NULL,
    [Mobile]  NVARCHAR (50) NULL,
    [Address] NVARCHAR (50) NULL,
    [Logo]    IMAGE         NULL,
    CONSTRAINT [PK_Company_Info] PRIMARY KEY CLUSTERED ([ID] ASC)
);

