CREATE TABLE [dbo].[Product_Unit] (
    [ID]            INT           IDENTITY (1, 1) NOT NULL,
    [Pro_ID]        INT           NOT NULL,
    [Unit_ID]       INT           NOT NULL,
    [Factor]        FLOAT (53)    NOT NULL,
    [Buy_Price]     FLOAT (53)    NOT NULL,
    [Sell_Price]    FLOAT (53)    NOT NULL,
    [Sell_Discount] FLOAT (53)    NOT NULL,
    [BarCode]       NVARCHAR (25) NOT NULL,
    CONSTRAINT [PK_Product_Unit] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Product_Unit_Product] FOREIGN KEY ([Pro_ID]) REFERENCES [dbo].[Product] ([ID]),
    CONSTRAINT [FK_Product_Unit_Unit] FOREIGN KEY ([Unit_ID]) REFERENCES [dbo].[Unit] ([ID])
);

