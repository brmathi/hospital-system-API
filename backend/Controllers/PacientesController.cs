using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/pacientes")]
[Produces("application/json")]
public class PacientesController : ControllerBase
{
    private readonly PacientesService _service;

    public PacientesController(PacientesService service)
    {
        _service = service;
    }

    /// <summary>Lista todos os pacientes cadastrados.</summary>
    /// <response code="200">Lista retornada com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<PacienteResponseDto>), 200)]
    public async Task<IActionResult> GetAll()
    {
        var pacientes = await _service.GetAllAsync();
        return Ok(pacientes);
    }

    /// <summary>Busca um paciente pelo ID.</summary>
    /// <param name="id">ID do paciente (ObjectId do MongoDB).</param>
    /// <response code="200">Paciente encontrado.</response>
    /// <response code="404">Paciente não encontrado.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PacienteResponseDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(string id)
    {
        var paciente = await _service.GetByIdAsync(id);
        if (paciente is null)
            return NotFound(new { mensagem = "Paciente não encontrado." });

        return Ok(paciente);
    }

    /// <summary>Cadastra um novo paciente.</summary>
    /// <response code="201">Paciente criado com sucesso.</response>
    /// <response code="400">Dados inválidos ou CPF já cadastrado.</response>
    [HttpPost]
    [ProducesResponseType(typeof(PacienteResponseDto), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] PacienteRequestDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var (paciente, erro) = await _service.CreateAsync(dto);
        if (erro is not null)
            return BadRequest(new { mensagem = erro });

        return CreatedAtAction(nameof(GetById), new { id = paciente!.Id }, paciente);
    }

    /// <summary>Atualiza os dados de um paciente.</summary>
    /// <param name="id">ID do paciente.</param>
    /// <response code="204">Atualizado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    /// <response code="404">Paciente não encontrado.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(string id, [FromBody] PacienteRequestDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var (sucesso, erro) = await _service.UpdateAsync(id, dto);
        if (erro == "Paciente não encontrado.")
            return NotFound(new { mensagem = erro });
        if (!sucesso)
            return BadRequest(new { mensagem = erro });

        return NoContent();
    }

    /// <summary>Remove um paciente pelo ID.</summary>
    /// <param name="id">ID do paciente.</param>
    /// <response code="204">Removido com sucesso.</response>
    /// <response code="404">Paciente não encontrado.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(string id)
    {
        var sucesso = await _service.DeleteAsync(id);
        if (!sucesso)
            return NotFound(new { mensagem = "Paciente não encontrado." });

        return NoContent();
    }
}