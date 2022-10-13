namespace Application.Models
{
    public class CarritoProductoRequest
    {
        public Guid CarritoId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
    }
}
