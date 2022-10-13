namespace Application.Response
{
    public class OrdenWithProductsResponse
    {
        public Guid OrdenId { get; set; }
        public ClienteResponse ClienteResponse { get; set; }
        public List<ProductoResponse> ProductosResponse { get; set; }
        public DateTime OrdenFecha { get; set; }
        public decimal OrdenTotal { get; set; }
    }
}
