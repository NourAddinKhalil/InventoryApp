CREATE TABLE [dbo].[Store] (
    [ID]                           INT           IDENTITY (1, 1) NOT NULL,
    [Name]                         NVARCHAR (70) NOT NULL,
    [Sales_Account_ID]             INT           NOT NULL,
    [Sales_Return_Account_ID]      INT           NOT NULL,
    [Inventory_Account_ID]         INT           NOT NULL,
    [Cost_Of_Sold_Good_Account_ID] INT           NOT NULL,
    [Discount_Received_Account_ID] INT           NOT NULL,
    [Discount_Allowed_Account_ID]  INT           NOT NULL,
    CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED ([ID] ASC)
);

