using Domain.Entities;

namespace Application.Interfaces.IProducto
{
    public interface IProductoQuery
    {
        Task<List<Producto>> GetListProductos();
        Task<Producto> GetProducto(int id);
    }
}
