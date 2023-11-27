CREATE DATABASE LS_Practica_SQL_2;
USE LS_Practica_SQL_2;

CREATE TABLE Envase (
	Codigo NUMERIC(6) PRIMARY KEY,
	Detalle CHAR(50)
);

CREATE TABLE Familia (
	Id CHAR(3) PRIMARY KEY,
	Detalle CHAR(50)
);

CREATE TABLE Rubro (
	Id CHAR(4) PRIMARY KEY,
	Detalle CHAR(50)
);

CREATE TABLE Zona (
	Codigo CHAR(3) PRIMARY KEY,
	Detalle CHAR(50)
);

CREATE TABLE Cliente (
	Codigo CHAR(6) PRIMARY KEY,
	RazonSocial CHAR(100),
	Telefono CHAR(100),
	Domicilio CHAR(100),
	LimiteCredito DECIMAL(12,2),
	Vendedor NUMERIC(6)
);

CREATE TABLE Departamento (
	Codigo NUMERIC(6) PRIMARY KEY,
	Detalle CHAR(50),
	Zona CHAR(3),
	CONSTRAINT FK_Departamento_Zona FOREIGN KEY (Zona) REFERENCES Zona(Codigo)
);

DROP TABLE Empleado

CREATE TABLE Empleado (
	Codigo NUMERIC(6) PRIMARY KEY,
	Nombre CHAR(50),
	Apellido CHAR(50),
	Nacimiento SMALLDATETIME,
	Ingreso SMALLDATETIME,
	Tareas CHAR(100),
	Salario DECIMAL(12,2),
	Comision DECIMAL(12,2),
	Jefe NUMERIC(6),
	Departamento NUMERIC(6),
	CONSTRAINT FK_Empleado_Departamento FOREIGN KEY (Departamento) REFERENCES Departamento(Codigo),
	CONSTRAINT FK_Empleado_Jefe FOREIGN KEY (Jefe) REFERENCES Empleado(Codigo)
);

CREATE TABLE Producto (
	Codigo CHAR(8) PRIMARY KEY,
	Detalle CHAR(50),
	Precio DECIMAL(12,2),
	IdFamilia CHAR(3),
	IdRubro CHAR(4),
	CodigoEnvase NUMERIC(6),
	CONSTRAINT FK_Producto_IdRubro FOREIGN KEY (IdRubro) REFERENCES Rubro(Id),
	CONSTRAINT FK_Producto_IdFamilia FOREIGN KEY (IdFamilia) REFERENCES Familia(Id),
	CONSTRAINT FK_Producto_CodigoEnvase FOREIGN KEY (CodigoEnvase) REFERENCES Envase(Codigo)
);

CREATE TABLE Composicion (
	Producto CHAR(8),
	Componente CHAR(8),
	Cantidad DECIMAL(12,2),
	CONSTRAINT PK_Composicion PRIMARY KEY (Producto, Componente),
	CONSTRAINT FK_Composicion_Producto FOREIGN KEY (Producto) REFERENCES Producto(Codigo),
	CONSTRAINT FK_Composicion_Componente FOREIGN KEY (Componente) REFERENCES Producto(Codigo),
);

CREATE TABLE Deposito (
	Codigo CHAR(2) PRIMARY KEY,
	Detalle CHAR(50),
	Domicilio CHAR(50),
	Telefono CHAR(50),
	Encargado NUMERIC(6),
	Zona CHAR(3),
	CONSTRAINT FK_Deposito_Encargado FOREIGN KEY (Encargado) REFERENCES Empleado(Codigo),
	CONSTRAINT FK_Deposito_Zona FOREIGN KEY (Zona) REFERENCES Zona(Codigo)
);

CREATE TABLE Stock (
	Producto CHAR(8),
	Deposito CHAR(2),
	Cantidad DECIMAL(12,2),
	PuntoReposicion DECIMAL(12,2),
	StockMaximo DECIMAL(12,2),
	Detalle CHAR(100),
	ProximaReposicion SMALLDATETIME,
	CONSTRAINT PK_Stock PRIMARY KEY (Producto, Deposito),
	CONSTRAINT FK_Stock_Producto FOREIGN KEY (Producto) REFERENCES Producto(Codigo),
	CONSTRAINT FK_Stock_Deposito FOREIGN KEY (Deposito) REFERENCES Deposito(Codigo)
);

CREATE TABLE Factura (
	Tipo CHAR(1),
	Sucursal CHAR(4),
	Numero CHAR(8),
	Fecha SMALLDATETIME,
	Vendedor NUMERIC(6),
	Total DECIMAL(12,2),
	TotalImpuestos DECIMAL(12,2),
	Cliente CHAR(6),
	CONSTRAINT PK_Factura PRIMARY KEY (Tipo, Sucursal, Numero),
	CONSTRAINT FK_Factura_Cliente FOREIGN KEY (Cliente) REFERENCES Cliente(Codigo)
);

CREATE TABLE ItemFactura (
	Tipo CHAR(1),
	Sucursal CHAR(4),
	Numero CHAR(8),
	Producto CHAR(8),
	Cantidad DECIMAL(12,2),
	Precio DECIMAL(12,2),
	CONSTRAINT PK_ItemFactura PRIMARY KEY (Tipo, Sucursal, Numero, Producto),
	CONSTRAINT FK_ItemFactura_Factura FOREIGN KEY (Tipo, Sucursal, Numero) REFERENCES Factura(Tipo, Sucursal, Numero),
	CONSTRAINT FK_ItemFactura_Producto FOREIGN KEY (Producto) REFERENCES Producto(Codigo)
);

GO

CREATE OR ALTER PROCEDURE P_Eliminar_Datos AS
BEGIN
	DELETE ItemFactura
	DELETE Factura
	DELETE Stock
	DELETE Deposito
	DELETE Composicion
	DELETE Producto
	DELETE Cliente
	DELETE Empleado
	DELETE Departamento
	DELETE Zona
	DELETE Envase
	DELETE Familia
	DELETE Rubro
END

GO

CREATE OR ALTER PROCEDURE P_Insertar_Valores AS
BEGIN
	INSERT INTO LS_Practica_SQL_2.dbo.Rubro(Id, Detalle) SELECT * FROM CAP_Practica_2.dbo.Rubro;
	INSERT INTO LS_Practica_SQL_2.dbo.Familia(Id, Detalle) SELECT * FROM CAP_Practica_2.dbo.Familia;
	INSERT INTO LS_Practica_SQL_2.dbo.Envase(Codigo, Detalle) SELECT * FROM CAP_Practica_2.dbo.Envases;
	INSERT INTO LS_Practica_SQL_2.dbo.Zona(Codigo, Detalle) SELECT * FROM CAP_Practica_2.dbo.Zona;
	INSERT INTO LS_Practica_SQL_2.dbo.Departamento(Codigo, Detalle, Zona) SELECT * FROM CAP_Practica_2.dbo.Departamento;
	INSERT INTO LS_Practica_SQL_2.dbo.Empleado(Codigo, Nombre, Apellido, Nacimiento, Ingreso, Tareas, Salario, Comision, Jefe, Departamento) SELECT * FROM CAP_Practica_2.dbo.Empleado;
	INSERT INTO LS_Practica_SQL_2.dbo.Cliente(Codigo, RazonSocial, Telefono, Domicilio, LimiteCredito, Vendedor) SELECT * FROM CAP_Practica_2.dbo.Cliente;
	INSERT INTO LS_Practica_SQL_2.dbo.Producto(Codigo, Detalle, Precio, IdFamilia, IdRubro, CodigoEnvase) SELECT * FROM CAP_Practica_2.dbo.Producto;
	INSERT INTO LS_Practica_SQL_2.dbo.Composicion(Cantidad, Producto, Componente) SELECT * FROM CAP_Practica_2.dbo.Composicion;
	INSERT INTO LS_Practica_SQL_2.dbo.Deposito(Codigo, Detalle, Domicilio, Telefono, Encargado, Zona) SELECT * FROM CAP_Practica_2.dbo.Deposito;
	INSERT INTO LS_Practica_SQL_2.dbo.Stock(Cantidad, PuntoReposicion, StockMaximo, Detalle, ProximaReposicion, Producto, Deposito) SELECT * FROM CAP_Practica_2.dbo.Stock;
	INSERT INTO LS_Practica_SQL_2.dbo.Factura(Tipo, Sucursal, Numero, Fecha, Vendedor, Total, TotalImpuestos, Cliente) SELECT * FROM CAP_Practica_2.dbo.Factura;
	INSERT INTO LS_Practica_SQL_2.dbo.ItemFactura(Tipo, Sucursal, Numero, Producto, Cantidad, Precio) SELECT * FROM CAP_Practica_2.dbo.Item_Factura;
END

EXECUTE P_Insertar_Valores

/* Práctica de SQL 
Según el modelo dado resuelva:*/

/* 1. Mostrar el código, razón social de todos los clientes cuyo límite de crédito 
sea mayor o igual a $ 1000 ordenado por código de cliente. */

SELECT c.Codigo, c.RazonSocial
FROM Cliente AS c
WHERE c.LimiteCredito >= 1000
ORDER BY c.Codigo;

/* 2. Mostrar el código, detalle de todos los artículos vendidos en el año 2012 ordenados por cantidad vendida. */

SELECT p.Codigo, p.Detalle, COUNT(*) CantidadVendida
FROM Factura AS f INNER JOIN ItemFactura AS i ON f.Tipo = i.Tipo AND f.Sucursal = i.Sucursal AND f.Numero = i.Numero
INNER JOIN Producto AS p ON p.Codigo = i.Producto
WHERE YEAR(f.Fecha) = 2012
GROUP BY p.Codigo, p.Detalle
ORDER BY CantidadVendida;

/* 3. Realizar una consulta que muestre código de producto, nombre de producto y el stock total, sin importar en 
que deposito se encuentre, los datos deben ser ordenados por nombre del artículo de menor a mayor. */

SELECT p.Codigo, p.Detalle, SUM(s.Cantidad) StockTotal
FROM Producto AS p INNER JOIN Stock AS s ON p.Codigo = s.Producto
GROUP BY p.Codigo, p.Detalle
ORDER BY p.Detalle;

/* 4. Realizar una consulta que muestre para todos los artículos código, detalle y cantidad de artículos que lo 
componen. Mostrar solo aquellos artículos para los cuales el stock promedio por depósito sea mayor a 100. */

SELECT c.Producto, p.Detalle, COUNT(DISTINCT c.Componente) CantidadArticulosCompuestos
FROM Composicion AS c INNER JOIN Producto AS p ON c.Producto = p.Codigo INNER JOIN Stock AS s ON s.Producto = c.Producto
GROUP BY c.Producto, p.Detalle
HAVING AVG(s.Cantidad) > 100;

/* 5. Realizar una consulta que muestre código de artículo, detalle y cantidad de egresos de stock que se 
realizaron para ese artículo en el año 2012 (egresan los productos que fueron vendidos). Mostrar solo aquellos 
que hayan tenido más egresos que en el 2011. */

CREATE OR ALTER VIEW V_Cantidad_Egresos_Por_Producto_2011 AS (
	SELECT p.Codigo, p.Detalle, COUNT(*) CantidadDeEgresos
	FROM Factura AS f INNER JOIN ItemFactura AS i ON f.Tipo = i.Tipo AND f.Sucursal = i.Sucursal AND f.Numero = i.Numero
	INNER JOIN Producto AS p ON p.Codigo = i.Producto
	WHERE YEAR(f.Fecha) = 2011
	GROUP BY p.Codigo, p.Detalle
);

CREATE OR ALTER VIEW V_Cantidad_Egresos_Por_Producto_2012 AS (
	SELECT p.Codigo, p.Detalle, COUNT(*) CantidadDeEgresos
	FROM Factura AS f INNER JOIN ItemFactura AS i ON f.Tipo = i.Tipo AND f.Sucursal = i.Sucursal AND f.Numero = i.Numero
	INNER JOIN Producto AS p ON p.Codigo = i.Producto
	WHERE YEAR(f.Fecha) = 2012
	GROUP BY p.Codigo, p.Detalle
);

SELECT v2012.*
FROM V_Cantidad_Egresos_Por_Producto_2011 AS v2011 INNER JOIN V_Cantidad_Egresos_Por_Producto_2012 AS v2012 ON v2011.Codigo = v2012.Codigo
WHERE v2012.CantidadDeEgresos > v2011.CantidadDeEgresos
 
/* 6. Mostrar para todos los rubros, artículos código, detalle, cantidad de artículos de ese rubro y stock total
de ese rubro de artículos. Solo tener en cuenta aquellos artículos que tengan un stock mayor al del 
artículo ‘00000000’ en el depósito ‘00’.*/

SELECT p.IdRubro AS Rubro, p.Codigo, p.Detalle, COUNT(*) AS 'Cantidad de articulos', SUM(s.Cantidad) AS 'Stock total'
FROM Producto AS p INNER JOIN Stock AS s ON p.Codigo = s.Producto
GROUP BY p.IdRubro, p.Codigo, p.Detalle
HAVING SUM(s.Cantidad) > (
	SELECT SUM(s.Cantidad) AS 'Stock total articulo 00000000'
	FROM Stock AS s
	WHERE s.Producto = '00000000' AND s.Deposito = '00'
	GROUP BY s.Producto
);