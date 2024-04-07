# API RESTful ASP.NET Core

Esta é uma aplicação web API RESTful desenvolvida em ASP.NET Core, seguindo boas práticas de arquitetura e design de software, incluindo os princípios SOLID.
A aplicação está dividida em responsabilidades distintas para a API, serviços e acesso ao banco de dados. 
Utiliza Entity Framework Core para persistência de dados, Identity para autenticação e autorização, e JWT para fornecer tokens de acesso seguro.

## Funcionalidades

A aplicação fornece operações CRUD (Create, Read, Update, Delete) para gerenciar fornecedores, seus endereços e produtos. As principais funcionalidades incluem:

- Registro e autenticação de usuários
- Geração de tokens JWT para acesso seguro às rotas protegidas
- CRUD para fornecedores, incluindo operações para listar, criar, atualizar e excluir fornecedores
- CRUD para endereços de fornecedores, permitindo a associação um endereço a um fornecedor
- CRUD para produtos de fornecedores, permitindo a associação de múltiplos produtos a um fornecedor

## Tecnologias Utilizadas

- ASP.NET Core
- Entity Framework Core
- Identity
- JWT (JSON Web Tokens)
- Injeção de Dependências
- Princípios SOLID (IUnitOfWork, Segregação de Interfaces)
- Docker
- Boas Práticas de Desenvolvimento

## Como usar este projeto com Docker

1. Clone o repositório para sua máquina local
2. Certifique-se de ter o Docker instalado em sua máquina. Você pode baixá-lo [aqui](https://www.docker.com/products/docker-desktop).
3. Abra um terminal na raiz do projeto e execute o seguinte comando para construir a imagem Docker:

docker build -t dev-api .

4. Após a construção da imagem, execute o seguinte comando para iniciar um contêiner Docker:

docker run -d -p 8080:8080 -p 8081:8081 --name dev-api dev-api

Isso iniciará o contêiner Docker com a aplicação em execução. Você pode acessar a API através da URL fornecida pelo Docker.

## Configuração do Banco de Dados SQL Server

Este projeto assume que você tem um banco de dados SQL Server configurado localmente. 

