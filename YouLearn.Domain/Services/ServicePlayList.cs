using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YouLearn.Domain.Arguments.Base;
using YouLearn.Domain.Arguments.PlayList;
using YouLearn.Domain.Entities;
using YouLearn.Domain.Interfaces.Repositories;
using YouLearn.Domain.Interfaces.Services;

namespace YouLearn.Domain.Services
{
    public class ServicePlayList : Notifiable, IServicePlayList
    {
        private readonly IRepositoryUsuario _repositoryUsuario;
        private readonly IRepositoryPlayList _repositoryPlayList;

        public ServicePlayList(IRepositoryUsuario repositoryUsuario, IRepositoryPlayList repositoryPlayList)
        {
            this._repositoryUsuario = repositoryUsuario;
            this._repositoryPlayList = repositoryPlayList;
        }

        public PlayListResponse AdicionarPlayList(AdicionarPlayListRequest request, Guid idUsuario)
        {
            Usuario usuario = _repositoryUsuario.Obter(idUsuario);

            PlayList playList = new PlayList(request.Nome, usuario);

            this.AddNotifications(playList);

            if (this.IsInvalid()) return null;

            playList = this._repositoryPlayList.Adicionar(playList);

            return (PlayListResponse)playList;
        }

        public Arguments.Base.Response ExcluirPlayList(Guid idPlayList)
        {
            //bool existe _repositoryVideo.ExistePlayListAssociada(idPlayList);
            bool existe = false;
            if (existe)
            {
                this.AddNotification("PlalyList", "Não é possível excluit uma playlist associado a um vídeo");
                return null;
            }

            PlayList playList = this._repositoryPlayList.Obter(idPlayList);

            if (playList == null)
            {
                this.AddNotification("PlayList", "Dados não encontrados");
            }

            if (this.IsInvalid()) return null;

            _repositoryPlayList.Excluir(playList);

            return new Arguments.Base.Response() { Message = "Operação realizada com sucesso" };
        }

        public IEnumerable<PlayListResponse> Listar(Guid idUsuario)
        {
            IEnumerable<PlayList> playListCollection = this._repositoryPlayList.Listar(idUsuario);
            var response = playListCollection.ToList().Select(entidade => (PlayListResponse)entidade);
            return response;
        }
    }
}
