CREATE TABLE [dbo].[Transfer_Money] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [From_Acc_ID] INT            NOT NULL,
    [To_Acc_ID]   INT            NOT NULL,
    [Date]        DATETIME       NOT NULL,
    [Type]        TINYINT        NOT NULL,
    [Amount]      FLOAT (53)     NOT NULL,
    [Notes]       NVARCHAR (150) NULL,
    [User_ID]     INT            NOT NULL,
    CONSTRAINT [PK_Transfer_Money] PRIMARY KEY CLUSTERED ([ID] ASC)
);



