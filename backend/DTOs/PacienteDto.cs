using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class PacienteRequestDto
{
    [Required(ErrorMessage = "Nome é obrigatorio")]
    public string Nome {get; set;} = string.Empty;

    [Required(ErrorMessage = "CPF é obrigatorio")]
    [StringLength(14, MinimumLength = 11, ErrorMessage = "CPF invalido.")]
    public string Cpf {get; set;} = string.Empty;

    [EmailAddress(ErrorMessage = "Email invalido.")]
    public string Email {get; set;} = string.Empty;

    public string Telefone {get; set;} = string.Empty;

    [Required(ErrorMessage = "Data de nascimento é obrigatoria")]
    public DateTime DataNascimento {get; set;}

    [Required(ErrorMessage = "Sexo é obrigatorio.")]
    public string Sexo {get; set;} = string.Empty;
}

public class PacienteResponseDto
{
    public string Id {get; set;} = string.Empty;
    public string Nome {get; set;} = string.Empty;
    public string Cpf {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string Telefone {get; set;} = string.Empty;
    public DateTime DataNascimento {get; set;}
    public string Sexo {get; set;} = string.Empty;
    public DateTime CreatedAt {get; set;}
}