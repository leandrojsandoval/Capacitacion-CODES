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
('Miguel', 'Gómez', 24, '56789012', 'miguel.g@gmail.com');

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

CREATE OR ALTER PROCEDURE P_Filtrar_Personas @nombreColumna NVARCHAR(128), @valor NVARCHAR(100) AS
BEGIN
	/* En esta variable se va a guardar la consulta dinamica */
	DECLARE @consulta NVARCHAR(MAX);

	/* Pongo a valor el comodin para que busque algun registro con ese valor
	 * https://learn.microsoft.com/es-es/sql/t-sql/language-elements/percent-character-wildcard-character-s-to-match-transact-sql?view=sql-server-ver16 */

	SET @valor = '%' + @valor + '%'
    
	/* Verifica que la columna @nombreColumna exista en la tabla Persona usando la vista sys.columns
	 * https://learn.microsoft.com/es-es/sql/relational-databases/system-catalog-views/sys-columns-transact-sql?view=sql-server-ver16
	 * Y tambien verifica que la tabla Persona exista con OBJECT_ID
	 * https://learn.microsoft.com/es-es/sql/t-sql/functions/object-id-transact-sql?view=sql-server-ver16
	 *
	 * SELECT * 
	 * FROM sys.columns
	 * WHERE OBJECT_ID = OBJECT_ID('Persona'); */

	IF EXISTS (SELECT 1 FROM sys.columns WHERE name = @nombreColumna AND OBJECT_ID = OBJECT_ID('Persona'))
    BEGIN

	/* Realiza una consulta dinamica (PREGUNTAR) guardandola en la variable
	 * https://learn.microsoft.com/es-es/sql/t-sql/functions/quotename-transact-sql?view=sql-server-ver16
	 * https://learn.microsoft.com/es-es/sql/relational-databases/system-stored-procedures/sp-executesql-transact-sql?view=sql-server-ver16
	 * Luego se llama a un procedure que ejecuta consultas dinamicas, como parametros recibe:
	 * 1 - La consulta dinamica
	 * 2 - Se indica los parametros que se va a pasar a la consulta con su tipo de dato
	 * 3 - Se indica el valor del parametro */

        SET @consulta = 'SELECT * FROM Persona WHERE ' + QUOTENAME(@nombreColumna) + ' LIKE @valor;';
        EXEC sp_executesql @consulta, N'@valor NVARCHAR(100)', @valor;
    END
    ELSE
        RAISERROR ('Nombre de columna no válido.', 16, 1);
END

GO

/*
CREATE PROCEDURE P_Filtrar_Personas_Mio @nombreColumna VARCHAR(50), @valor VARCHAR(30) AS
BEGIN
	SELECT * FROM Persona WHERE @nombreColumna LIKE '%' + @valor + '%';
END

EXECUTE P_Filtrar_Personas_Mio 'Email', '@hotmail.com';
*/

-- EXECUTE P_Eliminar_Personas

EXECUTE P_Obtener_Personas

EXECUTE P_Filtrar_Personas 'Email', '@hotmail.com';

