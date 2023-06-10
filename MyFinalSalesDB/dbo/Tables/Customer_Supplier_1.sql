CREATE TABLE [dbo].[Customer_Supplier] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (70) NOT NULL,
    [Phone]      NVARCHAR (40) NULL,
    [Mobile]     NVARCHAR (40) NULL,
    [Address]    NVARCHAR (80) NULL,
    [Account_ID] INT           NOT NULL,
    [Type]       TINYINT       NOT NULL,
    [Max_Credit] FLOAT (53)    NULL,
    [Opening_Balance] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_Customer_Supplier] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Customer_Supplier_Accounts] FOREIGN KEY ([Account_ID]) REFERENCES [dbo].[Accounts] ([ID]) ON UPDATE CASCADE
);



