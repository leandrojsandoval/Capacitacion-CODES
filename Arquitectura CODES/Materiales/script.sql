CREATE PROCEDURE sp_clientes_obtener (@nombre AS NVARCHAR(50), @apellido AS NVARCHAR(200), @fechaNacimiento AS DATE = NULL, @activo AS BIT = NULL)
AS
BEGIN
	SELECT ID_CLIENTE,
	       NOMBRE,
		   APELLIDO,
		   FECHA_NACIMIENTO,
		   ACTIVO
	  FROM TBL_CLIENTES
	 WHERE LOWER(NOMBRE) LIKE '%'+ @nombre +'%'
	   AND LOWER(APELLIDO) LIKE '%'+ @apellido +'%'
	   AND (@fechaNacimiento IS NULL OR @fechaNacimiento = FECHA_NACIMIENTO)
	   AND (@activo IS NULL OR ACTIVO = @activo)
END

GO

EXECUTE sp_clientes_obtener '','',NULL,NULL
