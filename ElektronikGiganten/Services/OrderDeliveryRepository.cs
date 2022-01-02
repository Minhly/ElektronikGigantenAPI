using ElektronikGigantenLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElektronikGiganten.Services
{
    public class OrderDeliveryRepository : GenericRepository<OrderDelivery, ElektronikGigantenContext>, IOrderDeliveryRepository
    {
        public OrderDeliveryRepository(ElektronikGigantenContext dbcontext)
            : base(dbcontext)
        {

        }
    }
}