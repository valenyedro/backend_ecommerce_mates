using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class CarritoRequest
    {
        public int clientId { get; set; }
        public int productId { get; set; }
        public int amount { get; set; }
    }
}
