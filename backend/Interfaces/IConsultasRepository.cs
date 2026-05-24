using backend.Models;

namespace backend.Interfaces;

public interface IConsultasRepository
{
    Task<List<Consulta>> GetAllAsync();

    Task<Consulta?> GetByIdAsync(string id);

    Task<List<Consulta>> GetByPacienteIdAsync(string pacienteId);

    Task CreateAsync(Consulta consulta);

    Task UpdateAsync(string id, Consulta consulta);

    Task DeleteAsync(string id);
}