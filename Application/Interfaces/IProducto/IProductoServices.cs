using Application.Models;
using Application.Response;
using Domain.Entities;

namespace Application.Interfaces.IProducto
{
    public interface IProductoServices
    {
        Task<Producto> UpdateProducto(Producto cliente);
        Task<Producto> DeleteProducto(Producto cliente);
        Task<List<ProductoResponse>> GetAllProductos();
        Task<ProductoResponse> GetProductoById(int id);
        Task<List<ProductoResponse>> GetProductosSort(string? name, bool? sort);
    }
}
