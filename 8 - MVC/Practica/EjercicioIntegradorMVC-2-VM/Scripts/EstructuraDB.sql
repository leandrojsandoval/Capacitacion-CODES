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

-- Insertar algunas provincias ficticias de Argentina 
INSERT INTO Provincia (Id, Descripcion) VALUES 
(1, 'Buenos Aires'),
(2, 'C�rdoba'),
(3, 'Santa Fe'); 
-- ... Puedes agregar m�s provincias seg�n tus necesidades 

-- Insertar algunas localidades ficticias en las provincias 
-- Buenos Aires 
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
-- ... Puedes agregar m�s localidades seg�n tus necesidades

-- Insertar algunos proveedores ficticios en las localidades 
INSERT INTO Proveedor (Nombre, Domicilio, IdProvincia, IdLocalidad) VALUES 
('Juan P�rez', 'Av. Corrientes 1234, Buenos Aires', 1, 1),
('Mar�a Rodr�guez', 'Calle San Mart�n 456, La Plata', 1, 2),
('Carlos Gonz�lez', 'Av. Col�n 789, C�rdoba', 2, 4),
('Ana L�pez', 'Calle Sarmiento 101, Santa Fe', 3, 6); 
-- ... Puedes agregar m�s proveedores con direcciones ficticias pero realistas seg�n tus necesidades

SELECT prove.Id, prove.Nombre, prove.Domicilio, provi.Descripcion AS Provincia, loc.Descripcion AS Localidad
FROM Proveedor AS prove INNER JOIN Provincia AS provi ON prove.IdProvincia = provi.Id 
						INNER JOIN Localidad loc ON loc.Id = prove.IdLocalidad
