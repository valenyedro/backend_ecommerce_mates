using Application.Interfaces.ICarrito;
using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoTp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly ICarritoServices _services;

        public CarritoController(ICarritoServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _services.GetAllCarritos();
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCarrito(CarritoRequest request)
        {
            var result = await _services.CreateCarrito(request);
            return new JsonResult(result) { StatusCode = 201 };
        }

        [HttpPut("{CarritoId}")]
        public async Task<IActionResult> UpdateCarrito(int CarritoId, Carrito request)
        {
            try
            {
                var result = await _services.UpdateCarrito(request);

                return new JsonResult(result) { StatusCode = 201 };
            }
            catch (NullReferenceException j)
            {
                return new JsonResult(null) { StatusCode = 404 };
            }
        }

        [HttpGet("{CarritoId}")]
        public async Task<IActionResult> GetById(Guid carritoId)
        {
            try
            {
                var result = await _services.GetCarritoById(carritoId);
                return new JsonResult(result) { StatusCode = 201 };
            }
            catch (NullReferenceException e)
            {
                return new JsonResult(null) { StatusCode = 404 };
            }
        }

        [HttpDelete("{CarritoId}")]
        public async Task<IActionResult> DeleteCarrito(Carrito carrito)
        {
            try
            {
                var result = await _services.DeleteCarrito(carrito);
                return new JsonResult(result) { StatusCode = 202 };
            }
            catch (NullReferenceException e)
            {
                return NotFound();
                //return new JsonResult(null) { StatusCode = 404 };
            }
        }
    }
}
