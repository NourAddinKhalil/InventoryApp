CREATE TABLE [dbo].[User_Actions_Per_Screen] (
    [ID]                  INT           IDENTITY (1, 1) NOT NULL,
    [User_ID]             INT           NOT NULL,
    [Date]                DATETIME      NOT NULL,
    [Screen_ID]           INT           NOT NULL,
    [Type]                TINYINT       NOT NULL,
    [Changed_Source_ID]   INT           NOT NULL,
    [Changed_Source_Name] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_User_Actions_Per_Screen] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_User_Actions_Per_Screen_User] FOREIGN KEY ([User_ID]) REFERENCES [dbo].[User] ([ID]) ON UPDATE CASCADE
);



