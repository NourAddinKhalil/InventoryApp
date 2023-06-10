CREATE VIEW dbo.MaxInvoiceView
AS
SELECT        inv.ID, inv.Type, inv.Code, inv.Person_Type, inv.Person_ID, dbo.Customer_Supplier.Name AS PersonName, inv.Date, inv.Delivalry_Date, inv.Store_ID, dbo.Store.Name AS StoreName, inv.Notes, inv.Is_Posted_To_Store, 
                         inv.Post_Date, inv.Total, inv.Discount_Val, inv.Discount_Perec, inv.Tax_Val, inv.Tax_Perce, inv.Expences, inv.Net, inv.Paid, inv.Drawer_ID, dbo.Drawer.Name AS DrawerName, inv.Remaining, inv.Shipping_Address, 
                         inv.Invoice_Back_ID, inv.User_ID, dbo.[User].Real_Name, ind.ID AS DetID, ind.Invoice_ID, ind.Product_ID, p.Code AS PCode, p.Name AS ProName, ind.Unit_ID, un.Name AS UnitName, ind.Qty, ind.Price, ind.Total_Price, ind.Cost, 
                         ind.Total_Cost, ind.Discount_Val AS DDiscount_Val, ind.Store_ID AS DStore_ID, s.Name AS DStoreName, invb.ID AS BID, invb.Code AS BCode, invb.Date AS BDate, s2.Name AS BStoreName, invb.Total AS BTotal, 
                         invb.Discount_Val AS BDiscount_Val, invb.Tax_Val AS BTax_Val, invb.Expences AS BExpences, invb.Net AS BNet, invb.Paid AS BPaid, d.Name AS BDrawerName, invb.Remaining AS BRemaining, 
                         u.Real_Name AS BReal_Name
FROM            dbo.Invoice AS inv INNER JOIN
                         dbo.Invoice_Details AS ind ON inv.ID = ind.Invoice_ID INNER JOIN
                         dbo.Product AS p ON ind.Product_ID = p.ID INNER JOIN
                         dbo.Unit AS un ON ind.Unit_ID = un.ID INNER JOIN
                         dbo.Store AS s ON ind.Store_ID = s.ID INNER JOIN
                         dbo.Customer_Supplier ON inv.Person_ID = dbo.Customer_Supplier.ID INNER JOIN
                         dbo.[User] ON inv.User_ID = dbo.[User].ID INNER JOIN
                         dbo.Drawer ON inv.Drawer_ID = dbo.Drawer.ID INNER JOIN
                         dbo.Store ON inv.Store_ID = dbo.Store.ID AND ind.Store_ID = dbo.Store.ID LEFT OUTER JOIN
                         dbo.Invoice AS invb ON dbo.Store.ID = invb.Store_ID AND dbo.Drawer.ID = invb.Drawer_ID AND dbo.[User].ID = invb.User_ID AND dbo.Customer_Supplier.ID = invb.Person_ID AND 
                         invb.Invoice_Back_ID = inv.ID LEFT OUTER JOIN
                         dbo.Drawer AS d ON invb.Drawer_ID = d.ID LEFT OUTER JOIN
                         dbo.Store AS s2 ON invb.Store_ID = s2.ID LEFT OUTER JOIN
                         dbo.[User] AS u ON invb.User_ID = u.ID
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'MaxInvoiceView';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'nd
         Begin Table = "s2"
            Begin Extent = 
               Top = 234
               Left = 543
               Bottom = 364
               Right = 802
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "u"
            Begin Extent = 
               Top = 252
               Left = 335
               Bottom = 371
               Right = 517
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Customer_Supplier"
            Begin Extent = 
               Top = 233
               Left = 906
               Bottom = 363
               Right = 1087
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "User"
            Begin Extent = 
               Top = 282
               Left = 38
               Bottom = 412
               Right = 220
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Drawer"
            Begin Extent = 
               Top = 366
               Left = 555
               Bottom = 479
               Right = 725
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Store"
            Begin Extent = 
               Top = 366
               Left = 763
               Bottom = 496
               Right = 1022
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 53
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 1875
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'MaxInvoiceView';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[63] 4[30] 2[3] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "inv"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 226
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ind"
            Begin Extent = 
               Top = 6
               Left = 264
               Bottom = 136
               Right = 463
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 6
               Left = 501
               Bottom = 136
               Right = 707
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "un"
            Begin Extent = 
               Top = 6
               Left = 745
               Bottom = 102
               Right = 915
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "s"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 280
               Right = 297
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "invb"
            Begin Extent = 
               Top = 102
               Left = 745
               Bottom = 232
               Right = 933
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "d"
            Begin Extent = 
               Top = 138
               Left = 335
               Bottom = 251
               Right = 505
            End
            DisplayFlags = 280
            TopColumn = 0
         E', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'MaxInvoiceView';



