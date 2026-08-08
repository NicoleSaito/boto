# Boto

> Plataforma para contratação e customização de agentes de IA sob medida para negócios.

Projeto pessoal desenvolvido para portfólio, aplicando Clean Architecture, CQRS e boas práticas de engenharia de software em C#/.NET, com deploy planejado em AWS.

## 🎯 Sobre o projeto

O Boto permite que clientes contratem agentes de IA e os customizem de acordo com o próprio negócio — o cliente fornece o contexto (informações sobre sua empresa, produtos, FAQ) e a plataforma cuida do resto: geração de embeddings, busca semântica (RAG) e respostas contextualizadas.

**Funcionalidades planejadas:**
- [x] Estrutura base com Clean Architecture
- [x] Modelagem inicial do banco de dados (Users, Plans, Agents)
- [ ] Autenticação e autorização (Identity + JWT)
- [ ] CRUD de agentes customizáveis
- [ ] Integração de pagamentos recorrentes (Stripe)
- [ ] RAG: geração de embeddings e busca vetorial (pgvector)
- [ ] Frontend em Next.js
- [ ] Deploy na AWS (ECS Fargate, RDS, S3, SQS + Lambda)

## 🏗️ Arquitetura

O backend segue **Clean Architecture**, dividido em 4 camadas:

A regra de dependência é sempre "de fora pra dentro": `Api` depende de `Infrastructure`, `Application` e `Domain`; mas `Domain` não depende de nada.

## 🛠️ Stack

**Backend**
- C# / .NET 10
- ASP.NET Core Web API
- Entity Framework Core + Npgsql
- PostgreSQL (com extensão pgvector planejada para embeddings)

**Infraestrutura (planejada)**
- AWS (ECS Fargate, RDS, S3, SQS, Lambda)
- Docker
- GitHub Actions (CI)

**Frontend (planejado)**
- Next.js + TypeScript
- Tailwind CSS

**Pagamentos (planejado)**
- Stripe

## 🚀 Como rodar localmente

### Pré-requisitos
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/) 16+
- [dotnet-ef tool](https://learn.microsoft.com/ef/core/cli/dotnet): `dotnet tool install --global dotnet-ef`

### Passo a passo

1. Clone o repositório:
```bash
git clone https://github.com/NicoleSaito/boto.git
cd boto
```

2. Crie o banco de dados e usuário no PostgreSQL:
```sql
CREATE DATABASE boto_db;
CREATE USER boto WITH PASSWORD 'sua_senha_aqui';
GRANT ALL PRIVILEGES ON DATABASE boto_db TO boto;
```
Depois, conectado no banco `boto_db` especificamente:
```sql
GRANT ALL ON SCHEMA public TO boto;
```

3. Copie o arquivo de configuração de exemplo e edite com suas credenciais:
```bash
cp Boto.Api/appsettings.Development.json.example Boto.Api/appsettings.Development.json
```

4. Restaure as dependências e aplique as migrations:
```bash
dotnet restore
dotnet ef database update --project Boto.Infrastructure --startup-project Boto.Api
```

5. Rode a aplicação:
```bash
dotnet run --project Boto.Api
```

## 📁 Estrutura de pastas
Boto/
├── Boto.Api/ # Camada de apresentação (Controllers, Program.cs)
├── Boto.Application/ # Casos de uso (Commands, Queries, Handlers)
├── Boto.Domain/ # Entidades e regras de negócio
├── Boto.Infrastructure/ # EF Core, Persistence, integrações externas
└── Boto.slnx

## 📝 Licença

Projeto pessoal para fins de portfólio.