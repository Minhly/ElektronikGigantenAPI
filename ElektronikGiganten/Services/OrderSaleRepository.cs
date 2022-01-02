using ElektronikGigantenLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElektronikGiganten.Services
{
    public class OrderSaleRepository : GenericRepository<OrderSale, ElektronikGigantenContext>, IOrderSaleRepository
    {
        public OrderSaleRepository(ElektronikGigantenContext dbcontext)
            : base(dbcontext)
        {

        }

        public async Task<ICollection> GetAllSales()
        {
            return await _dbcontext.OrderSales
            .Include(x => x.OrderLines)
            .ThenInclude(o => o.Product)
            .Include(x => x.Store)
            .Include(x => x.Orderstatus)
            .Include(x => x.OrderDeliveries)
            .Include(x => x.Customer)
            .AsSplitQuery()
            .ToListAsync();
        }
    }
}