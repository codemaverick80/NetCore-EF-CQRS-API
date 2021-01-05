namespace Application.Common.Interfaces
{
    public class IApplicationConfiguration
    {

        string DbProvider { get; }

        string ApiDbConnectionString { get; }

        string AspNetIdentityDbConnection { get; }

    }
}
