IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'Cinema')
BEGIN
    CREATE DATABASE Cinema;
END;

GO

USE Cinema;

GO

-- Creaci�n de la tabla Peliculas

CREATE TABLE Peliculas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Imagen VARCHAR(50) NOT NULL,
    Nombre VARCHAR(50) NOT NULL,
    Descripcion TEXT NOT NULL,
    FechaCreacion DATE NOT NULL,
    FechaActualizacion DATE NOT NULL
);

GO

-- Creaci�n de la tabla Usuarios

CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(20) NOT NULL,
    Email VARCHAR(20) NOT NULL,
    Contrasenia VARCHAR(20) NOT NULL,
    EsAdministrador TINYINT NOT NULL DEFAULT 0,
    FechaCreacion DATE NOT NULL,
    FechaActualizacion DATE NOT NULL
);

GO

-- Inserci�n de datos en la tabla Usuarios

INSERT INTO Usuarios (Nombre, Email, Contrasenia, EsAdministrador, FechaCreacion, FechaActualizacion)
VALUES
    ('Eduardo', 'eduardo@kodoti.com', '123456', 1, '2019-05-19', '2019-05-19'),
    ('Susana', 'susana@gmail.com', '123456', 0, '2019-05-19', '2019-05-19'),
    ('Alonso', 'alonso@gmail.com', '123456', 0, '2019-05-19', '2019-05-19'),
    ('Jos�', 'jose@gmail.com', '123456', 0, '2019-05-19', '2019-05-19'),
    ('Cesar', 'cesar@gmail.com', '123456', 0, '2019-05-19', '2019-05-19'),
    ('Sandra', 'sandra@gmail.com', '123456', 0, '2019-05-19', '2019-05-19'),
    ('Carla', 'carla@gmail.com', '123456', 0, '2019-05-19', '2019-05-19');
GO

-- Inserci�n de datos en la tabla Peliculas

INSERT INTO Peliculas (Imagen, Nombre, Descripcion, FechaCreacion, FechaActualizacion)
VALUES
    ('/static/terminator.jpg', 'Terminator', 'Un asesino cibern�tico del futuro es enviado a Los �ngeles, para matar a la mujer que procrear� a un l�der.', '2019-05-18', '2019-05-18'),
    ('/static/dragonball.jpg', 'Dragon Ball Super: Broly', 'Despu�s de disputarse el Torneo de la Fuerza, la Tierra se encuentra en paz. Goku al darse cuenta de que en el universo a�n hay personas extremadamente fuertes, contin�a entrenando para volverse m�s fuerte. Pero un d�a, Freezer aparece en la Tierra con un misterioso saiyajin llamado Broly, el cu�l enfrenta a Goku y Vegeta.', '2019-05-18', '2019-05-18'),
    ('/static/profesional.jpg', 'El perfecto asesino', 'Mathilda es una ni�a que no se lleva bien con su familia, excepto con su hermano peque�o. Su padre es un narcotraficante que hace negocios con Stan, un corrupto agente de la D.E.A. Un d�a Stan mata a su familia y Mathilda se refugia en casa de L�on, un solitario y misterioso vecino que resulta ser un asesino a sueldo, pero har� un pacto con �l: ella se encargar� de las tareas dom�sticas y le ense�ar� a leer a L�on y �ste le ense�ar� a disparar para poder vengarse de la muerte de su hermano.', '2019-05-18', '2019-05-18'),
    ('/static/johnwick.jpg', 'John Wick', 'John Wick es una pel�cula de acci�n estadounidense de 2014, dirigida por David Leitch y Chad Stahelski.', '2019-05-18', '2019-05-18');
GO

-- Creaci�n de la tabla Sucursales

CREATE TABLE Sucursales (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50) NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    FechaCreacion DATE NOT NULL,
    FechaActualizacion DATE NOT NULL
);

GO

-- Inserci�n de datos en la tabla Sucursales

INSERT INTO Sucursales (Nombre, Precio, FechaCreacion, FechaActualizacion)
VALUES
    ('KODOTI Star Zona Norte', 17.00, '2019-05-19', '2019-05-19'),
    ('KODOTI Stars Zona Sur', 18.00, '2019-05-19', '2019-05-19'),
    ('KODOTI Stars Zona Este', 24.00, '2019-05-19', '2019-05-19'),
    ('KODOTI Stars Zona Oeste', 21.50, '2019-05-19', '2019-05-19');
GO

-- Creaci�n de la tabla Horario

CREATE TABLE Horarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdPelicula INT NOT NULL,
    IdSucursal INT NOT NULL,
    Hora VARCHAR(20) NOT NULL,
    FechaCreacion DATE NOT NULL,
    FechaActualizacion DATE NOT NULL,
	CONSTRAINT FK_Horarios_IdPelicula FOREIGN KEY (IdPelicula) REFERENCES Peliculas(Id),
	CONSTRAINT FK_Horarios_IdSucursal FOREIGN KEY (IdSucursal) REFERENCES Sucursales(Id)
);

GO

-- Inserci�n de datos en la tabla schedules
INSERT INTO Horarios (IdPelicula, IdSucursal, Hora, FechaCreacion, FechaActualizacion)
VALUES
	(1, 1, '12:00:00', '2019-05-19', '2019-05-19'),
	(1, 3, '12:00:00', '2019-05-19', '2019-05-19'),
	(2, 1, '12:00:00', '2019-05-19', '2019-05-19'),
	(2, 2, '12:00:00', '2019-05-19', '2019-05-19'),
	(2, 3, '12:00:00', '2019-05-19', '2019-05-19'),
	(2, 4, '12:00:00', '2019-05-19', '2019-05-19'),
	(3, 1, '12:00:00', '2019-05-19', '2019-05-19'),
	(3, 2, '12:00:00', '2019-05-19', '2019-05-19'),
	(3, 3, '12:00:00', '2019-05-19', '2019-05-19'),
	(3, 4, '12:00:00', '2019-05-19', '2019-05-19'),
	(4, 2, '12:00:00', '2019-05-19', '2019-05-19'),
	(4, 3, '12:00:00', '2019-05-19', '2019-05-19'),
	(1, 1, '14:00:00', '2019-05-19', '2019-05-19'),
	(1, 2, '14:00:00', '2019-05-19', '2019-05-19'),
	(1, 4, '14:00:00', '2019-05-19', '2019-05-19'),
	(2, 1, '14:00:00', '2019-05-19', '2019-05-19'),
	(2, 2, '14:00:00', '2019-05-19', '2019-05-19'),
	(2, 3, '14:00:00', '2019-05-19', '2019-05-19'),
	(2, 4, '14:00:00', '2019-05-19', '2019-05-19'),
	(3, 1, '14:00:00', '2019-05-19', '2019-05-19'),
	(3, 2, '14:00:00', '2019-05-19', '2019-05-19'),
	(3, 3, '14:00:00', '2019-05-19', '2019-05-19'),
	(3, 4, '14:00:00', '2019-05-19', '2019-05-19'),
	(4, 1, '14:00:00', '2019-05-19', '2019-05-19'),
	(4, 2, '14:00:00', '2019-05-19', '2019-05-19'),
	(4, 3, '14:00:00', '2019-05-19', '2019-05-19'),
	(4, 4, '14:00:00', '2019-05-19', '2019-05-19'),
	(1, 1, '16:00:00', '2019-05-19', '2019-05-19'),
	(1, 2, '16:00:00', '2019-05-19', '2019-05-19'),
	(1, 3, '16:00:00', '2019-05-19', '2019-05-19'),
	(2, 1, '16:00:00', '2019-05-19', '2019-05-19'),
	(2, 2, '16:00:00', '2019-05-19', '2019-05-19'),
	(2, 3, '16:00:00', '2019-05-19', '2019-05-19'),
	(3, 1, '16:00:00', '2019-05-19', '2019-05-19'),
	(3, 2, '16:00:00', '2019-05-19', '2019-05-19'),
	(3, 3, '16:00:00', '2019-05-19', '2019-05-19'),
	(3, 4, '16:00:00', '2019-05-19', '2019-05-19'),
	(4, 1, '16:00:00', '2019-05-19', '2019-05-19'),
	(4, 2, '16:00:00', '2019-05-19', '2019-05-19'),
	(4, 3, '16:00:00', '2019-05-19', '2019-05-19'),
	(4, 4, '16:00:00', '2019-05-19', '2019-05-19'),
	(1, 1, '18:00:00', '2019-05-19', '2019-05-19'),
	(1, 2, '18:00:00', '2019-05-19', '2019-05-19'),
	(1, 3, '18:00:00', '2019-05-19', '2019-05-19'),
	(1, 4, '18:00:00', '2019-05-19', '2019-05-19'),
	(2, 1, '18:00:00', '2019-05-19', '2019-05-19'),
	(2, 2, '18:00:00', '2019-05-19', '2019-05-19'),
	(2, 3, '18:00:00', '2019-05-19', '2019-05-19'),
	(2, 4, '18:00:00', '2019-05-19', '2019-05-19'),
	(3, 1, '18:00:00', '2019-05-19', '2019-05-19'),
	(3, 3, '18:00:00', '2019-05-19', '2019-05-19'),
	(4, 1, '18:00:00', '2019-05-19', '2019-05-19'),
	(4, 3, '18:00:00', '2019-05-19', '2019-05-19'),
	(4, 4, '18:00:00', '2019-05-19', '2019-05-19'),
	(1, 1, '20:00:00', '2019-05-19', '2019-05-19'),
	(1, 2, '20:00:00', '2019-05-19', '2019-05-19'),
	(1, 4, '20:00:00', '2019-05-19', '2019-05-19'),
	(2, 3, '20:00:00', '2019-05-19', '2019-05-19'),
	(3, 1, '20:00:00', '2019-05-19', '2019-05-19'),
	(3, 2, '20:00:00', '2019-05-19', '2019-05-19'),
	(3, 3, '20:00:00', '2019-05-19', '2019-05-19'),
	(3, 4, '20:00:00', '2019-05-19', '2019-05-19'),
	(4, 1, '20:00:00', '2019-05-19', '2019-05-19'),
	(4, 2, '20:00:00', '2019-05-19', '2019-05-19'),
	(4, 3, '20:00:00', '2019-05-19', '2019-05-19'),
	(4, 4, '20:00:00', '2019-05-19', '2019-05-19');
GO

-- Creaci�n de la tabla Ventas

CREATE TABLE Ventas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT NOT NULL,
    IdHorario INT NOT NULL,
    Cantidad INT NOT NULL,
    Total DECIMAL(10,2) NOT NULL,
    FechaCreacion DATE NOT NULL,
    FechaActualizacion DATE NOT NULL,
	CONSTRAINT FK_Ventas_IdUsuario FOREIGN KEY (IdUsuario) REFERENCES Usuarios(Id),
	CONSTRAINT FK_Ventas_IdHorario FOREIGN KEY (IdHorario) REFERENCES Horarios(Id)
);

GO

-- Inserci�n de datos en la tabla Pedidos

INSERT INTO Ventas (IdUsuario, IdHorario, Cantidad, Total, FechaCreacion, FechaActualizacion)
VALUES
    (1, 1, 4, 17.00, '2019-05-21', '2019-05-21');
GO

/************************** Stored Procedures **************************/

CREATE OR ALTER PROCEDURE sp_obtener_sucursales AS
BEGIN
	SELECT [Id],[Nombre],[Precio],[FechaCreacion],[FechaActualizacion]
	FROM [dbo].[Sucursales]
END

GO

CREATE OR ALTER PROCEDURE sp_obtener_sucursal_por_id (@id INT) AS
BEGIN
	SELECT [Id],[Nombre],[Precio],[FechaCreacion],[FechaActualizacion]
	FROM [dbo].[Sucursales]
	WHERE [Id] = @id
END

exec sp_obtener_sucursales
exec sp_obtener_sucursal_por_id 2