using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Models;

public class Pacientes
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set;}

    public string Nome { get; set;} = string.Empty;

    public string Cpf { get; set;} = string.Empty;

    public string Email { get; set;} = string.Empty;

    public string Telefone { get; set;} = string.Empty;

    public DateTime DataNascimento { get; set;}

    public string Sexo { get; set;} = string.Empty;

    public DateTime CreateAt { get; set;} = DateTime.UtcNow;
}