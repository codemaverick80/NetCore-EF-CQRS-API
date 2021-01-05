
### WebAPi Project
This layer contains all the UI related stuff. Since this project is web api so it will contains all the api endpoints.

 

### How to setup



##### Install EF Core CLI
<code>dotnet tool install --global dotnet-ef</code>

##### Verify Installation
<code>dotnet ef</code>

### Database Migration

##### Create Migration (Manually) - Approach A
- Right click on WebApi project (Visual Studio 2019)
- Open Folder in File Explorer 
- Click on the address bar (it will select complete path), type CMD and press Enter
- Apply following command for database migration 

- C:\XXXXX\WebApi><code>dotnet ef migrations add initial-migration --startup-project ./ --project ../Infrastructure/ -o Persistence/Migrations/</code>

<strong><em>Tips:</em></strong>
> 1. <em>Statup project is WebApi and it contains appsettings.json where we have specify SQL Server connection</em>
> 2. <em>Infrastructure project contains ApplicationDbContext : DdContext</em>
> 3. <em>All the migrations files we want to keep inside Infrastructure project (Persistence/Migrations/)</em>
> 4. <em>Since our ApplicationDbContext class and appsettings.json are in different project, we need to use following command</em>

##### Update Database
 C:\XXXXX\WebApi><code>dotnet ef database update --startup-project ./ --project ../Infrastructure/</code>


##### Automatic Migration - Approach B

<p style='color:orange; font-weight:bold; font-style: italic;'>Note:</p>

While this approach is productive for local development and testing of migrations, 
this approach is inappropriate for managing production databases. Please read https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli


Assuming we do not have any migrations (Infrastructure > Persistence > Migrations > should be empty)

<em>Required Nuget Packages</em>
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

<p style='color:orange; font-weight:bold; font-style: italic;'>Note</p>

If we want to change the DbProvider from SqlServer to MySql or Postgressql or sqlite then 
first we have to create migration. Steps are below:

     1. Remove any previous migrations that we have created (Infrastructure > Persistense > Migrations > should be empty)
     2. Make sure DbProvider and db connection is configured correctly in appsetings.json
     3. Now, create migration.
     4. Build and run the app, if everything is setup correctly the app will run and create database           

```json
{ 
// SqlServer Setting
 "DbProvider": "SqlServer", //PostresSql, SqlServer, Sqlite, MySql
  "ApiDbConnectionString": "Server=localhost;Database=MusiApi;Trusted_Connection=false;User ID=username;Password=yourpassword",  

}
```


```json 
{
// PostgreSql Setting
 "DbProvider": "PostgreSql", //PostresSql, SqlServer, Sqlite, MySql
  "ApiDbConnectionString": "Host=localhost;Port=5432;Username=postgres;Password=yourpassword;Database=MusicApi;",

}
```




### Adding Swagger to web api

- Install <code>Swashbuckle.AspNetCore</code> in WebApi project
- Register Swagger service inside ConfigureServices() method of startup.cs file

```csharp
services.AddSwaggerGen(options=> {
            options.SwaggerDoc("v1",
                new Microsoft.OpenApi.Models.OpenApiInfo {
                    Title = "Music API",
                    Description = "This is music api", 
                    Version = "v1" }
                );
        });
```
- Add Swagger middleware inside Configure() method (add this at the end after app.UseEndpoints middleware)
```csharp
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseSwagger();
app.UseSwaggerUI(options => {
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Music API");            
});
``` 
- Run the application and navigate to https://localhost:5001/swagger/



