using Application.Interfaces.ICliente;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Query
{
    public class ClienteQuery : IClienteQuery
    {
        private readonly AppDbContext _context;

        public ClienteQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cliente> GetCliente(int id)
        {
            var Cliente = await _context.Cliente.FirstOrDefaultAsync(c => c.ClienteId == id);
            return Cliente;
        }

        public async Task<List<Cliente>> GetListClientes()
        {
            var Clientes = await _context.Cliente.Include(c => c.Carritos).ToListAsync();
            return Clientes;
        }
    }
}
