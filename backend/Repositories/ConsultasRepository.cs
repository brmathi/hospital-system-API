using backend.Data;
using backend.Interfaces;
using backend.Models;
using MongoDB.Driver;

namespace backend.Repositories;

public class ConsultasRepository : IConsultasRepository
{
    private readonly IMongoCollection<Consulta> _collection;
    public ConsultasRepository(MongoDbService db)
    {
        _collection = db.Consultas;
    }

    public async Task<List<Consulta>> GetAllAsync()
    => await _collection.Find(_ => true).ToListAsync();

    public async Task<Consulta?> GetByIdAsync(string id)
    => await _collection.Find(c => c.Id == id).FirstOrDefaultAsync();

    public async Task<List<Consulta>> GetByPacienteIdAsync(string pacienteId)
    => await _collection.Find(c => c.PacienteId == pacienteId).ToListAsync();

    public async Task CreateAsync(Consulta consulta)
    => await _collection.InsertOneAsync(consulta);

    public async Task UpdateAsync(string id, Consulta consulta)
    => await _collection.ReplaceOneAsync(c => c.Id == id, consulta);

    public async Task DeleteAsync(string id)
    => await _collection.DeleteOneAsync(c => c.Id == id);
}