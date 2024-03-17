using Dev.Business.Interfaces;
using Dev.Business.Models;
using Dev.Business.Notification;
using FluentValidation;
using FluentValidation.Results;

namespace Dev.Business.Services
{
    public abstract class BaseService
    {   
        private readonly INotificador _notificador;
        protected BaseService(INotificador notificador) 
        {
            _notificador = notificador;
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach(var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected bool ExecutarValidacao<TV, TE>(TV validator, TE entity ) 
            where TV : AbstractValidator<TE> 
            where TE : Entity
        {
            var validationResult = validator.Validate(entity);

            if(validationResult.IsValid) return true;

            Notificar(validationResult);

            return false;
            
        }

    }
}
