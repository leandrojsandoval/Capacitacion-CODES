/* Se desea realizar un sistema de gesti�n comercial que permita almacenar informaci�n de los 1
clientes, proveedores y productos.
A continuaci�n se detalla la informaci�n que se desea persistir en el sistema para lo 
cual deber�n crear la tablas correspondientes:

Informaci�n de los tipos de Documentos (Tabla TiposDocumentos)
	- Id (Incrementable)
	- Descripci�n (20 caracteres alfanum�ricos)

Informaci�n de las condiciones de IVA (Tabla CondicionesIva)
	- C�digo (5 caracteres alfanum�ricos)
	- Descripci�n (50 caracteres alfanum�ricos)

Informaci�n de los clientes (Tabla Clientes)
	- Tipo de documento (Obligatorio)
	- N�mero de Documento (Entero, obligatorio)
	- Apellido (50 caracteres, obligatorio)
	- Nombres (50 caracteres, obligatorio)
	- Condici�n de IVA (Obligatorio)
	- Tel�fono (20 caracteres, opcional)
	- Celular (20 caracteres, opcional)
	- Email (255 caracteres, opcional)
	- Domicilio
		Calle (100 caracteres, opcional) 
		Numero (entero, opcional)
		C�digo Postal (10 caracteres, opcional)
	- Estado (entero, Obligatorio) (Puede estar activo o deshabilitado)

Informaci�n de los proveedores (Tabla Proveedores)
	- Id (Incrementable)
	- Nombre de fantas�a (100 caracteres, opcional)
	- Raz�n Social (100 caracteres, obligatorio)
	- Numero de CUIT (Num�rico, obligatorio)
	- Domicilio
		Calle (100 caracteres, opcional) 
		Numero (Entero, opcional)
		C�digo Postal (10 caracteres, opcional)
	- Tel�fono (20 caracteres, opcional)
	- Email (255 caracteres, opcional)
	- Condici�n de IVA (Obligatorio)
	- Nombre Contacto (50 caracteres, obligatorio)
	- Estado (Entero, obligatorio) (Puede estar activo o deshabilitado)

Informaci�n de los Productos (Tabla Productos)
	- C�digo (5 caracteres alfanum�ricos)
	- Descripci�n (50 caracteres)
	- Precio de compra (Decimal, m�ximo dos d�gitos decimales)
	- Precio sugerido de venta (Decimal, m�ximo dos d�gitos decimales, opcional)
	- Estado (Entero, obligatorio) (Pueden estar disponibles o no para vender)
	
Informaci�n de los Dep�sitos donde existir� stock de Productos (Tabla Dep�sitos)
	- Id (Incrementable)
	- Nombre (100 caracteres)
	- Domicilio
		Calle (100 caracteres, opcional) 
		Numero (Entero, opcional)
		C�digo Postal (10 caracteres, opcional)
	- Tel�fono (10 caracteres, opcional)
	- Email (255 caracteres, opcional)	
*/

------------------ PARTE I - Creaci�n de tablas y carga inicial de datos ------------------

CREATE DATABASE LS_Practica_SQL_Basico_1;
USE LS_Practica_SQL_Basico_1;

/*1. Realizar los scripts de creaci�n de las tablas identificando claves primarias y for�neas.*/

CREATE TABLE TipoDocumento (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Descripcion VARCHAR(20) NOT NULL
);

CREATE TABLE CondicionIva (
	Codigo VARCHAR(5) PRIMARY KEY,
	Descripcion VARCHAR(50) NOT NULL
);

CREATE TABLE Cliente (
	TipoDocumento INT,
	NumeroDocumento INT,
	Apellido VARCHAR(50) NOT NULL,
	Nombre VARCHAR(50) NOT NULL,
	CondicionIVA VARCHAR(5),
	Telefono VARCHAR(20),
	Celular VARCHAR(20),
	Email VARCHAR(255),
	Calle VARCHAR(100),
	Numero INT,
	CodigoPostal VARCHAR(10),
	Estado BIT NOT NULL,
	CONSTRAINT PK_Cliente PRIMARY KEY (NumeroDocumento, TipoDocumento),
	CONSTRAINT FK_Cliente_TipoDocumento FOREIGN KEY (TipoDocumento) REFERENCES TipoDocumento(Id),
	CONSTRAINT FK_Cliente_CondicionIVA FOREIGN KEY (CondicionIVA) REFERENCES CondicionIva(Codigo)
);

CREATE TABLE Proveedor (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	NombreFantasia VARCHAR(100),
	RazonSocial VARCHAR(100) NOT NULL,
	CUIT NUMERIC NOT NULL,
	Calle VARCHAR(100),
	Numero INT,
	CodigoPostal VARCHAR(10),
	Telefono VARCHAR(20),
	Email VARCHAR(255),
	CondicionIVA VARCHAR(5),
	NombreContacto VARCHAR(50) NOT NULL,
	Estado BIT NOT NULL,
	CONSTRAINT FK_Proveedor_CondicionIVA FOREIGN KEY (CondicionIVA) REFERENCES CondicionIva(Codigo)
);

CREATE TABLE Producto (
	Codigo VARCHAR(5) PRIMARY KEY,
	Descripcion VARCHAR(50) NOT NULL,
	Precio DECIMAL(10,2) NOT NULL,
	PrecioVenta DECIMAL(10,2),
	Estado BIT NOT NULL
);

CREATE TABLE Deposito (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Nombre VARCHAR(10),
	Calle VARCHAR(100),
	Numero INT,
	CodigoPostal VARCHAR(10),
	Telefono VARCHAR(10),
	Email VARCHAR(255),
	-- Se agrega para referencias los productos en un deposito
	Producto VARCHAR(5),
	CONSTRAINT FK_Deposito_Producto FOREIGN KEY (Producto) REFERENCES Producto(Codigo)
);

/*2. Crear los scripts para realizar la carga inicial de las condiciones de IVA (MONOTRIBUTO, RESP. INSC., RESP NO INSC., NO RESPONSABLE, CONSUMIDOR FINAL, EXENTO).*/

INSERT INTO CondicionIva (Codigo, Descripcion) VALUES 
('M0N0T','Monotributo'),
('R3SP0','Responsable Inscripto'),
('R3SN1','Responsable No Inscripto'),
('C0NF1','Consumidor Final'),
('EX3NT','Exento');

/*3. Crear scripts para realizar la carga inicial de al menos 20 productos.*/

INSERT INTO Producto (Codigo, Descripcion, Precio, PrecioVenta, Estado) VALUES
('PRO01', 'Fideos', 50.00, 100.00, 1),
('PRO02', 'Harina', 30.00, 80.00, 1),
('PRO03', 'Pan', 20.00, 60.00, 1),
('PRO04', 'Sal', 40.00, 110.00, 1),
('PRO05', 'Mayonesa', 60.00, 150.00, 1),
('PRO06', 'Galletitas', 25.00, 70.00, 1),
('PRO07', 'Azucar', 70.00, 180.00, 1),
('PRO08', 'Arroz', 55.00, NULL, 1),
('PRO09', 'Salsa de Tomate', 45.00, 100.00, 1),
('PRO10', 'Mostaza', 35.00, 90.00, 1),
('PRO11', 'Te', 75.00, 190.00, 1),
('PRO12', 'Chocolatada', 40.00, 110.00, 1),
('PRO13', 'Plasticola', 65.00, 160.00, 1),
('PRO14', 'Cuaderno Tapa Dura', 85.00, NULL, 1),
('PRO15', 'Bloc Hojas A4', 70.00, 180.00, 1),
('PRO16', 'Cartuchera', 90.00, 230.00, 1),
('PRO17', 'Helado - Tres Sabores', 110.00, 280.00, 1),
('PRO18', 'Papas Fritas', 120.00, NULL, 1),
('PRO19', 'Agua Saborizada', 95.00, 240.00, 1),
('PRO20', 'Agua Mineral', 80.00, 200.00, 1);

/*4. Crear los scripts para realizar la carga inicial de al menos 10 Clientes.*/

INSERT INTO TipoDocumento (Descripcion) VALUES ('DNI'), ('LE'), ('LC');

INSERT INTO Cliente (TipoDocumento, NumeroDocumento, Apellido, Nombre, CondicionIva, Telefono, Celular, Email, Calle, Numero, CodigoPostal, Estado) VALUES
(1, 12345678, 'G�mez', 'Mar�a', 'M0N0T', '011-12345678', NULL, 'maria@gmail.com', 'Av. Rivadavia', 123, '1234', 1),
(2, 87654321, 'L�pez', 'Juan', 'R3SP0', NULL, '011-98765432', 'juan@hotmail.com', 'Av. De Mayo', 456, '5678', 1),
(3, 55555555, 'P�rez', 'Luc�a', 'C0NF1', NULL, NULL, NULL, NULL, NULL, NULL, 1),
(1, 98765432, 'Rodr�guez', 'Carlos', 'EX3NT', NULL, '011-88887777', 'carlos@yahoo.com', 'Cobo', 789, '9012', 1),
(2, 11111111, 'Fern�ndez', 'Laura', 'R3SN1', '011-11112222', NULL, 'laura@outlook.com', 'Arieta', 10, '3456', 1),
(1, 22222222, 'Mart�nez', 'Pablo', 'M0N0T', '011-22223333', NULL, NULL, NULL, NULL, NULL, 1),
(3, 33333333, 'Sanchez', 'Ana', 'R3SN1', NULL, NULL, NULL, NULL, NULL, NULL, 1),
(2, 44444444, 'Garc�a', 'Javier', 'R3SP0', '011-44445555', NULL, NULL, NULL, NULL, NULL, 1),
(1, 55555555, 'Gonz�lez', 'Eva', 'M0N0T', NULL, NULL, 'eva@gmail.com', 'Calle Falsa', 123, '6789', 1),
(2, 66666666, 'D�az', 'Marta', 'R3SP0', NULL, NULL, 'marta@hotmail.com', '', 50, '1234', 1);

/*5. Crear los scripts para realizar la carga inicial de al menos 15 Proveedores.*/

INSERT INTO Proveedor (NombreFantasia, RazonSocial, CUIT, Calle, Numero, CodigoPostal, Telefono, Email, CondicionIva, NombreContacto, Estado) VALUES
('ElectroComponentes', 'ElectroComponentes S.A.', 30500456001, 'Av. Industria', 123, '1234', '011-55556666', 'info@electrocomponentes.com', 'R3SP0', 'Juan P�rez', 1),
('Muebles Madera', 'Muebles Madera SRL', 30500456002, 'Calle Muebler�a', 456, '5678', '011-66667777', 'ventas@mueblesmadera.com', 'R3SP0', 'Ana G�mez', 1),
('Tecnolog�a Total', 'Tecnolog�a Total SA', 30500456003, 'Av. Innovaci�n', 789, '9012', '011-77778888', 'contacto@tecnologiatotal.com', 'R3SP0', 'Carlos Rodr�guez', 1),
('Construcci�n R�pida', 'Construcci�n R�pida S.A.', 30500456004, 'Calle Constructor', 10, '3456', '011-88889999', 'ventas@construccionrapida.com', 'R3SP0', 'Laura Mart�nez', 1),
('Suministros Industriales', 'Suministros Industriales SRL', 30500456005, 'Av. Proveedor', 50, '1234', '011-99991111', 'contacto@suministrosindustriales.com', 'R3SP0', 'Eva Gonz�lez', 1),
('Productos Pl�sticos', 'Productos Pl�sticos SA', 30500456006, 'Calle Pl�stica', 78, '5678', '011-12345678', 'contacto@productosplasticos.com', 'M0N0T', 'Javier D�az', 1),
('Log�stica Eficiente', 'Log�stica Eficiente SRL', 30500456007, 'Av. Log�stica', 12, '9012', '011-23452345', 'info@logisticaeficiente.com', 'R3SP0', 'Marta S�nchez', 1),
('Materiales de Construcci�n', 'Materiales de Construcci�n SA', 30500456008, 'Calle Materiales', 89, '6789', '011-34563456', 'contacto@materialesdeconstruccion.com', 'R3SP0', 'Pablo L�pez', 1),
('Importaciones Internacionales', 'Importaciones Internacionales SA', 30500456009, 'Av. Importaciones', 34, '1234', '011-98769876', 'info@importacionesinternacionales.com', 'R3SP0', 'Luc�a P�rez', 1),
('Alimentos Frescos', 'Alimentos Frescos SRL', 30500456010, 'Calle Alimentos', 67, '5678', '011-78997899', 'contacto@alimentosfrescos.com', 'M0N0T', 'Natalia Ram�rez', 1),
('Textiles y Moda', 'Textiles y Moda SA', 30500456011, 'Av. Textiles', 90, '9012', '011-76547654', 'ventas@textilesymoda.com', 'R3SP0', 'Carlos Fern�ndez', 1),
('Electrodom�sticos Baratos', 'Electrodom�sticos Baratos SRL', 30500456012, 'Calle Electrodom�sticos', 45, '1234', '011-45674567', 'info@electrodomesticosbaratos.com', 'R3SP0', 'Patricia Silva', 1),
('Ferreter�a Moderna', 'Ferreter�a Moderna SA', 30500456013, 'Av. Ferreter�a', 34, '5678', '011-56785678', 'ventas@ferreteriamoderna.com', 'M0N0T', 'Roberto Navarro', 1),
('Art�culos de Oficina', 'Art�culos de Oficina SRL', 30500456014, 'Calle Oficina', 21, '9012', '011-11111111', 'contacto@articulosdeoficina.com', 'R3SP0', 'Luisa Torres', 1),
('Equipos de Deportes', 'Equipos de Deportes SA', 30500456015, 'Av. Deportes', 88, '1234', '011-22222222', 'info@equiposdedeportes.com', 'M0N0T', 'Sergio Guti�rrez', 1);

/*6. Agregar la columna datos de contacto (dato opcional, 200 caracteres) a la tabla de proveedores.*/

ALTER TABLE Proveedor ADD DatosContacto VARCHAR(200);

/* 7. Actualizar la columna datos de contacto de los proveedores que tengan cargado el campo email 
(donde email sea distinto de NULL) con el valor de este campo (email).*/

UPDATE Proveedor
SET DatosContacto = Email
WHERE Email IS NOT NULL

------------------ PARTE II - CONSULTAS ------------------

/* 1. Listar tipo de documento (descripci�n), n�mero de doc., condici�n de IVA (descripci�n), apellido y nombres de los clientes. */

SELECT td.Descripcion, c.NumeroDocumento, ci.Descripcion, c.Apellido, c.Nombre
FROM Cliente AS c INNER JOIN TipoDocumento AS td ON c.TipoDocumento = td.Id INNER JOIN CondicionIva AS ci ON ci.Codigo = c.CondicionIVA

/* 2. Listar el CUIT y Raz�n Social y condici�n de IVA (descripci�n) de los proveedores que posean sean Monotributristas ordenados por CUIT. */

SELECT p.CUIT, p.RazonSocial, ci.Descripcion
FROM Proveedor AS p INNER JOIN CondicionIva AS ci ON p.CondicionIVA = ci.Codigo
WHERE ci.Descripcion LIKE 'Monotributo'
ORDER BY p.CUIT

/* 3. Listar los productos cuyo precio de venta sea mayor a 100 y menor a 250 (ver BEETWEN). */

SELECT *
FROM Producto 
WHERE PrecioVenta BETWEEN 100 AND 250;

/* 4. Listar los 5 productos m�s caros (mostrar c�digo, descripci�n, precio de compra). */

SELECT TOP(5) p.Codigo, p.Descripcion, p.Precio
FROM Producto AS p
ORDER BY p.Precio DESC

/* 5. Listar los 5 productos m�s baratos (mostrar c�digo, descripci�n, precio de compra. */

SELECT TOP(5) p.Codigo, p.Descripcion, p.Precio
FROM Producto AS p
ORDER BY p.Precio