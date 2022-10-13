namespace Application.Response
{
    public class OrdenResponse
    {
        public Guid OrdenId { get; set; }
        public Guid CarritoId { get; set; }
        public DateTime OrdenFecha { get; set; }
        public decimal OrdenTotal { get; set; }
    }
}
