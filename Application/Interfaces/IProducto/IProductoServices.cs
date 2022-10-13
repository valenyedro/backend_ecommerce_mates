using Application.Models;
using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IProducto
{
    public interface IProductoServices
    {
        Task<Producto> UpdateProducto(Producto cliente);
        Task<Producto> DeleteProducto(Producto cliente);
        Task<List<ProductoResponse>> GetAllProductos();
        Task<ProductoResponse> GetProductoById(int id);
        Task<List<ProductoResponse>> GetProductosSort(ProductoRequest request);
    }
}
