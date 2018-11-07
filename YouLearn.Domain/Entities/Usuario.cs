using prmToolkit.NotificationPattern;
using System;
using YouLearn.Domain.Entities.Base;
using YouLearn.Domain.Extensions;
using YouLearn.Domain.ValueObjects;

namespace YouLearn.Domain.Entities
{
    public class Usuario: EntityBase
    {
        protected Usuario()
        {

        }

        public Usuario(Nome nome, Email email, string senha)
        {
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;

            new AddNotifications<Usuario>(this).IfNullOrInvalidLength(x => x.Senha, 3, 32);
            //Criptografo Senha
            this.Senha = this.Senha.ConvertToMD5();

            this.AddNotifications(nome, email);
        }

        public Usuario(Email email, string senha)
        {
            this.Email = email;
            this.Senha = senha;

            //Criptografo Senha
            this.Senha = this.Senha.ConvertToMD5();

            this.AddNotifications(email);
        }

        public Nome Nome { get; private set; }
        public Email Email { get; private set; }
        public string Senha { get; private set; }
    }
}
