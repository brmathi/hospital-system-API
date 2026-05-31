using backend.DTOs;
using backend.Interfaces;
using backend.Models;

namespace backend.Services;

public class PacientesService
{
    private readonly IPacientesRepository _repository;

    public PacientesService(IPacientesRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<PacienteResponseDto>> GetAllAsync()
    {
        var pacientes = await _repository.GetAllAsync();
        return pacientes.Select(ToResponseDto).ToList();
    }

    public async Task<PacienteResponseDto?> GetByIdAsync(string id)
    {
        var paciente = await _repository.GetByIdAsync(id);
        return paciente is null ? null : ToResponseDto(paciente);
    }

    public async Task<(PacienteResponseDto? dto, string? erro)> CreateAsync(PacienteRequestDto dto)
    {
        // Regra de negócio: CPF único
        var existente = await _repository.GetByCpfAsync(dto.Cpf);
        if (existente is not null)
            return (null, "Já existe um paciente cadastrado com este CPF.");

        var paciente = ToModel(dto);
        await _repository.CreateAsync(paciente);
        return (ToResponseDto(paciente), null);
    }

    public async Task<(bool sucesso, string? erro)> UpdateAsync(string id, PacienteRequestDto dto)
    {
        var existente = await _repository.GetByIdAsync(id);
        if (existente is null)
            return (false, "Paciente não encontrado.");

        // Verifica se o CPF pertence a outro paciente
        var comMesmoCpf = await _repository.GetByCpfAsync(dto.Cpf);
        if (comMesmoCpf is not null && comMesmoCpf.Id != id)
            return (false, "CPF já está em uso por outro paciente.");

        var atualizado = ToModel(dto);
        atualizado.Id = id;
        atualizado.CreatedAt = existente.CreatedAt; // preserva data original

        await _repository.UpdateAsync(id, atualizado);
        return (true, null);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var existente = await _repository.GetByIdAsync(id);
        if (existente is null) return false;

        await _repository.DeleteAsync(id);
        return true;
    }

    // --- Mapeamentos ---

    private static PacienteResponseDto ToResponseDto(Paciente p) => new()
    {
        Id = p.Id!,
        Nome = p.Nome,
        Cpf = p.Cpf,
        Email = p.Email,
        Telefone = p.Telefone,
        DataNascimento = p.DataNascimento,
        Sexo = p.Sexo,
        CreatedAt = p.CreatedAt
    };

    private static Paciente ToModel(PacienteRequestDto dto) => new()
    {
        Nome = dto.Nome,
        Cpf = dto.Cpf,
        Email = dto.Email,
        Telefone = dto.Telefone,
        DataNascimento = dto.DataNascimento,
        Sexo = dto.Sexo
    };
}
