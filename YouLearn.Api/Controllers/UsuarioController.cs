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

namespace YouLearn.Api.Controllers
{
    public class UsuarioController : ControleBasico
    {
        private readonly IServiceUsuario _servicoUsuario;

        public UsuarioController(IUnitOfWork unitOfWork, IServiceUsuario servicoUsuario): base(unitOfWork)
        {
            this._servicoUsuario = servicoUsuario;
        }

        //[AllowAnonymous]
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
    }
}