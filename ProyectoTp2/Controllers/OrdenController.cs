using Application.Interfaces.IOrden;
using Application.Models;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoTp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ordenController : ControllerBase
    {
        private readonly IOrdenServices _services;

        public ordenController(IOrdenServices services)
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

            } catch(Exception)
            {
                return new JsonResult(null) { StatusCode = 500 };
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateBalance([FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            try
            {
                BalanceResponse BalanceResponse = await _services.CreateBalance(from, to);
                if (BalanceResponse == null)
                    return NotFound();
                return new JsonResult(BalanceResponse) { StatusCode = 200 };
            }
            catch (Exception)
            {
                return new JsonResult(null) { StatusCode = 500 };
            }
        }
    }
}
