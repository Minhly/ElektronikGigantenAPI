using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElektronikGiganten.Dtos
{
    public class OrderDeliveryDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string Address { get; set; }
        public int DeliveryStatusId { get; set; }
        public DateTime DateDelivered { get; set; }
        public int Postal { get; set; }
    }
}
