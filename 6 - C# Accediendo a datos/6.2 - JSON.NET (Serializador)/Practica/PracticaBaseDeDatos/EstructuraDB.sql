-- CREATE DATABASE LS_Practica_C#;

USE LS_Practica_C#;

CREATE TABLE Persona (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50),
    Apellido VARCHAR(50),
    Edad INT,
	DNI VARCHAR(8),
	Email VARCHAR(50)
);

INSERT INTO Persona (Nombre, Apellido, Edad, DNI, Email) VALUES
('Juan', 'Pérez', 25, '12345678', 'juanperez@gmail.com'),
('María', 'Gómez', 30, '23456789', 'mariagomez@hotmail.com'),
('Carlos', 'López', 21, '34567890', 'carlos.lopez@gmail.com'),
('Laura', 'Martínez', 19, '45678901', 'laura.m@hotmail.com'),
('Pedro', 'Sánchez', 28, '56789012', 'pedrosanchez@gmail.com'),
('Ana', 'Rodríguez', 22, '67890123', 'anarodriguez@hotmail.com'),
('Javier', 'García', 35, '78901234', 'javier.g@gmail.com'),
('Carmen', 'Fernández', 29, '89012345', 'carmen@hotmail.com'),
('Luis', 'Díaz', 20, '90123456', 'luisdiaz@gmail.com'),
('Elena', 'López', 18, '01234567', 'elena.lopez@hotmail.com'),
('Manuel', 'Torres', 26, '12345678', 'manueltorres@gmail.com'),
('Sofía', 'Hernández', 31, '23456789', 'sofia.h@hotmail.com'),
('Raúl', 'Ramírez', 27, '34567890', 'raul.ramirez@gmail.com'),
('Miguel', 'Gómez', 24, '56789012', 'miguel.g@gmail.com'),
('Juan', 'González', 25, '11223344', 'juang@gmail.com'),
('Pedro', 'López', 28, '22334455', 'pedrol@gmail.com'),
('Laura', 'Gómez', 30, '33445566', 'laurag@gmail.com'),
('María', 'Sánchez', 28, '44556677', 'marias@gmail.com'),
('Juan', 'Fernández', 35, '55667788', 'juanf@gmail.com'),
('Luis', 'Hernández', 20, '66778899', 'luish@gmail.com');

GO

CREATE OR ALTER PROCEDURE P_Obtener_Personas AS
	SELECT * FROM Persona;

GO

CREATE OR ALTER PROCEDURE P_Eliminar_Personas AS
	DELETE Persona;

GO

CREATE OR ALTER PROCEDURE P_Agregar_Persona @Nombre VARCHAR(50), @Apellido VARCHAR(50), @Edad INT, @DNI VARCHAR(8), @Email VARCHAR(50) AS
	INSERT INTO Persona (Nombre, Apellido, Edad, DNI, Email) VALUES (@Nombre, @Apellido, @Edad, @DNI, @Email);

GO

CREATE OR ALTER PROCEDURE P_Filtrar_Personas 
	@Nombre VARCHAR(50), @Apellido VARCHAR(50), 
	@DNI VARCHAR(8), @Email VARCHAR(50), @Edad INT AS
BEGIN
	SET @Nombre = NULLIF(@Nombre, '');
    SET @Apellido = NULLIF(@Apellido, '');
    SET @DNI = NULLIF(@DNI, '');
	SET @Email = NULLIF(@Email, '');
    SET @Edad = NULLIF(@Edad, '');

	SELECT p.Id, p.Nombre, p.Apellido, p.Edad, p.DNI, p.Email
	FROM Persona AS p
	WHERE	(@Nombre IS NULL OR p.Nombre LIKE '%' + @Nombre + '%') AND
			(@Apellido IS NULL OR p.Apellido LIKE '%' + @Apellido + '%') AND 
			(@DNI IS NULL OR p.DNI LIKE '%' + @DNI + '%') AND
			(@Email IS NULL OR p.Email LIKE '%' + @Email + '%') AND
			(@Edad IS NULL OR p.Edad = @Edad);
END

-- EXECUTE P_Eliminar_Personas

EXECUTE P_Obtener_Personas

EXECUTE P_Filtrar_Personas '', '', '', '@hotmail.com', '';

EXECUTE P_Filtrar_Personas NULL, NULL, NULL, '@hotmail.com', NULL;

