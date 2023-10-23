USE LS_Practica_MVC_1
GO

CREATE TABLE TipoDocumento (
	Id INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(5) NOT NULL,
);

CREATE TABLE Persona (
	Id INT PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR(30) NOT NULL,
	Apellido VARCHAR(30) NOT NULL,
	TipoDoc INT,
	NroDoc VARCHAR(8) NOT NULL,
	CONSTRAINT FK_Persona_TipoDocumento FOREIGN KEY (TipoDoc) REFERENCES TipoDocumento(Id)
);

INSERT INTO TipoDocumento(Descripcion) VALUES ('DNI'), ('LE'), ('LC');

-- Insertar valores en la tabla Persona con referencias a TipoDocumento
INSERT INTO Persona (Nombre, Apellido, TipoDoc, NroDoc) VALUES 
('Juan', 'Perez', 1, '12345678'),
('Maria', 'Lopez', 2, '78901234'),
('Carlos', 'Gonzalez', 3, '56789012');

SELECT * FROM Persona
SELECT * FROM TipoDocumento
SELECT MAX(Id) FROM Persona

CREATE OR ALTER PROCEDURE P_Get_Personas AS
	SELECT * FROM Persona

CREATE OR ALTER PROCEDURE P_Insertar_Persona @Nombre VARCHAR(30), @Apellido VARCHAR(30), @TipoDoc INT, @NroDoc VARCHAR(8) AS
	INSERT INTO Persona (Nombre, Apellido, TipoDoc, NroDoc) VALUES (@Nombre, @Apellido, @TipoDoc, @NroDoc)

CREATE OR ALTER PROCEDURE P_Actualizar_Persona @Id INT, @Nombre VARCHAR(30), @Apellido VARCHAR(30), @TipoDoc INT, @NroDoc VARCHAR(8) AS
	UPDATE Persona
	SET Nombre = @Nombre, Apellido = @Apellido, TipoDoc = @TipoDoc, NroDoc = @NroDoc
	WHERE Id = @Id;

EXECUTE P_Insertar_Persona 'Leandro', 'Sandoval', 1, '41548235';

EXECUTE P_Get_Personas;