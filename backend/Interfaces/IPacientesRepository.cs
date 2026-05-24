using backend.Models;

namespace backend.Interfaces;

public interface IPacientesRepository
{
    Task<List<Pacientes>> GetAllAsync();

    Task <Pacientes?> GetByIdAsync(string id);

    Task CreateAsync(Pacientes pacientes);

    Task UpdateAsync(string id, Pacientes pacientes);

    Task DeleteAsync(string id);
}