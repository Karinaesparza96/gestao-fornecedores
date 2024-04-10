<h1 align="center">
  API REST em ASP.NET Core 
</h1>
<p align="center">
Esta é uma aplicação web API REST desenvolvida em ASP.NET Core.
A aplicação está dividida em responsabilidades distintas para a API, serviços e acesso ao banco de dados. 
Utiliza Entity Framework Core para persistência de dados, Identity para autenticação e autorização, e JWT para fornecer tokens de acesso seguro.
</p>

## 🔥 Funcionalidades 

A aplicação fornece operações CRUD (Create, Read, Update, Delete) para gerenciar fornecedores, seus endereços e produtos. As principais funcionalidades incluem:

- Registro e autenticação de usuários
- Geração de tokens JWT para acesso seguro às rotas protegidas
- CRUD para fornecedores, incluindo operações para listar, criar, atualizar e excluir fornecedores e seu endereço. 
- CRUD para produtos de fornecedores, permitindo a associação de múltiplos produtos a um fornecedor

## 🚀 Tecnologias Utilizadas

- ASP.NET Core
- Entity Framework Core
- Identity
- Sql Server
- Docker 

## ✋🏻 Pré-requisitos
- Docker 
  
## Uso com Docker Compose 

1. Clone o repositório para sua máquina local
2. Abra o projeto em sua IDE de preferência (Visual Studio, Visual Studio Code, etc.)
3. Navegue até a pasta do projeto
4. Execute o comando `docker-compose up -d` para iniciar os containers Docker
5. Abra o navegador e acesse o Swagger UI em http://localhost:8080/swagger para visualizar e interagir com a API

## Instruções de Uso no Swagger

Para utilizar os endpoints protegidos pela autenticação JWT, siga estes passos:

1. No Swagger UI, navegue para a rota de autenticação (geralmente `/auth/register` ou `/auth/login`)
2. Registre ou faça login com suas credenciais
3. Após o login bem-sucedido, copie o token JWT gerado
4. Clique no botão "Authorize" no canto superior direito do Swagger UI
5. Cole o token JWT na caixa de texto e clique em "Authorize"
6. Agora você está autenticado e autorizado para acessar os recursos protegidos da API.

Feito com 💖 by Karina Esparza 👋
