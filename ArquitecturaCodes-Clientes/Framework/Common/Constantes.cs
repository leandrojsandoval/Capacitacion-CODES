﻿namespace Framework.Common {

    public static class Constantes {

        #region Keys
        
        public const string NOMBRE_DOMINIO_KEY = "Nombre_Dominio";
        public const string SERVIDOR_DOMINIO_KEY = "Servidor_Dominio";
        public const string PUERTO_LDAP_KEY = "Puerto_LDAP";
        public const string DB_CONFIG_KEY = "ARQContext";
        public const string CULTURA_DEFAULT = "es-AR";
        public const string GENERIC_PASSWORD = "CODES.-.PPAASSWWOORRDD";

        public const string SGAASERVICE_KEY_URL = "SGAAService.URL";
        public const string SGAASERVICE_KEY_URLRoles = "SGAAService.URLRoles";
        public const string SGAASERVICE_KEY_URLEVENTO = "SGAAService.URLEvento";
        public const string SGAASERVICE_KEY_USERNAME = "SGAAService.Auth.userName";
        public const string SGAASERVICE_KEY_PASSWORD = "SGAAService.Auth.password";
        public const string SGAASERVICE_KEY_IDAPP = "SGAAService.Auth.idApp";
        public const string IDAPP = "SGAAService.idApp";
        public const string SGAASERVICE_KEY_EVENT = "SGAAService.nombreEvento";
        public const string SGAASERVICE_KEY_URL_AUTOLOGIN = "SGAAService.URLAutologin";

        #endregion

        public const int ERROR_HTTP = 500;

        public const int VALOR_NO_CAMBIAR = -1;

        #region Roles
        
        public const string ARQ_MANAGER = "Manager";
        public const string ARQ_TURNOS = "Turnos";
        public const string ARQ_PROCESO = "Proceso";
        public const string ARQ_TALLER = "Taller";
        
        #endregion

        #region Permisos
        
        public const string CLAIMS_PERMISOS = "Permisos";

        public const int CONFIGURACIONES_FUNCIONALIDADES = 1;

        #endregion

        public const string SP_CLIENTES_OBTENER = "sp_clientes_obtener";

    }

}
