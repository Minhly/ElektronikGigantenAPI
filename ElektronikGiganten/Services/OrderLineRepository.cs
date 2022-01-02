using ElektronikGigantenLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElektronikGiganten.Services
{
    public class OrderLineRepository : GenericRepository<OrderLine, ElektronikGigantenContext>, IOrderLineRepository
    {
        public OrderLineRepository(ElektronikGigantenContext dbcontext)
            : base(dbcontext)
        {

        }
    }
}