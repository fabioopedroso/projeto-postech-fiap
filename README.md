
# 🎮 Fiap Cloud Games API

O projeto FiapCloudGamesApi foi desenvolvido para gerenciar a nossa loja virtual de games, incluindo funcionalidades de cadastro de usuários, autenticação, gerenciamento de jogos, carrinho de compras, biblioteca do usuário e promoções.

---

## 📜 Objetivo

O objetivo desta API é fornecer um backend robusto para uma loja de games na nuvem, permitindo que usuários possam:

- Registrar contas e realizar login.
- Navegar e buscar jogos no catálogo.
- Adicionar e remover jogos do carrinho.
- Realizar compras de jogos.
- Consultar sua biblioteca de jogos.
- Administradores podem gerenciar usuários, jogos e promoções (sales).

---

## 🚀 Tecnologias Utilizadas

- .NET 8.0
- ASP.NET Core Web API
- Swagger / OpenAPI (Swashbuckle)
- JWT (JSON Web Token) para autenticação
- Entity Framework Core
- Banco de Dados (PostgreSQL, Supabase)
- Memory Cache
- BDD

---

## 🔐 Autenticação

A API utiliza JWT (JSON Web Token) para autenticação. Para acessar os endpoints protegidos, é necessário:

1. Realizar o login via `/api/Auth/Login` para obter o token.
2. Inserir o token no Swagger (Authorize) ou no header das requisições:

---

## 📖 Documentação Swagger

Após rodar o projeto, acesse:

```
https://localhost:5106/swagger
```

---

## 🔗 Endpoints Principais

### 🧑‍💼 **Administração de Usuários**

| Método  | Endpoint                    					| Descrição                                |
|---------|-------------------------------------------------|------------------------------------------|
| POST    | `/api/Administrator/CreateUser` 				| Cria um novo usuário                     |
| GET     | `/api/Administrator/GetUsers`   				| Lista todos os usuários                  |
| GET     | `/api/Administrator/GetUserById/{id}` 			| Retorna detalhes de um usuário           |
| PUT     | `/api/Administrator/PromoteUser/{id}` 			| Promove usuário para administrador       |
| PUT     | `/api/Administrator/DemoteUser/{id}` 			| Remove permissão de administrador        |
| PUT     | `/api/Administrator/SetUserActiveStatus/{id}` 	| Ativa/Desativa um usuário                |

### 🔑 **Autenticação**

| Método  | Endpoint                 | Descrição                   |
|---------|--------------------------|-----------------------------|
| POST    | `/api/Auth/Login`        | Realiza login e gera token  |

### 🛒 **Carrinho**

| Método  | Endpoint                        | Descrição                              |
|---------|---------------------------------|----------------------------------------|
| GET     | `/api/Cart/GetCartSummary`      | Retorna resumo do carrinho             |
| POST    | `/api/Cart/AddGame/{gameId}`    | Adiciona jogo ao carrinho            	 |
| DELETE  | `/api/Cart/RemoveGame/{gameId}` | Remove jogo do carrinho             	 |
| DELETE  | `/api/Cart/ClearCart`     		| Limpa todo o carrinho                  |

### 🧾 **Checkout**

| Método  | Endpoint                     | Descrição                               |
|---------|------------------------------|---------------------------------------- |
| POST    | `/api/Checkout/CheckoutCart` | Processa a compra dos jogos no carrinho |

### 🎮 **Jogos**

| Método  | Endpoint                    	 | Descrição                            |
|---------|----------------------------------|--------------------------------------|
| GET     | `/api/Game/GetAll`          	 | Lista todos os jogos                 |
| GET     | `/api/Game/GetById/{id}`    	 | Detalhes de um jogo                  |
| POST    | `/api/Game/Add`             	 | Adiciona um novo jogo                |
| PUT     | `/api/Game/Update/{id}`     	 | Atualiza informações de um jogo      |
| PUT     | `/api/Game/SetActiveStatus/{id}` | Ativa ou desativa um jogo            |
| PUT     | `/api/Game/SetPrice/{id}`   	 | Atualiza o preço de um jogo          |

### 🎁 **Promoções (Sales)**

| Método  | Endpoint                         				 | Descrição                                   |
|---------|--------------------------------------------------|---------------------------------------------|
| POST    | `/api/Sale/CreateSale`           				 | Cria uma nova promoção                      |
| PUT     | `/api/Sale/UpdateSale/{id}`      				 | Atualiza uma promoção                       |
| GET     | `/api/Sale/GetAllSales`          				 | Lista todas as promoções                    |
| GET     | `/api/Sale/GetSaleById/{id}`     				 | Detalhes de uma promoção                    |
| POST    | `/api/Sale/AddGameToSale/{saleId}/{gameId}` 	 | Adiciona jogo a uma promoção      		   |
| DELETE  | `/api/Sale/RemoveGameFromSale/{saleId}/{gameId}` | Remove jogo de uma promoção 				   |

### 📚 **Biblioteca**

| Método  | Endpoint                        | Descrição                                    |
|---------|---------------------------------|----------------------------------------------|
| GET     | `/api/Library/ListLibraryGames` | Lista todos os jogos adquiridos pelo usuário |

### 👤 **Usuário**

| Método  | Endpoint                        | Descrição                                   |
|---------|---------------------------------|---------------------------------------------|
| POST    | `/api/User/Register`            | Cadastra um novo usuário                    |
| PUT     | `/api/User/ChangePassword`      | Altera a senha do usuário                   |
| DELETE  | `/api/User/DeleteUser`          | Remove permanentemente a conta do usuário   |

---

## 🧪 Exemplos de Requisição

### ✔️ Login

```http
POST /api/Auth/Login
Content-Type: application/json

{
  "email": "usuario@exemplo.com",
  "password": "suaSenha123"
}
```

**Resposta:**

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6..."
}
```

---

## 📝 Licença

Este projeto está licenciado sob a Licença MIT.
