USE LS_PRACTICA_MVC_2;
GO

CREATE TABLE Provincia (
	Id INT PRIMARY KEY,
	Descripcion VARCHAR(30)
)

CREATE TABLE Localidad (
	Id INT PRIMARY KEY,
	Descripcion VARCHAR(50),
	IdProvincia INT,
	CONSTRAINT FK_Localidad_IdProvincia FOREIGN KEY (IdProvincia) REFERENCES Provincia(Id) ON DELETE CASCADE ON UPDATE CASCADE
)

CREATE TABLE Proveedor (
	Id INT IDENTITY (1,1) PRIMARY KEY,
	Nombre VARCHAR(30),
	Domicilio VARCHAR(50),
	IdProvincia INT,
	IdLocalidad INT,
	CONSTRAINT FK_Proveedor_IdProvincia FOREIGN KEY (IdProvincia) REFERENCES Provincia(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT FK_Proveedor_IdLocalidad FOREIGN KEY (IdLocalidad) REFERENCES Localidad(Id)
)

INSERT INTO Provincia (Id, Descripcion) VALUES 
(1, 'Buenos Aires'),
(2, 'C�rdoba'),
(3, 'Santa Fe'),
(4, 'Mendoza'),
(5, 'Tucum�n'),
(6, 'Salta');

INSERT INTO Localidad (Id, Descripcion, IdProvincia) VALUES 
(1, 'Ciudad de Buenos Aires', 1),
(2, 'La Plata', 1),
(3, 'Mar del Plata', 1); 
-- C�rdoba 
INSERT INTO Localidad (Id, Descripcion, IdProvincia) VALUES 
(4, 'Ciudad de C�rdoba', 2),
(5, 'Villa Carlos Paz', 2); 
-- Santa Fe 
INSERT INTO Localidad (Id, Descripcion, IdProvincia) VALUES 
(6, 'Santa Fe', 3),
(7, 'Rosario', 3);
-- Mendoza
INSERT INTO Localidad (Id, Descripcion, IdProvincia) VALUES
(8, 'Mendoza Capital', 4);
-- Tucuman
INSERT INTO Localidad (Id, Descripcion, IdProvincia) VALUES 
(9, 'San Miguel de Tucum�n', 5);
-- Salta
INSERT INTO Localidad (Id, Descripcion, IdProvincia) VALUES 
(10, 'Salta Capital', 6);


INSERT INTO Proveedor (Nombre, Domicilio, IdProvincia, IdLocalidad) VALUES 
('Juan P�rez', 'Av. Corrientes 1234', 1, 1),
('Mar�a Rodr�guez', 'Calle San Mart�n 456', 1, 2),
('Carlos Gonz�lez', 'Av. Col�n 789', 2, 4),
('Ana L�pez', 'Calle Sarmiento 101', 3, 6),
('Luis Torres', 'Av. San Juan 567', 4, 8),
('Elena Mart�nez', 'Av. Belgrano 789', 5, 9),
('Pedro S�nchez', 'Calle Buenos Aires 123', 6, 10),
('Laura Garc�a', 'Av. Rivadavia 567', 1, 1),
('Miguel Fern�ndez', 'Calle Uruguay 789', 1, 2),
('Luc�a Ram�rez', 'Av. Belgrano 123', 1, 1),
('Jorge Soto', 'Av. Col�n 101', 2, 4),
('Carolina P�rez', 'Calle San Mart�n 202', 2, 5),
('Roberto L�pez', 'Calle Espa�a 303', 3, 6),
('Liliana Gonz�lez', 'Av. San Mart�n 505', 3, 7),
('Gustavo Mart�nez', 'Av. San Juan 707', 4, 8),
('Natalia Torres', 'Calle Jujuy 101', 5, 9),
('Alejandro P�rez', 'Av. San Mart�n 111', 6, 10);

DELETE FROM Provincia;

SELECT * FROM Proveedor

CREATE OR ALTER PROCEDURE P_Obtener_Localidades AS
	SELECT * FROM Localidad

CREATE OR ALTER PROCEDURE P_Obtener_Provincias AS
	SELECT * FROM Provincia

CREATE OR ALTER PROCEDURE P_Obtener_Proveedores AS
	SELECT * FROM Proveedor

CREATE OR ALTER PROCEDURE P_Eliminar_Proveedor @Id INT AS
	DELETE FROM Proveedor WHERE Id = @Id;

CREATE OR ALTER PROCEDURE P_Agregar_Proveedor @Nombre VARCHAR(30), @Domicilio VARCHAR(50), @IdProvincia INT, @IdLocalidad INT AS
	INSERT INTO Proveedor (Nombre, Domicilio, IdProvincia, IdLocalidad) VALUES (@Nombre, @Domicilio, @IdProvincia, @IdLocalidad);

CREATE OR ALTER PROCEDURE P_Filtrar_Proveedor @Nombre VARCHAR(30), @Provincia VARCHAR(30), @Localidad VARCHAR(50) AS
        SELECT * FROM Proveedor AS prove 
			INNER JOIN Provincia AS provi ON prove.IdProvincia = provi.Id 
			INNER JOIN Localidad AS loc ON prove.IdLocalidad = loc.Id 
		WHERE prove.Nombre LIKE '%' + @Nombre + '%' AND
			  provi.Descripcion LIKE '%' + @Provincia + '%' AND
			  loc.Descripcion LIKE '%' + @Localidad + '%';

EXECUTE P_Obtener_Localidades;
EXECUTE P_Obtener_Provincias;
EXECUTE P_Obtener_Proveedores;


SELECT prove.Id, prove.Nombre, prove.Domicilio, provi.Descripcion AS Provincia, loc.Descripcion AS Localidad
FROM Proveedor AS prove INNER JOIN Provincia AS provi ON prove.IdProvincia = provi.Id 
						INNER JOIN Localidad loc ON loc.Id = prove.IdLocalidad

SELECT * 
FROM Proveedor AS prove INNER JOIN Provincia AS provi ON prove.IdProvincia = provi.Id INNER JOIN Localidad AS loc ON prove.IdLocalidad = loc.Id 
WHERE prove.Nombre LIKE '%Leandro%' AND loc.Descripcion LIKE '%Buenos Aires%' AND provi.Descripcion LIKE '%Ciudad de Buenos Aires%'