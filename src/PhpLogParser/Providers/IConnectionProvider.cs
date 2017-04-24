using System.Data;

namespace PhpLogParser.Providers
{
    public interface IConnectionProvider
    {
        IDbConnection CreateConnection();
    }
}
