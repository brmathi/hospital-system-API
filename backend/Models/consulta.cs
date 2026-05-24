using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Models;

public class Consultas
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set;}

    public string PacienteId { get; set;} = string.Empty;

    public DateTime DataConsulta { get; set;}

    public string Medico { get; set;} = string.Empty;

    public string Especialidade { get; set;} = string.Empty;

    public string Observacoes { get; set;} = string.Empty;

    public string Status { get; set;} = "Agendada";
}