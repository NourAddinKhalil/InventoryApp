CREATE TYPE [dbo].[Pro_Movement_List_Type] AS TABLE
(
    [Source_Type] TINYINT        NOT NULL,
    [Source_ID]   INT            NOT NULL,
    [Product_ID]  INT            NOT NULL,
    [Store_ID]    INT            NOT NULL,
    [Export_Qty]  FLOAT (53)     NOT NULL,
    [Import_Qty]  FLOAT (53)     NOT NULL,
    [Cost_Value]  FLOAT (53)     NOT NULL,
    [Insert_Date] DATETIME       NOT NULL,
    [Notes]       NVARCHAR (200) NOT NULL,
    [User_ID]     INT            NOT NULL
)
