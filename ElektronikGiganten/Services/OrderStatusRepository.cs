using ElektronikGigantenLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElektronikGiganten.Services
{
    public class OrderStatusRepository : GenericRepository<OrderStatus, ElektronikGigantenContext>, IOrderStatusRepository
    {
        public OrderStatusRepository(ElektronikGigantenContext dbcontext)
            : base(dbcontext)
        {

        }
    }
}