-- =============================================================
-- Script: CriarBancoDados.sql
-- Banco:  PostgreSQL
-- Desc:   Cria o banco EinsteinGestaoAcademica e todas as tabelas
--         com seus relacionamentos, baseado nas entidades do domínio
-- =============================================================

-- 1. Criar o banco de dados
CREATE DATABASE "EinsteinGestaoAcademica";

-- ATENÇÃO: Após executar o CREATE DATABASE, conecte-se ao banco
-- criado antes de executar o restante do script.
-- No psql:  \c EinsteinGestaoAcademica
-- No DBeaver/pgAdmin: selecione o banco e execute a partir daqui.

-- =============================================================
-- 2. Tabela Pessoa (base para Aluno e Professor - estratégia TPT)
--    Campos comuns herdados pelas subclasses
-- =============================================================
CREATE TABLE pessoa (
    id        SERIAL       PRIMARY KEY,
    nome      VARCHAR(150) NOT NULL,
    cpf       CHAR(11)     NOT NULL UNIQUE,
    telefone  VARCHAR(20),
    cidade    VARCHAR(100),
    estado    CHAR(2)
);

-- =============================================================
-- 3. Tabela Curso
-- =============================================================
CREATE TABLE curso (
    id            SERIAL          PRIMARY KEY,
    nome          VARCHAR(200)    NOT NULL,
    carga_horaria INT             NOT NULL,
    valor         NUMERIC(10, 2)  NOT NULL
);

-- =============================================================
-- 4. Tabela Disciplina
--    Pertence a um Curso (N:1)
--    periodo: 1 = Noturno, 2 = Diurno  (mapeado do enum Periodo)
-- =============================================================
CREATE TABLE disciplina (
    id         SERIAL       PRIMARY KEY,
    nome       VARCHAR(200) NOT NULL,
    periodo    SMALLINT     NOT NULL CHECK (periodo IN (1, 2)),
    dia_semana VARCHAR(20)  NOT NULL,
    id_curso   INT          NOT NULL,

    CONSTRAINT fk_disciplina_curso
        FOREIGN KEY (id_curso) REFERENCES curso (id)
        ON DELETE CASCADE
);

-- =============================================================
-- 5. Tabela Aluno (herda de Pessoa - TPT)
--    Cada aluno está matriculado em um único Curso (N:1)
-- =============================================================
CREATE TABLE aluno (
    id        INT NOT NULL,
    id_curso  INT NOT NULL,

    CONSTRAINT pk_aluno
        PRIMARY KEY (id),

    CONSTRAINT fk_aluno_pessoa
        FOREIGN KEY (id) REFERENCES pessoa (id)
        ON DELETE CASCADE,

    CONSTRAINT fk_aluno_curso
        FOREIGN KEY (id_curso) REFERENCES curso (id)
);

-- =============================================================
-- 6. Tabela Professor (herda de Pessoa - TPT)
-- =============================================================
CREATE TABLE professor (
    id INT NOT NULL,

    CONSTRAINT pk_professor
        PRIMARY KEY (id),

    CONSTRAINT fk_professor_pessoa
        FOREIGN KEY (id) REFERENCES pessoa (id)
        ON DELETE CASCADE
);

-- =============================================================
-- 7. Tabela ProfessorCurso (junção N:N entre Professor e Curso)
--    Um professor pode lecionar em vários cursos
--    Um curso pode ter vários professores
-- =============================================================
CREATE TABLE professor_curso (
    id_professor INT NOT NULL,
    id_curso     INT NOT NULL,

    CONSTRAINT pk_professor_curso
        PRIMARY KEY (id_professor, id_curso),

    CONSTRAINT fk_pc_professor
        FOREIGN KEY (id_professor) REFERENCES professor (id)
        ON DELETE CASCADE,

    CONSTRAINT fk_pc_curso
        FOREIGN KEY (id_curso) REFERENCES curso (id)
        ON DELETE CASCADE
);

-- =============================================================
-- 8. Tabela Usuario (acesso ao sistema - independente)
-- =============================================================
CREATE TABLE usuario (
    id    SERIAL       PRIMARY KEY,
    email VARCHAR(255) NOT NULL UNIQUE,
    senha VARCHAR(255) NOT NULL
);
