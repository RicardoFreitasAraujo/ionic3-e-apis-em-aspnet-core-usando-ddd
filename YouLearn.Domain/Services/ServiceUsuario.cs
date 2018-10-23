using System;
using YouLearn.Domain.Arguments.Usuario;
using YouLearn.Domain.Entities;
using YouLearn.Domain.Interfaces.Services;

namespace YouLearn.Domain.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        public object RepositoryUsuario { get; private set; }

        public AdicionarUsuarioResponse AdicionarUsuario(AdicionarUsuarioRequest request)
        {
            if (request == null)
            {
                throw new Exception("Objeto AdicionarUsuarioRequest é obrigatório");
            }

            //Cria entidade
            Usuario usuario = new Usuario();
            usuario.Nome.PrimeiroNome = "Ricardo Freitas";
            usuario.Nome.UltimoNome = "Araújo";
            usuario.Email.Endereco = "ricardo.freitas.araujo@gmail.com";
            usuario.Senha = "123456";

            //Validações
            if (usuario.Nome.PrimeiroNome.Length < 3 || usuario.Nome.PrimeiroNome.Length > 50)
            {
                throw new Exception("Primerio Nome deve conter de 3 a 50 caracteres");
            }

            if (usuario.Nome.UltimoNome.Length < 3 || usuario.Nome.UltimoNome.Length > 50)
            {
                throw new Exception("Último Nome deve conter de 3 a 50 caracteres");
            }

            if (usuario.Email.Endereco.IndexOf("@") < 1)
            {
                throw new Exception("Email inválido");
            }

            if (usuario.Senha.Length >= 3)
            {
                throw new Exception("Senha deve ter no minimo 3 caracteres");
            }

            //Persiste no banco de dados
            AdicionarUsuarioResponse response = new RepositoryUsuario().Salvar(usuario);
            return response;

        }

        public AutenticarUsuarioResponse AutenticarUsuario(AutenticarUsuarioRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
