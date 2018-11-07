using prmToolkit.NotificationPattern;
using System;
using YouLearn.Domain.Entities.Base;

namespace YouLearn.Domain.Entities
{
    public class Canal: EntityBase
    {
        public Canal()
        {

        }

        public Canal(string nome, string urlLogo, Usuario usuario)
        {
            this.Nome = nome;
            this.UrlLogo = urlLogo;
            this.Usuario = usuario;

            new AddNotifications<Canal>(this)
                .IfNullOrInvalidLength(x => x.Nome, 2, 50)
                .IfNullOrInvalidLength(x => x.UrlLogo, 4, 200);

            this.AddNotifications(usuario);
        }

        public string Nome { get; private set; }
        public string UrlLogo { get; private set; }
        public Usuario Usuario { get; private set; }
    }
}
