using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YouLearn.Domain.Arguments.Base;
using YouLearn.Domain.Arguments.Canal;
using YouLearn.Domain.Entities;
using YouLearn.Domain.Interfaces.Repositories;
using YouLearn.Domain.Interfaces.Services;

namespace YouLearn.Domain.Services
{
    public class ServiceCanal : Notifiable, IServiceCanal
    {
        private readonly IRepositoryCanal _repositoryCanal;
        private readonly IRepositoryUsuario _repositoryUsuario;

        public ServiceCanal(IRepositoryCanal repositoryCanal, IRepositoryUsuario repositoryUsuario)
        {
            this._repositoryCanal = repositoryCanal;
            this._repositoryUsuario = repositoryUsuario;
        }

        public CanalResponse AdicionarCanal(AdicionarCanalRequest request, Guid idUsuario)
        {
            Usuario usuario = this._repositoryUsuario.Obter(idUsuario);
            Canal canal = new Canal(request.Nome, request.UrlLogo, usuario);
            this.AddNotifications(canal);

            if (this.IsInvalid()) return null;

            canal = _repositoryCanal.Adicionar(canal);

            return (CanalResponse)canal;
        }

        public Arguments.Base.Response ExcluirCanal(Guid idCanal)
        {
            //bool existe = this._repositoryVideo.ExisteCanalAssociado(idCanal);
            bool existe = true;

            if (existe)
            {
                this.AddNotification("Canal", "Não é possível excluir canal pois tem vídeo associado");
            }

            Canal canal = this._repositoryCanal.Obter(idCanal);

            if (canal ==  null)
            {
                this.AddNotification("Canal", "Dados não encontrados");
            }

            if (this.IsInvalid()) return null;

            this._repositoryCanal.ExcluirCanal(canal);

            return new Arguments.Base.Response() { Message = "Operação realizada com sucesso" };
        }

        public IEnumerable<CanalResponse> Listar(Guid idUsuario)
        {
            IEnumerable<Canal> canalCollection = _repositoryCanal.Listar(idUsuario);
            var response = canalCollection.ToList().Select(entidade => (CanalResponse)entidade);
            return response;
        }
    }
}
