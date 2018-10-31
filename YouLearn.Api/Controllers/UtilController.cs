using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouLearn.Api.Controllers
{
    public class UtilController
    {
        [HttpGet]
        [Route("")]
        public string Get()
        {
            return "Seja Bem Vindo ao YouLearn";
        }

        [HttpGet]
        [Route("version")]
        public string Versao()
        {
            return "0.0.1";
        }

        [HttpPost]
        [Route("")]
        public string Post()
        {
            return "Executou o verbo Post - Inserir nova informação";
        }

        [HttpPut]
        [Route("")]
        public object Put()
        {
            return "Put - Atualizar informação";
        }

        [HttpDelete]
        [Route("")]
        public object Delete()
        {
            return "Delete - Excluir informação";
        }


    }
}
