using Application.Interfaces.ICarrito;
using Application.Interfaces.ICarritoProducto;
using Application.Interfaces.ICliente;
using Application.Interfaces.IOrden;
using Application.Interfaces.IProducto;
using Application.Models;
using Application.Response;
using Domain.Entities;

namespace Application.UseCase
{
    public class OrdenServices : IOrdenServices
    {
        private readonly IOrdenCommand _command;
        private readonly IOrdenQuery _query;
        private readonly ICarritoServices _servicesCarrito;
        private readonly IClienteServices _servicesCliente;
        private readonly ICarritoProductoQuery _queryCarritoProducto;
        private readonly IProductoQuery _queryProducto;

        public OrdenServices(IOrdenCommand command, IOrdenQuery query, ICarritoServices servicesCarrito, IClienteServices servicesCliente, ICarritoProductoQuery queryCarritoProducto, IProductoQuery queryProducto)
        {
            _command = command;
            _query = query;
            _servicesCarrito = servicesCarrito;
            _servicesCliente = servicesCliente;
            _queryCarritoProducto = queryCarritoProducto;
            _queryProducto = queryProducto;
        }

        public async Task<BalanceResponse> CreateBalance(DateTime? from, DateTime? to)
        {
            List<CarritoProducto> CarritosProd = await _queryCarritoProducto.GetListCarritoProductos();
            List<Orden> Ordenes = await GetAllOrdenes();
            if (Ordenes == null)
                return null;

            var OrdenesPorFecha = from Orden in Ordenes
                                  where Orden.Fecha >= ((DateTime)@from).Date && Orden.Fecha <= ((DateTime)@to).Date.AddDays(1)
                                  select Orden;
            if (from == null && to == null)
            {
                OrdenesPorFecha = Ordenes;
            }
            else if (from == null && to != null)
            {
                OrdenesPorFecha = from Orden in Ordenes
                                  where Orden.Fecha <= ((DateTime)@to).Date.AddDays(1)
                                  select Orden;
            }
            else if (from != null && to == null)
            {
                OrdenesPorFecha = from Orden in Ordenes
                                  where Orden.Fecha >= ((DateTime)@from).Date
                                  select Orden;
            }

            if (OrdenesPorFecha.Count() == 0)
                return null;

            List<OrdenWithProductsResponse> OrdenesWithProductsResponse = new List<OrdenWithProductsResponse>();
            decimal Recaudacion = 0;

            foreach (Orden orden in OrdenesPorFecha)
            {
                List<ProductoResponse> Productos = new List<ProductoResponse>();
                Carrito Carrito = await _servicesCarrito.GetCarritoById(orden.CarritoId);
                OrdenWithProductsResponse OrdenWithProductResponse = await GenerateOrdenWithProducts(Carrito, orden);
                OrdenesWithProductsResponse.Add(OrdenWithProductResponse);
                Recaudacion += orden.Total;
            }

            BalanceResponse Balance = new BalanceResponse
            {
                Recaudacion = Recaudacion,
                Ordenes = OrdenesWithProductsResponse
            };

            return Balance;
        }

        public async Task<OrdenWithProductsResponse> CreateOrden(OrdenRequest request)
        {
            Carrito Carrito = await _servicesCarrito.GetCarritoCliente(request.ClientId);
            if (Carrito == null)
                return null;

            var Orden = new Orden
            {
                OrdenId = Guid.NewGuid(),
                CarritoId = Carrito.CarritoId,
                Fecha = DateTime.Now,
                Total = await GetTotalCarrito(request.ClientId),
            };
            if (Orden.Total == 0)
                return null;

            await _command.InsertOrden(Orden);
            await _servicesCliente.NewCarritoActive(request.ClientId);

            OrdenWithProductsResponse OrdenWithProductsResponse = await GenerateOrdenWithProducts(Carrito, Orden);
            return OrdenWithProductsResponse;
        }

        public async Task<decimal> GetTotalCarrito(int clientId)
        {
            Carrito Carrito = await _servicesCarrito.GetCarritoCliente(clientId);
            List<CarritoProducto> ListCarritoProd = await _queryCarritoProducto.GetListCarritoProductos();
            decimal Total = 0;
            foreach (CarritoProducto carritoProd in Carrito.CarritoProductos)
            {
                Producto Prod = await _queryProducto.GetProducto(carritoProd.ProductoId);
                Total += Prod.Precio * carritoProd.Cantidad;
            }
            return Total;
        }

        public async Task<Orden> UpdateOrden(Orden orden)
        {
            await _command.UpdateOrden(orden);
            return orden;
        }

        public async Task<Orden> DeleteOrden(Orden orden)
        {
            await _command.RemoveOrden(orden);
            return orden;
        }

        public async Task<List<Orden>> GetAllOrdenes()
        {
            var Ordenes = await _query.GetListOrdenes();
            return Ordenes;
        }

        public async Task<Orden> GetOrdenById(Guid id)
        {
            var Orden = await _query.GetOrden(id);
            return Orden;
        }

        public async Task<OrdenWithProductsResponse> GenerateOrdenWithProducts(Carrito carrito, Orden orden)
        {
            Cliente Cliente = await _servicesCliente.GetClienteById(carrito.ClienteId);
            ClienteResponse ClienteResponse = new ClienteResponse
            {
                ClienteId = Cliente.ClienteId,
                dni = Cliente.DNI,
                name = Cliente.Nombre,
                lastname = Cliente.Apellido,
                address = Cliente.Direccion,
                phoneNumber = Cliente.Telefono
            };
            List<ProductoResponse> Productos = new List<ProductoResponse>();

            foreach (CarritoProducto carritoProd in carrito.CarritoProductos)
            {
                Producto Prod = await _queryProducto.GetProducto(carritoProd.ProductoId);
                ProductoResponse ProdResponse = new ProductoResponse
                {
                    ProductoId = Prod.ProductoId,
                    ProductoNombre = Prod.Nombre,
                    ProductoDescripcion = Prod.Descripcion,
                    ProductoMarca = Prod.Marca,
                    ProductoCodigo = Prod.Codigo,
                    ProductoPrecio = Prod.Precio,
                    ProductoImage = Prod.Image
                };
                Productos.Add(ProdResponse);
            }
            OrdenWithProductsResponse OrdenWithProductResponse = new OrdenWithProductsResponse
            {
                OrdenId = orden.OrdenId,
                ClienteResponse = ClienteResponse,
                ProductosResponse = Productos,
                OrdenFecha = orden.Fecha,
                OrdenTotal = orden.Total
            };
            return OrdenWithProductResponse;
        }
    }
}
