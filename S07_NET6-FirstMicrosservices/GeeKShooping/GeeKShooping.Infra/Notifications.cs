using System.ComponentModel.DataAnnotations.Schema;

namespace GeeKShooping.Infra
{
    public class Notification
    {
        public Notification()
        {

        }

        [NotMapped]
        public string NomePropriedade { get; set; }

        [NotMapped]
        public string Mensagem { get; set; }

        [NotMapped]
        public bool Sucesso { get; set; } = true;

        [NotMapped]
        private static List<Notification> ListaNotification { get; set; } = new List<Notification>();

        public static List<Notification> Notify(string mensagem, string nomePropriedade = null)
        {

            if (!string.IsNullOrWhiteSpace(mensagem) && IsValid())
            {
                ClearNotifications();

                ListaNotification.Add(new Notification
                {
                    Mensagem = mensagem,
                    NomePropriedade = nomePropriedade,
                    Sucesso = false
                });

            }
            return ListaNotification;
        }

        public static List<Notification> NotifyList(string mensagem, string nomePropriedade = null)
        {

            if (!string.IsNullOrWhiteSpace(mensagem))
            {
                ListaNotification.Add(new Notification
                {
                    Mensagem = mensagem,
                    NomePropriedade = nomePropriedade,
                    Sucesso = false
                });

            }
            return ListaNotification;
        }
        public static bool IsValid()
        {
            return !ListaNotification.Any();
        }
        public static void ClearNotifications()
        {
            ListaNotification.Clear();
        }
        public static List<string> GetErrors()
        {
            List<string> notifications = ListaNotification.Where(x => !x.Sucesso).Select(x => x.Mensagem).ToList();
            ClearNotifications();
            return notifications;
        }
    }
}
