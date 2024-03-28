
# Desafio CREA-MT

Primeiramente, obrigado pelo seu interesse em trabalhar conosco. Abaixo você encontrará todos as informações necessárias para iniciar o seu teste.

## 📢 Avisos antes de começar

- **Fork do Repositório**: Realize um fork deste repositório para começar o desenvolvimento em seu próprio espaço de trabalho.

- **Commits**: Mantenha seus commits organizados em seu repositório para facilitar a avaliação do progresso e das decisões tomadas durante o desenvolvimento.

- **Pull Request**: Ao concluir, crie um pull request com suas alterações para avaliação.

- **Recursos**: Você está livre para consultar o Google, Stackoverflow ou projetos particulares para inspiração ou solução de dúvidas.

## 🔗 Links
- [Duvidas com Git e GitHub](https://productoversee.com/tudo-que-voce-queria-saber-sobre-git-e-github-mas-tinha-vergonha-de-perguntar/)
- [Dúvidas em como fazer um Fork](https://github.com/UNIVALI-LITE/Portugol-Studio/wiki/Fazendo-um-Fork-do-reposit%C3%B3rio)
- [Dúvidas em como fazer um Pull Request](https://blog.da2k.com.br/2015/02/04/git-e-github-do-clone-ao-pull-request/)


Fique tranquilo, respire, assim como você, também já passamos por essa etapa. Boa sorte! :)

## 🤌 O que será avaliado
- **Aplicação dos Princípios de DDD em uma API RESTful utilizando .NET Core**: Esperamos uma estrutura clara que siga os princípios de Design Driven Development (DDD) para garantir que a lógica de negócios esteja bem organizada.

- **Uso Eficiente do Entity Framework e SQL Server**: A habilidade para aplicar práticas eficientes no uso do Entity Framework e gerenciar dados no SQL Server é essencial.

- **Implementação de Migrations**: Demonstração de conhecimento em versionamento e gerenciamento de banco de dados.

- **Código Limpo e Organizado**: A clareza do código, incluindo nomenclaturas adequadas, é crucial para a manutenibilidade e compreensão do projeto.

- **Conhecimento e Aplicação de Padrões**: Espera-se que o candidato demonstre conhecimento em padrões de projeto (Design Patterns), DDD e princípios SOLID, aplicando-os de forma efetiva.

- **Justificativa das Escolhas**: Ser capaz de argumentar e justificar as escolhas de design e arquitetura tomadas no projeto.

- **Expertise nas Soluções Apresentadas**: Valorizamos candidatos que apresentem soluções em áreas que dominam, garantindo a entrega de funcionalidades robustas.

- **Modelagem de Dados e Manutenibilidade do Código**: A capacidade de modelar os dados de maneira eficiente e escrever código que seja fácil de manter e evoluir.

- **Tratamento de Erros e Segurança**: A atenção ao tratamento de erros e à segurança da aplicação é fundamental.

- **Arquitetura Pensada e Desacoplamento de Componentes**: Buscamos alguém que estruture bem seus pensamentos antes de codificar e que priorize o desacoplamento de componentes para promover a modularidade do sistema.

## 🚀 Diferenciais

- **Uso de Docker para Containerização**: O conhecimento em Docker será um diferencial, pois indica familiaridade com práticas modernas de desenvolvimento e deployment.

- **Testes Unitários e de Integração**: A habilidade de escrever testes sólidos que validam a funcionalidade da aplicação de maneira isolada e integrada.

- **Propostas de Melhoria na Arquitetura**: Iniciativas para sugerir e implementar melhorias arquitetônicas serão altamente valorizadas.

## 😎 Descrição do Projeto

Este projeto envolve a criação de uma API RESTful desenvolvida com .NET Core, projetada para facilitar a gestão de usuários e clientes de uma empresa, assim como a administração de serviços oferecidos e documentos relacionados. A API desempenhará um papel fundamental em diversas operações, incluindo cadastros, gerenciamento de pagamentos e acesso a documentos específicos.

### Observações importantes
- **Persistência de Dados**: Ao invés de excluir registros diretamente do banco de dados, opte por ocultá-los em consultas. Assegure que cada tabela armazene a data de cadastro e alteração do registro, além do ID do último usuário que modificou ou criou o registro.
- **Acesso a Serviços**: Apenas clientes com anuidades pagas podem solicitar novos serviços. É fundamental oferecer uma funcionalidade para indicar o pagamento de serviços específicos, além de registrar a frequência de downloads dos documentos associados.

- Cada cliente deve pagar uma taxa anual para poder utilizar os serviços da empresa.
- Cada cliente pode solicitar um serviço que ira disponibilizar um documento específico a ele, porem para cada serviço existe um valor a ser cobrado, e somente após o pagamento do serviço o usuário poderá ter acesso ao documento final.

- **Não é necessário a implementação de integração para cobrança e pagamento, apenas ter alguma forma de informar se um serviço ou anuidade de um cliente foi pago.**

- **Gestão de Usuários**
	- Funcionalidades: Ativar/Desativar, Listar, Criar, Editar, Excluir
	- Todo usuário deve ter em seu cadastro: Nome, E-mail, Telefone, CPF ou CNPJ, Senha
	
 - **Gestão de Clientes**
  - Funcionalidades: Ativar/Desativar, Listar, Criar, Editar, Excluir
	- No cadastro do cliente deve ter o Id do usuário que o cadastrou
	- Funcionalidade para informar o pagamento da anuidade de um cliente	
	
 - **Gestão de Documentos**
	- Funcionalidades: Listar, Criar, Excluir
	- Observação: O documento em si pode ser um arquivo PDF, assim como o anexado como exemplo neste repositório ([DOCUMENTO EXEMPLO](https://github.com/CristhyanKo/creatmt-challenge/blob/main/DOCUMENTO%20EXEMPLO.pdf)).
	
 - **Gestão de Serviços**
	- Funcionalidades: Ativar/Desativar, Listar, Criar, Editar, Excluir
	- Todo Serviço deve ter em seu cadastro: Nome, Descrição, Valor e Lista de documentos que vão ser disponibilizados
	
 - **Gestão de Solicitações**
	- Listar, Criar
	- Toda solicitação deve ter em seu cadastro: Id do Cliente, Id do Serviço, Campo informando se a solicitação já foi paga ou não
	- Apenas clientes com a anuidade paga podem solicitar novos serviços
	- Funcionalidade para informar o pagamento do serviço
	- Listagem de Serviços Solicitados por um cliente
		- Exibir os serviços solicitados pelo cliente, exibindo os dados do serviço, se a solicitação feita foi paga ou não e se o cliente já fez o download do documento atrelado ao serviço solicitados
	- Download do documento
		- Disponibilizar o download para solicitações que já foram pagas
		- Registrar a quantidade de vezes que o documento foi baixado
