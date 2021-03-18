namespace Infrastructure.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.IO;
    //public abstract class DesignTimeDbContextFactoryBase<TContext> :
    //     IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    //{

    //    private const string _connectionStringName = "MusicDbConnectionString";
    //    private const string _aspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

    //    protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

    //    public TContext CreateDbContext(string[] args)
    //    {
    //        var basePath = Directory.GetCurrentDirectory() + string.Format("{0}..{0}WebApi", Path.DirectorySeparatorChar);
    //        return Create(basePath, Environment.GetEnvironmentVariable(_aspNetCoreEnvironment));
    //    }

    //    private TContext Create(string basePath, string environmentName)
    //    {
    //        var configuration = new ConfigurationBuilder()
    //            .SetBasePath(basePath)
    //            .AddJsonFile("applications.json")
    //            .AddJsonFile($"applications.Local.json", optional: true)
    //            .AddJsonFile($"application.{environmentName}.json", optional: true)
    //            .AddEnvironmentVariables()
    //            .Build();

    //        var connectionString = configuration.GetConnectionString(_connectionStringName);
    //        return Create(connectionString);
    //    }

    //    private TContext Create(string connectionString)
    //    {
    //        if (string.IsNullOrEmpty(connectionString))
    //            throw new ArgumentException($"Connection string '{_connectionStringName}' is null or empty.", nameof(connectionString));


    //        Console.WriteLine($"DesignTimeDbContextFactotyBase.Create(string):connection string : '{connectionString}'.");

    //        var optionBuilder = new DbContextOptionsBuilder<TContext>();
    //        optionBuilder.UseSqlServer(connectionString);
    //        return CreateNewInstance(optionBuilder.Options);
    //    }


    //}
}
