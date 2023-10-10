USE DBPersonas
GO

/*DROP TABLE Persona
DROP TABLE TipoDocumento*/

CREATE TABLE TipoDocumento
(
	TipoDoc INT,	-- 1 = DNI, 2 = LE, 3 = LC
    NroDoc NVARCHAR(15) NOT NULL,
	--CONSTRAINT PK_TipoDocumento PRIMARY KEY (TipoDoc, NroDoc),
	--CONSTRAINT CK_TipoDoc_ValidValues CHECK (TipoDoc IN (1, 2, 3))
);

CREATE TABLE Persona (
	Id INT PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR(30) NOT NULL,
	Apellido VARCHAR(30) NOT NULL,
	TipoDoc INT,
	NroDoc NVARCHAR(15),
	--CONSTRAINT FK_Persona_TipoDocumento FOREIGN KEY (TipoDoc, NroDoc) REFERENCES TipoDocumento(TipoDoc, NroDoc)
);


-- Insertar valores en la tabla TipoDocumento
INSERT INTO TipoDocumento (TipoDoc, NroDoc)
VALUES (1, '12345678');

INSERT INTO TipoDocumento (TipoDoc, NroDoc)
VALUES (2, '78901234');

INSERT INTO TipoDocumento (TipoDoc, NroDoc)
VALUES (3, '56789012');

-- Insertar valores en la tabla Persona con referencias a TipoDocumento
INSERT INTO Persona (Nombre, Apellido, TipoDoc, NroDoc)
VALUES ('Juan', 'Perez', 1, '12345678');

INSERT INTO Persona (Nombre, Apellido, TipoDoc, NroDoc)
VALUES ('Maria', 'Lopez', 2, '78901234');

INSERT INTO Persona (Nombre, Apellido, TipoDoc, NroDoc)
VALUES ('Carlos', 'Gonzalez', 3, '56789012');

-- Intentar insertar un valor no permitido en TipoDoc (debería generar un error)
-- Esto solo es un ejemplo de lo que no debes hacer
INSERT INTO TipoDocumento (TipoDoc, NroDoc)
VALUES (4, 'Pasaporte');

SELECT * FROM Persona

SELECT * FROM TipoDocumento

SELECT MAX(Id) FROM Persona

TRUNCATE TABLE Persona

/*UPDATE Persona
SET Nombre = @Nombre, Apellido = @Apellido, TipoDoc = @TipoDoc, NroDoc = @NroDoc
WHERE Id = @Id;*/