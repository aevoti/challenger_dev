
# Visão Geral do Projeto:

A API deste projeto foi desenvolvida para dar suporte a um fórum online, onde os usuários podem criar tópicos de discussão e comentários. Ela permite a interação dos usuários por meio de operações básicas de criação, leitura, atualização e exclusão (CRUD) em tópicos e comentários. Além disso, a API oferece recursos para pesquisa, ordenação e filtragem de tópicos.

Principais Funcionalidades:

A API possui as seguintes funcionalidades principais:
    Tópicos:
        Listar todos os tópicos existentes.
        Recuperar informações detalhadas de um tópico específico com base em seu ID.
        Criar um novo tópico.
        Atualizar as informações de um tópico existente.
        Excluir um tópico com base em seu ID.

    Comentários:
        Criar um novo comentário em um tópico específico.
        Atualizar as informações de um comentário existente (apenas se o usuário for o autor do comentário).
        Excluir um comentário existente (apenas se o usuário for o autor do comentário).

    Pesquisa e Ordenação:
        Pesquisar tópicos com base no conteúdo de texto em seus títulos e descrições.
        Ordenar tópicos existentes por data de criação, em ordem crescente ou decrescente.
        
# Tecnologias Utilizadas:
    .NET Core 7.0: A plataforma principal para o desenvolvimento do backend.
    Entity Framework Core: Um ORM (Object-Relational Mapping) para interagir com o banco de dados.
    SQLite: O banco de dados utilizado para armazenar os dados da aplicação.
    ASP.NET Core Web API: Um framework para a criação de APIs web.
    C#: A linguagem de programação utilizada para desenvolver o backend.
    Angular 16.2.0: A estrutura utilizada para desenvolver a interface do usuário (frontend).
    HTML/SCSS/TypeScript: Linguagens e tecnologias front-end comuns para o desenvolvimento de aplicativos da web.

# Executando o Aplicativo Angular (ForumApp):

Para executar o aplicativo Angular, siga os seguintes passos:

Certifique-se de que você possui o Angular CLI instalado globalmente em sua máquina. Se ainda não tiver, você pode instalá-lo usando o seguinte comando:
    npm install -g @angular/cli
Navegue até a raiz do aplicativo Angular (./ForumApp)
Instale as dependências do projeto com o seguinte comando:
    npm install
Após a conclusão da instalação, você pode iniciar o servidor do Angular com o seguinte comando:
    ng serve
O servidor de desenvolvimento será iniciado e estará disponível em 'http://localhost:4200/'


# Executando a API .NET Core (Forum.WebAPI):

Certifique-se de que você possui o .NET SDK 7.0 instalado em sua máquina. Você pode baixá-lo em https://dotnet.microsoft.com/pt-br/download/dotnet/7.0
Navegue até a raiz do aplicativo Angular (./Forum.WebAPI)
Dentro do diretório da API, execute o seguinte comando para restaurar as dependências do projeto:
    dotnet restore
Após a restauração das dependências, você pode iniciar a aplicação .NET Core com o seguinte comando:
    dotnet run

# Lista dos endpoints da API.

    /forum - [GET] - Deve Retornar todos os topicos enviados
    /topico/{id} - [GET] - Deve retornar um topico com id especificado
    /topico - [POST] - Deve cadastrar um novo topico
    /topico/{id} - [PUT] - Deve atualizar um topico com o id especificado
    /topico/{id} - [DELETE] - Deve deletar um topico com o id especificado
    /comentario/{idTopico} - [POST] - Deve cadastrar um novo comentario no topico de id especificado
    /comentario/{idTopico}/{id} - [PUT] - Deve atualizar um comentario com o id especificado (Se o usuário for autor do comentario)
    /comentario/{idTopico}/{id} - [DELETE] - Deve deletar um comentario com o id especificado (Se o usuário for autor do comentario)

Para maiores detalhes do uso de cada endpoint, visitar http://localhost:5148/swagger/index.html com o ambiente rodando.


