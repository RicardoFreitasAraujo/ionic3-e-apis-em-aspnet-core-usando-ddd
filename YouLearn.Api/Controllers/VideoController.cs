using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouLearn.Api.Controllers.Base;
using YouLearn.Domain.Arguments.Usuario;
using YouLearn.Domain.Arguments.Video;
using YouLearn.Domain.Interfaces.Services;
using YouLearn.Infra.Transactions;

namespace YouLearn.Api.Controllers
{
    public class VideoController: ControleBasico
    {
        private readonly IServiceVideo _serviceVideo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public VideoController(IServiceVideo serviceVideo, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this._serviceVideo = serviceVideo;
            this._httpContextAccessor = httpContextAccessor;
            this._unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/v1/Video/Listar/{tags}")]
        public async Task<IActionResult> Listar(string tags)
        {
            try
            {
                var response = this._serviceVideo.Listar(tags);
                return await this.ResponseAsync(response, this._serviceVideo);
            }
            catch (Exception ex)
            {
                return await this.ResponseException(ex);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/v1/Video/Listar/{idPlayList:Guid}")]
        public async Task<IActionResult> Listar(Guid idPlayList)
        {
            try
            {
                var response = this._serviceVideo.Listar(idPlayList);
                return await ResponseAsync(response, this._serviceVideo);
            }
            catch (Exception ex)
            {
                return await this.ResponseException(ex);
            }
        }

        [HttpPost]
        [Route("api/v1/Video/Adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] AdicionarVideoRequest request)
        {
            try
            {
                string usuarioClaims = this._httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(usuarioClaims);

                var response = this._serviceVideo.AdicionarVideo(request, usuarioResponse.Id);

                return await this.ResponseAsync(response, this._serviceVideo);
            }
            catch (Exception ex)
            {
                return await this.ResponseException(ex);
            }
        }




    }
}
