USE LS_Practica_SQL_2
GO

----------------------------------- Práctica de T-SQL -----------------------------------

/* 1) Hacer una función que dado un artículo y un deposito devuelva un string que indique el estado 
del depósito según el artículo. Si la cantidad almacenada es menor al límite retornar “OCUPACION DEL
DEPOSITO XX %” siendo XX el % de ocupación. Si la cantidad almacenada es mayor o igual al límite 
retornar “DEPOSITO COMPLETO”.*/

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

SELECT dbo.F_Estado_Deposito_De_Articulo('00000102', '02') AS Estado;

/* 2) Realizar una función que dado un artículo y una fecha, retorne el stock que existía a esa fecha. */

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

SELECT * FROM Stock WHERE Producto = '00000102'

SELECT dbo.F_Stock_De_Producto_Hasta_Fecha('00000102', '2023-10-17') AS StockExistenteHastaLaFecha;

/* 3) Cree el/los objetos de base de datos necesarios para actualizar la columna de empleado 
empl_comision con la sumatoria del total de lo vendido por ese empleado a lo largo del último año. 
Se deberá retornar el código del vendedor que más vendió (en monto) a lo largo del último año. */

-- https://learn.microsoft.com/es-es/sql/t-sql/functions/getdate-transact-sql?view=sql-server-ver16

CREATE OR ALTER FUNCTION F_Vendedor_Con_Mas_Ventas_Del_Ultimo_Anio() 
RETURNS NUMERIC(6)
AS
BEGIN
	DECLARE @vendedorQueMasVendio NUMERIC(6);
	DECLARE @totalVendidoPorEmpleadosUltimoAnio TABLE(Codigo NUMERIC(6), TotalVendido DECIMAL(12,2));
	
	DECLARE @ultimoAnio INT = (SELECT MAX(YEAR(f.Fecha)) FROM Factura AS f);

	INSERT INTO @totalVendidoPorEmpleadosUltimoAnio (Codigo, TotalVendido)
		SELECT f.Vendedor, SUM(f.Total + f.TotalImpuestos) TotalVendido
		FROM Factura AS f
		WHERE YEAR(f.Fecha) = @ultimoAnio
		GROUP BY f.Vendedor;

	SET @vendedorQueMasVendio = (
		SELECT t.Codigo
		FROM @totalVendidoPorEmpleadosUltimoAnio AS t
		WHERE t.TotalVendido = (SELECT MAX(TotalVendido) FROM @totalVendidoPorEmpleadosUltimoAnio)
	);

	RETURN @vendedorQueMasVendio;
END

SELECT f.Vendedor, SUM(f.Total + f.TotalImpuestos) TotalVendido
FROM Factura AS f
WHERE YEAR(f.Fecha) = (SELECT MAX(YEAR(f.Fecha)) FROM Factura AS f)
GROUP BY f.Vendedor
ORDER BY Vendedor

SELECT dbo.F_Vendedor_Con_Mas_Ventas_Del_Ultimo_Anio() AS VendedorConMasVentasEnElUltimoAño;

/* 4) Realizar un procedimiento que complete con los datos existentes en el modelo 
provisto la tabla de hechos denominada Fact_table tiene la siguiente definición:*/

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
-- https://www.sqlservertutorial.net/sql-server-basics/sql-server-select-distinct/

CREATE OR ALTER PROCEDURE P_Completar_Fact_Table AS
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
        SUM(i.Cantidad) AS Cantidad,
        SUM(i.Precio * i.Cantidad) AS Monto
    FROM Factura AS f 
		INNER JOIN ItemFactura AS i ON f.Tipo = i.Tipo AND f.Sucursal = i.Sucursal AND f.Numero = i.Numero
		INNER JOIN Producto AS p ON i.Producto = p.Codigo
		INNER JOIN Stock AS s ON p.Codigo = s.Producto
		INNER JOIN Deposito AS d ON s.Deposito = d.Codigo AND d.Zona IS NOT NULL
	GROUP BY 
		CONVERT(CHAR(4), YEAR(f.Fecha)), 
		LEFT('0' + CONVERT(CHAR(2), MONTH(f.Fecha)), 2), 
		p.IdFamilia, 
		p.IdRubro, 
		d.Zona, 
		f.Cliente, 
		i.Producto
END;

EXECUTE P_Completar_Fact_Table

SELECT * FROM Fact_table

/* 5) Realizar los triggers para las distintas operaciones (Alta, Baja, Modificación) sobre la 
tabla “clientes”, generando un nuevo registro en la tabla de auditoría. */

-- https://learn.microsoft.com/es-es/sql/t-sql/language-elements/coalesce-transact-sql?view=sql-server-ver16
-- https://learn.microsoft.com/es-es/sql/t-sql/functions/concat-transact-sql?view=sql-server-ver16
-- https://learn.microsoft.com/es-es/sql/t-sql/functions/rtrim-transact-sql?view=sql-server-ver16

CREATE TABLE AuditoriaCliente (
	Id INT IDENTITY(1,1),
	Operacion VARCHAR(20),
	FechaOperacion SMALLDATETIME,
	Codigo CHAR(6),
	CadenaRegistro TEXT,
	CONSTRAINT FK_AuditoriaCliente_Codigo FOREIGN KEY (Codigo) REFERENCES Cliente(Codigo)
)

CREATE OR ALTER TRIGGER TG_Alta_Clientes ON Cliente AFTER INSERT AS
BEGIN
	INSERT INTO AuditoriaCliente (Operacion, FechaOperacion, Codigo, CadenaRegistro)
	SELECT 'Alta', GETDATE(), i.Codigo, 
	CONCAT(
		COALESCE(RTRIM(i.RazonSocial), ''), ' - ',
		COALESCE(RTRIM(i.Telefono), ''), ' - ',
		COALESCE(RTRIM(i.Domicilio), ''), ' - ',
		COALESCE(CAST(RTRIM(i.LimiteCredito) AS VARCHAR(10)), ''), ' - ',
		COALESCE(CAST(RTRIM(i.Vendedor) AS VARCHAR(5)), '')
	)
	FROM Inserted AS i
END

CREATE OR ALTER TRIGGER TG_Baja_Clientes ON Cliente AFTER DELETE AS
BEGIN
	INSERT INTO AuditoriaCliente (Operacion, FechaOperacion, Codigo, CadenaRegistro)
	SELECT 'Baja', GETDATE(), d.Codigo, 
	CONCAT(
		COALESCE(RTRIM(d.RazonSocial), ''), ' - ',
		COALESCE(RTRIM(d.Telefono), ''), ' - ',
		COALESCE(RTRIM(d.Domicilio), ''), ' - ',
		COALESCE(CAST(RTRIM(d.LimiteCredito) AS VARCHAR(10)), ''), ' - ',
		COALESCE(CAST(RTRIM(d.Vendedor) AS VARCHAR(5)), '')
	)
	FROM Deleted AS d
END

CREATE OR ALTER TRIGGER TG_Modificacion_Clientes ON Cliente AFTER UPDATE AS
BEGIN
	INSERT INTO AuditoriaCliente (Operacion, FechaOperacion, Codigo, CadenaRegistro)
	SELECT 'Modificacion', GETDATE(), i.Codigo, 
	CONCAT(
		COALESCE(RTRIM(i.RazonSocial), ''), ' - ',
		COALESCE(RTRIM(i.Telefono), ''), ' - ',
		COALESCE(RTRIM(i.Domicilio), ''), ' - ',
		COALESCE(CAST(RTRIM(i.LimiteCredito) AS VARCHAR(10)), ''), ' - ',
		COALESCE(CAST(RTRIM(i.Vendedor) AS VARCHAR(5)), '')
	)
	FROM Inserted AS i
END

UPDATE Cliente 
SET Telefono = '11-3949-0820'
WHERE Codigo = '00001';

SELECT * FROM AuditoriaCliente;

/* 6) Realizar una vista de los siguientes datos del producto:
	- Código
	- Detalle
	- Precio
	- Descripción de Familia
	- Descripción de Rubro
	- Descripción de Envase
*/

CREATE OR ALTER VIEW V_Datos_Del_Producto AS (
	SELECT p.Codigo, p.Detalle, p.Precio, f.Detalle AS DetalleFamilia, r.Detalle AS DetalleRubro, e.Detalle AS DetalleEnvase
	FROM Producto AS p INNER JOIN Familia AS f ON p.IdFamilia = f.Id 
	INNER JOIN Rubro AS r ON p.IdRubro = r.Id
	INNER JOIN Envase AS e ON p.CodigoEnvase = e.Codigo
);

SELECT *
FROM V_Datos_Del_Producto;