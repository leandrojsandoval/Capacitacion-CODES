CREATE DATABASE LS_Practica_MVC_1

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
('Juan', 'Pérez', 1, '12345678'),
('María', 'González', 2, '98765432'),
('Carlos', 'López', 3, '87654321'),
('Laura', 'Martínez', 1, '76543210'),
('Pedro', 'Sánchez', 2, '65432109'),
('Ana', 'Rodríguez', 3, '54321098'),
('Eduardo', 'Fernández', 1, '43210987'),
('Luisa', 'Torres', 2, '32109876'),
('Javier', 'Ramírez', 3, '21098765'),
('Carmen', 'Díaz', 1, '10987654'),
('Ricardo', 'Vega', 2, '09876543'),
('Marta', 'Silva', 3, '98765432'),
('Guillermo', 'Luna', 1, '87654321'),
('Sofía', 'Mendoza', 2, '76543210'),
('Pablo', 'Ortega', 3, '65432109');

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

CREATE OR ALTER PROCEDURE P_Get_Max_Id_Persona AS
	SELECT MAX(Id) FROM Persona;


CREATE OR ALTER PROCEDURE P_Get_Ids_Personas AS
	SELECT Id FROM Persona;

CREATE OR ALTER PROCEDURE P_Get_Persona_Por_Id @Id INT AS
BEGIN
	IF(@Id > 0)
		SELECT * FROM Persona WHERE Id = @Id;
END

EXECUTE P_Get_Persona_Por_Id 1;

EXECUTE P_Insertar_Persona 'Leandro', 'Sandoval', 1, '41548235';

EXECUTE P_Get_Personas;

EXECUTE P_Get_Max_Id_Persona;

EXECUTE P_Get_Ids_Personas;
