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
('Juan', 'P�rez', 25, '12345678', 'juanperez@gmail.com'),
('Mar�a', 'G�mez', 30, '23456789', 'mariagomez@hotmail.com'),
('Carlos', 'L�pez', 21, '34567890', 'carlos.lopez@gmail.com'),
('Laura', 'Mart�nez', 19, '45678901', 'laura.m@hotmail.com'),
('Pedro', 'S�nchez', 28, '56789012', 'pedrosanchez@gmail.com'),
('Ana', 'Rodr�guez', 22, '67890123', 'anarodriguez@hotmail.com'),
('Javier', 'Garc�a', 35, '78901234', 'javier.g@gmail.com'),
('Carmen', 'Fern�ndez', 29, '89012345', 'carmen@hotmail.com'),
('Luis', 'D�az', 20, '90123456', 'luisdiaz@gmail.com'),
('Elena', 'L�pez', 18, '01234567', 'elena.lopez@hotmail.com'),
('Manuel', 'Torres', 26, '12345678', 'manueltorres@gmail.com'),
('Sof�a', 'Hern�ndez', 31, '23456789', 'sofia.h@hotmail.com'),
('Ra�l', 'Ram�rez', 27, '34567890', 'raul.ramirez@gmail.com'),
('Miguel', 'G�mez', 24, '56789012', 'miguel.g@gmail.com'),
('Juan', 'Gonz�lez', 25, '11223344', 'juang@gmail.com'),
('Pedro', 'L�pez', 28, '22334455', 'pedrol@gmail.com'),
('Laura', 'G�mez', 30, '33445566', 'laurag@gmail.com'),
('Mar�a', 'S�nchez', 28, '44556677', 'marias@gmail.com'),
('Juan', 'Fern�ndez', 35, '55667788', 'juanf@gmail.com'),
('Luis', 'Hern�ndez', 20, '66778899', 'luish@gmail.com');

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

