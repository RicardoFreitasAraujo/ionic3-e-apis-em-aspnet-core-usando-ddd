using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YouLearn.Api.Controllers.Base;
using YouLearn.Domain.Arguments.PlayList;
using YouLearn.Domain.Arguments.Usuario;
using YouLearn.Domain.Interfaces.Services;
using YouLearn.Infra.Transactions;

namespace YouLearn.Api.Controllers
{
    public class PlayListController : ControleBasico
    {
        private readonly IServicePlayList _servicePlayList;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PlayListController(IServicePlayList servicePlayList, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork): base(unitOfWork)
        {
            this._servicePlayList = servicePlayList;
            this._httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("api/v1/PlayList/Listar")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                string usuarioClaims = this._httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(usuarioClaims);

                var response = this._servicePlayList.Listar(usuarioResponse.Id);
                return await this.ResponseAsync(response, this._servicePlayList);
            }
            catch (Exception ex)
            {
                return await this.ResponseException(ex);
            }
        }

        [HttpPost]
        [Route("api/v1/PlayList/Adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] AdicionarPlayListRequest request)
        {
            try
            {
                string usuarioClaims = this._httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(usuarioClaims);

                var response = this._servicePlayList.AdicionarPlayList(request, usuarioResponse.Id);
                return await this.ResponseAsync(response, this._servicePlayList);
            }
            catch (Exception ex)
            {
                return await this.ResponseException(ex);
            }
        }

    }
}