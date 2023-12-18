using System.ComponentModel.DataAnnotations;
using Framework.Helpers;
using ARQ.Recursos;

namespace Web.Models.Account
{
    public class SigninViewModel
    {
        [Required(ErrorMessageResourceName = nameof(Global.ValidacionCampoUsuarioRequerido), ErrorMessageResourceType = typeof(Global))]
        public string Usuario { get; set; }
        [Required(ErrorMessageResourceName = nameof(Global.ValidacionCampoPasswordRequerido), ErrorMessageResourceType = typeof(Global))]
        public string Password { get { return encriptPasword; } set { encriptPasword = EncryptionHelper.Encrypt(value); } }

        private string encriptPasword;

        [Compare(nameof(Password), ErrorMessageResourceName = nameof(Global.ValidacionConfirmarClave), ErrorMessageResourceType = typeof(Global))]
        public string ConfirmPassword { get { return encriptConfirmPasword; } set { encriptConfirmPasword = EncryptionHelper.Encrypt(value); } }

        private string encriptConfirmPasword;
    }
}
