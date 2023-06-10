CREATE TABLE [dbo].[Product] (
    [ID]                  INT             IDENTITY (1, 1) NOT NULL,
    [Code]                NVARCHAR (25)   CONSTRAINT [DF_Product_Code] DEFAULT ((0)) NOT NULL,
    [Name]                NVARCHAR (70)   NOT NULL,
    [Type]                TINYINT         CONSTRAINT [DF_Product_Type] DEFAULT ((1)) NOT NULL,
    [Is_Active]           BIT             CONSTRAINT [DF_Product_Is_Active] DEFAULT ((1)) NOT NULL,
    [Order_Limit]         FLOAT (53)      CONSTRAINT [DF__tmp_ms_xx__Order__30AEFB81] DEFAULT ((5)) NOT NULL,
    [Image]               VARBINARY (MAX) NULL,
    [Cate_ID]             INT             CONSTRAINT [DF_Product_Cate_ID] DEFAULT ((1)) NOT NULL,
    [Cost_Calc_Method]    TINYINT         CONSTRAINT [DF_Product_Cost_Calc_Method] DEFAULT ((1)) NOT NULL,
    [Discribtion]         NVARCHAR (200)  NULL,
    [Has_Opening_Balance] BIT             CONSTRAINT [DF_Product_Has_Opening_Balance] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([ID] ASC)
);







