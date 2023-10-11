USE LS_PRACTICA_MVC
GO

CREATE TABLE TipoDocumento (
	TipoDoc INT IDENTITY(1,1) PRIMARY KEY,
	Descripcion VARCHAR(5)
)

CREATE TABLE Persona (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Nombre VARCHAR(100),
	Apellido VARCHAR(100),
	TipoDoc INT,
	NroDoc VARCHAR(8),
	CONSTRAINT FK_TipoDoc FOREIGN KEY (TipoDoc) REFERENCES TipoDocumento (TipoDoc)
)

INSERT INTO TipoDocumento (TipoDoc, Descripcion) VALUES ('DNI'), ('LE'), ('LC');

SELECT * FROM Persona;

SELECT p.Nombre, p.Apellido, td.Descripcion, p.NroDoc
FROM Persona AS p INNER JOIN TipoDocumento AS td ON p.TipoDoc = td.TipoDoc
