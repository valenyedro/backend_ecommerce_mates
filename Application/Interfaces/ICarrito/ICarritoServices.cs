using Application.Models;
using Domain.Entities;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ICarrito
{
    public interface ICarritoServices
    {
        Task<Carrito> CreateCarrito(int idCliente);
        Task<Carrito> UpdateCarrito(Carrito carrito);
        Task<Carrito> DeleteCarrito(Carrito carrito);
        Task<List<Carrito>> GetAllCarritos();
        Task<Carrito> GetCarritoById(Guid id);
        Task<CarritoResponse> AddProductoToCarrito(CarritoRequest carritoRequest);
        Task<Carrito> GetCarritoCliente(int idCliente);
        Task<CarritoResponse> ModifyCarrito(CarritoRequest carritoRequest);
        Task<bool> DeleteProductoFromCarrito(int idCliente, int idProducto);
    }
}
