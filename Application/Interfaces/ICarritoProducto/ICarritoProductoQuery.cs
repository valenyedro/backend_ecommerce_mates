using Domain.Entities;

namespace Application.Interfaces.ICarritoProducto
{
    public interface ICarritoProductoQuery
    {
        Task<List<CarritoProducto>> GetListCarritoProductos();
        Task<CarritoProducto> GetCarritoProducto(Guid carritoId);
    }
}
