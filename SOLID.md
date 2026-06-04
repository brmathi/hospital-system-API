## S — Single Responsibility Principle (Responsabilidade Única)

**Onde:** Separação entre `Repositories`, `Services` e `Controllers`.

- **`PacientesRepository`** / **`ConsultasRepository`**: responsáveis exclusivamente pelo acesso ao banco de dados (MongoDB). Não conhecem regras de negócio nem formatação de resposta.
- **`PacientesService`** / **`ConsultasService`**: responsáveis pelas regras de negócio (ex: validar CPF duplicado, verificar se paciente existe antes de criar consulta). Não acessam o banco diretamente nem conhecem detalhes HTTP.
- **`PacientesController`** / **`ConsultasController`**: responsáveis apenas por receber as requisições HTTP, chamar o serviço e retornar a resposta adequada. Não contêm lógica de negócio.

## I — Interface Segregation Principle (Segregação de Interfaces)

**Onde:** `IPacientesRepository` e `IConsultasRepository` em `backend/Interfaces/`.

Cada entidade possui sua própria interface com os métodos específicos que ela precisa. Não existe uma interface genérica `IRepository<T>` forçando todos os repositórios a implementar métodos que não utilizam.

Por exemplo, `IConsultasRepository` possui `GetByPacienteIdAsync` — um método específico de consultas — enquanto `IPacientesRepository` possui `GetByCpfAsync` — específico de pacientes. Nenhuma das duas interfaces força a outra a implementar métodos que não fazem sentido para ela.

## D — Dependency Inversion Principle (Inversão de Dependência)

**Onde:** `Program.cs` e construtores de `PacientesService` / `ConsultasService`.

Os serviços dependem de **abstrações** (interfaces), não de implementações concretas:

```csharp
// PacientesService depende de IPacientesRepository, não de PacientesRepository
public PacientesService(IPacientesRepository repository) { ... }

// ConsultasService depende de ambas as interfaces
public ConsultasService(IConsultasRepository consultasRepository, IPacientesRepository pacientesRepository) { ... }
```

No `Program.cs`, o container de DI do .NET registra as implementações concretas vinculadas às interfaces:

```csharp
builder.Services.AddScoped<IPacientesRepository, PacientesRepository>();
builder.Services.AddScoped<IConsultasRepository, ConsultasRepository>();