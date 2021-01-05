using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Infrastructure.Configuration
{
    public class ApplicationConfiguration: IApplicationConfiguration
    {
        public string DbProvider { get; }

        public string ApiDbConnectionString { get; }

       // public string AspNetIdentityDbConnection { get; }

        public List<string> DbProviderList { get; } = new List<string>();

        public ApplicationConfiguration(IConfiguration configuration)
        {
            DbProviderList.Add("SqlServer");
            DbProviderList.Add("Sqlite");
            DbProviderList.Add("MySql");
            DbProviderList.Add("PostgreSql");

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            DbProvider= configuration.GetValue<string>(nameof(DbProvider));
            ApiDbConnectionString = configuration.GetValue<string>(nameof(ApiDbConnectionString));
           // AspNetIdentityDbConnection = configuration.GetValue<string>(nameof(AspNetIdentityDbConnection));
            ValidateConfiguration();
        }

        private void ValidateConfiguration()
        {
            if (string.IsNullOrWhiteSpace(this.DbProvider))
                throw new InvalidConfigurationException("DbProvider cannot be null or empty"); 

            if (!DbProviderList.Contains(this.DbProvider))
                throw new InvalidConfigurationException("Unsupported DbProvider. Valid DbProvider are one of these: SqlServer, Sqlite, MySql, PostgreSql");

            if (string.IsNullOrWhiteSpace(this.ApiDbConnectionString))
                throw new InvalidConfigurationException("ApiDbConnectionString cannot be null or empty");

            //if (string.IsNullOrWhiteSpace(this.AspNetIdentityDbConnection))
            //    throw new InvalidConfigurationException("AspNetIdentityDbConnection cannot be null or empty");

        }      

    }
}
