
### EF Core database Migration and database update

#### Create migration (EF Core 3.1/5.0)
 


#### Install EF Core CLI
<code>dotnet tool install --global dotnet-ef</code>

#### Verify Installation
<code>dotnet ef</code>

#### Create Migration
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

#### Update Database
 C:\XXXXX\WebApi><code>dotnet ef database update --startup-project ./ --project ../Infrastructure/</code>

#### Adding Swagger to web api

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



