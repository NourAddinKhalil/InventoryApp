# InventoryApp
A Profissional C# window form inventory application designed with devexpress &amp; Dapper &amp; sql

How to use:
1 - download devexpress v19.2 or lateset version (you will need to migrate)
2 - edit sql database connection 
3 - on folder MyFinalSalesDB you'll find the database there you can navigate to publish manager and publish 
the database to your sql server database or whatever database you have but you need to provide conncetion string
4 - after all steps the app should work fine and you'll need to Enable server broker with this command
  alter database [<dbname>] set enable_broker with rollback immediate;
this helps to make applicaion realtime on different devices
  
5 - help me to improve this app and add new ideas
