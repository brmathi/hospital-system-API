using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class ConsultaRequestDto
{
    [Required(ErrorMessage = "PacienteId é obrigatorio.")]
    public string PacienteId {get; set;} = string.Empty;

    [Required(ErrorMessage = "Data da consulta é obrigatoria.")]
    public DateTime DataConsulta {get; set;}

    [Required(ErrorMessage = "Médico é obrigatorio.")]
    public string Medico {get; set;} = string.Empty;

    [Required(ErrorMessage = "Especialidade é obrigatoria.")]
    public string Especialidade {get; set;} = string.Empty;
    public string Observacoes {get; set;} = string.Empty;
    public string Status {get; set;} = "Agendada";
}

public class ConsultaResponseDto
{
    public string Id {get; set;} = string.Empty;
    public string PacienteId {get; set;} = string.Empty;
    public string PacienteNome {get; set;} = string.Empty;
    public DateTime DataConsulta {get; set;}
    public string Medico {get; set;} = string.Empty;
    public string Especialidade {get; set;} = string.Empty;
    public string Observacoes {get; set;} = string.Empty;
    public string Status {get; set;} = string.Empty;
    public DateTime CreatedAt {get; set;}
}