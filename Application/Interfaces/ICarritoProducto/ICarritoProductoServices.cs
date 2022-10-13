using Application.Models;
using Domain.Entities;

namespace Application.Interfaces.ICarritoProducto
{
    public interface ICarritoProductoServices
    {
        Task<CarritoProducto> CreateCarritoProducto(CarritoProductoRequest request);
        Task<CarritoProducto> UpdateCarritoProducto(CarritoProducto carritoProducto);
        Task<CarritoProducto> DeleteCarritoProducto(CarritoProducto carritoProducto);
        Task<List<CarritoProducto>> GetAllCarritoProductos();
        Task<CarritoProducto> GetCarritoProductoById(Guid carritoId);
    }
}
