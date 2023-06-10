CREATE TABLE [dbo].[Drawer] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (70) NOT NULL,
    [Account_ID] INT           NOT NULL,
    CONSTRAINT [PK_Drawer] PRIMARY KEY CLUSTERED ([ID] ASC)
);

