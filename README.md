# Hospital Sytem

Sistema web de gestão hospitalar simples, com cadastro de pacientes e agendamento de consultas.

## Domínio

Aplicação para gerenciamento de um hospital, permitindo:
- Cadastro e manutenção de **pacientes**
- Agendamento e controle de **consultas médicas**
- Visualização de histórico de consultass por paciente

---

## Stack

| Camada    | Tecnologia |
|------------------------|
| Backend   | .net 8 (C#)|
| Banco     | MongoDb    |
| Frontend  | Html + JS  |
| Container | Docker     |


## Endpoints da API

### Pacientes `/api/pacientes`

| Método | Rota                    | Descrição                    |
|--------|-------------------------|------------------------------|
| GET    | `/api/pacientes`        | Lista todos os pacientes     |
| GET    | `/api/pacientes/{id}`   | Busca paciente por ID        |
| POST   | `/api/pacientes`        | Cadastra novo paciente       |
| PUT    | `/api/pacientes/{id}`   | Atualiza dados do paciente   |
| DELETE | `/api/pacientes/{id}`   | Remove paciente              |

### Consultas `/api/consultas`

| Método | Rota                              | Descrição                         |
|--------|-----------------------------------|-----------------------------------|
| GET    | `/api/consultas`                  | Lista todas as consultas          |
| GET    | `/api/consultas/{id}`             | Busca consulta por ID             |
| GET    | `/api/consultas/paciente/{id}`    | Lista consultas de um paciente    |
| POST   | `/api/consultas`                  | Agenda nova consulta              |
| PUT    | `/api/consultas/{id}`             | Atualiza consulta                 |
| DELETE | `/api/consultas/{id}`             | Remove consulta                   |

---

## Variáveis de ambiente

| Variável         | Descrição                    | Exemplo         |
|------------------|------------------------------|-----------------|
| `MONGO_USER`     | Usuário do MongoDB           | `admin`         |
| `MONGO_PASSWORD` | Senha do MongoDB             | `senha123`      |

No backend, via `appsettings.json`:

| Chave                             | Descrição                     |
|-----------------------------------|-------------------------------|
| `MongoSettings:ConnectionString`  | URI de conexão com o MongoDB  |
| `MongoSettings:DatabaseName`      | Nome do banco de dados        |

---

## Estrutura do projeto

```
hospital-system/
├── backend/
│   ├── Configurations/   # MongoSettings
│   ├── Controllers/      # PacientesController, ConsultasController
│   ├── Data/             # MongoDbService
│   ├── DTOs/             # PacienteDto, ConsultaDto
│   ├── Interfaces/       # IPacientesRepository, IConsultasRepository
│   ├── Models/           # Paciente, Consulta
│   ├── Repositories/     # Implementações das interfaces
│   ├── Services/         # Regras de negócio
│   └── Program.cs
├── frontend/
│   └── index.html        # SPA com navegação assíncrona
├── docker-compose.yml
└── README.md
```