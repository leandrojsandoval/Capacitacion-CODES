USE LS_PRACTICA_MVC;
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
('Juan Pérez', 'Av. Corrientes 1234, Buenos Aires', 1, 1),
('María Rodríguez', 'Calle San Martín 456, La Plata', 1, 2),
('Carlos González', 'Av. Colón 789, Córdoba', 2, 4),
('Ana López', 'Calle Sarmiento 101, Santa Fe', 3, 6),
('Luis Torres', 'Av. San Juan 567, Mendoza Capital', 4, 8),
('Elena Martínez', 'Av. Belgrano 789, San Miguel de Tucumán', 5, 9),
('Pedro Sánchez', 'Calle Buenos Aires 123, Salta Capital', 6, 10);

DELETE FROM Provincia;

select * from Proveedor

SELECT prove.Id, prove.Nombre, prove.Domicilio, provi.Descripcion AS Provincia, loc.Descripcion AS Localidad
FROM Proveedor AS prove INNER JOIN Provincia AS provi ON prove.IdProvincia = provi.Id 
						INNER JOIN Localidad loc ON loc.Id = prove.IdLocalidad

SELECT * 
FROM Proveedor AS prove INNER JOIN Provincia AS provi ON prove.IdProvincia = provi.Id INNER JOIN Localidad AS loc ON prove.IdLocalidad = loc.Id 
WHERE prove.Nombre LIKE '%Leandro%' AND loc.Descripcion LIKE '%Buenos Aires%' AND provi.Descripcion LIKE '%Ciudad de Buenos Aires%'