using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/consultas")]
[Produces("application/json")]
public class ConsultasController : ControllerBase
{
    private readonly ConsultasService _service;

    public ConsultasController(ConsultasService service)
    {
        _service = service;
    }

    /// <summary>Lista todas as consultas agendadas.</summary>
    /// <response code="200">Lista retornada com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<ConsultaResponseDto>), 200)]
    public async Task<IActionResult> GetAll()
    {
        var consultas = await _service.GetAllAsync();
        return Ok(consultas);
    }

    /// <summary>Busca uma consulta pelo ID.</summary>
    /// <param name="id">ID da consulta.</param>
    /// <response code="200">Consulta encontrada.</response>
    /// <response code="404">Consulta não encontrada.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ConsultaResponseDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(string id)
    {
        var consulta = await _service.GetByIdAsync(id);
        if (consulta is null)
            return NotFound(new { mensagem = "Consulta não encontrada." });

        return Ok(consulta);
    }

    /// <summary>Lista todas as consultas de um paciente.</summary>
    /// <param name="pacienteId">ID do paciente.</param>
    /// <response code="200">Lista retornada com sucesso.</response>
    [HttpGet("paciente/{pacienteId}")]
    [ProducesResponseType(typeof(List<ConsultaResponseDto>), 200)]
    public async Task<IActionResult> GetByPaciente(string pacienteId)
    {
        var consultas = await _service.GetByPacienteIdAsync(pacienteId);
        return Ok(consultas);
    }

    /// <summary>Agenda uma nova consulta.</summary>
    /// <response code="201">Consulta criada com sucesso.</response>
    /// <response code="400">Dados inválidos ou paciente inexistente.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ConsultaResponseDto), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] ConsultaRequestDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var (consulta, erro) = await _service.CreateAsync(dto);
        if (erro is not null)
            return BadRequest(new { mensagem = erro });

        return CreatedAtAction(nameof(GetById), new { id = consulta!.Id }, consulta);
    }

    /// <summary>Atualiza os dados de uma consulta.</summary>
    /// <param name="id">ID da consulta.</param>
    /// <response code="204">Atualizado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    /// <response code="404">Consulta não encontrada.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(string id, [FromBody] ConsultaRequestDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var (sucesso, erro) = await _service.UpdateAsync(id, dto);
        if (erro != null && erro.Contains("não encontrada"))
            return NotFound(new { mensagem = erro });
        if (!sucesso)
            return BadRequest(new { mensagem = erro });

        return NoContent();
    }

    /// <summary>Remove uma consulta pelo ID.</summary>
    /// <param name="id">ID da consulta.</param>
    /// <response code="204">Removido com sucesso.</response>
    /// <response code="404">Consulta não encontrada.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(string id)
    {
        var sucesso = await _service.DeleteAsync(id);
        if (!sucesso)
            return NotFound(new { mensagem = "Consulta não encontrada." });

        return NoContent();
    }
}
