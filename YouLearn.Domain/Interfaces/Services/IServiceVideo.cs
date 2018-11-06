using System;
using System.Collections.Generic;
using YouLearn.Domain.Arguments.Video;

namespace YouLearn.Domain.Interfaces.Services
{
    public interface IServiceVideo
    {
        AdicionarVideoResponse AdicionarVideo(AdicionarVideoRequest request, Guid idUsuario);
        IEnumerable<VideoResponse> Listar(string tags);
        IEnumerable<VideoResponse> Listar(Guid idPlayList);
    }
}
