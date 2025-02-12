CREATE TABLE Users(
	Id INT IDENTITY(1, 1),
	UserName VARCHAR(100),
	LastName VARCHAR(100),
	Email VARCHAR(100),
	Position VARCHAR(100)
)


-- Executar Procedures
EXEC [DBO].[LIST_ALL_USERS]
EXEC [DBO].[LIST_USER_BY_ID] @id = 2
EXEC [DBO].[INSERT_USER] 'Criação Proc', 'Criação', 'tese@teste.com', 'Criação teste'
EXEC [DBO].[EDIT_USER] 3, 'Paola', 'Melo', 'paolam@teste.com', 'Analista'
EXEC [DBO].[REMOVE_USER] 4


-- Listagem de todos usuários
CREATE PROCEDURE [DBO].[LIST_ALL_USERS]
AS
BEGIN
   SELECT Id, UserName, LastName, Email, Position FROM Users
END




-- Retorna Usuário Por ID
CREATE PROCEDURE [DBO].[LIST_USER_BY_ID]
(
	@Id INT
)
AS
BEGIN
	SELECT Id, UserName, LastName, Email, Position FROM Users
	WHERE Id LIKE(@Id)
END


-- Inserir Usuário
CREATE PROCEDURE [DBO].[INSERT_USER]
(
	@UserName VARCHAR(100),
	@LastName VARCHAR(100),
	@Email VARCHAR(100),
	@Position VARCHAR(100)
)
AS
BEGIN
	INSERT INTO Users (UserName, LastName, Email, Position)

	VALUES(@UserName, @LastName, @Email, @Position)
END


-- Editar Usuário
CREATE PROCEDURE [DBO].[EDIT_USER]
(
	@Id INT,
	@UserName VARCHAR(100),
	@LastName VARCHAR(100),
	@Email VARCHAR(100),
	@Position VARCHAR(100)
)
AS
BEGIN
	DECLARE @COUNT INT = 0

	SET @COUNT = (SELECT COUNT(1) FROM Users WHERE Id LIKE(@Id))

	IF (@COUNT > 0)
		BEGIN
			UPDATE Users
				SET UserName = @UserName,
					LastName = @LastName,
					Email = @Email,
					Position = @Position
				WHERE Id LIKE(@Id)
		END
END


-- Remover Usuário
CREATE PROCEDURE [DBO].[REMOVE_USER]
(
	@id  INT
)
AS
BEGIN
	
	DECLARE @COUNT INT = 0

	SET @COUNT = (SELECT COUNT(1) FROM Users WHERE Id LIKE(@id))

	IF (@COUNT > 0)
	BEGIN
		DELETE FROM Users WHERE Id LIKE(@Id)
	END
END


ALTER PROCEDURE [DBO].[INSERT_USER]
(
	@UserName VARCHAR(100),
	@LastName VARCHAR(100),
	@Email VARCHAR(100),
	@Position VARCHAR(100)
)
AS
BEGIN
	INSERT INTO Users (UserName, LastName, Email, Position)

	VALUES(@UserName, @LastName, @Email, @Position)
END
