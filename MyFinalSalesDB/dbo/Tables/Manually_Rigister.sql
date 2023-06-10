CREATE TABLE [dbo].[Manually_Rigister] (
    [ID]    INT            IDENTITY (1, 1) NOT NULL,
    [Date]  DATETIME       NOT NULL,
    [Notes] NVARCHAR (150) NULL,
    CONSTRAINT [PK_Manually_Rigister] PRIMARY KEY CLUSTERED ([ID] ASC)
);

