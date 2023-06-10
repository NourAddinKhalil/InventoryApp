CREATE TABLE [dbo].[Transfer_Bal_Details] (
    [ID]          INT        IDENTITY (1, 1) NOT NULL,
    [Transfer_ID] INT        NOT NULL,
    [Product_ID]  INT        NOT NULL,
    [Unit_ID]     INT        NOT NULL,
    [Qty]         FLOAT (53) NOT NULL,
    [Price]       FLOAT (53) NOT NULL,
    [Total_Price] FLOAT (53) NOT NULL,
    CONSTRAINT [PK_Transfer_Bal_Details] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Transfer_Bal_Details_Transfer_Bal_BetweenStores] FOREIGN KEY ([Transfer_ID]) REFERENCES [dbo].[Transfer_Bal_BetweenStores] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);

