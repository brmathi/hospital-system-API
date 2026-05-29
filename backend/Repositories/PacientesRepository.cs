using backend.Data;
using backend.Interfaces;
using backend.Models;
using MongoDB.Driver;

namespace backend.Repositories;

public class PacientesRepository : IPacientesRepository
{
    private readonly IMongoCollection<Paciente> _collection;
    public PacientesRepository(MongoDbService db)
    {
        _collection = db.Pacientes;
    }

    public async Task<List<Paciente>> GetAllAsync()
    => await _collection.Find(_ => true).ToListAsync();

    public async Task<Paciente?> GetByIdAsync(string id)
    => await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();

    public async Task<Paciente?> GetByCpfAsync(string cpf)
    => await _collection.Find(p => p.Cpf == cpf).FirstOrDefaultAsync();

    public async Task CreateAsync(Paciente paciente)
    => await _collection.InsertOneAsync(paciente);

    public async Task UpdateAsync(string id, Paciente paciente)
    => await _collection.ReplaceOneAsync(p => p.Id == id, paciente);

    public async Task DeleteAsync(string id)
    => await _collection.DeleteOneAsync(p => p.Id == id);
}