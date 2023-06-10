CREATE TABLE [dbo].[Coupes_Of_Account] (
    [ID]          INT             IDENTITY (1, 1) NOT NULL,
    [Code]        NVARCHAR (25)   NOT NULL,
    [Account_ID]  INT             NOT NULL,
    [Debit]       FLOAT (53)      NOT NULL,
    [Credit]      FLOAT (53)      NOT NULL,
    [Insert_Date] DATETIME        NOT NULL,
    [Source_Type] TINYINT         NOT NULL,
    [Source_ID]   INT             NOT NULL,
    [Notes]       NVARCHAR (2000) NOT NULL,
    [Move_Name]   TINYINT         NOT NULL,
    [User_ID]     INT             NOT NULL,
    CONSTRAINT [PK_Coupes_Of_Account] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Coupes_Of_Account_Accounts] FOREIGN KEY ([Account_ID]) REFERENCES [dbo].[Accounts] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Coupes_Of_Account_User] FOREIGN KEY ([User_ID]) REFERENCES [dbo].[User] ([ID]) ON UPDATE CASCADE
);





