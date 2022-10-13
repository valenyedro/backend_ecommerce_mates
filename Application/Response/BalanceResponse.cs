using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class BalanceResponse
    {
        public decimal Recaudacion { get; set; }
        public List<OrdenWithProductsResponse> Ordenes { get; set; }
        
    }
}
