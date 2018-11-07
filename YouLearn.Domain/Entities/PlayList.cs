using prmToolkit.NotificationPattern;
using System;
using YouLearn.Domain.Entities.Base;
using YouLearn.Domain.Enums;

namespace YouLearn.Domain.Entities
{
    public class PlayList: EntityBase
    {
        protected PlayList()
        {

        }

        public PlayList(string nome, Usuario usuario)
        {
            this.Nome = nome;
            this.Usuario = usuario;
            this.Status = EnumStatus.EmAnalise;

            new AddNotifications<PlayList>(this)
                .IfNullOrInvalidLength(x => x.Nome, 2, 100);

            this.AddNotifications(usuario);
        }

        public string Nome { get; private set; }
        public Usuario Usuario { get; private set; }
        public EnumStatus Status { get; private set; }
    }
}
