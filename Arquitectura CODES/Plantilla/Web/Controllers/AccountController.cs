using Framework.Common;
using Framework.Utils;
using Framework.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ARQ.Entidades;
using ARQ.Framework.Web;
using ARQ.Recursos;
using ARQ.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Web.Models.Account;

namespace Web.Controllers.Account
{
    public class AccountController : BaseController
    {
        #region Propiedades de servicio
        private IServicioAutenticacion ServicioAutenticacion { get; set; }
        private IConfiguration Configuration { get; set; }
        private IServicioGenerico ServicioGenerico { get; set; }
        private IServicioLogEventos ServicioLogEventos { get; set; }

        private IServicioRoles ServicioRoles { get; set; }

        #endregion

        public AccountController(IServicioAutenticacion servicioAutenticacion,
                                 IConfiguration configuration,
                                 IServicioGenerico servicioGenerico,
                                 IServicioLogEventos servicioLogEventos,
                                 IServicioRoles servicioRoles)
        {
            this.ServicioAutenticacion = servicioAutenticacion;
            this.Configuration = configuration;
            this.ServicioGenerico = servicioGenerico;
            this.ServicioLogEventos = servicioLogEventos;
            this.ServicioRoles = servicioRoles;
        }

        #region Actions

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Autologin()
        {
            try
            {
                if (User.Identity.IsAuthenticated && Request.Cookies.ContainsKey(".AspNetCore.SGT"))
                {
                    log.Info($"Usuario autenticado como: {HttpContext.User.Identity.Name}");
                    return Redirect("/Home/Index");
                }

                string userName = string.Empty;

                try
                {
                    userName = HttpContext.User.Identity.Name;
                    if (userName == null)
                        throw new Exception("No se pudo obtener el nombre de usuario");

                    log.Info($"Nombre para autologin {userName}");
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    return Redirect("/Account/Login");
                }

                var apiResponse = this.ServicioAutenticacion.AutenticarUsuarioAplicacionAutologin(userName, Configuration["SGAAService.idApp"]);

                if (apiResponse.result == JsonApiData.Result.Error)
                {

                    var loginVM = new LoginViewModel()
                    {
                        Mensaje = apiResponse.message
                    };

                    return View("Login", loginVM);
                }

                var usuario = ObtenerUsuarioInterno(apiResponse);

                string rol = apiResponse.content.rol.ToString();
                var identity = new ClaimsIdentity(new[] {
                            new Claim(ClaimTypes.Sid, usuario.Id.ToString()),
                            new Claim(ClaimTypes.Name, usuario.Nombre),
                            new Claim(ClaimTypes.Role, rol),
                            new Claim(Constantes.CLAIMS_PERMISOS, JsonConvert.SerializeObject(ObtenerFuncionalidadesPermitidas(rol)))
                        }, Configuration[Constantes.IDAPP].ToString());

                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(Configuration[Constantes.IDAPP].ToString(), principal);

                return Redirect("/Home/Index");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return Redirect("/Home/Index");
            }
        }

        [HttpGet]
        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }
        #endregion

        #region Get
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            try
            {
                await HttpContext.SignOutAsync(this.Configuration[Constantes.IDAPP].ToString());

                //Limpio el IdentityUser y las cookies de autenticacion
                HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }

                ViewData["ReturnUrl"] = returnUrl;
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            
            return View("Login", new LoginViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> Signin(string returnUrl = null)
        {
            try
            {
                await HttpContext.SignOutAsync(this.Configuration[Constantes.IDAPP].ToString());

                //Limpio el IdentityUser y las cookies de autenticacion
                HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }

                ViewData["ReturnUrl"] = returnUrl;
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
           
            return View("Signin");
        }

        #endregion

        #region Post
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            JsonData jsonData = new JsonData();
            List<string> mensajes = new List<string>();

            try
            {
                if (!ModelState.IsValid)
                {                       
                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var error in modelState.Errors)
                        {
                            mensajes.Add(error.ErrorMessage);
                        }
                    }
                    return Json(new JsonData() { content = new { mensajes = mensajes }, result = JsonData.Result.Error });
                }

                JsonApiData apiResponse = this.ServicioAutenticacion.AutenticarUsuarioAplicacion(model.Usuario,
                                                                                                 model.Password,
                                                                                                 this.Configuration[Constantes.IDAPP].ToString());
                if (apiResponse.result == JsonApiData.Result.Error)
                {
                    mensajes.Add(Global.MensajeCredencialesInvalidas);

                    return Json(new JsonData() { content = new { mensajes = mensajes }, result = JsonData.Result.Error });
                }

                var usuario = ObtenerUsuarioInterno(apiResponse);

                string rol = apiResponse.content.rol.ToString();
                var identity = new ClaimsIdentity(new[] {
                            new Claim(ClaimTypes.Sid, usuario.Id.ToString()),
                            new Claim(ClaimTypes.Name, usuario.Nombre),
                            new Claim(ClaimTypes.Role, rol),
                            new Claim(Constantes.CLAIMS_PERMISOS, JsonConvert.SerializeObject(ObtenerFuncionalidadesPermitidas(rol)))
                        }, this.Configuration[Constantes.IDAPP].ToString());
                
                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(this.Configuration[Constantes.IDAPP].ToString(), principal);               
                
            }
            catch (Exception ex)
            {
                log.Error(ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = Global.ErrorGenerico;
            }
            return Json(new JsonData() { result = JsonData.Result.Ok });
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            JsonData jsonData = new JsonData();

            try
            {
                int idUser = UserUtils.GetId(User);
                Usuario usuario = ServicioGenerico.GetById<Usuario>(idUser);
                if (usuario != null)
                {
                    string nombreUsuario = usuario.Login;
                    string nombreEvento = "Log Out";

                    JsonApiData apiResponse = this.ServicioLogEventos.LogInfo(nombreUsuario, this.Configuration[Constantes.IDAPP].ToString(), nombreEvento);
                    if (apiResponse.result == JsonApiData.Result.Error)
                    {
                        jsonData.content = new { mensaje = Global.MensajeCredencialesInvalidas };
                        jsonData.result = JsonData.Result.Error;
                        return Json(jsonData);
                    }
                }

                 await HttpContext.SignOutAsync(this.Configuration[Constantes.IDAPP].ToString());
            }
            catch (Exception ex)
            {
                log.Error(ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = Global.ErrorGenerico;
            }
            return Json(new JsonData() { result = JsonData.Result.Ok }); 
        }

        /// <summary>
        /// Obtiene el usuario interno de Rollshop a partir del id del usuario de SGAA retornado por la 
        /// consulta a la API. Si el servicio de SGAA lo autentica correctamente y no se encuentra 
        /// en la base de Rollshop, el usuario se inserta
        /// </summary>
        /// <param name="apiResponse"></param>
        /// <returns></returns>
        private Usuario ObtenerUsuarioInterno(JsonApiData apiResponse)
        {
            var usuario = this.ServicioGenerico.Get<Usuario>(u => u.IdUsuarioSGAA == int.Parse(apiResponse.content.id.ToString()));

            if (usuario == null)
            {
                var usuarioNuevo = new Usuario() 
                                   { 
                                     Activo = true,
                                     IdUsuarioSGAA = apiResponse.content.id,
                                     Nombre = apiResponse.content.nombre.ToString(),
                                     Email = apiResponse.content.email.ToString(),
                                     Login = apiResponse.content.login.ToString(),
                                     IdTurnoTrabajo = 1
                                   };

                this.ServicioGenerico.Add<Usuario>(usuarioNuevo);

                usuario = usuarioNuevo;
            }

            return usuario;
        }

        private IList<int> ObtenerFuncionalidadesPermitidas(string rolDesc)
        {
            int idRol = ResolverRol(rolDesc);
            return this.ServicioGenerico.GetAll<FuncionalidadRol>(func => func.IdRol == idRol && func.IdTipoAcceso == (int)TipoAccesoEnum.AccesoTotal).Select(funcRol => funcRol.Funcionalidad.Id).ToList();
        }

        private int ResolverRol(string rolDesc)
        {
            List<Rol> roles = this.ServicioRoles.ObtenerRoles(this.Configuration[Constantes.IDAPP].ToString());
            var rol = roles.FirstOrDefault(r => r.Descripcion.ToUpper() == rolDesc.ToUpper());

            log.Info($"INFO Se pudo obtener el rol {rol.Id} {rol.Descripcion}");
            log.Info($"INFO Roles obtenidos: {string.Join(',', roles.Select(r => r.Descripcion).ToList())}");

            if (rol == null)
            {
                log.Error($"No se pudo obtener el rol {rolDesc}");
                log.Error($"Roles obtenidos: {string.Join(',', roles.Select(r => r.Descripcion).ToList())}");
                throw new Exception("No se pudo encontrar el rol");
            }

            return rol.Id;
        }
    }
}
#endregion