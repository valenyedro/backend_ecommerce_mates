using Application.Models;
using Application.Response;
using Domain.Entities;

namespace Application.Interfaces.IOrden
{
    public interface IOrdenServices
    {
        Task<OrdenWithProductsResponse> CreateOrden(OrdenRequest request);
        Task<Orden> UpdateOrden(Orden orden);
        Task<Orden> DeleteOrden(Orden orden);
        Task<List<Orden>> GetAllOrdenes();
        Task<Orden> GetOrdenById(Guid id);
        Task<decimal> GetTotalCarrito(int clientId);
        Task<BalanceResponse> CreateBalance(DateTime? from, DateTime? to);
    }
}
