CREATE DATABASE LS_Practica_SQL_Basico_2;
USE LS_Practica_SQL_Basico_2;

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

INSERT INTO Envase (Codigo, Detalle) VALUES
(1, 'Botella de vidrio'),
(2, 'Caja de cartón'),
(3, 'Bolsa de plástico');

INSERT INTO Familia (Id, Detalle) VALUES
('F01', 'Bebidas'),
('F02', 'Alimentos'),
('F03', 'Electrónica');

INSERT INTO Rubro (Id, Detalle) VALUES
('R001', 'Refrescos'),
('R002', 'Snacks'),
('R003', 'Televisores'),
('R004', 'Hogar'),
('R005', 'Ropa');

INSERT INTO Producto (Codigo, Detalle, Precio, IdFamilia, IdRubro, CodigoEnvase) VALUES
('P001', 'Refresco de cola', 1.99, 'F01', 'R001', 1),
('P002', 'Papas fritas', 0.99, 'F02', 'R002', 2),
('P003', 'Televisor LED', 499.99, 'F03', 'R003', 3),
('P004', 'Lámpara de mesa', 19.99, 'F03', 'R004', 1),
('P005', 'Camisa', 29.99, 'F03', 'R005', 2);

INSERT INTO Zona (Codigo, Detalle) VALUES
('Z01', 'Zona Norte'),
('Z02', 'Zona Sur'),
('Z03', 'Zona Este');

INSERT INTO Departamento (Codigo, Detalle, Zona) VALUES
(101, 'Ventas', 'Z01'),
(102, 'Almacén', 'Z02'),
(103, 'Soporte', 'Z03');

INSERT INTO Empleado (Codigo, Nombre, Apellido, Nacimiento, Ingreso, Tareas, Salario, Comision, Jefe, Departamento) VALUES
(1001, 'Juan', 'Pérez', '15-05-1980', '10-02-2020', 'Ventas', 35000.00, 0.05, NULL, 101),
(1002, 'Ana', 'López', '20-11-1990', '05-04-2021', 'Almacén', 28000.00, 0.03, NULL, 102);

INSERT INTO Deposito (Codigo, Detalle, Domicilio, Telefono, Encargado, Zona) VALUES
('01', 'Depósito Central', '123 Main St', '123-456-7890', 1002, 'Z02'),
('02', 'Depósito Norte', '456 Elm St', '987-654-3210', 1001, 'Z01');

INSERT INTO Cliente (Codigo, RazonSocial, Telefono, Domicilio, LimiteCredito, Vendedor) VALUES
('C001', 'Supermercado ABC', '555-123-4567', '789 Oak St', 10000.00, 1001),
('C002', 'Tienda XYZ', '555-987-6543', '321 Pine St', 5000.00, 1001),
('C003', 'Tienda A', '555-555-5555', '123 Elm St', 1500.00, 1001),
('C004', 'Supermercado XYZ', '555-789-1234', '456 Oak St', 2000.00, 1001),
('C005', 'Bodega B', '555-111-2222', '789 Pine St', 800.00, 1002);

INSERT INTO Stock (Producto, Deposito, Cantidad, PuntoReposicion, StockMaximo, Detalle, ProximaReposicion) VALUES
('P001', '01', 1000.00, 200.00, 5000.00, 'Stock de refrescos', '20-10-2023'),
('P002', '01', 500.00, 100.00, 2000.00, 'Stock de papas fritas', '22-10-2023'),
('P003', '01', 1000.00, 200.00, 5000.00, 'Stock de refrescos', '20-10-2023'),
('P004', '01', 800.00, 100.00, 2000.00, 'Stock de papas fritas', '22-10-2023'),
('P005', '01', 50.00, 20.00, 200.00, 'Stock de televisores', '15-10-2023'),
('P001', '02', 500.00, 200.00, 3000.00, 'Stock de refrescos', '20-10-2023'),
('P002', '02', 600.00, 100.00, 1500.00, 'Stock de papas fritas', '22-10-2023'),
('P003', '02', 30.00, 20.00, 150.00, 'Stock de televisores', '15-10-2023'),
('P004', '02', 100.00, 20.00, 300.00, 'Stock de lámparas', '15-10-2023'),
('P005', '02', 50.00, 20.00, 200.00, 'Stock de lámparas', '15-10-2023');

INSERT INTO Composicion (Producto, Componente, Cantidad) VALUES
('P003', 'P001', 1.00),
('P003', 'P002', 1.00),
('P001', 'P002', 0.5),
('P001', 'P004', 0.2),
('P002', 'P003', 0.1),
('P003', 'P004', 1.0),
('P003', 'P005', 0.5);

INSERT INTO Factura (Tipo, Sucursal, Numero, Fecha, Vendedor, Total, TotalImpuestos, Cliente) VALUES
('V', 'S001', '20231001', '2023-10-01', 1001, 50.00, 7.50, 'C001'),
('V', 'S002', '20231002', '2023-10-02', 1001, 30.00, 4.50, 'C002'),
('V', 'S001', '20120101', '2012-01-01', 1001, 500.00, 75.00, 'C001'),
('V', 'S001', '20120102', '2012-01-02', 1001, 320.00, 48.00, 'C002'),
('V', 'S002', '20120103', '2012-01-03', 1002, 750.00, 112.50, 'C003'),
('V', 'S002', '20120104', '2012-01-04', 1001, 600.00, 90.00, 'C004'),
('V', 'S001', '20110101', '2011-01-01', 1001, 100.00, 15.00, 'C001'),
('V', 'S002', '20110102', '2011-01-02', 1001, 200.00, 30.00, 'C002'),
('V', 'S001', '20110103', '2011-01-03', 1001, 300.00, 45.00, 'C003'),
('V', 'S002', '20110104', '2011-01-04', 1001, 400.00, 60.00, 'C004'),
('V', 'S002', '20120105', '2012-01-05', 1001, 400.00, 60.00, 'C004'),
('V', 'S001', '20120105', '2012-01-05', 1001, 400.00, 60.00, 'C004');

INSERT INTO ItemFactura (Tipo, Sucursal, Numero, Producto, Cantidad, Precio) VALUES
('V', 'S001', '20231001', 'P001', 10.00, 1.99),
('V', 'S001', '20231001', 'P002', 5.00, 0.99),
('V', 'S002', '20231002', 'P001', 6.00, 1.99),
('V', 'S002', '20231002', 'P002', 4.00, 0.99),
('V', 'S001', '20120101', 'P001', 100.00, 1.99),
('V', 'S001', '20120101', 'P002', 50.00, 0.99),
('V', 'S001', '20120102', 'P001', 64.00, 1.99),
('V', 'S001', '20120102', 'P003', 15.00, 499.99),
('V', 'S002', '20120103', 'P002', 80.00, 0.99),
('V', 'S002', '20120104', 'P001', 120.00, 1.99),
('V', 'S002', '20120104', 'P002', 40.00, 0.99),
('V', 'S001', '20110101', 'P001', 50.00, 1.99),
('V', 'S001', '20110101', 'P002', 30.00, 0.99),
('V', 'S002', '20110102', 'P001', 100.00, 1.99),
('V', 'S002', '20110102', 'P002', 60.00, 0.99),
('V', 'S001', '20110103', 'P001', 150.00, 1.99),
('V', 'S001', '20110103', 'P002', 90.00, 0.99),
('V', 'S002', '20110104', 'P001', 200.00, 1.99),
('V', 'S002', '20110104', 'P002', 120.00, 0.99),
('V', 'S001', '20120105', 'P001', 120.00, 1.99),
('V', 'S002', '20120105', 'P001', 120.00, 1.99);

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

SELECT p.IdRubro, p.Codigo, p.Detalle, COUNT(*) CantidadDeArticulos, SUM(s.Cantidad) StockTotal
FROM Producto AS p INNER JOIN Stock AS s ON p.Codigo = s.Producto
GROUP BY p.IdRubro, p.Codigo, p.Detalle
HAVING SUM(s.Cantidad) > (
	SELECT SUM(s.Cantidad) AS StockTotalProducto0
	FROM Stock AS s
	WHERE s.Producto = '00000000' AND s.Deposito = '00'
	GROUP BY s.Producto
);