using Application.Models;
using Application.Request;
using Application.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IOrden
{
    public interface IOrdenServices
    {
        Task<OrdenResponse> CreateOrden(OrdenRequest request);
        Task<Orden> UpdateOrden(Orden orden);
        Task<Orden> DeleteOrden(Orden orden);
        Task<List<Orden>> GetAllOrdenes();
        Task<Orden> GetOrdenById(Guid id);
        Task<decimal> GetTotalCarrito(int clientId);
        Task<BalanceResponse> CreateBalance(BalanceRequest request);
    }
}
