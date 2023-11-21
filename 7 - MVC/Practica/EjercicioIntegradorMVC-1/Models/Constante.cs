namespace EjercicioIntegradorMVC_1.Models {
    public class Constante {

        public const int ID_NO_ENCONTRADO = -1;

        public const string VARIABLE_ID = "@Id";
        public const string VARIABLE_NOMBRE = "@Nombre";
        public const string VARIABLE_APELLIDO = "@Apellido";
        public const string VARIABLE_TIPO_DOCUMENTO = "@TipoDoc";
        public const string VARIABLE_NUMERO_DOCUMENTO = "@NroDoc";

        public const string SP_GET_PERSONAS = "P_Get_Personas;";
        public const string SP_INSERTAR_PERSONA = "P_Insertar_Persona";
        public const string SP_ACTUALIZAR_PERSONA = "P_Actualizar_Persona";
        public const string SP_GET_MAX_ID_PERSONA = "P_Get_Max_Id_Persona";
        public const string SP_GET_IDS_PERSONAS = "P_Get_Ids_Personas";
        public const string SP_GET_PERSONA_POR_ID = "P_Get_Persona_Por_Id";

        public const string MENSAJE_ERROR_INSERCION = "Error al insertar la persona en la base de datos.";
        public const string MENSAJE_ERROR_ACTUALIZACION = "Error al modificar la persona en la base de datos.";
        public const string MENSAJE_ERROR_ARGUMENTOS_NULOS = "Algunos de los argumentos son nulos.";

    }
}