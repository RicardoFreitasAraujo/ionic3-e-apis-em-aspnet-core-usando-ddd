using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YouLearn.Domain.Arguments.Video;
using YouLearn.Domain.Entities;
using YouLearn.Domain.Interfaces.Repositories;
using YouLearn.Domain.Interfaces.Services;

namespace YouLearn.Domain.Services
{
    public class ServiceVideo : Notifiable, IServiceVideo
    {
        private readonly IRepositoryUsuario _repositoryUsuario;
        private readonly IRepositoryCanal _repositoryCanal;
        private readonly IRepositoryPlayList _repositoryPlayList;
        private readonly IRepositoryVideo _repositoryVideo;

        public ServiceVideo(IRepositoryUsuario repositoryUsuario, IRepositoryCanal repositoryCanal, IRepositoryPlayList repositoryPlayList, IRepositoryVideo repositoryVideo)
        {
            this._repositoryUsuario = repositoryUsuario;
            this._repositoryCanal = repositoryCanal;
            this._repositoryPlayList = repositoryPlayList;
            this._repositoryVideo = repositoryVideo;
        }

        public AdicionarVideoResponse AdicionarVideo(AdicionarVideoRequest request, Guid idUsuario)
        {
            if (request == null)
            {
                this.AddNotification("AdicionarVideoRequest", "Objeto não informado");
                return null;
            }

            Usuario usuario = this._repositoryUsuario.Obter(idUsuario);
            if (usuario == null)
            {
                this.AddNotification("Usuario", "Usuário não localizado");
                return null;
            }

            Canal canal = this._repositoryCanal.Obter(request.IdCanal);
            if (canal == null)
            {
                this.AddNotification("Canal", "Canal não localizado");
                return null;
            }

            PlayList playList = null;
            if (request.IdPlayList != Guid.Empty)
            {
                playList = this._repositoryPlayList.Obter(request.IdPlayList);
                if (playList == null)
                {
                    this.AddNotification("PlayList", "Playlist não localizada");
                    return null;
                }
            }

            var video = new Video(canal, playList, request.Titulo, request.Descricao, request.Tags, request.OrdemNaPlayList, request.IdVideoYoutube, usuario);

            this.AddNotifications(video);

            if (this.IsInvalid()) return null;
            
            this._repositoryVideo.Adicionar(video);

            return new AdicionarVideoResponse(video.Id);
        }

        public IEnumerable<VideoResponse> Listar(string tags)
        {
            IEnumerable<Video> videoCollection = this._repositoryVideo.Listar(tags);
            var response = videoCollection.ToList().Select(entidade => (VideoResponse)entidade );
            return response;
        }

        public IEnumerable<VideoResponse> Listar(Guid idPlayList)
        {
            throw new NotImplementedException();
        }
    }
}
