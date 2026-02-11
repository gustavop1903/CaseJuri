# CaseJuri

Sistema de gerenciamento de tarefas (To-Do) construído com .NET 10 e MongoDB, com suporte para arquitetura em camadas (Domain, Application, Infrastructure, API).

## 📋 Sobre o Projeto

CaseJuri é uma API REST para gerenciar tarefas com as seguintes funcionalidades:

- ✅ **Criar tarefas** com título, descrição e autor
- ✅ **Listar todas as tarefas**
- ✅ **Buscar tarefa por ID**
- ✅ **Atualizar tarefa** (título e descrição)
- ✅ **Iniciar tarefa** (mudar status para "Em Andamento")
- ✅ **Concluir tarefa** (mudar status para "Concluída")
- ✅ **Deletar tarefa**
- ✅ **Persistência em MongoDB** com suporte a Volume Docker

## 🏗️ Estrutura do Projeto

```
CaseJuri/
├── CaseJuri/                          # Raiz do código-fonte
│   ├── CaseJuri.API/                  # Camada de API (Controllers, Middleware)
│   │   ├── Controllers/
│   │   ├── Middleware/
│   │   ├── Properties/
│   │   └── Program.cs
│   ├── CaseJuri.Application/          # Camada de Aplicação (UseCases, DTOs, Mappings)
│   │   ├── DTOs/
│   │   ├── UseCases/
│   │   ├── Mappings/
│   │   └── Interfaces/
│   ├── CaseJuri.Domain/               # Camada de Domínio (Entidades, Enums)
│   │   ├── Entities/
│   │   └── Enums/
│   ├── CaseJuri.Infrastructure/       # Camada de Infraestrutura (Repositórios, DB)
│   │   ├── Dynamo/
│   │   ├── Mongo/
│   │   └── DependencyInjection.cs
│   └── CaseJuri.slnx
├── docker-compose.yml                 # Orquestração Docker (API, MongoDB, Mongo Express)
├── Dockerfile                         # Build da aplicação .NET
└── README.md
```

## 🛠️ Tech Stack

- **Backend**: .NET 10 (C#)
- **Banco de Dados**: MongoDB 7.0
- **ORM/Mapper**: AutoMapper, MongoDB Driver
- **UI Admin DB**: Mongo Express
- **Containerização**: Docker & Docker Compose
- **Arquitetura**: Camadas (Domain-Driven Design)

## 📦 Pré-requisitos

Para rodar localmente, você precisa de:

- **Docker** e **Docker Compose** instalados
  - [Instalar Docker](https://docs.docker.com/get-docker/)
  - Docker Compose geralmente vem junto

Caso queira compilar sem Docker:
- **.NET SDK 10.0** ou superior
  - [Instalar .NET](https://dotnet.microsoft.com/download)

## 🚀 Como Iniciar Localmente

### Opção 1: Com Docker (Recomendado)

1. **Clone ou navegue até o diretório do projeto:**
```bash
cd /CaseJuri
```

2. **Inicie os containers:**
```bash
docker compose up -d --build
```

Isto irá:
- Compilar a aplicação .NET
- Iniciar a API na porta **5001**
- Iniciar MongoDB na porta **27017**
- Iniciar Mongo Express (UI) na porta **8081**

3. **Verifique se está tudo rodando:**
```bash
docker compose ps
```

Você deve ver 3 containers em "Up": `casejuri-api-1`, `mongo-local`, `mongo-express`.

4. **Teste a API:**
```bash
curl http://localhost:5001/api/tasks
```

### Opção 2: Localmente (sem Docker)

1. **Navegue até o diretório do projeto:**
```bash
cd /Users/gustavopereira/Documents/Projetos/CaseJuri/CaseJuri
```

2. **Instale dependências:**
```bash
dotnet restore
```

3. **Construa a solução:**
```bash
dotnet build
```

4. **Inicie o MongoDB separadamente** (localmente ou via container):
```bash
# Se quiser rodar apenas o MongoDB em container:
docker run -d -p 27017:27017 \
  -e MONGO_INITDB_ROOT_USERNAME=root \
  -e MONGO_INITDB_ROOT_PASSWORD=example \
  mongo:7.0
```

5. **Rode a aplicação:**
```bash
dotnet run --project CaseJuri.API/CaseJuri.API.csproj
```

A API estará disponível em `http://localhost:5000` (ou conforme configurado em `launchSettings.json`).

## 📡 Endpoints da API

### Base URL
```
http://localhost:5001/api/tasks
```

### Criar Tarefa
```
POST /api/tasks
Content-Type: application/json

{
  "titulo": "Implementar autenticação",
  "descricao": "Adicionar JWT ao projeto",
  "criadoPor": "João Silva"
}

Resposta: 201 Created
```

### Listar Todas as Tarefas
```
GET /api/tasks

Resposta:
[
  {
    "id": "05060892-8354-464d-a1c5-2b9e73d6ed91",
    "titulo": "Implementar autenticação",
    "descricao": "Adicionar JWT ao projeto",
    "status": "Pendente",
    "criadoPor": "João Silva",
    "dataCriacao": "2026-02-10T05:36:52.679Z",
    "dataConclusao": null
  }
]
```

### Buscar Tarefa por ID
```
GET /api/tasks/{id}

Exemplo: GET /api/tasks/05060892-8354-464d-a1c5-2b9e73d6ed91

Resposta: 200 OK com tarefa
```

### Atualizar Tarefa
```
PUT /api/tasks/{id}
Content-Type: application/json

{
  "titulo": "Novo título",
  "descricao": "Nova descrição"
}

Resposta: 204 No Content
```

### Iniciar Tarefa (mudar para "Em Andamento")
```
POST /api/tasks/{id}/start

Resposta: 204 No Content
```

### Concluir Tarefa
```
POST /api/tasks/{id}/complete

Resposta: 204 No Content
```

### Deletar Tarefa
```
DELETE /api/tasks/{id}

Resposta: 204 No Content
```

## 🗄️ Acessar MongoDB (Mongo Express)

Você pode gerenciar o banco de dados visualmente:

**URL**: `http://localhost:8081`
- **Usuário**: `admin`
- **Senha**: `pass`

Ou use a linha de comando:
```bash
mongosh -u root -p example --authenticationDatabase admin mongodb://localhost:27017/CaseJuri
```

## 🛑 Parar os Containers

```bash
docker compose down
```

Para parar **e remover volumes** (banco de dados):
```bash
docker compose down -v
```

## 📝 Notas sobre Persistência

- O MongoDB usa um volume Docker chamado `mongo-data` para persistir dados
- Mesmo depois de parar os containers, os dados são mantidos
- Use `docker compose down -v` para limpar tudo (cuidado: apaga os dados!)

## 🔧 Variáveis de Ambiente

O arquivo `docker-compose.yml` define:

```yaml
DOTNET_ENVIRONMENT=Development        # Ambiente do .NET
ASPNETCORE_ENVIRONMENT=Development    # Ambiente do ASP.NET Core
ASPNETCORE_URLS=http://+:8080        # URL da aplicação no container
```

Se precisar mudar para Production, atualize o `docker-compose.yml`.

## 📚 Estrutura de Camadas

### Domain (CaseJuri.Domain)
- Entidades: `ToDoTask`, `StatusTask`
- Sem dependências externas

### Application (CaseJuri.Application)
- UseCases: `CreateToDoTaskUseCase`, `UpdateToDoTaskUseCase`, etc.
- DTOs: `CreateToDoTaskRequest`, `ToDoTaskResponseDto`
- Mappings: AutoMapper Profile
- Interfaces de repositório

### Infrastructure (CaseJuri.Infrastructure)
- Implementações de repositório: `MongoToDoTaskRepository`, `ToDoTaskRepository` (Dynamo)
- Injeção de Dependência
- Configuração de BD (Mongo, DynamoDB)

### API (CaseJuri.API)
- Controllers: `ToDoTasksController`
- Middleware: `ExceptionMiddleware`
- Program.cs (configuração da aplicação)

## 🐛 Troubleshooting

### Erro: "Connection refused" ao chamar a API
- Verifique se os containers estão rodando: `docker compose ps`
- Espere um pouco para a aplicação inicializar
- Veja os logs: `docker compose logs api`

### Erro: "Cannot connect to MongoDB"
- Verifique se MongoDB está healthy: `docker compose ps`
- Veja os logs do MongoDB: `docker compose logs mongodb`

### Compilação falha no Docker
- Limpe o build: `docker compose down && docker system prune`
- Tente novamente: `docker compose up -d --build`


