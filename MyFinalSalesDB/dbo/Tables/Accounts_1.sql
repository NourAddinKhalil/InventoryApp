CREATE TABLE [dbo].[Accounts] (
    [ID]        INT           IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (70) NOT NULL,
    [Code]      NVARCHAR (25) NOT NULL,
    [Parent_ID] INT           NULL DEFAULT 0,
    CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED ([ID] ASC)
);

