# Roteiro: Repositórios, Use Cases e Autenticação JWT com .NET 8
## Einstein Gestão Acadêmica — Aula Prática de Desenvolvimento de APIs

---

## INSTRUÇÕES PARA O NOTEBOOKLM

Este documento é um roteiro completo de aula para ser usado como fonte no NotebookLM.
Ele cobre a implementação de uma Web API em .NET 8 com Clean Architecture, padrão Repository,
Use Cases e autenticação JWT Bearer Token. O tom é didático, direto e voltado para alunos
de graduação em Tecnologia da Informação.

---

## BLOCO 1 — ABERTURA E CONTEXTO GERAL

Olá, pessoal! Hoje a gente vai ver na prática como construir uma API segura usando .NET 8 com
autenticação JWT. Mas antes de sair escrevendo código, preciso que vocês entendam o porquê de
cada decisão que a gente vai tomar. Porque na nossa área, não basta saber o que escrever —
precisa entender por que está escrevendo daquela forma.

O projeto que a gente vai trabalhar se chama Einstein Gestão Acadêmica. É um sistema de gestão
acadêmica que tem cursos, alunos, professores e usuários com login. A nossa tarefa hoje é
implementar tudo que envolve usuário e aluno: criar conta, fazer login, gerar um token JWT,
e proteger os endpoints que não podem ser acessados por qualquer pessoa.

Mas pra chegar nisso, a gente precisa primeiro entender a estrutura do projeto, que segue um
padrão arquitetural chamado Clean Architecture, ou Arquitetura Limpa.

---

## BLOCO 2 — ARQUITETURA DO PROJETO: AS QUATRO CAMADAS

Imagina um cebola. Ela tem várias camadas, e o miolo está no centro, completamente isolado
do mundo externo. A Clean Architecture funciona assim. O centro, o núcleo, é o Domínio.
Em volta vem a Aplicação. Na camada seguinte ficam os Dados. E na mais externa, a API.

No nosso projeto, isso se traduz em quatro projetos separados dentro da solução:

O primeiro é o projeto de Domínio, chamado EinsteinGestaoAcademica.Dominio. Esse é o coração
do sistema. Aqui ficam as entidades — as classes que representam os objetos do mundo real, como
Curso, Aluno, Pessoa e Usuario. Aqui também ficam as interfaces dos repositórios, que definem
os contratos de acesso a dados. O Domínio não conhece banco de dados, não conhece ASP.NET, não
conhece nada externo. Ele é puro C#.

O segundo é o projeto de Aplicação, EinsteinGestaoAcademica.Aplicacao. Aqui ficam os Use Cases,
que são as operações do sistema: criar aluno, criar usuário, realizar login. Também fica aqui
o TokenService, que gera o JWT. A camada de Aplicação conhece o Domínio, mas não conhece
banco de dados diretamente.

O terceiro é o projeto de Dados, EinsteinGestaoAcademica.Dados. Aqui ficam as implementações
concretas dos repositórios, usando Entity Framework Core com PostgreSQL. Essa camada sabe
como fazer um INSERT, como fazer um SELECT — coisas de banco. Ela depende do Domínio para
saber quais entidades manipular.

O quarto é o projeto de API, EinsteinGestaoAcademica.API. Aqui ficam os Controllers, os
Requests que representam o corpo das requisições HTTP, e o Program.cs que configura tudo.
É a porta de entrada do sistema. Ela depende de todos os outros projetos.

A grande vantagem dessa arquitetura é o isolamento. Se amanhã você quiser trocar o PostgreSQL
por SQL Server, só muda a camada de Dados. O resto fica intacto. Se quiser trocar o ASP.NET
por outra tecnologia de API, só muda a camada de API. O negócio, representado no Domínio e
na Aplicação, não é tocado.

---

## BLOCO 3 — O QUE É JWT E POR QUE USAR

Antes de escrever uma linha de código de autenticação, a gente precisa entender o que é JWT.

JWT significa JSON Web Token. É um padrão, definido pela RFC 7519, para transmitir informações
entre duas partes de forma compacta e segura. Ele é muito usado para autenticação em APIs REST.

Um token JWT é uma string dividida em três partes separadas por pontos. A primeira parte é o
Header, que diz qual algoritmo foi usado para assinar o token. A segunda é o Payload, que
contém os dados do usuário, chamados de claims — como o ID do usuário, o email, quando o
token foi criado e quando ele expira. A terceira é a Signature, que é a assinatura digital —
ela garante que o token não foi adulterado.

O fluxo de autenticação funciona assim: o cliente manda o email e a senha para a API. A API
valida no banco de dados. Se as credenciais estiverem corretas, a API gera um JWT e devolve
para o cliente. A partir daí, em todas as requisições seguintes, o cliente envia esse token
no cabeçalho da requisição, no campo Authorization, com o prefixo Bearer. A API valida o
token automaticamente — sem precisar ir ao banco de dados de novo — e permite ou nega o acesso.

Isso é diferente de sessões tradicionais, onde o servidor precisa guardar o estado de cada
usuário. Com JWT, o servidor é sem estado, stateless. O próprio token carrega todas as
informações necessárias, e a assinatura garante que ele é legítimo.

---

## BLOCO 4 — ENTIDADE PESSOA: A BASE DA HERANÇA

Agora vamos ao código. A primeira coisa que a gente fez foi criar a entidade Pessoa, dentro
do projeto de Domínio, na pasta Entidades.

Por que criar uma classe Pessoa? Porque tanto o Aluno quanto o Professor são, antes de tudo,
pessoas. Eles têm nome, CPF, telefone, cidade e estado em comum. Em vez de duplicar esses
campos nas duas classes, a gente cria uma classe base que centraliza essas propriedades.
Isso é o princípio DRY — Don't Repeat Yourself, Não Se Repita.

A classe Pessoa tem os campos: id, que é a chave primária; nome; cpf; telefone; cidade; e
estado. Simples assim. Ela não sabe nada de banco de dados, não tem atributos do Entity
Framework, é C# puro.

No banco de dados PostgreSQL, ela corresponde à tabela pública chamada "pessoa".

---

## BLOCO 5 — ENTIDADE ALUNO: HERANÇA E NOTMAPPED

Com a Pessoa pronta, a gente cria a entidade Aluno. Aluno herda de Pessoa. Na prática, isso
significa que a classe Aluno automaticamente já tem os campos id, nome, cpf, telefone, cidade
e estado — ela herda tudo de Pessoa sem precisar declarar novamente.

O Aluno acrescenta dois campos próprios. O primeiro é id_curso, que é a chave estrangeira para
o curso no qual o aluno está matriculado. O segundo é o objeto Curso em si, marcado com o
atributo NotMapped.

O que é esse NotMapped? É um atributo do .NET que instrui o Entity Framework a ignorar esse
campo na hora de gerar o SQL. Ele existe só em memória, para facilitar o acesso ao objeto
Curso quando a gente já tiver ele carregado. Mas na tabela aluno do banco, não existe uma
coluna "curso" — existe só a coluna id_curso, que é a chave estrangeira.

Essa técnica de ter um campo de navegação em memória junto com a chave estrangeira é muito
comum quando se usa Entity Framework Core, e o NotMapped é a forma explícita de dizer ao
framework para não persistir aquela propriedade.

---

## BLOCO 6 — ENTIDADE USUARIO: SEPARAÇÃO DE RESPONSABILIDADES

Agora vem uma decisão de design importante: por que a entidade Usuario é separada de Pessoa?

Imagine o seguinte cenário: um aluno é cadastrado no sistema — nome, CPF, curso, tudo certo.
Mas ele ainda não tem uma conta de acesso ao sistema. Em outro momento, o administrador cria
um login para esse aluno. Se a gente misturasse os campos de credenciais diretamente na classe
Pessoa ou na classe Aluno, toda Pessoa teria que ter email e senha, mesmo aquelas que não
precisam de acesso.

Separando em Usuario, a gente mantém as responsabilidades claras. A entidade Usuario tem: id,
id_professor que é opcional e pode ser nulo, id_aluno que também é opcional e pode ser nulo,
email e senha. Assim, um usuário pode ser vinculado a um professor, ou a um aluno, ou ser
um administrador sem vínculo — tudo flexível.

No C#, os campos id_professor e id_aluno são declarados como int interrogação, o que significa
que são tipos anuláveis — eles podem ou não ter valor.

---

## BLOCO 7 — INTERFACES DE REPOSITÓRIO: O CONTRATO DO DOMÍNIO

Aqui chegamos em um dos conceitos mais importantes da Clean Architecture: as interfaces de
repositório ficam no Domínio, não na camada de Dados.

Por quê? Porque o Domínio define o que precisa ser feito — o contrato. A camada de Dados
define como isso é feito — a implementação. Isso é a inversão de dependência, o D do SOLID.

A interface IAlunoRepository fica no projeto de Domínio, na pasta Repositorios. Ela declara
um único método: Criar, que recebe um Aluno e retorna uma Task, pois é assíncrono.

A interface IUsuarioRepository fica no mesmo lugar e declara dois métodos: Criar, para inserir
um novo usuário, e ObterUsuarioPorEmailESenha, que recebe uma string de email e uma string de
senha e retorna uma Task que pode trazer um Usuario ou nulo.

Essas interfaces são o vocabulário que a camada de Aplicação usa para falar com o banco de
dados, sem saber nada sobre banco de dados. Lindo, né?

---

## BLOCO 8 — INTERFACE ITOKENSERVICE: ABSTRAINDO O JWT

Seguindo o mesmo princípio, a interface ITokenService também fica no Domínio. Ela declara um
único método: GerarToken, que recebe um Usuario e retorna uma string, que será o JWT.

Por que a interface fica no Domínio e não na Aplicação? Porque o Domínio define os contratos
fundamentais do sistema. Se amanhã a gente mudar de JWT para outra tecnologia de token, o
contrato no Domínio permanece. Só a implementação concreta na camada de Aplicação muda.

---

## BLOCO 9 — APPLICATION DB CONTEXT: MAPEANDO AS ENTIDADES

Agora entramos na camada de Dados. O ApplicationDbContext é a classe principal do Entity
Framework Core. Ela é a ponte entre as classes C# e as tabelas do banco de dados.

Quando a gente adicionou Aluno e Usuario ao projeto, precisou registrá-los no DbContext.
A gente adicionou um DbSet de Aluno, chamado Alunos, e um DbSet de Usuario, chamado Usuarios.
Esses DbSets representam as tabelas do banco.

No método OnModelCreating, a gente configura o mapeamento fino. Para o Aluno, dizemos que
ele mapeia para a tabela "aluno" no schema "public" do PostgreSQL, e ignoramos a propriedade
"curso" que está marcada com NotMapped.

Para o Usuario, dizemos que ele mapeia para a tabela "usuario" no schema "public", e
definimos o "id" como chave primária.

O Entity Framework usa essas configurações para saber exatamente como traduzir as operações
C# para comandos SQL.

---

## BLOCO 10 — ALUNOREPOSITORY: IMPLEMENTANDO O CONTRATO

O AlunoRepository fica na camada de Dados e implementa a interface IAlunoRepository do Domínio.
Ele recebe o ApplicationDbContext por injeção de dependência — nunca cria o contexto
manualmente. Isso é importante: quem gerencia o ciclo de vida do contexto é o container de
injeção de dependência do ASP.NET Core.

O método Criar é simples: chama AddAsync para adicionar o aluno ao rastreamento do Entity
Framework, e depois chama SaveChangesAsync para persistir no banco. Esse SaveChangesAsync é
o momento em que o Entity Framework gera e executa o comando INSERT no PostgreSQL.

---

## BLOCO 11 — USUARIOREPOSITORY: SQL BRUTO PARAMETRIZADO

O UsuarioRepository é um pouco mais interessante porque usa SQL bruto no método
ObterUsuarioPorEmailESenha.

Por que SQL bruto aqui? Às vezes, por questões de performance ou necessidade específica, a
gente precisa escrever o SQL na mão, sem deixar o Entity Framework gerar. O método
FromSqlRaw permite isso.

O detalhe mais importante aqui é a forma como os parâmetros são passados. A gente usa {0}
e {1} como placeholders, e os valores reais são passados como argumentos separados. Isso faz
o Entity Framework usar prepared statements internamente, que são resistentes a SQL Injection.

Nunca faça interpolação direta de string dentro do SQL. Nunca escreva algo como cifrão-ponto-
pois-WHERE-email-igual-email. Isso abre uma brecha enorme de SQL Injection, onde um atacante
pode enviar uma string maliciosa como email e manipular a query.

Com os parâmetros {0} e {1}, o Entity Framework garante que os valores são tratados como
dados, nunca como código SQL.

---

## BLOCO 12 — USE CASES: A CAMADA DE ORQUESTRAÇÃO

Os Use Cases são a estrela da Clean Architecture. Eles representam as operações do sistema
de forma isolada e testável.

Cada Use Case tem uma interface que define o contrato, e uma implementação que executa a lógica.
A gente tem três Use Cases novos nessa aula: CriarAluno, CriarUsuario e RealizarLogin.

O CriarAlunoUseCase é o mais simples: recebe um Aluno, chama o repositório para criar.
O CriarUsuarioUseCase é igual: recebe um Usuario, chama o repositório para criar.
O RealizarLoginUseCase recebe email e senha, chama o repositório para buscar o usuario
correspondente e retorna o resultado — seja um Usuario ou nulo.

Percebam que os Use Cases não sabem nada de HTTP, nada de banco de dados. Eles falam com
o Domínio por meio das interfaces. Quem decide o que fazer com o resultado é o Controller,
na camada de API.

---

## BLOCO 13 — TOKENSERVICE: O CORAÇÃO DA AUTENTICAÇÃO JWT

O TokenService é a implementação concreta do ITokenService. É aqui que o JWT é gerado de
fato.

O método GerarToken recebe um Usuario e executa cinco passos.

Primeiro, cria os claims — as informações que vão dentro do token. A gente inclui o ID do
usuário e o email. Os claims são pares de chave e valor que ficam no Payload do JWT.

Segundo, cria a chave de assinatura, lendo o valor do campo Jwt:Key do arquivo
appsettings.json. Essa chave deve ter pelo menos 32 caracteres para o algoritmo HMAC-SHA256.

Terceiro, cria as credenciais de assinatura, combinando a chave com o algoritmo HmacSha256.

Quarto, constrói o token JWT com o JwtSecurityToken, definindo quem emitiu o token
— o Issuer —, para quem é o token — o Audience —, os claims, e a validade de 8 horas a
partir do momento da geração.

Quinto e último, serializa o token para uma string usando JwtSecurityTokenHandler. Essa string
é o que o cliente vai receber e usar nas próximas requisições.

---

## BLOCO 14 — REQUESTS: OS DTOs DE ENTRADA

Os Requests, ou DTOs — Data Transfer Objects — representam exatamente o que o cliente deve
enviar no corpo da requisição HTTP. Eles são classes simples, sem lógica.

Por que separar os Requests das Entidades do Domínio? Porque o cliente não precisa enviar tudo
que a entidade tem. Por exemplo, o campo "id" do Aluno é gerado pelo banco — o cliente não
manda o id. O cliente manda nome, cpf, telefone, cidade, estado e id_curso. Isso é o
CriarAlunoRequest.

O CriarUsuarioRequest tem os campos id_professor, id_aluno — ambos opcionais —, email e senha.

O RealizarLoginRequest tem só email e senha. Simples.

O Controller é responsável por converter o Request em uma Entidade do Domínio antes de
chamar o Use Case.

---

## BLOCO 15 — ALUNOSCONTROLLER: PROTEGENDO O ENDPOINT

O AlunosController está na camada de API. Ele tem o atributo ApiController, que configura
comportamentos padrão do ASP.NET Core, e o atributo Authorize, que protege todos os endpoints
do controller.

O atributo Route define a rota base como api/alunos.

O único endpoint é o método CriarAluno, que responde ao verbo HTTP POST. Ele recebe no corpo
da requisição um CriarAlunoRequest, cria uma entidade Aluno a partir dos dados do Request, e
chama o Use Case.

O ponto chave aqui é o atributo Authorize na classe. Qualquer pessoa que tente chamar esse
endpoint sem um JWT válido no cabeçalho vai receber um HTTP 401 Unauthorized automaticamente.
O ASP.NET Core faz essa verificação antes mesmo de entrar no método.

---

## BLOCO 16 — USUARIOSCONTROLLER: A COMBINAÇÃO DE AUTHORIZE E ALLOWANONYMOUS

O UsuariosController é mais interessante porque combina dois atributos: Authorize na classe
inteira, e AllowAnonymous em um endpoint específico.

A lógica é: o controller inteiro é protegido por padrão. Mas o endpoint de login — POST
api/usuarios/login — precisa ser público, porque é justamente ele que gera o token. Uma
pessoa que ainda não tem token não pode logar.

Então, o método RealizarLogin tem o atributo AllowAnonymous, que diz ao ASP.NET Core:
este endpoint específico pode ser acessado sem autenticação, mesmo que o controller exija.

O fluxo do login é: recebe email e senha no corpo da requisição, chama o RealizarLoginUseCase,
que vai ao banco buscar o usuário. Se o resultado for nulo — ou seja, nenhum usuário encontrado
com aquelas credenciais —, retorna HTTP 401 Unauthorized. Se encontrou, chama o TokenService
para gerar o JWT e retorna HTTP 200 com o token no corpo da resposta, em formato JSON.

O endpoint de criar usuário, no POST api/usuarios sem o sufixo login, está protegido. Isso
significa que só quem já tem um JWT válido pode criar novos usuários — geralmente, um
administrador autenticado.

---

## BLOCO 17 — PROGRAM.CS: A COLA DE TUDO

O Program.cs é onde tudo se junta. Ele configura os serviços da aplicação e o pipeline de
requisições HTTP.

Vamos passar pelas partes mais importantes.

A primeira é a configuração da autenticação JWT. A gente chama AddAuthentication com o esquema
JwtBearerDefaults.AuthenticationScheme, e encadeia AddJwtBearer com as opções de validação.

As opções de validação configuram cinco coisas: ValidateIssuer, que verifica se o campo "iss"
do token bate com o valor do appsettings; ValidateAudience, que verifica o campo "aud";
ValidateLifetime, que verifica se o token não expirou; ValidateIssuerSigningKey, que verifica
se a assinatura é válida; e por último os valores concretos para o Issuer, Audience e a chave
de assinatura, todos lidos do appsettings.json.

A segunda parte importante é o registro de todas as dependências no container de injeção de
dependência. Para cada interface, a gente diz qual implementação concreta deve ser usada.
Por exemplo: AddTransient de ICriarAlunoUseCase para CriarAlunoUseCase. Isso significa que
toda vez que alguém pedir um ICriarAlunoUseCase, o container vai criar e injetar um
CriarAlunoUseCase.

O AddTransient significa que uma nova instância é criada a cada vez que é pedida. É o
comportamento mais comum para Use Cases e Repositórios.

A terceira parte é a configuração do Swagger para suportar o Bearer Token. Adicionamos uma
definição de segurança do tipo Bearer no campo Authorization, e um requisito de segurança
global. Isso faz aparecer o botão "Authorize" na interface do Swagger, onde o desenvolvedor
pode colar o JWT e fazer chamadas autenticadas diretamente pelo navegador.

A quarta parte é o pipeline de middlewares, e aqui a ordem é CRÍTICA. A gente chama
UseAuthentication antes de UseAuthorization. Isso não é opcional. O middleware de autenticação
identifica quem é o usuário — lê o token, valida, extrai as informações. O middleware de
autorização decide o que esse usuário pode fazer — verifica os atributos Authorize. Se você
inverter a ordem, os atributos Authorize nunca vão funcionar.

---

## BLOCO 18 — INJEÇÃO DE DEPENDÊNCIA: ENTENDENDO O CONCEITO

Vou fazer uma pausa para explicar melhor a Injeção de Dependência, porque ela está em todo
lugar nesse código.

Injeção de Dependência é um padrão de design onde um objeto não cria suas próprias dependências
— elas são "injetadas", geralmente pelo construtor.

Olha o CriarAlunoUseCase. Ele precisa de um IAlunoRepository para funcionar. Em vez de fazer
"new AlunoRepository()" dentro do Use Case, o repositório é declarado como parâmetro do
construtor. Quem vai fornecer o repositório é o container de injeção de dependência do
ASP.NET Core, configurado no Program.cs.

Por que isso é bom? Porque o Use Case não está acoplado a nenhuma implementação específica.
Ele conhece apenas a interface. Nos testes, posso substituir o repositório real por um mock,
sem mudar uma linha do Use Case.

O ASP.NET Core resolve a cadeia completa automaticamente. Quando uma requisição chega ao
AlunosController, o framework cria o controller injetando o ICriarAlunoUseCase. Para criar o
Use Case, injeta o IAlunoRepository. Para criar o repositório, injeta o ApplicationDbContext.
E o DbContext já foi configurado com a connection string do PostgreSQL no AddDbContext.

---

## BLOCO 19 — DEMONSTRAÇÃO PRÁTICA NO SWAGGER

Vamos ver como testar tudo isso na prática usando o Swagger.

Primeiro, abra o Swagger acessando a rota /swagger no navegador enquanto a API está rodando.
Você vai ver os endpoints organizados por controller: Alunos, Cursos e Usuarios.

Passo um: tente chamar o POST /api/alunos sem nenhum token. O retorno será um HTTP 401.
Isso confirma que o endpoint está protegido.

Passo dois: chame o POST /api/usuarios/login com um email e senha válidos, já cadastrados
no banco. Se as credenciais estiverem corretas, a resposta será um JSON com o campo Token,
contendo a string do JWT.

Passo três: copie o token. No Swagger, clique no botão "Authorize" no canto superior direito.
Cole o token no campo e clique em Authorize. O Swagger vai incluir esse token em todas as
chamadas seguintes.

Passo quatro: tente chamar o POST /api/alunos novamente. Agora, com o token no cabeçalho,
a requisição será processada e você receberá um HTTP 201 Created.

Passo cinco: para ilustrar a validação do token, modifique um caractere do token no Swagger
e tente novamente. O retorno será 401 — a assinatura não bate mais.

---

## BLOCO 20 — PONTOS DE ATENÇÃO E EVOLUÇÃO FUTURA

Para fechar, quero destacar alguns pontos que são importantes para produção e que nessa
implementação didática foram simplificados.

O primeiro é o armazenamento de senhas. Nessa versão, a senha é guardada em texto puro no
banco. Em produção, isso nunca deve acontecer. A senha deve ser transformada em um hash
usando um algoritmo como BCrypt ou PBKDF2, com sal — um valor aleatório adicionado antes
de fazer o hash. Assim, mesmo que o banco seja comprometido, as senhas reais não são expostas.

O segundo é a chave JWT. Ela está no appsettings.json. Em produção, segredos nunca devem
ficar em arquivos versionados. Use variáveis de ambiente, Azure Key Vault, AWS Secrets Manager
ou outro cofre de segredos.

O terceiro é a validade do token. Oito horas pode ser muito para alguns casos de uso.
Tokens com validade curta — 15 a 60 minutos — combinados com refresh tokens aumentam a
segurança, pois um token roubado fica inútil rapidamente.

O quarto é o HTTPS. Em produção, a API deve obrigatoriamente usar HTTPS para que os tokens
não trafeguem em texto claro na rede.

---

## BLOCO 21 — RESUMO DO FLUXO COMPLETO

Vamos consolidar tudo num resumo do fluxo completo.

Para criar um aluno, o cliente precisa estar autenticado. Ele envia um POST para /api/alunos
com um JWT válido no header Authorization Bearer. O AlunosController recebe a requisição,
o middleware de autenticação já validou o token, o atributo Authorize permite o acesso, o
controller constrói um objeto Aluno a partir do CriarAlunoRequest e chama o CriarAlunoUseCase.
O Use Case chama o IAlunoRepository. O AlunoRepository usa o Entity Framework para fazer o
INSERT na tabela aluno do PostgreSQL. O controller retorna HTTP 201.

Para fazer login, o cliente envia um POST para /api/usuarios/login — esse é o único endpoint
público. O UsuariosController chama o RealizarLoginUseCase com email e senha. O Use Case chama
o IUsuarioRepository. O UsuarioRepository executa o SELECT com o email e senha usando SQL
parametrizado. Se encontrar, retorna o Usuario. Se não encontrar, retorna nulo. O controller
verifica: se nulo, retorna 401. Se encontrou, chama o ITokenService. O TokenService gera o
JWT com os claims do usuário, assina com HMAC-SHA256, define validade de 8 horas e retorna
a string do token. O controller retorna HTTP 200 com o token.

---

## BLOCO 22 — ENCERRAMENTO

Pessoal, o que a gente viu hoje foi muito além de um simples login. A gente explorou como
organizar um projeto seguindo princípios sólidos de arquitetura e design, como separar
responsabilidades entre as camadas, como usar interfaces para criar código desacoplado e
testável, e como implementar autenticação JWT de ponta a ponta numa Web API.

Esses conceitos — Clean Architecture, SOLID, Repository Pattern, Use Cases, Injeção de
Dependência — são amplamente usados no mercado. Não importa se você vai trabalhar com .NET,
Java, Python ou qualquer outra tecnologia: esses princípios se aplicam em todos os lugares.

A implementação que fizemos serve como base. Daqui pra frente, os passos naturais são: adicionar
hash de senhas, implementar refresh tokens, adicionar roles de autorização para diferenciar
o que alunos e administradores podem fazer, e adicionar validações mais robustas nos campos
de entrada.

Se tiver dúvida sobre qualquer parte, reveja o código. Tudo está conectado de forma explícita
e intencional. Cada arquivo tem uma razão de existir.

Até a próxima aula!

---

## GLOSSÁRIO DE TERMOS TÉCNICOS

**JWT — JSON Web Token**: Padrão aberto para transmitir informações entre partes de forma
compacta e segura. Composto por Header, Payload e Signature, separados por pontos.

**Bearer Token**: Esquema de autenticação HTTP onde o token é enviado no header Authorization
com o prefixo "Bearer". Quem "porta" o token tem acesso.

**Claims**: Informações contidas no Payload de um JWT, como ID do usuário, email, roles.
São pares de chave e valor.

**Clean Architecture**: Arquitetura de software que organiza o código em camadas concêntricas,
onde as camadas internas não conhecem as externas. Proposta por Robert C. Martin.

**Use Case**: Representa uma operação específica do sistema, como "criar aluno" ou "realizar
login". Contém a lógica de negócio orquestrada.

**Repository Pattern**: Padrão de projeto que abstrai o acesso a dados atrás de uma interface,
separando a lógica de negócio da lógica de persistência.

**Injeção de Dependência**: Padrão onde as dependências de um objeto são fornecidas externamente,
em vez de criadas pelo próprio objeto.

**Entity Framework Core**: ORM oficial da Microsoft para .NET. Mapeia classes C# para tabelas
de banco de dados e gera SQL automaticamente.

**FromSqlRaw**: Método do Entity Framework Core para executar SQL bruto de forma segura,
usando parâmetros para evitar SQL Injection.

**SOLID**: Conjunto de cinco princípios de design orientado a objetos. O D — Princípio de
Inversão de Dependência — é o mais visível nesse projeto.

**DRY — Don't Repeat Yourself**: Princípio que diz para nunca duplicar conhecimento ou
lógica no código.

**AddTransient**: Ciclo de vida de injeção de dependência onde uma nova instância do serviço
é criada toda vez que é solicitada.

**HMAC-SHA256**: Algoritmo de assinatura digital usado no JWT. Usa uma chave secreta para
criar e verificar a assinatura.

**SQL Injection**: Ataque onde código SQL malicioso é inserido em campos de entrada para
manipular queries do banco de dados. Prevenido com parâmetros.

**Middleware**: Componentes do pipeline de requisições HTTP no ASP.NET Core. Cada middleware
processa a requisição e passa para o próximo.

**NotMapped**: Atributo do Entity Framework Core que instrui o ORM a ignorar uma propriedade
na hora de mapear para o banco de dados.

**AllowAnonymous**: Atributo do ASP.NET Core que libera um endpoint específico para acesso
sem autenticação, mesmo que o controller exija.

**Nullable (int?)**: Em C#, o ponto de interrogação após um tipo de valor indica que ele pode
ser nulo. Permite campos opcionais em entidades e DTOs.

---

## ESTRUTURA DE ARQUIVOS CRIADOS

Para referência, os arquivos criados ou modificados nessa aula foram:

Na camada de Domínio:
- Entidades/Pessoa.cs — classe base com campos comuns
- Entidades/Aluno.cs — herda de Pessoa, adiciona id_curso
- Entidades/Usuario.cs — credenciais de acesso ao sistema
- Repositorios/IAlunoRepository.cs — contrato de persistência do aluno
- Repositorios/IUsuarioRepository.cs — contrato com Criar e ObterPorEmailESenha
- ITokenService.cs — contrato de geração de token

Na camada de Dados:
- Data/ApplicationDbContext.cs — adicionados DbSet de Aluno e Usuario, mapeamentos
- Data/Repositorios/AlunoRepository.cs — implementação com EF Core
- Data/Repositorios/UsuarioRepository.cs — implementação com FromSqlRaw parametrizado

Na camada de Aplicação:
- Services/Alunos/CriarAluno/ICriarAlunoUseCase.cs
- Services/Alunos/CriarAluno/CriarAlunoUseCase.cs
- Services/Usuarios/CriarUsuario/ICriarUsuarioUseCase.cs
- Services/Usuarios/CriarUsuario/CriarUsuarioUseCase.cs
- Services/Usuarios/RealizarLogin/IRealizarLoginUseCase.cs
- Services/Usuarios/RealizarLogin/RealizarLoginUseCase.cs
- Services/Token/TokenService.cs — geração JWT com claims, chave e validade

Na camada de API:
- Requests/CriarAlunoRequest.cs
- Requests/CriarUsuarioRequest.cs
- Requests/RealizarLoginRequest.cs
- Controllers/AlunosController.cs — com Authorize
- Controllers/UsuariosController.cs — com Authorize + AllowAnonymous no login
- Program.cs — AddAuthentication JWT, AddSwaggerGen com Bearer, AddTransient de tudo

---

*Roteiro gerado com base no código-fonte do projeto EinsteinGestaoAcademica — .NET 8 — Branch: camadas-com-autenticacao*
