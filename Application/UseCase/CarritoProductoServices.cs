using Application.Interfaces.ICarritoProducto;
using Application.Models;
using Domain.Entities;

namespace Application.UseCase
{
    public class CarritoProductoServices : ICarritoProductoServices
    {
        private readonly ICarritoProductoCommand _command;
        private readonly ICarritoProductoQuery _query;

        public CarritoProductoServices(ICarritoProductoCommand command, ICarritoProductoQuery query)
        {
            _command = command;
            _query = query;
        }

        public async Task<CarritoProducto> CreateCarritoProducto(CarritoProductoRequest request)
        {
            var carritoProducto = new CarritoProducto
            {
                CarritoId = request.CarritoId,
                ProductoId = request.ProductoId,
                Cantidad = request.Cantidad,
            };
            await _command.InsertCarritoProducto(carritoProducto);
            return carritoProducto;
        }

        public async Task<CarritoProducto> UpdateCarritoProducto(CarritoProducto carritoProducto)
        {
            await _command.UpdateCarritoProducto(carritoProducto);
            return carritoProducto;
        }

        public async Task<CarritoProducto> DeleteCarritoProducto(CarritoProducto carritoProducto)
        {
            await _command.RemoveCarritoProducto(carritoProducto);
            return carritoProducto;
        }

        public async Task<List<CarritoProducto>> GetAllCarritoProductos()
        {
            var CarritoProductos = await _query.GetListCarritoProductos();
            return CarritoProductos;
        }

        public async Task<CarritoProducto> GetCarritoProductoById(Guid carritoId)
        {
            var CarritoProducto = await _query.GetCarritoProducto(carritoId);
            return CarritoProducto;
        }
    }
}
