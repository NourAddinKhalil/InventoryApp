CREATE TABLE [dbo].[Guaranteed_Notes] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [Is_Cash_In]    BIT            NOT NULL,
    [Code]          NVARCHAR (25)  NOT NULL,
    [Invoice_ID]    INT            CONSTRAINT [DF_Guaranteed_Notes_Invoice_ID] DEFAULT ((0)) NOT NULL,
    [Invoice_Type]  TINYINT        CONSTRAINT [DF_Guaranteed_Notes_Invoice_Type] DEFAULT ((1)) NOT NULL,
    [inv_Type_Name] NVARCHAR (50)  CONSTRAINT [DF_Guaranteed_Notes_inv_Type_Name] DEFAULT ('ooo') NOT NULL,
    [Store_ID]      INT            NOT NULL,
    [Drawer_ID]     INT            NOT NULL,
    [Part_ID]       INT            NOT NULL,
    [Part_Type]     TINYINT        NOT NULL,
    [Amount]        FLOAT (53)     NOT NULL,
    [Discount_Val]  FLOAT (53)     NOT NULL,
    [Date]          DATETIME       NOT NULL,
    [Note]          NVARCHAR (200) NOT NULL,
    [Total_Lett]    NVARCHAR (150) CONSTRAINT [DF__tmp_ms_xx__Total__6FCAA12F] DEFAULT ('Zero') NOT NULL,
    [User_ID]       INT            NOT NULL,
    CONSTRAINT [PK_Guaranteed_Notes] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Guaranteed_Notes_Drawer] FOREIGN KEY ([Drawer_ID]) REFERENCES [dbo].[Drawer] ([ID]) ON UPDATE CASCADE,
    CONSTRAINT [FK_Guaranteed_Notes_Store] FOREIGN KEY ([Store_ID]) REFERENCES [dbo].[Store] ([ID]) ON UPDATE CASCADE,
    CONSTRAINT [FK_Guaranteed_Notes_User] FOREIGN KEY ([User_ID]) REFERENCES [dbo].[User] ([ID]) ON UPDATE CASCADE
);





