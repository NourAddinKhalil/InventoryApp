CREATE TABLE [dbo].[Opening_Destructor_Details] (
    [ID]           INT        IDENTITY (1, 1) NOT NULL,
    [Open_Dest_ID] INT        NOT NULL,
    [Product_ID]   INT        NOT NULL,
    [Unit_ID]      INT        NOT NULL,
    [Qty]          FLOAT (53) NOT NULL,
    [Price]        FLOAT (53) NOT NULL,
    [Total_Price]  FLOAT (53) NOT NULL,
    CONSTRAINT [PK_Opening_Destructor_Details] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Opening_Destructor_Details_Opening_And_Destuctor_Product] FOREIGN KEY ([Open_Dest_ID]) REFERENCES [dbo].[Opening_And_Destuctor_Product] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);

