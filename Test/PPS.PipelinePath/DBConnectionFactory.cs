using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PPS.PipelinePath
{
    public class DBConnectionFactory : IDBConnectionFactory
    {
        private string ConnectionString;
        public DBConnectionFactory(IConfiguration configuration)
        {
            ConnectionString = configuration["ConnectionString"];
        }
        public IDbConnection GetInstance()
        {
            return new SqlConnection(ConnectionString);
        }

    }

    public interface IDBConnectionFactory
    {
        IDbConnection GetInstance();
    }
}
