using Domain.Entities;

namespace Application.Interfaces.IProducto
{
    public interface IProductoCommand
    {
        Task InsertProducto(Producto producto);
        Task UpdateProducto(Producto producto);
        Task RemoveProducto(Producto producto);
    }
}
