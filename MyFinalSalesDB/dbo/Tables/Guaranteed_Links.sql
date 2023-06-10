CREATE TABLE [dbo].[Guaranteed_Links] (
    [ID]            INT     IDENTITY (1, 1) NOT NULL,
    [Guaranteed_ID] INT     NOT NULL,
    [Invoice_Type]  TINYINT NOT NULL,
    [Invoice_ID]    INT     NOT NULL,
    CONSTRAINT [PK_Guaranteed_Links] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Guaranteed_Links_Guaranteed_Notes] FOREIGN KEY ([Guaranteed_ID]) REFERENCES [dbo].[Guaranteed_Notes] ([ID])
);

