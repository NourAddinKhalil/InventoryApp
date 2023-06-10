CREATE TABLE [dbo].[User] (
    [ID]                 INT           IDENTITY (1, 1) NOT NULL,
    [Real_Name]          NVARCHAR (70) NOT NULL,
    [User_Name]          NVARCHAR (50) NOT NULL,
    [Password]           NVARCHAR (50) NOT NULL,
    [Type]               TINYINT       NOT NULL,
    [Setting_Profile_ID] INT           NOT NULL,
    [Setting_Screen_ID]  INT           NOT NULL,
    [Is_Active]          BIT           NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_User_Profile_Prope_Name] FOREIGN KEY ([Setting_Profile_ID]) REFERENCES [dbo].[Profile_Prope_Name] ([ID]),
    CONSTRAINT [FK_User_Screen_Roles_Name] FOREIGN KEY ([Setting_Screen_ID]) REFERENCES [dbo].[Screen_Roles_Name] ([ID])
);

