# API RESTful ASP.NET Core

Esta é uma aplicação web API RESTful desenvolvida em ASP.NET Core, seguindo boas práticas de arquitetura e design de software, incluindo os princípios SOLID.
A aplicação está dividida em responsabilidades distintas para a API, serviços e acesso ao banco de dados. 
Utiliza Entity Framework Core para persistência de dados, Identity para autenticação e autorização, e JWT para fornecer tokens de acesso seguro.

## Funcionalidades

A aplicação fornece operações CRUD (Create, Read, Update, Delete) para gerenciar fornecedores, seus endereços e produtos. As principais funcionalidades incluem:

- Registro e autenticação de usuários
- Geração de tokens JWT para acesso seguro às rotas protegidas
- CRUD para fornecedores, incluindo operações para listar, criar, atualizar e excluir fornecedores
- CRUD para endereços de fornecedores, permitindo a associação de um endereço a um fornecedor
- CRUD para produtos de fornecedores, permitindo a associação de múltiplos produtos a um fornecedor

## Tecnologias Utilizadas

- ASP.NET Core
- Entity Framework Core
- Identity
- JWT (JSON Web Tokens)
- Injeção de Dependências
- Princípios SOLID (IUnitOfWork, Segregação de Interfaces)
- Boas Práticas de Desenvolvimento

## Configuração e Uso

1. Clone o repositório para sua máquina local
2. Abra o projeto em sua IDE de preferência (Visual Studio, Visual Studio Code, etc.)
3. Certifique-se de ter o .NET Core SDK instalado em sua máquina
4. Abra um terminal na raiz do projeto entre na pasta src/Dev.Api e execute o comando `dotnet run` para iniciar a aplicação
5. Acesse a API através da URL fornecida ex: http://localhost:5197


