CREATE TABLE [dbo].[Screen_Roles_Detail] (
    [ID]             INT IDENTITY (1, 1) NOT NULL,
    [Screen_Role_ID] INT NOT NULL,
    [Screen_ID]      INT NOT NULL,
    [Can_Show]       BIT NOT NULL,
    [Can_Open]       BIT NOT NULL,
    [Can_Add]        BIT NOT NULL,
    [Can_Edit]       BIT NOT NULL,
    [Can_Delete]     BIT NOT NULL,
    [Can_Print]      BIT NOT NULL,
    CONSTRAINT [PK_Screen_Roles_Detail] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Screen_Roles_Detail_Screen_Roles_Name] FOREIGN KEY ([Screen_Role_ID]) REFERENCES [dbo].[Screen_Roles_Name] ([ID])
);

