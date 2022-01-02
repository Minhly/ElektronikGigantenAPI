using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElektronikGiganten.Dtos
{
    public class OrderSaleDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public int OrderStatusId { get; set; }
    }
}
