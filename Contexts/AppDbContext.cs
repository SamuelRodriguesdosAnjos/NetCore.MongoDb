using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace MongoDB
{
    public class AppDbContext
    {
        public readonly string _connectionString;

        public readonly string _databaseName;

        public readonly bool _ssl;

        public readonly IMongoDatabase Database;

        public IMongoCollection<Pessoa> Pessoas => Database.GetCollection<Pessoa>("Pessoas");

        public AppDbContext()
        {
            _connectionString = "mongodb://localhost:27017";
            _databaseName = "Teste-01";
            _ssl = true;

            Database = OnConnectionDatabase();
        }

        public AppDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("MongoConnection:ConnectionString").Value;
            _databaseName = configuration.GetSection("MongoConnection:Database").Value;
            _ssl = Convert.ToBoolean(configuration.GetSection("MongoConnection:IsSSL").Value);

            Database = OnConnectionDatabase();
        }

        public AppDbContext(string ConnectionString, string DatabaseName, bool SSL = true)
        {
            _connectionString = ConnectionString;
            _databaseName = DatabaseName;
            _ssl = SSL;

            Database = OnConnectionDatabase();
        }

        private IMongoDatabase OnConnectionDatabase()
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(_connectionString));

            if (_ssl)
            {
                settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
            }

            var mongoClient = new MongoClient(settings);
            return mongoClient.GetDatabase(_databaseName);
        }
    }
}