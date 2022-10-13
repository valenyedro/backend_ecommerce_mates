using Domain.Entities;

namespace Application.Interfaces.ICliente
{
    public interface IClienteQuery
    {
        Task<List<Cliente>> GetListClientes();
        Task<Cliente> GetCliente(int id);
    }
}
