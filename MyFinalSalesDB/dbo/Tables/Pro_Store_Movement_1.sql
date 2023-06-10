CREATE TABLE [dbo].[Pro_Store_Movement] (
    [ID]          INT         IDENTITY (1, 1) NOT NULL,
    [Source_Type] TINYINT        NOT NULL,
    [Source_ID]   INT            NOT NULL,
    [Product_ID]  INT            NOT NULL,
    [Store_ID]    INT            NOT NULL,
    [Export_Qty]  FLOAT (53)     NOT NULL,
    [Import_Qty]  FLOAT (53)     NOT NULL,
    [Cost_Value]  FLOAT (53)     NOT NULL,
    [Insert_Date] DATETIME       NOT NULL,
    [Notes]       NVARCHAR (200) NOT NULL,
    [User_ID]     INT            NOT NULL,
    CONSTRAINT [PK_Pro_Store_Movement] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Pro_Store_Movement_Store] FOREIGN KEY ([Store_ID]) REFERENCES [dbo].[Store] ([ID]) ON UPDATE CASCADE,
    CONSTRAINT [FK_Pro_Store_Movement_User] FOREIGN KEY ([User_ID]) REFERENCES [dbo].[User] ([ID]) ON UPDATE CASCADE
);



