using Application.Interfaces.IProducto;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoTp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productosController : ControllerBase
    {
        private readonly IProductoServices _services;

        public productosController(IProductoServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductosSort([FromQuery] string? name, [FromQuery] bool? sort)
        {
            try
            {
                List<ProductoResponse> Result = await _services.GetProductosSort(name, sort);
                if (Result == null)
                    return NotFound();
                return new JsonResult(Result);
            }
            catch (Exception)
            {
                return new JsonResult(null) { StatusCode = 500 };
            }
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductoById(int id)
        {
            try
            {
                var Result = await _services.GetProductoById(id);
                if (Result == null)
                    return NotFound();
                return new JsonResult(Result) { StatusCode = 200 };
            }
            catch (Exception)
            {
                return new JsonResult(null) { StatusCode = 500 };
            }
        }
    }
}
