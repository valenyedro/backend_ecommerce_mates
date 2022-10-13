using Domain.Entities;

namespace Application.Interfaces.ICarrito
{
    public interface ICarritoQuery
    {
        Task<List<Carrito>> GetListCarritos();
        Task<Carrito> GetCarrito(Guid id);
    }
}
