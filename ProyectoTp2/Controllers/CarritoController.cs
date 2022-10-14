using Application.Interfaces.ICarrito;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoTp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class carritoController : ControllerBase
    {
        private readonly ICarritoServices _services;

        public carritoController(ICarritoServices services)
        {
            _services = services;
        }

        [HttpPut]
        public async Task<IActionResult> AddProductoToCarrito(CarritoRequest request)
        {
            try
            {
                var CarritoResponse = await _services.AddProductoToCarrito(request);
                if (CarritoResponse == null)
                    return BadRequest();
                return new JsonResult(CarritoResponse) { StatusCode = 200 };
            }
            catch (Exception)
            {
                return new JsonResult(null) { StatusCode = 500 };
            }
        }

        [HttpPatch]
        public async Task<IActionResult> ModifyCarrito(CarritoRequest request)
        {
            try
            {
                var carritoResponse = await _services.ModifyCarrito(request);
                if (carritoResponse == null)
                    return BadRequest();
                return new JsonResult(carritoResponse) { StatusCode = 200 };

            } catch (Exception)
            {
                return new JsonResult(null) { StatusCode = 500 };
            }
        }

        [HttpDelete("{clientId:Int}/{productId:Int}")]
        public async Task<IActionResult> DeleteProductoFromCarrito(int clientId, int productId)
        {
            try
            {
                var Result = await _services.DeleteProductoFromCarrito(clientId, productId);
                if (!Result)
                    return BadRequest();
                return new JsonResult(Result) { StatusCode = 200 };
            }
            catch (Exception)
            {
                return new JsonResult(null) { StatusCode = 500 };
            }
        }
    }
}
