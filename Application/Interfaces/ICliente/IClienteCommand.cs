using Domain.Entities;

namespace Application.Interfaces.ICliente
{
    public interface IClienteCommand
    {
        Task InsertCliente(Cliente cliente);
        Task UpdateCliente(Cliente cliente);
        Task RemoveCliente(Cliente cliente);
    }
}
