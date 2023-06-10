CREATE TABLE [dbo].[Pro_Store_Movement] (
    [ID]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [Source_Type] TINYINT        NOT NULL,
    [Source_ID]   INT            NOT NULL,
    [Store_ID]    INT            NOT NULL,
    [Export_Qty]  FLOAT (53)     NOT NULL,
    [Import_Qty]  FLOAT (53)     NOT NULL,
    [Cost_Vlaue]  FLOAT (53)     NOT NULL,
    [Insert_Date] DATETIME       NOT NULL,
    [Notes]       NVARCHAR (200) NOT NULL,
    [User_ID]     INT            NOT NULL,
    CONSTRAINT [PK_Pro_Store_Movement] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Pro_Store_Movement_Store] FOREIGN KEY ([Store_ID]) REFERENCES [dbo].[Store] ([ID]),
    CONSTRAINT [FK_Pro_Store_Movement_User] FOREIGN KEY ([User_ID]) REFERENCES [dbo].[User] ([ID])
);

