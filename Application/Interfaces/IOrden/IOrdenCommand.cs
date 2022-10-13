using Domain.Entities;

namespace Application.Interfaces.IOrden
{
    public interface IOrdenCommand
    {
        Task InsertOrden(Orden orden);
        Task UpdateOrden(Orden orden);
        Task RemoveOrden(Orden orden);
    }
}
