### Infrastructure Layer

This layer contains classes for accessing external resources such as 
-	File systems
-	Web services
-	Smtp
-   SMS
-	Data persistense (DbContext, EntityConfiguration,Migrations and so on.

These classes should be based on interfaces defined within the application layer.


##### EF Core CLI
<code>dotnet tool install --global dotnet-ef</code>

##### Verify Installation
<code>dotnet ef</code>


#### Required Nuget Packages for this layer

    Microsoft.EntityFrameworkCore.SqlServer
    Microsoft.EntityFrameworkCore.Design
	  
	Microsoft.EntityFrameworkCore.Sqlite.Core
	Npgsql.EntityFrameworkCore.PostgreSQL
	Pomelo.EntityFrameworkCore.MySql

	Microsoft.AspNetCore.Hosting.Abstractions
     
	 




