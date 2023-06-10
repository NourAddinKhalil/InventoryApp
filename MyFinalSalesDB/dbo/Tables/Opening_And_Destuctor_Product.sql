CREATE TABLE [dbo].[Opening_And_Destuctor_Product] (
    [ID]       INT            IDENTITY (1, 1) NOT NULL,
    [Code]     NVARCHAR (25)  NOT NULL,
    [Type]     TINYINT        NOT NULL,
    [Store_ID] INT            NOT NULL,
    [Date]     DATETIME       NOT NULL,
    [Total]    FLOAT (53)     NOT NULL,
    [Notes]    NVARCHAR (150) NULL,
    CONSTRAINT [PK_Opening_And_Destuctor_Product] PRIMARY KEY CLUSTERED ([ID] ASC)
);

