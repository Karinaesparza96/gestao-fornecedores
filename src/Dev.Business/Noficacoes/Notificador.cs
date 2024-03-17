using Dev.Business.Interfaces;
using Dev.Business.Notification;

namespace Dev.Business.Noficacoes
{
    public class Notificador : INotificador
    {   
        private List<Notificacao> _notificacoes;

        public Notificador() 
        { 
            _notificacoes = new List<Notificacao>();
        }
        public void Handle(Notificacao noficacao)
        {
            _notificacoes.Add(noficacao);
        }

        public List<Notificacao> ObterNotificacoes()
        {
            return _notificacoes;
        }

        public bool TemNotificacao()
        {
            return _notificacoes.Any();
        }
    }
}
