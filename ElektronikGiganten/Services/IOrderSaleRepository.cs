using ElektronikGigantenLibrary.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElektronikGiganten.Services
{
    public interface IOrderSaleRepository : IGenericRepository<OrderSale>
    {
        Task<ICollection> GetAllSales();
    }
}
