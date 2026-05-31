using backend.DTOs;
using backend.Interfaces;
using backend.Models;

namespace backend.Services;

public class ConsultasService
{
    private readonly IConsultasRepository _consultasRepository;
    private readonly IPacientesRepository _pacientesRepository;

    public ConsultasService(
        IConsultasRepository consultasRepository,
        IPacientesRepository pacientesRepository)
    {
        _consultasRepository = consultasRepository;
        _pacientesRepository = pacientesRepository;
    }

    public async Task<List<ConsultaResponseDto>> GetAllAsync()
    {
        var consultas = await _consultasRepository.GetAllAsync();
        var dtos = new List<ConsultaResponseDto>();

        foreach (var c in consultas)
        {
            var paciente = await _pacientesRepository.GetByIdAsync(c.PacienteId);
            dtos.Add(ToResponseDto(c, paciente?.Nome));
        }

        return dtos;
    }

    public async Task<ConsultaResponseDto?> GetByIdAsync(string id)
    {
        var consulta = await _consultasRepository.GetByIdAsync(id);
        if (consulta is null) return null;

        var paciente = await _pacientesRepository.GetByIdAsync(consulta.PacienteId);
        return ToResponseDto(consulta, paciente?.Nome);
    }

    public async Task<List<ConsultaResponseDto>> GetByPacienteIdAsync(string pacienteId)
    {
        var consultas = await _consultasRepository.GetByPacienteIdAsync(pacienteId);
        var paciente = await _pacientesRepository.GetByIdAsync(pacienteId);

        return consultas.Select(c => ToResponseDto(c, paciente?.Nome)).ToList();
    }

    public async Task<(ConsultaResponseDto? dto, string? erro)> CreateAsync(ConsultaRequestDto dto)
    {
        // Regra de negócio: paciente deve existir
        var paciente = await _pacientesRepository.GetByIdAsync(dto.PacienteId);
        if (paciente is null)
            return (null, "Paciente não encontrado.");

        // Regra de negócio: data não pode ser no passado
        if (dto.DataConsulta < DateTime.UtcNow.AddMinutes(-5))
            return (null, "A data da consulta não pode ser no passado.");

        var consulta = ToModel(dto);
        await _consultasRepository.CreateAsync(consulta);
        return (ToResponseDto(consulta, paciente.Nome), null);
    }

    public async Task<(bool sucesso, string? erro)> UpdateAsync(string id, ConsultaRequestDto dto)
    {
        var existente = await _consultasRepository.GetByIdAsync(id);
        if (existente is null)
            return (false, "Consulta não encontrada.");

        var paciente = await _pacientesRepository.GetByIdAsync(dto.PacienteId);
        if (paciente is null)
            return (false, "Paciente não encontrado.");

        var atualizada = ToModel(dto);
        atualizada.Id = id;
        atualizada.CreatedAt = existente.CreatedAt;

        await _consultasRepository.UpdateAsync(id, atualizada);
        return (true, null);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var existente = await _consultasRepository.GetByIdAsync(id);
        if (existente is null) return false;

        await _consultasRepository.DeleteAsync(id);
        return true;
    }

    // --- Mapeamentos ---

    private static ConsultaResponseDto ToResponseDto(Consulta c, string? pacienteNome) => new()
    {
        Id = c.Id!,
        PacienteId = c.PacienteId,
        PacienteNome = pacienteNome ?? "Desconhecido",
        DataConsulta = c.DataConsulta,
        Medico = c.Medico,
        Especialidade = c.Especialidade,
        Observacoes = c.Observacoes,
        Status = c.Status,
        CreatedAt = c.CreatedAt
    };

    private static Consulta ToModel(ConsultaRequestDto dto) => new()
    {
        PacienteId = dto.PacienteId,
        DataConsulta = dto.DataConsulta,
        Medico = dto.Medico,
        Especialidade = dto.Especialidade,
        Observacoes = dto.Observacoes,
        Status = dto.Status
    };
}