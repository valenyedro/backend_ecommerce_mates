using Domain.Entities;

namespace Application.Interfaces.ICarrito
{
    public interface ICarritoCommand
    {
        Task InsertCarrito(Carrito carrito);
        Task UpdateCarrito(Carrito carrito);
        Task RemoveCarrito(Carrito carrito);
    }
}
