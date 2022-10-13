using Application.Interfaces.IProducto;
using Application.Models;
using Application.Response;
using Domain.Entities;

namespace Application.UseCase
{
    public class ProductoServices : IProductoServices
    {
        private readonly IProductoCommand _command;
        private readonly IProductoQuery _query;

        public ProductoServices(IProductoCommand command, IProductoQuery query)
        {
            _command = command;
            _query = query;
        }

        public async Task<List<ProductoResponse>> GetProductosSort(ProductoRequest request)
        {
            List<Producto> ListaProductos = await _query.GetListProductos();
            List<Producto> ListaProductosSeleccionados = new List<Producto>();

            if (request.sort)
            {
                var ProductosOrdenados = from Producto in ListaProductos
                                         where Producto.Nombre.ToLower().Contains(request.name.ToLower())
                                         orderby Producto.Precio ascending
                                         select Producto;
                foreach (Producto prod in ProductosOrdenados)
                    ListaProductosSeleccionados.Add(prod);
            }
            else
            {
                var ProductosOrdenados = from Producto in ListaProductos
                                         where Producto.Nombre.ToLower().Contains(request.name.ToLower())
                                         orderby Producto.Precio descending
                                         select Producto;
                foreach (Producto prod in ProductosOrdenados)
                    ListaProductosSeleccionados.Add(prod);
            }

            List<ProductoResponse> ListaProdResponse = new List<ProductoResponse>();
            foreach (Producto prod in ListaProductosSeleccionados)
            {
                ProductoResponse Response = new ProductoResponse
                {
                    ProductoId = prod.ProductoId,
                    ProductoNombre = prod.Nombre,
                    ProductoDescripcion = prod.Descripcion,
                    ProductoMarca = prod.Marca,
                    ProductoCodigo = prod.Codigo,
                    ProductoPrecio = prod.Precio,
                    ProductoImage = prod.Image
                };
                ListaProdResponse.Add(Response);
            }
            return ListaProdResponse;
        }

        public async Task<List<ProductoResponse>> GetAllProductos()
        {
            var ProductoList = await _query.GetListProductos();
            List<ProductoResponse> Result = new List<ProductoResponse>();

            foreach (Producto producto in ProductoList)
            {
                var ProductoResponse = new ProductoResponse
                {
                    ProductoId = producto.ProductoId,
                    ProductoNombre = producto.Nombre,
                    ProductoDescripcion = producto.Descripcion,
                    ProductoMarca = producto.Marca,
                    ProductoCodigo = producto.Codigo,
                    ProductoPrecio = producto.Precio,
                    ProductoImage = producto.Image,
                };

                Result.Add(ProductoResponse);
            }
            return await Task.FromResult(Result);
        }

        public async Task<ProductoResponse> GetProductoById(int id)
        {
            var Producto = await _query.GetProducto(id);
            if (Producto == null)
                return null;

            var ProResponse = new ProductoResponse
            {
                ProductoId = Producto.ProductoId,
                ProductoNombre = Producto.Nombre,
                ProductoDescripcion = Producto.Descripcion,
                ProductoMarca = Producto.Marca,
                ProductoCodigo = Producto.Codigo,
                ProductoPrecio = Producto.Precio,
                ProductoImage = Producto.Image,
            };
            return ProResponse;
        }

        public async Task<Producto> UpdateProducto(Producto producto)
        {
            await _command.UpdateProducto(producto);
            return producto;
        }

        public async Task<Producto> DeleteProducto(Producto producto)
        {
            await _command.RemoveProducto(producto);
            return producto;
        }
    }
}
