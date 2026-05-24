using backend.Models;

namespace backend.Interfaces;

public interface IPacientesRepository
{
    Task<List<Paciente>> GetAllAsync();

    Task <Paciente?> GetByIdAsync(string id);

    Task <Paciente?> GetByCpfAsync(string cpf);

    Task CreateAsync(Paciente paciente);

    Task UpdateAsync(string id, Paciente paciente);

    Task DeleteAsync(string id);
}