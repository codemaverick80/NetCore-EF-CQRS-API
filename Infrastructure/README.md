### Infrastructure Layer

This layer contains classes for accessing external resources such as 
-	File systems
-	Web services
-	Smtp
-   SMS
-	Data persistense (DbContext, EntityConfiguration,Migrations and so on.

These classes should be based on interfaces defined within the application layer.


#### Install EF Core CLI
<code>dotnet tool install --global dotnet-ef</code>

### Verify Installation
<code>dotnet ef</code>

#### Create Migration (Manually) - Approach A
- Right click on WebApi project (Visual Studio 2019)
- Open Folder in File Explorer 
- Click on the address bar (it will select complete path), type CMD and press Enter
- Apply following command to create migration

- C:\XXXXX\WebApi><code>dotnet ef migrations add initial-migration --startup-project ./ --project ../Infrastructure/ -o Persistence/Migrations/</code>

#### Update Database (Manually)
- C:\XXXXX\WebApi><code>dotnet ef database update --startup-project ./ --project ../Infrastructure/</code>



#### Automatic Migration  Approach B

Note : While this approach is productive for local development and testing of migrations, 
this approach is inappropriate for managing production databases. Please read https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli


Assuming we do not have any migration (Infrastructure > Persistence > Migrations > is empty)
<em>Required Nuget Packages:</em>
> 1. <em>Microsoft.AspNetCore.Hosting.Abstractions</em>

- Apply following command to create migration
- C:\XXXXX\WebApi><code>dotnet ef migrations add initial-migration --startup-project ./ --project ../Infrastructure/ -o Persistence/Migrations/</code>
- Create a file called MigrationManager.cs inside Infrastructure folder as below

```charp
namespace Infrastructure
{   
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using(var scope = host.Services.CreateScope())
            {
               using(var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    try
                    {
                        appContext.Database.Migrate();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            return host;
        }
    }
}
```

- Go to Program.cs file (WebApi project) and update the Main method as below

```csharp
public static void Main(string[] args)
{
    var host= CreateHostBuilder(args).Build();
    host.MigrateDatabase();
    host.Run();
}

```

- Build the project and run. if everything goes as expeceted then it should create the Database