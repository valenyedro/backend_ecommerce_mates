using Application.Interfaces.ICliente;
using Application.Interfaces.IProducto;
using Application.Models;
using Application.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProyectoTp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoServices _services;

        public ProductosController(IProductoServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductos()
        {
            try
            {
                var Result = await _services.GetAllProductos();
                if (Result == null)
                    return NotFound();
                return new JsonResult(Result);
            }
            catch (Exception e)
            {
                return new JsonResult(null) { StatusCode = 500 };
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetProductosSort(ProductoRequest request)
        {
            try
            {
                List<ProductoResponse> Result = await _services.GetProductosSort(request);
                if (Result == null)
                    return NotFound();
                return new JsonResult(Result);
            }
            catch (Exception e)
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
            catch (NullReferenceException e)
            {
                return new JsonResult(null) { StatusCode = 500 };
            }
        }
    }
}
