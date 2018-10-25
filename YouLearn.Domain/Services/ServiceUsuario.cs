using prmToolkit.NotificationPattern;
using System;
using YouLearn.Domain.Arguments.Usuario;
using YouLearn.Domain.Entities;
using YouLearn.Domain.Interfaces.Services;
using YouLearn.Domain.Resources;
using YouLearn.Domain.ValueObjects;

namespace YouLearn.Domain.Services
{
    public class ServiceUsuario : Notifiable, IServiceUsuario 
    {
        public object RepositoryUsuario { get; private set; }

        public AdicionarUsuarioResponse AdicionarUsuario(AdicionarUsuarioRequest request)
        {
            if (request == null)
            {
                this.AddNotification("AdicionarUsuarioRequest",string.Format(MSG.X0_E_OBRIGATORIA, "AdicionarUsuarioRequest"));
                return null;
            }

            //Cria value objects
            Nome nome = new Nome(request.PrimeiroNome, request.UltimoNome);
            Email email = new Email(request.Email);
            //Cria entidade
            Usuario usuario = new Usuario(nome,email,request.Senha);

            this.AddNotifications(nome, email, usuario);

            if (this.IsInvalid() == true)
            {
                return null;
            }

            //Persiste no banco de dados
            AdicionarUsuarioResponse response = new AdicionarUsuarioResponse(Guid.NewGuid());//RepositoryUsuario().Salvar(usuario);
            return response;

        }

        public AutenticarUsuarioResponse AutenticarUsuario(AutenticarUsuarioRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
