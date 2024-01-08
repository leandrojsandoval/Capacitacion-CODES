CREATE PROCEDURE sp_materiales_obtener
(
	@nombre AS NVARCHAR(50),
	@descripcion AS NVARCHAR(200),
	@multiplicador AS FLOAT = NULL,
	@activo AS BIT = NULL
)
AS
BEGIN
	SELECT ID_MATERIAL_RODILLO,
	       NOMBRE,
		   DESCRIPCION,
		   MULTIPLICADOR_TONELADAS,
		   ACTIVO
	  FROM TBL_MATERIALES
	 WHERE LOWER(NOMBRE) LIKE '%'+ @nombre +'%'
	   AND LOWER(DESCRIPCION) LIKE '%'+ @descripcion +'%'
	   AND (@multiplicador IS NULL OR @multiplicador = MULTIPLICADOR_TONELADAS)
	   AND (@activo IS NULL OR ACTIVO = @activo)
END
