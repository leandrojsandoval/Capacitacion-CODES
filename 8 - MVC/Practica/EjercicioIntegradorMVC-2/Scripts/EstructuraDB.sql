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
(2, 'Córdoba'),
(3, 'Santa Fe'),
(4, 'Mendoza'),
(5, 'Tucumán'),
(6, 'Salta');

INSERT INTO Localidad (Id, Descripcion, IdProvincia) VALUES 
(1, 'Ciudad de Buenos Aires', 1),
(2, 'La Plata', 1),
(3, 'Mar del Plata', 1); 
-- Córdoba 
INSERT INTO Localidad (Id, Descripcion, IdProvincia) VALUES 
(4, 'Ciudad de Córdoba', 2),
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
(9, 'San Miguel de Tucumán', 5);
-- Salta
INSERT INTO Localidad (Id, Descripcion, IdProvincia) VALUES 
(10, 'Salta Capital', 6);


INSERT INTO Proveedor (Nombre, Domicilio, IdProvincia, IdLocalidad) VALUES 
('Juan Pérez', 'Av. Corrientes 1234', 1, 1),
('María Rodríguez', 'Calle San Martín 456', 1, 2),
('Carlos González', 'Av. Colón 789', 2, 4),
('Ana López', 'Calle Sarmiento 101', 3, 6),
('Luis Torres', 'Av. San Juan 567', 4, 8),
('Elena Martínez', 'Av. Belgrano 789', 5, 9),
('Pedro Sánchez', 'Calle Buenos Aires 123', 6, 10),
('Laura García', 'Av. Rivadavia 567', 1, 1),
('Miguel Fernández', 'Calle Uruguay 789', 1, 2),
('Lucía Ramírez', 'Av. Belgrano 123', 1, 1),
('Jorge Soto', 'Av. Colón 101', 2, 4),
('Carolina Pérez', 'Calle San Martín 202', 2, 5),
('Roberto López', 'Calle España 303', 3, 6),
('Liliana González', 'Av. San Martín 505', 3, 7),
('Gustavo Martínez', 'Av. San Juan 707', 4, 8),
('Natalia Torres', 'Calle Jujuy 101', 5, 9),
('Alejandro Pérez', 'Av. San Martín 111', 6, 10);

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