using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YouLearn.Domain.Arguments.Usuario;
using YouLearn.Api.Controllers.Base;
using YouLearn.Domain.Interfaces.Services;
using YouLearn.Infra.Transactions;
using YouLearn.Api.Security;
using System.Security.Claims;
using System.Security.Principal;
using System.IO;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace YouLearn.Api.Controllers
{
    public class UsuarioController : ControleBasico
    {
        private readonly IServiceUsuario _servicoUsuario;

        public UsuarioController(IUnitOfWork unitOfWork, IServiceUsuario servicoUsuario): base(unitOfWork)
        {
            this._servicoUsuario = servicoUsuario;
        }

        public BinaryReader JwtRegisteredClaimsNames { get; private set; }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/v1/Usuario/Adicionar")]
        public async Task<IActionResult> Adicionar([FromBody]AdicionarUsuarioRequest request)
        {
            try
            {
                var response = _servicoUsuario.AdicionarUsuario(request);
                return await ResponseAsync(response, _servicoUsuario);
            }
            catch(Exception ex)
            {
                return await ResponseException(ex);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/v1/Usuario/Autenticar")]
        public object Autenticar(
            [FromBody] AutenticarUsuarioRequest request,
            [FromServices] SigningConfigurations signingConfigurations,
            [FromServices] TokenConfigurations tokenConfigurations)
        {
            bool credenciaisValidas = false;
            AutenticarUsuarioResponse response = this._servicoUsuario.AutenticarUsuario(request);

            credenciaisValidas = response != null;

            if (credenciaisValidas)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(response.Id.ToString(),"Id"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim("Usuario", JsonConvert.SerializeObject(response))
                    }
                );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                #region Criação do Token
                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);
                #endregion

                return new
                {
                    authenticated = true,
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    message = "OK",
                    primerioNomeDoProprietario = response.PrimeiroNome
                };
            }
            else
            {
                return new
                {
                    authenticated = false,
                    this._servicoUsuario.Notifications
                };
            }

        }

    }
}