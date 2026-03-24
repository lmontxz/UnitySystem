🧩 Unity System API

API desenvolvida para gerenciamento de recursos, projetada com arquitetura multitenant, permitindo atender múltiplas empresas em uma única aplicação.

O sistema é genérico e pode ser aplicado em diversos contextos, como:

📚 Gerenciamento de livros
📦 Controle de estoque
🏫 Gestão escolar
🏢 Sistemas empresariais em geral

🚀 Objetivo

Este projeto foi desenvolvido como parte da disciplina de Sistemas Distribuídos, ministrada pelo professor Alexandre Montanha.

O objetivo é demonstrar a construção de uma API escalável, reutilizável e preparada para múltiplos clientes (empresas), seguindo boas práticas de arquitetura.

🏗️ Arquitetura Multitenant

O Unity System utiliza o modelo:

Banco de Dados Compartilhado com Chave de Discriminação

🔐 Isolamento de Dados

Cada requisição deve conter um identificador de empresa (empresa), garantindo que os dados sejam filtrados corretamente.

📈 Escalabilidade

Novas empresas podem ser adicionadas sem necessidade de alteração no código principal, apenas cadastrando um novo identificador.

🔗 Exemplos de Requisições
GET http://localhost:5000/status
GET http://localhost:5000/produtos?empresa=fornodouro
GET http://localhost:5000/produtos?empresa=lflupas
GET http://localhost:5000/produtos?empresa=escola

⚙️ Funcionamento
Todas as requisições dependem do parâmetro empresa
Esse parâmetro define o contexto dos dados
A API filtra automaticamente as informações com base nesse identificador

🧠 Conceitos Aplicados
Arquitetura Multitenant
APIs RESTful
Separação de responsabilidades
Escalabilidade horizontal
Sistemas distribuídos
