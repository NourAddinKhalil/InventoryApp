CREATE TABLE [dbo].[Transfer_Bal_BetweenStores] (
    [ID]         INT            IDENTITY (1, 1) NOT NULL,
    [Code]       NVARCHAR (25)  NOT NULL,
    [From_Store] INT            NOT NULL,
    [To_Store]   INT            NOT NULL,
    [Date]       DATETIME       NOT NULL,
    [Total]      FLOAT (53)     NOT NULL,
    [Notes]      NVARCHAR (150) NULL,
    CONSTRAINT [PK_Transfer_Bal_BetweenStores] PRIMARY KEY CLUSTERED ([ID] ASC)
);

