using Application.Interfaces.ICliente;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoTp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteServices _services;

        public ClientesController(IClienteServices services)
        {
            _services = services;
        }

        [HttpPost]
        public async Task<IActionResult> UploadCliente(ClienteRequest request)
        {
            try
            {
                var result = await _services.CreateCliente(request);
                if (result == null)
                    return BadRequest();
                return new JsonResult(result) { StatusCode = 201 };
            }
            catch (Exception e)
            {
                return new JsonResult(null) { StatusCode = 500 };
            }
        }
    }
}
