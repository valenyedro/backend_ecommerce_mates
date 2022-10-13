using Domain.Entities;

namespace Application.Interfaces.IOrden
{
    public interface IOrdenQuery
    {
        Task<List<Orden>> GetListOrdenes();
        Task<Orden> GetOrden(Guid id);
    }
}
