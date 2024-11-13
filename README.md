# Read this carefully to use the Entity Frameworks
- Installed Entityframework.tools
- Installed Entityframework.sqlserver
- Installed EntityframeworkCore
Get the SQL Connect String (Run .sql successfully)
  Open ~\reg-pa-onl\Code\Register-Patient-Online\Models\RegisterPatientOnlineContext.cs
  Change the optionsBuilder.UseSqlServer("Data Source=NGHIA\\SQLEXPRESS;Initial Catalog=RegisterPatientOnline;Integrated Security=True;Trust Server Certificate=True");
  to optionsBuilder.UseSqlServer("Data Source=<Your SQL Name>;Initial Catalog=<Name of databases>;Integrated Security=True;Trust Server Certificate=True");
  
