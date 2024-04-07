using Dev.Business.Interfaces;
using Dev.Business.Models.Base;
using Dev.Business.Notification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace Dev.Api.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly INotificador _notificador;
        protected Guid UserId { get; set; }
        protected string? UserName { get; set; }
        public MainController(INotificador notificador,
                              IAppIdentityUser user)
        {
            _notificador = notificador;

            if (user.IsAuthenticated())
            {
                UserId = user.GetUserId();
                UserName = user.GetUserName();
            }
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected ActionResult CustomResponse(HttpStatusCode statusCode = HttpStatusCode.OK, object result = null)
        {
            if(OperacaoValida())
            {
                return new ObjectResult(result)
                {
                    StatusCode = Convert.ToInt32(statusCode)
                };
            }

            return BadRequest(new
            {
                errors = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
            }); 
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            
            if(!ModelState.IsValid) NotificarErroModelInvalida(modelState);

            return CustomResponse();
           
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(x => x.Errors);

            foreach(var erro in erros)
            {
               var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(erroMsg);
            }
        }

        protected void NotificarErro(string msg)
        {
            _notificador.Handle(new Notificacao(msg));
        }
    }
}
