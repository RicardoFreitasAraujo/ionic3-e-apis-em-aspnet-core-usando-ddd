using prmToolkit.NotificationPattern;
using System;
using YouLearn.Domain.Arguments.Usuario;
using YouLearn.Domain.Entities;
using YouLearn.Domain.Interfaces.Repositories;
using YouLearn.Domain.Interfaces.Services;
using YouLearn.Domain.Resources;
using YouLearn.Domain.ValueObjects;

namespace YouLearn.Domain.Services
{
    public class ServiceUsuario : Notifiable, IServiceUsuario 
    {
        //Dependencias
        private readonly IRepositoryUsuario _repositoryUsuario;

        //Construtor
        public ServiceUsuario(IRepositoryUsuario repositoryUsuario)
        {
            this._repositoryUsuario = repositoryUsuario;
        }

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

            if (this.IsInvalid()) return null;

            //Persiste no banco de dados
            _repositoryUsuario.Salvar(usuario);

            return new AdicionarUsuarioResponse(usuario.Id);
        }

        public AutenticarUsuarioResponse AutenticarUsuario(AutenticarUsuarioRequest request)
        {
            if (request == null)
            {
                this.AddNotification("AutenticarUsuarioRequest", "objeto obrigatório");
                return null;
            }

            Email email = new Email(request.Email);
            Usuario usuario = new Usuario(email,request.Senha);

            this.AddNotifications(usuario);

            if (this.IsInvalid()) return null;

            usuario = _repositoryUsuario.Obter(usuario.Email.Endereco, usuario.Senha);

            if (usuario == null)
            {
                this.AddNotification("Usuario", "dados não encontrado");
                return null;
            }

            return (AutenticarUsuarioResponse)usuario;
        }
    }
}
