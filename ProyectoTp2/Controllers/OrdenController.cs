using Application.Interfaces.IOrden;
using Application.Models;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoTp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenController : ControllerBase
    {
        private readonly IOrdenServices _services;

        public OrdenController(IOrdenServices services)
        {
            _services = services;
        }

        [HttpPost("{clientId:int}")]
        public async Task<IActionResult> EfectuarCompra(int clientId)
        {
            try
            {
                var OrdenResponse = await _services.CreateOrden(new OrdenRequest { ClientId = clientId });
                if (OrdenResponse == null)
                    return BadRequest();
                return new JsonResult(OrdenResponse) { StatusCode = 201 };

            } catch(Exception e)
            {
                return new JsonResult(null) { StatusCode = 500 };
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBalance(BalanceRequest request)
        {
            try
            {
                BalanceResponse BalanceResponse = await _services.CreateBalance(request);
                if (BalanceResponse == null)
                    return BadRequest();
                return new JsonResult(BalanceResponse) { StatusCode = 200 };
            }
            catch (Exception)
            {
                return new JsonResult(null) { StatusCode = 500 };
            }
        }
    }
}
