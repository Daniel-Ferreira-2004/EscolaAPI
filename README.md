# EscolaAPI
# 🏫 EscolaAPI – ASP.NET Core Web API

API REST desenvolvida em **ASP.NET Core** para gerenciamento de **alunos e endereços**, com integração a **APIs externas**, uso de **Entity Framework Core** e documentação via **Swagger**. O projeto tem foco educacional e de portfólio, demonstrando conceitos essenciais de backend em .NET.

---

## 🚀 Funcionalidades

* CRUD de alunos
* Associação de aluno com endereço
* Integração com a API **ViaCEP** para preenchimento automático de endereço
* Integração com a API **Gemini (IA)**
* Mapeamento de DTOs com **AutoMapper**
* Persistência de dados com **MySQL**
* API documentada com **Swagger**

---

## 🛠️ Tecnologias Utilizadas

* **C#**
* **ASP.NET Core Web API**
* **Entity Framework Core**
* **MySQL**
* **AutoMapper**
* **Swagger / OpenAPI**
* **HttpClient**
* **ViaCEP API**
* **Gemini API (IA)**

---

## 📂 Estrutura do Projeto

```bash
EscolaAPI/
│
├── Controllers/
│   ├── AlunosController.cs
│   └── ChatController.cs
│
├── DTO/
│   ├── AlunoDTO.cs
│   └── EnderecoDTO.cs
│
├── Model/
│   ├── Aluno.cs
│   └── Endereco.cs
│
├── Data/
│   └── AppDbContext.cs
│
├── Services/
│   ├── ViaCepServices.cs
│   └── GeminiServices.cs
│
├── Migrations/
├── Program.cs
└── appsettings.json
```

---

## 🌐 Integrações Externas

### 📍 ViaCEP

Utilizada para buscar e preencher automaticamente os dados de endereço a partir do CEP informado.

### 🤖 Gemini API

Utilizada para funcionalidades de chat/IA através de requisições HTTP via `HttpClient`.

---

## ▶️ Como Executar o Projeto

### Pré-requisitos

* .NET SDK 7 ou superior
* MySQL
* Visual Studio ou VS Code

### Passos

```bash
# Clone o repositório
git clone https://github.com/Daniel-Ferreira-2004/EscolaAPI.git

# Acesse a pasta
cd EscolaAPI

# Restaure os pacotes
dotnet restore

# Atualize o banco de dados
dotnet ef database update

# Execute o projeto
dotnet run
```

A API estará disponível em:

```
https://localhost:5001
```

---

## 📑 Documentação da API

A documentação interativa pode ser acessada via **Swagger**:

```
https://localhost:5001/swagger
```

---

## 🎯 Objetivo do Projeto

Projeto desenvolvido com foco em:

* Praticar **ASP.NET Core Web API**
* Trabalhar com **integrações externas**
* Aplicar **Entity Framework Core com MySQL**
* Organizar um backend seguindo boas práticas
* Compor portfólio para **vaga júnior .NET**

---

## 👨‍💻 Autor

**Daniel Ferreira**

* GitHub: [@Daniel-Ferreira-2004](https://github.com/Daniel-Ferreira-2004)

---

⭐ Se este projeto te ajudou ou serviu como referência, deixe uma estrela!
