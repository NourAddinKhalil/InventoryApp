CREATE TABLE [dbo].[Invoice_Details] (
    [ID]                 INT        IDENTITY (1, 1) NOT NULL,
    [Invoice_ID]         INT        NOT NULL,
    [Product_ID]         INT        NOT NULL,
    [Unit_ID]            INT        NOT NULL,
    [Qty]                FLOAT (53) NOT NULL,
    [Price]              FLOAT (53) NOT NULL,
    [Total_Price]        FLOAT (53) NOT NULL,
    [Cost]               FLOAT (53) NOT NULL,
    [Total_Cost]         FLOAT (53) NOT NULL,
    [Discount_Val]       FLOAT (53) NOT NULL,
    [Discount_Prece]     FLOAT (53) NOT NULL,
    [Store_ID]           INT        NOT NULL,
    [Source_Back_Row_ID] INT        NULL,
    CONSTRAINT [PK_Invoice_Details] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Invoice_Details_Invoice] FOREIGN KEY ([Invoice_ID]) REFERENCES [dbo].[Invoice] ([ID]),
    CONSTRAINT [FK_Invoice_Details_Product] FOREIGN KEY ([Product_ID]) REFERENCES [dbo].[Product] ([ID]),
    CONSTRAINT [FK_Invoice_Details_Store] FOREIGN KEY ([Store_ID]) REFERENCES [dbo].[Store] ([ID])
);

