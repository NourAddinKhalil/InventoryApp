CREATE TABLE [dbo].[Product] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [Code]             NVARCHAR (25)  NOT NULL,
    [Name]             NVARCHAR (70)  NOT NULL,
    [Type]             TINYINT        NOT NULL,
    [Is_Active]        BIT            NOT NULL,
    [Image]            IMAGE          NULL,
    [Cate_ID]          INT            NOT NULL,
    [Cost_Calc_Method] TINYINT        NOT NULL,
    [Discribtion]      NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Product_Category] FOREIGN KEY ([Cate_ID]) REFERENCES [dbo].[Category] ([ID])
);

