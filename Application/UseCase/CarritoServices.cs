using Application.Interfaces.ICarrito;
using Application.Interfaces.ICarritoProducto;
using Application.Interfaces.ICliente;
using Application.Interfaces.IProducto;
using Application.Models;
using Application.Response;
using Domain.Entities;

namespace Application.UseCase
{
    public class CarritoServices : ICarritoServices
    {
        private readonly ICarritoCommand _command;
        private readonly ICarritoQuery _query;
        private readonly IClienteQuery _queryCliente;
        private readonly IProductoQuery _queryProducto;
        private readonly ICarritoProductoServices _servicesCarritoProducto;

        public CarritoServices(ICarritoCommand command, ICarritoQuery query, IClienteQuery queryCliente, IProductoQuery queryProducto, ICarritoProductoServices servicesCarritoProducto)
        {
            _command = command;
            _query = query;
            _queryCliente = queryCliente;
            _queryProducto = queryProducto;
            _servicesCarritoProducto = servicesCarritoProducto;
        }

        public async Task<CarritoResponse> AddProductoToCarrito(CarritoRequest carritoRequest)
        {
            List<CarritoProducto> ListCarritoProd = await _servicesCarritoProducto.GetAllCarritoProductos();
            Cliente Cliente = await _queryCliente.GetCliente(carritoRequest.clientId);
            Producto Producto = await _queryProducto.GetProducto(carritoRequest.productId);
            if (Cliente == null || Producto == null)
                return null;
            Carrito Carrito = await GetCarritoCliente(Cliente.ClienteId);

            if (Carrito == null)
            {
                Carrito = await CreateCarrito(Cliente.ClienteId);
                Cliente.Carritos.Add(Carrito);
            }

            foreach (CarritoProducto carritoProd in Carrito.CarritoProductos)
            {
                if (carritoProd.ProductoId == Producto.ProductoId)
                    return null;
            }

            CarritoProductoRequest CarritoProdRequest = new CarritoProductoRequest
            {
                CarritoId = Carrito.CarritoId,
                ProductoId = Producto.ProductoId,
                Cantidad = carritoRequest.amount,
            };
            CarritoProducto CarritoProducto = await _servicesCarritoProducto.CreateCarritoProducto(CarritoProdRequest);
            Carrito.CarritoProductos.Add(CarritoProducto);
            await _command.UpdateCarrito(Carrito);
            CarritoResponse CarritoResponse = new CarritoResponse
            {
                clientId = carritoRequest.clientId,
                productId = carritoRequest.productId,
                amount = carritoRequest.amount,
            };
            return CarritoResponse;
        }

        public async Task<Carrito> GetCarritoCliente(int idCliente)
        {
            Cliente Cliente = await _queryCliente.GetCliente(idCliente);
            List<Carrito> ListaCarritos = await _query.GetListCarritos();

            if (Cliente == null)
                return null;

            foreach (Carrito carrito in Cliente.Carritos)
            {
                if (carrito.Estado)
                    return await _query.GetCarrito(carrito.CarritoId);
            }
            return null;
        }

        public async Task<Carrito> CreateCarrito(int idCliente)
        {
            var Carrito = new Carrito
            {
                CarritoId = Guid.NewGuid(),
                Estado = true,
                ClienteId = idCliente,
            };
            await _command.InsertCarrito(Carrito);
            return Carrito;
        }

        public async Task<CarritoResponse> ModifyCarrito(CarritoRequest carritoRequest)
        {
            Carrito Carrito = await GetCarritoCliente(carritoRequest.clientId);
            List<CarritoProducto> ListaCarritoProductos = await _servicesCarritoProducto.GetAllCarritoProductos();
            CarritoResponse CarritoResponse;

            if (Carrito == null)
                return null;

            foreach (CarritoProducto carritoProd in Carrito.CarritoProductos)
            {
                if (carritoProd.ProductoId == carritoRequest.productId)
                {
                    carritoProd.Cantidad = carritoRequest.amount;
                    await _servicesCarritoProducto.UpdateCarritoProducto(carritoProd);
                    CarritoResponse = new CarritoResponse
                    {
                        clientId = carritoRequest.clientId,
                        productId = carritoRequest.productId,
                        amount = carritoRequest.amount
                    };
                    return CarritoResponse;
                }
            }
            return null;
        }

        public async Task<bool> DeleteProductoFromCarrito(int idCliente, int idProducto)
        {
            Carrito Carrito = await GetCarritoCliente(idCliente);
            List<CarritoProducto> CarritoProducto = await _servicesCarritoProducto.GetAllCarritoProductos();

            if (Carrito == null)
                return false;

            foreach (CarritoProducto carritoProd in Carrito.CarritoProductos)
            {
                if (carritoProd.ProductoId == idProducto)
                {
                    await _servicesCarritoProducto.DeleteCarritoProducto(carritoProd);
                    return true;
                }
            }
            return false;
        }

        public async Task<Carrito> UpdateCarrito(Carrito carrito)
        {
            await _command.UpdateCarrito(carrito);
            return carrito;
        }

        public async Task<Carrito> DeleteCarrito(Carrito carrito)
        {
            await _command.RemoveCarrito(carrito);
            return carrito;
        }

        public async Task<List<Carrito>> GetAllCarritos()
        {
            var Carritos = await _query.GetListCarritos();
            return Carritos;
        }

        public async Task<Carrito> GetCarritoById(Guid id)
        {
            var Carrito = await _query.GetCarrito(id);
            return Carrito;
        }
    }
}
