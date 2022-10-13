namespace Application.Response
{
    public class BalanceResponse
    {
        public decimal Recaudacion { get; set; }
        public List<OrdenWithProductsResponse> Ordenes { get; set; }
        
    }
}
