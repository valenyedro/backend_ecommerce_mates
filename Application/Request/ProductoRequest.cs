namespace Application.Models
{
    public class ProductoRequest
    {
        public string ProductoNombre { get; set; }
        public string ProductoDescripcion { get; set; }
        public string ProductoMarca { get; set; }
        public string ProductoCodigo { get; set; }
        public decimal ProductoPrecio { get; set; }
        public string ProductoImage { get; set; }
    }
}
