using backend.Models;

namespace backend.Interfaces;

public interface IConsultasRepository
{
    Task<List<Consultas>> GetAllAsync();

    Task<Consultas?> GetByIdAsync(string id);

    Task CreateAsync(Consultas consultas);

    Task UpdateAsync(string id, Consultas consultas);

    Task DeleteAsync(string id);
}