
# Task - Prática

Olá! 👋
Primeiramente, parabéns por ter chegado até aqui! Essa tem sido uma jornada seletiva de altíssimo nível, você se destacou na Etapa de Cadastro e não temos dúvidas de que irá brilhar neste desafio!
E como funcionará a Task Prática?
 
Dividimos em duas etapas, para que você possa aplicar seus conhecimentos e práticas, em cada uma das frentes de desenvolvimento.
O desafio consiste em uma pequena implementação para avaliarmos seu conhecimento em Back-End (.NET, C#) e Front-End (HTML5, CSS, JavaScript e Angular)

Para realizá-lo, você deverá dar um fork neste repositório e depois cloná-lo em alguma pasta de sua preferência, na máquina que estiver realizando o teste.
Crie um branch com seu nome a partir da master e, quando finalizar todo o desenvolvimento, você deverá enviar um pull-request com sua versão.

Caso precise consultar algum material de apoio, recomendamos:<br>
Para o back: https://www.macoratti.net/19/10/ang7_apinc1.htm<br>
Para o front: https://www.youtube.com/@loianegroner <br>

# O Desafio
## Back-End/.NET
A primeira etapa será o desenvolvimento back-end!

Descrição:

O Objetivo dessa etapa é criar apis para um fórum onde um usuário pode fazer o CRUD básico de um tópico e realizar comentários dentro desse tópico, como uma discussão. A parte referente aos usuários não precisa ser implementado, podendo mockar os dados no front ou no back de acordo com a sua preferencia, sugerimos utilizar a seguinte estrutura para usuários:

    {
	    Id: int;
	    Nome: string;
	    Email: string;
	    Foto: string;
    }

**Obrigatorio**  - Você deverá desenvolver as seguintes rotas em .Net Core(Versão de sua preferencia):

    /forum - [GET] - Deve Retornar todos os topicos enviados
    /topico/{id} - [GET] - Deve retornar um topico com id especificado
    /topico - [POST] - Deve cadastrar um novo topico
    /topico/{id} - [PUT] - Deve atualizar um topico com o id especificado
    /topico/{id} - [DELETE] - Deve deletar um topico com o id especificado
    /comentario/{idTopico} - [POST] - Deve cadastrar um novo comentario no topico de id especificado
    /comentario/{idTopico}/{id} - [PUT] - Deve atualizar um comentario com o id especificado (Se o usuário for autor do comentario)
    /comentario/{idTopico}/{id} - [DELETE] - Deve deletar um comentario com o id especificado (Se o usuário for autor do comentario)

Você pode utilizar um banco de dados local SQL Server para a persistência dos dados.

## Front-End /Angular
Para a segunda etapa do teste, você deverá desenvolver uma SPA (Single Page Application) utilizando Angular. Nela, deverá ser possível:

**Obrigatorio**  - Você deverá desenvolver no minimo uma tela com as seguintes funcionalidades:

- Ver lista de tópicos
- Criar tópico
- Editar um tópico existente
- Excluir um tópico existente
- Ordenar os tópicos existentes (Data Crescente e Decrescente) 
- Pesquisar um tópico (Conteúdo do texto do tópico)
- Criar comentário
- Editar comentário
- Excluir comentário
- Visualizar um tópico e seus comentários

Seguindo o exemplo:
![image](https://github.com/aevoti/challenger_dev/assets/13247527/f6a63f36-aab0-4422-b92f-da8c2da48a4a)
![image](https://github.com/aevoti/challenger_dev/assets/13247527/fd47382f-db77-4dcf-ab83-0aa9885cc0e0)
![image](https://github.com/aevoti/challenger_dev/assets/13247527/25c7520d-bcdf-4253-ab3d-370ae583b130)
![image](https://github.com/aevoti/challenger_dev/assets/13247527/de1cd467-1bf2-4567-9aff-48a188fe18d0)


### Observações importantes:
Você pode desenvolver o front na sua versão do Angular de preferência, se atentando para utilizar uma versão superior ou igual a 6.<br>
Você pode modelar as classes de comentário e de tópicos da forma que achar mais conveniente para o desenvolvimento.<br>
Você pode fazer adequações no front visando melhorar a experiencia do usuário, mas tente manter o mais fidedigno ao protótipo.<br>
Você pode usar ferramentas de automação, mas deverá informar o uso completo para funcionamento do desafio.<br><br>

Serão considerados pontos positivos, porém não são obrigatórios: 

 1. Diferencial - Escrever testes unitarios para os endpoints;
 2. Diferencial - Utilização de documentação para o mini projeto;
 3. Diferencial - Publicação do projeto em algum ambiente online;
 4. Diferencial - Filtragem por texto no back;
 5. Diferencial - Ordenação dos topicos no back;
 6. Diferencial - Design patterns e rotinas para testes;
 7. Diferencial - Boas Práticas de orientação a objetos;

<br>

Qualquer problema ou dificuldade com o repositório, você pode entrar em contato conosco pelos e-mails, carlos.pedroni@aevo.com.br ou rh@aevo.com.br para que possamos sanar todas as dúvidas!
<br><br>
Estamos sempre em busca de melhoria. Por isso, caso tenha alguma sugestão, fique à vontade para compartilhar conosco! Boa sorte! 💛




