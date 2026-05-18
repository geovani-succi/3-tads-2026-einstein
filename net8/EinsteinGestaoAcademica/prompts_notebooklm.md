# Prompts para NotebookLM
## Einstein Gestão Acadêmica — JWT, Repositórios e Use Cases

Copie e cole cada prompt individualmente no chat do NotebookLM após subir os dois arquivos
(roteiro_notebooklm.md e tutorial_jwt.pdf) como fontes.

---

## PROMPT 1 — ROTEIRO DE VÍDEO AULA (principal)

```
Com base nos documentos carregados, gere um roteiro completo para uma vídeo aula de
aproximadamente 40 minutos voltada para alunos de graduação em Análise e Desenvolvimento
de Sistemas. A aula é sobre a implementação de uma Web API em .NET 8 com Clean Architecture,
padrão Repository, Use Cases e autenticação JWT.

O roteiro deve ter o seguinte formato:

1. ABERTURA (2 min): saudação, apresentação do tema e do que será aprendido ao final
2. TEORIA — ARQUITETURA (5 min): explicar as 4 camadas do projeto com analogias simples
3. TEORIA — JWT (5 min): o que é, como funciona, estrutura do token, fluxo de autenticação
4. CÓDIGO AO VIVO — PARTE 1 (10 min): entidades Pessoa, Aluno, Usuario e as interfaces
   de repositório e ITokenService no Domínio
5. CÓDIGO AO VIVO — PARTE 2 (8 min): implementações na camada de Dados — DbContext,
   AlunoRepository e UsuarioRepository com SQL parametrizado
6. CÓDIGO AO VIVO — PARTE 3 (8 min): Use Cases (CriarAluno, CriarUsuario, RealizarLogin)
   e TokenService gerando o JWT
7. CÓDIGO AO VIVO — PARTE 4 (5 min): Controllers com Authorize e AllowAnonymous,
   Program.cs com JWT e injeção de dependência
8. DEMO NO SWAGGER (5 min): testar login, copiar token, usar Authorize, testar endpoint protegido
9. ENCERRAMENTO (2 min): recapitulação dos conceitos, próximos passos, call to action

Para cada seção, escreva:
- O que o professor fala (script narrativo completo, tom didático e acessível)
- [TELA] O que deve aparecer na tela naquele momento
- [DESTAQUE] O conceito-chave da seção em uma frase

Escreva o script em português brasileiro, linguagem natural de aula, como se o professor
estivesse falando diretamente para os alunos.
```

---

## PROMPT 2 — SLIDES (PowerPoint / Google Slides)

```
Com base nos documentos, gere o conteúdo completo para uma apresentação de slides sobre
JWT, Repositórios e Use Cases em .NET 8. Para cada slide, escreva:

SLIDE [número]: [Título do Slide]
Conteúdo: [bullets ou texto curto que vai no slide]
Nota do apresentador: [o que o professor fala ao exibir esse slide]

A apresentação deve ter entre 20 e 25 slides, organizada nas seguintes seções:

- Capa (1 slide)
- Agenda (1 slide)
- O que vamos construir — diagrama do projeto (1 slide)
- Clean Architecture: as 4 camadas (2 slides)
- JWT: o que é e como funciona (3 slides — teoria, estrutura do token, fluxo)
- Entidades do Domínio: Pessoa, Aluno, Usuario (2 slides)
- Interfaces de Repositório e ITokenService (2 slides)
- Camada de Dados: DbContext, Repositórios (2 slides)
- Use Cases: o padrão e os 3 casos de uso (2 slides)
- TokenService: geração do JWT passo a passo (2 slides)
- Controllers: Authorize vs AllowAnonymous (2 slides)
- Program.cs: DI + JWT + Swagger (2 slides)
- Demo Swagger (1 slide)
- Boas práticas para produção (1 slide)
- Resumo e próximos passos (1 slide)

Mantenha os bullets curtos (máximo 5 palavras por bullet), evite paredes de texto nos slides.
Inclua sugestões de ícones ou diagramas onde for relevante.
```

---

## PROMPT 3 — LISTA DE EXERCÍCIOS

```
Com base no conteúdo dos documentos, crie uma lista de 10 exercícios práticos para alunos
de graduação consolidarem o aprendizado sobre JWT, Repository Pattern e Use Cases em .NET 8.

Para cada exercício inclua:
- Título
- Nível (Fácil / Médio / Difícil)
- Enunciado claro e detalhado
- Critérios de aceite — o que o aluno deve entregar ou demonstrar
- Dica de por onde começar

Os exercícios devem cobrir:
- 3 exercícios de nível Fácil: criar uma entidade nova, criar um repositório, criar um Use Case
- 4 exercícios de nível Médio: adicionar hash de senha, criar endpoint com role, adicionar
  validação de campos, criar Use Case de busca por ID
- 3 exercícios de nível Difícil: implementar refresh token, adicionar endpoint para listar
  alunos com paginação e proteção JWT, criar testes unitários para um Use Case com mock

Use linguagem clara e direta, como se fosse um professor passando tarefa.
```

---

## PROMPT 4 — QUIZ / AVALIAÇÃO

```
Crie uma avaliação com 15 questões de múltipla escolha sobre os temas dos documentos:
Clean Architecture, JWT, Repository Pattern, Use Cases e Injeção de Dependência em .NET 8.

Para cada questão:
- Escreva o enunciado
- Forneça 4 alternativas (A, B, C, D)
- Indique a alternativa correta
- Escreva uma explicação de 2-3 linhas sobre por que essa é a resposta certa

Distribua as questões assim:
- 3 questões sobre arquitetura em camadas
- 3 questões sobre JWT (estrutura, claims, validação)
- 3 questões sobre Repository Pattern e Injeção de Dependência
- 3 questões sobre Use Cases
- 3 questões sobre código prático (Authorize, AllowAnonymous, Program.cs)

Nível adequado para alunos do 3º semestre de Análise e Desenvolvimento de Sistemas.
```

---

## PROMPT 5 — RESUMO EXECUTIVO (1 PÁGINA)

```
Gere um resumo executivo de uma página sobre o conteúdo dos documentos. O resumo deve ser
adequado para um aluno revisar antes da prova ou antes de uma entrevista de emprego.

Estrutura:
1. O que é Clean Architecture em 3 linhas
2. As 4 camadas e suas responsabilidades em tópicos
3. O que é JWT e o fluxo de autenticação em 5 linhas
4. O que é Repository Pattern em 2 linhas
5. O que é Use Case em 2 linhas
6. O que é Injeção de Dependência em 2 linhas
7. Os 3 erros mais comuns a evitar
8. Checklist de implementação — o que verificar antes de dizer que está pronto

Tom: técnico mas acessível, como um cheat sheet de alta qualidade.
```

---

## PROMPT 6 — PODCAST (AUDIO OVERVIEW PERSONALIZADO)

```
Gere um roteiro de podcast em formato de conversa entre dois apresentadores — um professor
experiente e um aluno curioso — sobre o tema JWT e Clean Architecture em .NET 8.

Duração aproximada: 20 minutos
Tom: descontraído, educativo, com analogias do dia a dia

O roteiro deve cobrir naturalmente, por meio do diálogo:
- Por que organizar o projeto em camadas (analogia com empresa)
- O que é JWT e por que não usar sessão (analogia com crachá de evento)
- A diferença entre autenticação e autorização
- Por que a senha não pode ficar em texto puro no banco
- O que é um Use Case e por que não colocar tudo no Controller
- O papel do Authorize e AllowAnonymous nos Controllers
- A importância da ordem dos middlewares no Program.cs

O aluno deve fazer perguntas naturais do tipo "mas por que não fazer de um jeito mais simples?"
e o professor deve responder com argumentos práticos e sem jargão desnecessário.

Escreva em português brasileiro informal, como numa conversa de podcast técnico.
```

---

## PROMPT 7 — MAPA MENTAL (estrutura textual)

```
Com base nos documentos, gere a estrutura de um mapa mental completo sobre o projeto
Einstein Gestão Acadêmica com JWT. Formate como uma hierarquia de tópicos e subtópicos,
que eu possa importar em ferramentas como Miro, Mermaid ou XMind.

Use o formato de identação abaixo:
[Nó central]
  [Ramo 1]
    [Sub-ramo 1.1]
    [Sub-ramo 1.2]
  [Ramo 2]
    ...

O mapa deve ter o projeto como nó central e 5 ramos principais:
1. Arquitetura (com as 4 camadas)
2. Segurança JWT (claims, assinatura, validade, fluxo)
3. Entidades e Repositórios (Pessoa > Aluno, Usuario, interfaces, implementações)
4. Use Cases (CriarAluno, CriarUsuario, RealizarLogin, TokenService)
5. API (Controllers, Requests, Program.cs, Swagger)
```

---

## DICA DE USO

- **Gerar o Audio Overview**: clique no botão "Audio Overview" do NotebookLM antes de usar
  os prompts — ele gera automaticamente um podcast de ~10 min com base nas fontes.

- **Melhor resultado**: use os Prompts 1 e 2 juntos — roteiro de vídeo + slides se
  complementam perfeitamente para gravar uma aula.

- **Para Reels / Shorts**: pegue o conteúdo do Bloco 3 (JWT) e do Bloco 19 (Demo Swagger)
  do roteiro e peça ao NotebookLM: *"Resuma o bloco de JWT em um roteiro de 60 segundos
  para um vídeo curto no estilo reel educativo."*

- **Para o PDF do tutorial**: suba também o arquivo tutorial_jwt.pdf como segunda fonte.
  O NotebookLM vai cruzar as duas fontes e gerar respostas ainda mais ricas.
