CREATE TABLE Cargos(
    CargoId INT NOT NULL AUTO_INCREMENT,
    Descricao VARCHAR(255) NOT NULL,
    PRIMARY KEY (CargoId)
);

CREATE TABLE Funcionarios(
    FuncionarioId INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    NomeDoFuncionario VARCHAR(255) NOT NULL,
    Cpf VARCHAR(14) NOT NULL,
    NascimentoFuncionario DATE NOT NULL,
    DataDeAdmissao DATE NOT NULL,
    CelularFuncionario VARCHAR(11) NOT NULL,
    EmailFuncionario VARCHAR(255) NOT NULL,
    CargoId INT NOT NULL,
    FOREIGN KEY (CargoId) REFERENCES Cargos(CargoId)
);

CREATE TABLE Liderancas(
    LiderancaId INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    DescricaoEquipe VARCHAR(255) NOT NULL,
    FuncionarioId INT NOT NULL,
    FOREIGN KEY (FuncionarioId) REFERENCES Funcionarios(FuncionarioId)
);

CREATE TABLE Equipes(
    EquipeId INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    LiderancaId INT NOT NULL,
    FuncionarioId INT NOT NULL,
    FOREIGN KEY (LiderancaId) REFERENCES Liderancas(LiderancaId),
    FOREIGN KEY (FuncionarioId) REFERENCES Funcionarios(FuncionarioId)
);

CREATE TABLE Ponto(
    PontoId BIGINT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    DataHorarioPonto DATETIME NOT NULL,
    Justificativa VARCHAR(255),
    FuncionarioId INT NOT NULL, 
    FOREIGN KEY (FuncionarioId) REFERENCES Funcionarios(FuncionarioId)
);
