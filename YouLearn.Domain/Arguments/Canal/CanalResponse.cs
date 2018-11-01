using System;
using YouLearn.Domain.Entities;

namespace YouLearn.Domain.Arguments.Canal
{
    public class CanalResponse
    {
        public Guid IdCanal { get; set; }
        public string Nome { get; set; }
        public string UrlLogo { get; set; }

        public static explicit operator CanalResponse(Entities.Canal entidade)
        {
            return new CanalResponse()
            {
                IdCanal = entidade.Id,
                Nome = entidade.Nome,
                UrlLogo = entidade.UrlLogo
            };
        }
    }
}
