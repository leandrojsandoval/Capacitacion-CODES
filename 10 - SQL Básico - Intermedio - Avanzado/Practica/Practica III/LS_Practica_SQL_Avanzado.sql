USE LS_Practica_SQL_Basico_2
GO

----------------------------------- Pr�ctica de T-SQL -----------------------------------

/* 1) Hacer una funci�n que dado un art�culo y un deposito devuelva un string que indique el estado 
del dep�sito seg�n el art�culo. Si la cantidad almacenada es menor al l�mite retornar �OCUPACION DEL
DEPOSITO XX %� siendo XX el % de ocupaci�n. Si la cantidad almacenada es mayor o igual al l�mite 
retornar �DEPOSITO COMPLETO�.*/

-- https://learn.microsoft.com/es-es/sql/t-sql/functions/cast-and-convert-transact-sql?view=sql-server-ver16
-- https://learn.microsoft.com/es-es/sql/t-sql/functions/logical-functions-iif-transact-sql?view=sql-server-ver16

CREATE OR ALTER FUNCTION F_Estado_Deposito_De_Articulo (@articulo AS CHAR(8), @deposito AS CHAR(2))
RETURNS VARCHAR(50)
AS
BEGIN
	DECLARE @resultado VARCHAR(50);
	DECLARE @cantidad DECIMAL(12,2) = (SELECT s.Cantidad FROM Stock AS s WHERE s.Producto = @articulo AND s.Deposito = @deposito);
	DECLARE @stockMaximo DECIMAL(12,2) = (SELECT s.StockMaximo FROM Stock AS s WHERE s.Producto = @articulo AND s.Deposito = @deposito);
	IF(@cantidad IS NULL OR @stockMaximo IS NULL)
		SET @resultado = 'ARTICULO Y/O DEPOSITO NO ENCONTRADO';
	ELSE
	BEGIN
		DECLARE @porcentaje DECIMAL(12,2) = @cantidad * 100 / @stockMaximo;
		SET @resultado = IIF(@cantidad < @stockMaximo, 'OCUPACION DEL DEPOSITO ' + CAST(@porcentaje AS VARCHAR) + '%', 'DEPOSITO COMPLETO');
	END
	RETURN @resultado;
END

SELECT * FROM Stock;
SELECT dbo.F_Estado_Deposito_De_Articulo('P001', '02') AS Estado;

/* 2) Realizar una funci�n que dado un art�culo y una fecha, retorne el stock que exist�a a esa fecha. */

CREATE OR ALTER FUNCTION F_Stock_De_Producto_Hasta_Fecha (@articulo AS CHAR(8), @fecha AS SMALLDATETIME) 
RETURNS DECIMAL(12,2)
AS
BEGIN
	DECLARE @stockExistenteHastaLaFecha DECIMAL(12,2) = (
		SELECT SUM(s.Cantidad)
		FROM Stock AS s 
		WHERE s.Producto = @articulo AND s.ProximaReposicion <= @fecha
		GROUP BY s.Producto
	);
	IF(@stockExistenteHastaLaFecha IS NULL)
		SET @stockExistenteHastaLaFecha = 0;
	RETURN @stockExistenteHastaLaFecha;
END

SELECT * FROM Stock WHERE Producto = 'P003'

SELECT dbo.F_Stock_De_Producto_Hasta_Fecha('P003', '17-10-2023');
SELECT dbo.F_Stock_De_Producto_Hasta_Fecha('P003', '20-10-2023');

/* 3) Cree el/los objetos de base de datos necesarios para actualizar la columna de empleado 
empl_comision con la sumatoria del total de lo vendido por ese empleado a lo largo del �ltimo a�o. 
Se deber� retornar el c�digo del vendedor que m�s vendi� (en monto) a lo largo del �ltimo a�o. */

-- https://learn.microsoft.com/es-es/sql/t-sql/functions/getdate-transact-sql?view=sql-server-ver16

CREATE OR ALTER FUNCTION F_Vendedor_Con_Mas_Ventas_Del_Ultimo_Anio() 
RETURNS NUMERIC(6)
AS
BEGIN
	DECLARE @vendedorQueMasVendio NUMERIC(6);

	DECLARE @totalVendidoPorEmpleadosUltimoAnio TABLE(Codigo NUMERIC(6), TotalVendido DECIMAL(12,2));
	INSERT INTO @totalVendidoPorEmpleadosUltimoAnio (Codigo, TotalVendido)
		SELECT f.Vendedor, SUM(f.Total + f.TotalImpuestos) TotalVendido
		FROM Factura AS f
		WHERE YEAR(f.Fecha) = YEAR(GETDATE())
		GROUP BY f.Vendedor;

	SET @vendedorQueMasVendio = (
		SELECT t.Codigo
		FROM @totalVendidoPorEmpleadosUltimoAnio AS t
		WHERE t.TotalVendido = (SELECT MAX(TotalVendido) FROM @totalVendidoPorEmpleadosUltimoAnio)
	);

	RETURN @vendedorQueMasVendio;

END

SELECT * FROM Empleado
SELECT * FROM Factura
SELECT * FROM Empleado AS e INNER JOIN Factura AS f ON e.Codigo = f.Vendedor WHERE YEAR(f.Fecha) = YEAR(GETDATE())

SELECT dbo.F_Vendedor_Con_Mas_Ventas_Del_Ultimo_Anio() 

/* 4) Realizar un procedimiento que complete con los datos existentes en el modelo 
provisto la tabla de hechos denominada Fact_table tiene la siguiente definici�n:*/

CREATE TABLE Fact_table (
	Anio CHAR(4) NOT NULL,
	Mes CHAR(2) NOT NULL,
	Familia CHAR(3) NOT NULL,
	Rubro CHAR(4) NOT NULL,
	Zona CHAR(3) NOT NULL,
	Cliente CHAR(6) NOT NULL,
	Producto CHAR(8) NOT NULL,
	Cantidad DECIMAL(12,2),
	Monto DECIMAL(12,2)
);

ALTER TABLE Fact_table ADD CONSTRAINT PK_Fact_Table PRIMARY KEY (Anio, Mes, Familia, Rubro, Zona, Cliente, Producto);

-- https://learn.microsoft.com/en-us/sql/t-sql/functions/month-transact-sql?view=sql-server-ver16
-- https://learn.microsoft.com/es-es/sql/t-sql/functions/left-transact-sql?view=sql-server-ver16

CREATE OR ALTER PROCEDURE P_Completar_Fact_Table
AS
BEGIN
    INSERT INTO Fact_table (Anio, Mes, Familia, Rubro, Zona, Cliente, Producto, Cantidad, Monto)
    SELECT
        CONVERT(CHAR(4), YEAR(f.Fecha)) AS Anio,
        LEFT('0' + CONVERT(CHAR(2), MONTH(f.Fecha)), 2) AS Mes,
        p.IdFamilia AS Familia,
        p.IdRubro AS Rubro,
        d.Zona AS Zona,
        f.Cliente AS Cliente,
        i.Producto AS Producto,
        i.Cantidad AS Cantidad,
        i.Precio * i.Cantidad AS Monto
    FROM Factura AS f INNER JOIN ItemFactura AS i ON f.Tipo = i.Tipo AND f.Sucursal = i.Sucursal AND f.Numero = i.Numero
    INNER JOIN Producto AS p ON i.Producto = p.Codigo
    INNER JOIN Stock AS s ON p.Codigo = s.Producto
	INNER JOIN Deposito AS d ON s.Deposito = d.Codigo
END;

EXECUTE P_Completar_Fact_Table

SELECT * FROM Fact_table

/* 5) Realizar los triggers para las distintas operaciones (Alta, Baja, Modificaci�n) sobre la 
tabla �clientes�, generando un nuevo registro en la tabla de auditor�a. */

CREATE TABLE AuditoriaCliente (
	Operacion VARCHAR(20),
	FechaOperacion SMALLDATETIME,
	Codigo VARCHAR(6),
	CONSTRAINT PK_AudotoriaCliente PRIMARY KEY (Operacion, FechaOperacion, Codigo)
)

CREATE OR ALTER TRIGGER TG_Alta_Clientes ON Cliente AFTER INSERT AS
BEGIN
	INSERT INTO AuditoriaCliente (Operacion, FechaOperacion, Codigo)
	SELECT 'Alta', GETDATE(), Codigo
	FROM Inserted
END

CREATE OR ALTER TRIGGER TG_Baja_Clientes ON Cliente AFTER DELETE AS
BEGIN
	INSERT INTO AuditoriaCliente (Operacion, FechaOperacion, Codigo)
	SELECT 'Baja', GETDATE(), Codigo
	FROM Deleted
END

CREATE OR ALTER TRIGGER TG_Modificacion_Clientes ON Cliente AFTER UPDATE AS
BEGIN
	INSERT INTO AuditoriaCliente (Operacion, FechaOperacion, Codigo)
	SELECT 'Modificacion', GETDATE(), Codigo
	FROM Inserted
END

INSERT INTO Cliente (Codigo, RazonSocial, Telefono, Domicilio, LimiteCredito, Vendedor) 
VALUES ('C006', 'Tienda A', '555-123-4567', '789 Oak St', 3000, 1002);

UPDATE Cliente 
SET Telefono = '11-3949-0820'
WHERE Codigo = 'C006';

DELETE FROM Cliente WHERE Codigo = 'C006';

SELECT * FROM AuditoriaCliente;

/* 6) Realizar una vista de los siguientes datos del producto:
	- C�digo
	- Detalle
	- Precio
	- Descripci�n de Familia
	- Descripci�n de Rubro
	- Descripci�n de Envase
*/

CREATE OR ALTER VIEW V_Datos_Del_Producto AS (
	SELECT p.Codigo, p.Detalle, p.Precio, f.Detalle AS DetalleFamilia, r.Detalle AS DetalleRubro, e.Detalle AS DetalleEnvase
	FROM Producto AS p INNER JOIN Familia AS f ON p.IdFamilia = f.Id 
	INNER JOIN Rubro AS r ON p.IdRubro = r.Id
	INNER JOIN Envase AS e ON p.CodigoEnvase = e.Codigo
);

SELECT *
FROM V_Datos_Del_Producto;