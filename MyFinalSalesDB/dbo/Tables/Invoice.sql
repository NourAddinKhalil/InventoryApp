﻿CREATE TABLE [dbo].[Invoice] (
    [ID]                 INT            NOT NULL,
    [Type]               INT            NOT NULL,
    [Code]               NVARCHAR (50)  NOT NULL,
    [Person_Type]        TINYINT        NOT NULL,
    [Person_ID]          INT            NOT NULL,
    [Date]               DATETIME       NOT NULL,
    [Delivalry_Date]     DATETIME       NULL,
    [Store_ID]           INT            NOT NULL,
    [Notes]              NVARCHAR (200) NOT NULL,
    [Is_Posted_To_Store] BIT            NOT NULL,
    [Post_Date]          DATETIME       NULL,
    [Total]              FLOAT (53)     NOT NULL,
    [Discount_Val]       FLOAT (53)     NOT NULL,
    [Discount_Perec]     FLOAT (53)     NOT NULL,
    [Tax_Val]            FLOAT (53)     NOT NULL,
    [Tax_Perce]          FLOAT (53)     NOT NULL,
    [Expences]           FLOAT (53)     NOT NULL,
    [Net]                FLOAT (53)     NOT NULL,
    [Paid]               FLOAT (53)     NOT NULL,
    [Drawer_ID]          INT            NOT NULL,
    [Remaining]          FLOAT (53)     NOT NULL,
    [Shipping_Address]   NVARCHAR (100) NULL,
    [Invoice_Back_ID]    BIGINT         NULL,
    [User_ID]            INT            NOT NULL,
    CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Invoice_Customer_Supplier] FOREIGN KEY ([Person_ID]) REFERENCES [dbo].[Customer_Supplier] ([ID]),
    CONSTRAINT [FK_Invoice_Drawer] FOREIGN KEY ([Drawer_ID]) REFERENCES [dbo].[Drawer] ([ID]),
    CONSTRAINT [FK_Invoice_Store] FOREIGN KEY ([Store_ID]) REFERENCES [dbo].[Store] ([ID]),
    CONSTRAINT [FK_Invoice_User] FOREIGN KEY ([User_ID]) REFERENCES [dbo].[User] ([ID])
);

