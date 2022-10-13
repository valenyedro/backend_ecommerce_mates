using Domain.Entities;

namespace Application.Interfaces.ICarritoProducto
{
    public interface ICarritoProductoCommand
    {
        Task InsertCarritoProducto(CarritoProducto carritoProducto);
        Task UpdateCarritoProducto(CarritoProducto carritoProducto);
        Task RemoveCarritoProducto(CarritoProducto carritoProducto);
    }
}
