using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouLearn.Domain.Interfaces.Services.Base;
using YouLearn.Infra.Transactions;

namespace YouLearn.Api.Controllers.Base
{
    public class ControleBasico: Controller
    {
        private IUnitOfWork _unitofWork;
        private IServiceBase _serviceBase;

        public ControleBasico(IUnitOfWork unitofWork)
        {
            this._unitofWork = unitofWork;
        }

        public async Task<IActionResult> ResponseAsync(object result, IServiceBase serviceBase)
        {
            this._serviceBase = serviceBase;

            if (!serviceBase.Notifications.Any())
            {
                try
                {
                    _unitofWork.Commit();
                    return Ok(result);
                    //return Request.CreateResponse(HttpStatusCode.OK,result);
                }
                catch (Exception ex)
                {
                    //Aqui devo logar o erro
                    return BadRequest($"Houve um problema intenro com o servidor. Entre em contato {ex.Message}");
                }
            }
            else
            {
                return BadRequest(new { erros = serviceBase.Notifications });
            }    
        }

        public async Task<IActionResult> ResponseException(Exception ex)
        {
            return BadRequest(new { erros = ex.Message, exception = ex.ToString() });
        }

        protected override void Dispose(bool disposing)
        {
            if (this._serviceBase != null)
            {
                _serviceBase.Dispose();
            }

            base.Dispose(disposing);
        }

    }
}
