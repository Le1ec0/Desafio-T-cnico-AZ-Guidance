-- Após me conectar ao SQL Server criei o banco de dados com o comando:

CREATE DATABASE "DesafioTecnicoAZGuidance"

-- Após criar o banco, criei as tabelas e seus relacionamentos:

USE DesafioTecnicoAZGuidance;

CREATE TABLE Permissao_Tipo
(
    TipoEmailID INT PRIMARY KEY,
    Descricao VARCHAR(100)
);

CREATE TABLE Permissao_Enviar_Para
(
    EnviarParaID INT PRIMARY KEY,
    Descricao VARCHAR(100)
);

CREATE TABLE Permissao_Forma_Envio
(
    FormaEnvioRmID INT PRIMARY KEY,
    Descricao VARCHAR(100)
);

CREATE TABLE Permissao_Cliente
(
    ClienteID INT PRIMARY KEY,
    Permitido BIT,
    TipoEmailID INT FOREIGN KEY REFERENCES Permissao_Tipo(TipoEmailID),
    EnviarParaID INT FOREIGN KEY REFERENCES Permissao_Enviar_Para(EnviarParaID),
    FormaEnvioRmID INT FOREIGN KEY REFERENCES Permissao_Forma_Envio(FormaEnvioRmID)
);

-- Depois foi definida as mensagens de tipo:

INSERT INTO Permissao_Tipo
    (TipoEmailID, Descricao)
VALUES
    (1, 'Comunicação de Movimentos'),
    (2, 'Rebalanceamento da Carteira'),
    (3, 'Mensagem de Feliz Aniversário');

INSERT INTO Permissao_Enviar_Para
    (EnviarParaID, Descricao)
VALUES
    (1, 'Cliente'),
    (2, 'Consultor'),
    (3, 'Ambos');

INSERT INTO Permissao_Forma_Envio
    (FormaEnvioRmID, Descricao)
VALUES
    (1, 'Destinatario'),
    (2, 'Copia'),
    (3, 'Copia Oculta');


-- Foi importante observar a ordem da criação das tabelas para que os relacionamentos pudessem ser feitos da forma correta.

-- (Imagem do diagrama está na pasta "diagrama.png")

-- Para inserir os clientes foi utilizado o seguinte script:

-- TipoEmailID 1 = Comunicação de Movimentos
-- TipoEmailID 2 = Comunicação de Rebalanceamento da Carteira
-- TipoEmailID 3 = Mensagem de Feliz Aniversário
-- EnviarParaID 1 = Cliente
-- EnviarParaID 2 = Consultor
-- FormaEnvioRmID 1 = Destinatário
-- FormaEnvioRmID 2 = Cópia
-- FormaEnvioRmID 3 = Cópia Oculta

INSERT INTO Permissao_Cliente
    (ClienteID, Permitido, TipoEmailID, EnviarParaID, FormaEnvioRmID)
VALUES
    (1, 1, 1, 1, 1),
    -- Cliente 1: Recebe todos como destinatário
    (2, 1, 1, 1, 1),
    -- Cliente 2: Recebe todos como destinatário
    (3, 1, 1, 2, 2),
    -- Cliente 3: Recebe todos como cópia para consultor
    (4, 1, 1, 2, 2),
    -- Cliente 4: Recebe todos como cópia para consultor
    (5, 1, 1, 2, 1),
    -- Cliente 5: Consultor como destinatário
    (6, 1, 1, 2, 1),
    -- Cliente 6: Consultor como destinatário
    (7, 1, 1, 2, 2),
    -- Cliente 7: Consultor como cópia
    (8, 1, 1, 2, 2),
    -- Cliente 8: Consultor como cópia
    (9, 1, 1, 2, 3),
    -- Cliente 9: Consultor como cópia oculta
    (10, 1, 1, 2, 3),
    -- Cliente 10: Consultor como cópia oculta
    (11, 0, 1, 1, 1); -- Cliente 11: Não recebe nenhum email


-- OBS: O cliente 11, apesar de não receber e-mail foi necessário inserir parâmetros para 

-- TipoEmailID, EnviarParaID e FormaEnvioRmID pois não foram criados como possíveis nulos no momento.

-- Com isso nosso banco de dados foi populado com sucesso, com seus relacionamentos.