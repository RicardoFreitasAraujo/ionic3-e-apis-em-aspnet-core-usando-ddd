using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YouLearn.Api.Controllers.Base;
using YouLearn.Domain.Arguments.Canal;
using YouLearn.Domain.Arguments.Usuario;
using YouLearn.Domain.Interfaces.Services;
using YouLearn.Infra.Transactions;

namespace YouLearn.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CanalController : ControleBasico
    {
        private readonly IServiceCanal _serviceCanal;
        private readonly IUnitOfWork _unitOfWork;

        public CanalController(IServiceCanal serviceCanal, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this._serviceCanal = serviceCanal;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("/Listar")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                Guid idUsuario = Guid.NewGuid();
                var response = this._serviceCanal.Listar(idUsuario);
                return await this.ResponseAsync(response, this._serviceCanal);
            }
            catch (Exception ex)
            {
                return await this.ResponseException(ex);
            }
        }

        [HttpPost]
        [Route("/Adicionar")]
        public async Task<IActionResult> Adicionar([FromBody]AdicionarCanalRequest request)
        {
            try
            {
                //string usuarioClaims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value();
                //AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(usuarioClaims);

                Guid idUsuario = Guid.NewGuid();

                var response = this._serviceCanal.AdicionarCanal(request, idUsuario);

                return await this.ResponseAsync(response, this._serviceCanal);
            }
            catch (Exception ex)
            {
                return await this.ResponseException(ex);
            }
        }

        [HttpDelete]
        [Route("Excluir/{id:Guid}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            try
            {
                var response = this._serviceCanal.ExcluirCanal(id);
                return await this.ResponseAsync(response, this._serviceCanal);
            }
            catch(Exception ex)
            {
                return await this.ResponseException(ex);
            }
        }
    }
}