USE Integrador;

/**************************** INSERCION DE DATOS A LA TABLA ****************************/

/* LS: Tenia problemas al realizar la siguiente sentencia para pasarme los datos de la tabla: 
INSERT INTO TBL_PRODUCTOS_LS SELECT * FROM TBL_PRODUCTOS_GF;

La solucion la encontre en este post:
https://stackoverflow.com/questions/2005437/an-explicit-value-for-the-identity-column-in-table-can-only-be-specified-when-a */

SET IDENTITY_INSERT TBL_PRODUCTOS_LS ON;

INSERT INTO TBL_PRODUCTOS_LS (ID_PRODUCTO, DESCRIPCION, PRECIO) 
SELECT ID_PRODUCTO, DESCRIPCION, PRECIO FROM TBL_PRODUCTOS_GF;

SET IDENTITY_INSERT TBL_PRODUCTOS_LS OFF;

GO

/**************************** CREACION DE STORED PRODECURES ****************************/

/* LS: Para saber como es la estructura del stored procedure a desarrollar fui a la implementacion
haciendo clic en la opcion Script Stored Procedure as */

CREATE OR ALTER PROCEDURE [dbo].[sp_delete_productos_ls] (@id INT) AS
BEGIN
	DELETE FROM TBL_PRODUCTOS_LS
	WHERE ID_PRODUCTO = @id
END

GO

CREATE OR ALTER PROCEDURE [dbo].[sp_insert_productos_ls] (@descripcion VARCHAR(100), @precio INT, @idCategoria INT) AS
BEGIN
	INSERT INTO TBL_PRODUCTOS_LS (DESCRIPCION, PRECIO, ID_CATEGORIA) VALUES (@descripcion, @precio, @idCategoria)
END

GO

CREATE OR ALTER PROCEDURE [dbo].[sp_obtener_por_filtro_productos_ls] 
	(@id INT = NULL, @descripcion VARCHAR(100) = '', @precio INT = NULL, @idCategoria INT = NULL)
AS
BEGIN
	SELECT ID_PRODUCTO, DESCRIPCION, PRECIO
	FROM TBL_PRODUCTOS_LS
	WHERE	(CASE WHEN @id IS NULL THEN ID_PRODUCTO ELSE @id END = ID_PRODUCTO)
		AND (CASE WHEN @descripcion = '' THEN '' ELSE DESCRIPCION END LIKE '%' + @descripcion + '%')
		AND (CASE WHEN @precio IS NULL THEN PRECIO ELSE @precio END = PRECIO)
		AND (CASE WHEN @idCategoria IS NULL THEN ID_CATEGORIA ELSE @idCategoria END = ID_CATEGORIA)
END

GO

/* LS: Cambio el procedure para mostrar las categorias */

CREATE OR ALTER PROCEDURE [dbo].[sp_obtener_productos_ls] 
	(@descripcion AS VARCHAR(100) = '', @precio INT = NULL, @idCategoria INT = NULL, @intPagNumero INT, @intPagTamano INT)
AS
BEGIN
	
	DECLARE @tblDatos AS TABLE (
		ID_PRODUCTO INT,
		DESCRIPCION VARCHAR(100),
		PRECIO INT,
		ID_CATEGORIA INT,
		CATEGORIA VARCHAR(100),
		LINEA INT
	)

	INSERT INTO @tblDatos
	SELECT ID_PRODUCTO, p.DESCRIPCION, PRECIO, p.ID_CATEGORIA, c.DESCRIPCION, ROW_NUMBER() OVER(ORDER BY ID_PRODUCTO ASC) AS LINEA
	FROM TBL_PRODUCTOS_LS AS p INNER JOIN TBL_CATEGORIAS_LS AS c ON p.ID_CATEGORIA = c.ID_CATEGORIA
	WHERE	(CASE WHEN @descripcion = '' THEN '' ELSE p.DESCRIPCION END LIKE '%' + @descripcion + '%')
		AND (CASE WHEN @precio IS NULL THEN PRECIO ELSE @precio END = PRECIO)
		AND (CASE WHEN @idCategoria IS NULL THEN p.ID_CATEGORIA ELSE @idCategoria END = p.ID_CATEGORIA)
	ORDER BY ID_PRODUCTO ASC

	--Registros totales de la consulta
	IF @intPagTamano > 0
	BEGIN
		--Relizo paginación de la consulta
		SET @intPagNumero = @intPagNumero - 1;

		SELECT ID_PRODUCTO, DESCRIPCION, PRECIO, ID_CATEGORIA, CATEGORIA
		FROM @tblDatos
		WHERE LINEA BETWEEN (@intPagTamano * @intPagNumero) + 1 AND @intPagTamano * (@intPagNumero + 1)
	END
	ELSE
	BEGIN
		SELECT ID_PRODUCTO, DESCRIPCION, PRECIO, ID_CATEGORIA, CATEGORIA
		FROM @tblDatos
	END
END

exec [sp_obtener_productos_ls]  '', NULL, 3, 1, 10

GO

CREATE OR ALTER PROCEDURE [dbo].[sp_update_productos_ls] ( @id INT, @descripcion VARCHAR(100), @precio INT, @idCategoria INT ) AS
BEGIN
	UPDATE TBL_PRODUCTOS_LS
	SET DESCRIPCION = @descripcion, PRECIO = @precio, ID_CATEGORIA = @idCategoria
	WHERE ID_PRODUCTO = @id
END

GO

/**************************** CATEGORIA DEL PRODUCTO ****************************/

/* LS: Agrego una tabla nueva que se llame TBL_CATEGORIA_LS para llevarlo a un nivel de normalizacion 
mayor al de la primera forma normal */

CREATE TABLE TBL_CATEGORIAS_LS (
	ID_CATEGORIA INT IDENTITY (1,1) PRIMARY KEY,
	DESCRIPCION VARCHAR(100) NOT NULL,
)

INSERT INTO TBL_CATEGORIAS_LS VALUES ('Comestible'), ('Limpieza'), ('Insumo Oficina');

/* LS: Agrego una columna nueva a la tabla de productos que se llame categoria, haciendo como FK
el ID de categoria, logrando la integridad referencial de las tablas. 

MODIFICADO: Se cambio el nombre de la columna CATEGORIA a ID_CATEGORIA */

ALTER TABLE TBL_PRODUCTOS_LS ADD ID_CATEGORIA INT;

ALTER TABLE TBL_PRODUCTOS_LS ADD CONSTRAINT FK_PRODUCTOS_ID_CATEGORIA FOREIGN KEY (ID_CATEGORIA) REFERENCES TBL_CATEGORIAS_LS(ID_CATEGORIA)

UPDATE TBL_PRODUCTOS_LS
SET ID_CATEGORIA = 1
WHERE ID_PRODUCTO = 6 OR ID_PRODUCTO = 7

UPDATE TBL_PRODUCTOS_LS
SET ID_CATEGORIA = 2
WHERE ID_PRODUCTO = 8 OR ID_PRODUCTO = 9

ALTER TABLE TBL_PRODUCTOS_LS ALTER COLUMN ID_CATEGORIA INT NOT NULL;

/* LS: Creo el Stored Procedure de Obtener Categorias */

GO

CREATE OR ALTER PROCEDURE [dbo].[sp_obtener_categorias_ls] AS 
BEGIN
	SELECT [ID_CATEGORIA] ,[DESCRIPCION] 
	FROM [dbo].[TBL_CATEGORIAS_LS];
END

GO