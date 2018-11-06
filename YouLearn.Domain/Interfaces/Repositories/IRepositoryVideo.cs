using System;
using System.Collections.Generic;
using System.Text;
using YouLearn.Domain.Entities;

namespace YouLearn.Domain.Interfaces.Repositories
{
    public interface IRepositoryVideo
    {
        void Adicionar(Video Video);
        IEnumerable<Video> Listar(string tags);
        IEnumerable<Video> Listar(Guid idPlayList);
        bool ExisteCanalAssociado(Guid idCanal);
        bool ExistePlayListAssociado(Guid idPlayList);
    }
}
