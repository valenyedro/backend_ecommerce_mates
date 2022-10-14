using Application.Interfaces.ICarrito;
using Application.Interfaces.ICliente;
using Application.Models;
using Application.Response;
using Domain.Entities;

namespace Application.UseCase
{
    public class ClienteServices : IClienteServices
    {
        private readonly IClienteCommand _command;
        private readonly IClienteQuery _query;
        private readonly ICarritoServices _servicesCarrito;

        public ClienteServices(IClienteCommand command, IClienteQuery query, ICarritoServices servicesCarrito)
        {
            _command = command;
            _query = query;
            _servicesCarrito = servicesCarrito;
        }

        public async Task<ClienteResponse> CreateCliente(ClienteRequest request)
        {
            var Clientes = await GetAllClientes();
            var Cliente = new Cliente
            {
                DNI = request.dni,
                Nombre = request.name,
                Apellido = request.lastname,
                Direccion = request.address,
                Telefono = request.phoneNumber
            };
            foreach (Cliente cliente in Clientes)
            {
                if (cliente.DNI == Cliente.DNI)
                    return null;
            }
            
            await _command.InsertCliente(Cliente);
            Cliente.Carritos.Add(await _servicesCarrito.CreateCarrito(Cliente.ClienteId));
            await _command.UpdateCliente(Cliente);

            var ClienteResponse = new ClienteResponse
            {
                ClienteId = Cliente.ClienteId,
                dni = request.dni,
                name = request.name,
                lastname = request.lastname,
                address = request.address,
                phoneNumber = request.phoneNumber
            };
            return ClienteResponse;
        }

        public async Task NewCarritoActive(int clienteId)
        {
            Cliente Cliente = await _query.GetCliente(clienteId);
            Carrito Carrito = await _servicesCarrito.GetCarritoCliente(clienteId);
            Carrito.Estado = false;
            await _servicesCarrito.UpdateCarrito(Carrito);
            Cliente.Carritos.Add(await _servicesCarrito.CreateCarrito(Cliente.ClienteId));
            await _command.UpdateCliente(Cliente);
        }

        public async Task<Cliente> UpdateCliente(Cliente cliente)
        {
            await _command.UpdateCliente(cliente);
            return cliente;
        }

        public async Task<Cliente> DeleteCliente(Cliente cliente)
        {
            await _command.RemoveCliente(cliente);
            return cliente;
        }

        public async Task<List<Cliente>> GetAllClientes()
        {
            var Clientes = await _query.GetListClientes();
            return Clientes;
        }

        public async Task<Cliente> GetClienteById(int id)
        {
            var Cliente = await _query.GetCliente(id);
            return Cliente;
        }

    }
}
