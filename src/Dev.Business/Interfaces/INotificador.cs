using Dev.Business.Notification;

namespace Dev.Business.Interfaces
{
    public interface INotificador
    {
        List<Notificacao> ObterNotificacoes();

        void Handle(Notificacao noficacao);

        bool TemNotificacao();
    }
}
