using ARQ.Recursos;
using Framework.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.Account {
    public class LoginViewModel : BaseViewModel {

        [Required(ErrorMessageResourceName = nameof(Global.ValidacionCampoUsuarioRequerido), ErrorMessageResourceType = typeof(Global))]
        public string Usuario { get; set; }
        public string Mensaje { get; set; }

        private string encriptPasword;
        [Required(ErrorMessageResourceName = nameof(Global.ValidacionCampoPasswordRequerido), ErrorMessageResourceType = typeof(Global))]
        public string Password {
            get { return encriptPasword; }
            set { encriptPasword = EncryptionHelper.Encrypt(value); }
        }

    }

}
