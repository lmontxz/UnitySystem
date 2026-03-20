🧩 Arquitetura Multitenant
O Unity System utiliza uma arquitetura de "Banco de Dados Compartilhado com Chave de Discriminação".

Isolamento de Dados: Cada requisição exige um identificador da empresa.

Escalabilidade: Novas empresas podem ser adicionadas sem alterar o código principal.

Exemplo de Chamada:localhost:5000/status
localhost:5000/produtos?empresa=fornodouro
localhost:5000/produtos?empresa=lflupas
localhost:5000/produtos?empresa=escola