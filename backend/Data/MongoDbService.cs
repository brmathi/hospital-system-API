using backend.Configurations;
using backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace backend.Data;

public class MongoDbService
{
    private readonly IMongoDatabase _database;

    public MongoDbService(IOptions<MongoSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);

        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<Pacientes> Pacientes
    => _database.GetCollection<Pacientes>("Pacientes");

    public IMongoCollection<Consultas> Consultas
    => _database.GetCollection<Consultas>("Consultas");
}