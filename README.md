# ChallengeCSharp - Sistema de Gestão e Análise de Sinistros Odontológicos

## Visão Geral do Projeto

Este projeto foi criado para resolver um problema importante da Odontoprev relacionado à análise e gestão de sinistros odontológicos. O sistema permite que a equipe da Odontoprev gerencie os dados de pacientes, dentistas, consultas e tratamentos. A finalidade principal é dar mais clareza sobre esses dados e fornecer informações que permitam determinar o resultado de um sinistro.

O uso desse sistema permite à equipe otimizar a tomada de decisões e reduzir fraudes ou abusos no uso de serviços odontológicos.

---

## Funcionalidades Principais

- **Gerenciamento completo** de pacientes, dentistas, sinistros, consultas e tratamentos odontológicos.
- **Criação automática de endereço** via CEP para agilizar o cadastro de pacientes e profissionais.
- **Modelo de predição de sinistro** baseado em Machine Learning para estimar a probabilidade de aprovação ou reprovação de um sinistro, auxiliando na análise automatizada.
- Integração com APIs externas para consulta e validação de dados, quando aplicável.
- Interface web intuitiva para cadastro, visualização e análise dos dados.
- Dashboard e relatórios para acompanhamento e tomada de decisão.
- Funcionalidades de IA generativa para auxiliar na interpretação dos dados e geração de insights.

---

## Arquitetura do Projeto

O projeto está organizado em uma solução com múltiplos projetos que seguem princípios de Clean Architecture e SOLID, visando alta manutenção, testabilidade e escalabilidade:

- **Domain:** Contém entidades, interfaces, regras de negócio e modelos centrais.
- **Application:** Serviços de aplicação que orquestram operações, regras e integrações.
- **Infrastructure:** Implementações concretas de repositórios, acesso a dados (EF Core), serviços externos e configurações.
- **Web:** Aplicação ASP.NET Core MVC para interface do usuário (Views, Controllers).
- **Api:** API RESTful que expõe serviços para consumo externo e integração.
- **Tests:** Projetos de testes automatizados divididos em:
  - **Unitários:** Testam serviços e regras isoladamente usando mocks.
  - **Integração:** Testam endpoints da API com banco em memória.
  - **Sistemas:** Testes end-to-end simulando interações reais.

---

## APIs Externas e Integrações

O sistema pode ser configurado para consumir APIs externas para:

- Validação e busca de dados de CEP.
- Serviços de consulta a bases governamentais ou de saúde, caso necessário.
- Integração futura com sistemas legados da Odontoprev.

---

## Testes Automatizados

O projeto possui uma cobertura abrangente de testes usando xUnit, Moq e Entity Framework InMemory para:

- **Testes unitários:** Validam a lógica das regras de negócio e serviços com mocks para dependências.
- **Testes de integração:** Validam o funcionamento dos controllers e APIs consumindo a aplicação completa com banco em memória.
- **Testes de sistema:** Testes end-to-end utilizando WebApplicationFactory para simular requisições HTTP e garantir fluxo funcional.

Esses testes asseguram a qualidade, evitam regressões e dão confiança para evoluir o sistema com segurança.

---

## Práticas de Clean Code Aplicadas

- Separação clara entre camadas e responsabilidades.
- Uso de injeção de dependência para facilitar testabilidade e manutenção.
- Nomes claros e consistentes para classes, métodos e variáveis.
- Código modular e reutilizável, com funções pequenas e coesas.
- Tratamento adequado de erros e validações.
- Documentação e comentários quando necessário para clareza.
- Uso de DTOs e ViewModels para separar modelos de domínio e apresentação.

---

## Funcionalidades de IA Generativa

O sistema incorpora funcionalidades de IA generativa para:

- Auxiliar na análise de sinistros, sugerindo possíveis decisões baseado em padrões aprendidos.
- Gerar relatórios e insights automáticos a partir dos dados cadastrados.
- Potencial integração futura com modelos mais avançados para otimização da operação.

---

## Estrutura do Projeto

```

/ChallengeCSharp.sln
\|-- /ChallengeCSharp.Domain
\|-- /ChallengeCSharp.Application
\|-- /ChallengeCSharp.Infrastructure
\|-- /ChallengeCSharp.Web
\|-- /ChallengeCSharp.Api
\|-- /ChallengeCSharp.Tests

````

---

## Como Executar

### Pré-requisitos

- .NET SDK (versão 7.0 ou superior)
- SQL Server ou outra base configurada (ou usar banco em memória para testes)
- Rider, Visual Studio ou VS Code

### Passos para rodar localmente

1. Clone o repositório:
```bash
git clone https://github.com/seuusuario/challengecsharp.git
````

2. Configure a string de conexão no `appsettings.json` dos projetos `.Web` e `.Api`.

3. Restaure os pacotes e compile:

```bash
dotnet restore
dotnet build
```

4. Rode as migrações para criar o banco (se aplicável):

```bash
dotnet ef database update --project ChallengeCSharp.Infrastructure
```

5. Execute a aplicação Web ou API:

```bash
dotnet run --project ChallengeCSharp.Web
```

ou

```bash
dotnet run --project ChallengeCSharp.Api
```

6. Para rodar os testes:

```bash
dotnet test
```
