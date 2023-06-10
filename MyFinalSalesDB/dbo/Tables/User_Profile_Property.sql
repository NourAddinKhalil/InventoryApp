CREATE TABLE [dbo].[User_Profile_Property] (
    [ID]          INT             IDENTITY (1, 1) NOT NULL,
    [Profile_ID]  INT             NOT NULL,
    [Prope_Name]  NVARCHAR (500)  NOT NULL,
    [Prope_Value] VARBINARY (MAX) NOT NULL,
    CONSTRAINT [PK_User_Profile_Property] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_User_Profile_Property_Profile_Prope_Name] FOREIGN KEY ([Profile_ID]) REFERENCES [dbo].[Profile_Prope_Name] ([ID])
);

