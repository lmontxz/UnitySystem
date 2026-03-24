# 📦 Stock Management API

API desenvolvida para **controle de estoque**, permitindo o gerenciamento de produtos, entradas, saídas e organização de itens de forma eficiente.

O sistema foi projetado com arquitetura **multitenant**, possibilitando que múltiplas empresas utilizem a mesma aplicação com isolamento de dados.


## 🚀 Objetivo

Este projeto foi desenvolvido para a disciplina de **Sistemas Distribuídos**, ministrada pelo professor Alexandre Montanha, com o objetivo de demonstrar a construção de uma API:

* Escalável
* Reutilizável
* Preparada para múltiplos clientes (empresas)
* Aplicável em cenários reais de mercado


## 🏗️ Arquitetura

A API utiliza o modelo:

> **Banco de Dados Compartilhado com Chave de Discriminação**


## 🧩 Multitenancy

### 🔐 Isolamento de Dados

Cada requisição deve informar a empresa através do parâmetro:

```http
?empresa=nome_da_empresa
```

Isso garante que cada empresa tenha acesso apenas aos seus próprios dados.


### 📈 Escalabilidade

Novas empresas podem ser adicionadas facilmente, sem necessidade de alterações no código da aplicação.


## 📁 Estrutura do Projeto

O projeto segue o princípio de **separação de responsabilidades**, dividido em camadas:

```
src/
├── Controllers/    # Endpoints da API
├── Services/       # Regras de negócio
├── Repositories/   # Acesso ao banco de dados
├── Models/         # Entidades do sistema
├── DTOs/           # Objetos de entrada e saída
├── Middlewares/    # Controle de requisições (multitenant)
└── Config/         # Configurações gerais
```


## 🔗 Exemplos de Requisições

```http
GET http://localhost:5000

GET http://localhost:5000/lista
```


## ⚙️ Funcionalidades

* 📦 Cadastro de produtos
* 📥 Registro de entrada de estoque
* 📤 Registro de saída de estoque
* 📊 Consulta de itens disponíveis
* 🏢 Separação de dados por empresa


## 🧠 Conceitos Aplicados

* Arquitetura Multitenant
* APIs RESTful
* Sistemas Distribuídos
* Separação de responsabilidades
* Escalabilidade


## 🛠️ Tecnologias Utilizadas


* .NET
* C#
* Entity Framework (se aplicável)
* SQL Server / MySQL


## 📌 Observações

Este projeto foi desenvolvido como parte da disciplina de Sistemas Distribuídos, abordando conceitos aplicados em sistemas reais de controle de estoque.

