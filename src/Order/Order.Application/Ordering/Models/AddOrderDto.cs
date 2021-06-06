using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Ordering.Models
{
    public class AddOrderDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
