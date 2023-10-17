/* Se desea realizar un sistema de gestión comercial que permita almacenar información de los 1
clientes, proveedores y productos.
A continuación se detalla la información que se desea persistir en el sistema para lo 
cual deberán crear la tablas correspondientes:

Información de los tipos de Documentos (Tabla TiposDocumentos)
	- Id (Incrementable)
	- Descripción (20 caracteres alfanuméricos)

Información de las condiciones de IVA (Tabla CondicionesIva)
	- Código (5 caracteres alfanuméricos)
	- Descripción (50 caracteres alfanuméricos)

Información de los clientes (Tabla Clientes)
	- Tipo de documento (Obligatorio)
	- Número de Documento (Entero, obligatorio)
	- Apellido (50 caracteres, obligatorio)
	- Nombres (50 caracteres, obligatorio)
	- Condición de IVA (Obligatorio)
	- Teléfono (20 caracteres, opcional)
	- Celular (20 caracteres, opcional)
	- Email (255 caracteres, opcional)
	- Domicilio
		Calle (100 caracteres, opcional) 
		Numero (entero, opcional)
		Código Postal (10 caracteres, opcional)
	- Estado (entero, Obligatorio) (Puede estar activo o deshabilitado)

Información de los proveedores (Tabla Proveedores)
	- Id (Incrementable)
	- Nombre de fantasía (100 caracteres, opcional)
	- Razón Social (100 caracteres, obligatorio)
	- Numero de CUIT (Numérico, obligatorio)
	- Domicilio
		Calle (100 caracteres, opcional) 
		Numero (Entero, opcional)
		Código Postal (10 caracteres, opcional)
	- Teléfono (20 caracteres, opcional)
	- Email (255 caracteres, opcional)
	- Condición de IVA (Obligatorio)
	- Nombre Contacto (50 caracteres, obligatorio)
	- Estado (Entero, obligatorio) (Puede estar activo o deshabilitado)

Información de los Productos (Tabla Productos)
	- Código (5 caracteres alfanuméricos)
	- Descripción (50 caracteres)
	- Precio de compra (Decimal, máximo dos dígitos decimales)
	- Precio sugerido de venta (Decimal, máximo dos dígitos decimales, opcional)
	- Estado (Entero, obligatorio) (Pueden estar disponibles o no para vender)
	
Información de los Depósitos donde existirá stock de Productos (Tabla Depósitos)
	- Id (Incrementable)
	- Nombre (100 caracteres)
	- Domicilio
		Calle (100 caracteres, opcional) 
		Numero (Entero, opcional)
		Código Postal (10 caracteres, opcional)
	- Teléfono (10 caracteres, opcional)
	- Email (255 caracteres, opcional)	
*/

------------------ PARTE I - Creación de tablas y carga inicial de datos ------------------

/*1. Realizar los scripts de creación de las tablas identificando claves primarias y foráneas.*/

CREATE TABLE TipoDocumento (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Descripcion VARCHAR(20)
);

CREATE TABLE CondicionIva (
	Codigo VARCHAR(5) PRIMARY KEY,
	Descripcion VARCHAR(50)
);

CREATE TABLE Cliente (
	NumeroDeDocumento INT PRIMARY KEY,
	TipoDeDocumento INT,
	Apellido VARCHAR(50) NOT NULL,
	Nombre VARCHAR(50) NOT NULL,
	CondicionDeIVA VARCHAR(5),
	Telefono VARCHAR(20),
	Celular VARCHAR(20),
	Email VARCHAR(255),
	Calle VARCHAR(100),
	Numero INT,
	CodigoPostal VARCHAR(10),
	Estado BIT NOT NULL,
	CONSTRAINT FK_Cliente_TipoDeDocumento FOREIGN KEY (TipoDeDocumento) REFERENCES TipoDocumento(Id),
	CONSTRAINT FK_Cliente_CondicionDeIVA FOREIGN KEY (CondicionDeIVA) REFERENCES CondicionIva(Codigo)
);

CREATE TABLE Proveedor (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	NombreFantasia VARCHAR(100),
	RazonSocial VARCHAR(100) NOT NULL,
	CUIT INT NOT NULL,
	Calle VARCHAR(100),
	Numero INT,
	CodigoPostal VARCHAR(10),
	Telefono VARCHAR(20),
	Email VARCHAR(255),
	CondicionDeIVA VARCHAR(5),
	NombreContacto VARCHAR(50) NOT NULL,
	Estado BIT NOT NULL,
	CONSTRAINT FK_Proveedor_CondicionDeIVA FOREIGN KEY (CondicionDeIVA) REFERENCES CondicionIva(Codigo)
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
	IdProducto VARCHAR(5),
	CONSTRAINT FK_Deposito_IdProducto FOREIGN KEY (IdProducto) REFERENCES Producto(Codigo)
);

/*2. Crear los scripts para realizar la carga inicial de las condiciones de IVA (MONOTRIBUTO, 
RESP. INSC., RESP NO INSC., NO RESPONSABLE, CONSUMIDOR FINAL, EXENTO).*/

INSERT INTO CondicionIva (Codigo, Descripcion) VALUES 
('','Monotributo'),
('','Responsable Inscripto'),
('','Responsable No Inscripto'),
('','Consumidor Final'),
('','Exento');

/*3. Crear scripts para realizar la carga inicial de al menos 20 productos.*/

INSERT INTO Producto (Codigo, Descripcion, Precio, PrecioVenta, Estado) VALUES

/*4. Crear los scripts para realizar la carga inicial de al menos 10 Clientes.*/

/*5. Crear los scripts para realizar la carga inicial de al menos 15 Proveedores.*/

/*6. Agregar la columna datos de contacto (dato opcional, 200 caracteres) a la tabla de proveedores.*/

/* 7. Actualizar la columna datos de contacto de los proveedores que tengan cargado el campo email 
(donde email sea distinto de NULL) con el valor de este campo (email).*/

/*PARTE II - CONSULTAS
1. Listar tipo de documento (descripción), número de doc., condición de IVA (descripción), 
apellido y nombres de los clientes 
2. Listar el CUIT y Razón Social y condición de IVA (descripción) de los proveedores que 
posean sean Monotributristas ordenados por CUIT
3. Listar los productos cuyo precio de venta sea mayor a 100 y menor a 250 (ver BEETWEN)
4. Listar los 5 productos más caros (mostrar código, descripción, precio de compra)
5. Listar los 5 productos más baratos (mostrar código, descripción, precio de compra*/