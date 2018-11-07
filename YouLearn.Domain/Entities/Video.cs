using prmToolkit.NotificationPattern;
using YouLearn.Domain.Entities.Base;
using YouLearn.Domain.Enums;

namespace YouLearn.Domain.Entities
{
    public class Video: EntityBase
    {
        protected Video()
        {

        }

        public Video(Canal canal, PlayList playList, string titulo, string descricao, string tags, int? ordemNaPlayList, string idVideoYoutube, Usuario usuarioSugeriu)
        {
            this.Canal = canal;
            this.PlayList = playList;
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.Tags = tags;
            this.OrdemNaPlayList = ordemNaPlayList;
            this.IdVideoYoutube = idVideoYoutube;
            this.UsuarioSugeriu = usuarioSugeriu;
            this.Status = EnumStatus.EmAnalise;

            new AddNotifications<Video>(this)
                .IfNullOrInvalidLength(x => x.Titulo, 1, 200)
                .IfNullOrInvalidLength(x => x.Descricao, 1, 255)
                .IfNullOrInvalidLength(x => x.Tags, 1, 50)
                .IfNullOrInvalidLength(x => x.IdVideoYoutube, 1, 50);

            this.AddNotifications(this.Canal);

            if (playList != null)
            {
                this.AddNotifications(playList);
            }
        }

        public Canal Canal { get; private set; }
        public PlayList PlayList { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string Tags { get; private set; }
        public int? OrdemNaPlayList { get; private set; }
        public string IdVideoYoutube { get; private set; }
        public Usuario UsuarioSugeriu { get; private set; }
        public EnumStatus Status { get; private set; }
    }
}
