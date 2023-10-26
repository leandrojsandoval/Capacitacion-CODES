using System.Configuration;

namespace PracticaBaseDeDatos.Clases {
    public class Constante {

        public const string MENSAJE_EXITOSO = "La persona se agrego correctamente a la base de datos.";
        public const string MENSAJE_BORRADO_EXITOSO = "Los datos de la tabla Persona se borraron exitosamente.";
        public const string MENSAJE_BORRADO_FALLIDO = "Los datos de la tabla no se borraron o estaba vacía.";
        public const string MENSAJE_ERROR_MENOR_DE_EDAD = "ERROR: El campo [Edad] no es válido, debe tener al menos 18 años.";
        public const string MENSAJE_ERROR_DNI_INVALIDO = "ERROR: El campo [DNI] no es válido, debe tener dígitos sin guiones.";
        public const string MENSAJE_ERROR_EMAIL_INVALIDO = "ERROR: El campo [Email] no es válido, dominio en el e-mail debe ser Gmail.com ó Hotmail.com";

        public const string SP_ELIMINAR_PERSONAS = "P_Eliminar_Personas";
        public const string SP_FILTRAR_PERSONAS = "P_Filtrar_Personas";
        public const string SP_OBTENER_PERSONAS = "P_Obtener_Personas";
        public const string SP_AGREGAR_PERSONA = "P_Agregar_Persona";

        public const string PARAMETRO_NOMBRE = "@Nombre";
        public const string PARAMETRO_APELLIDO = "@Apellido";
        public const string PARAMETRO_EDAD = "@Edad";
        public const string PARAMETRO_DNI = "@DNI";
        public const string PARAMETRO_EMAIL = "@Email";
        public const string PARAMETRO_NOMBRE_COLUMNA = "@nombreColumna";
        public const string PARAMETRO_VALOR = "@valor";

    }
}