using ElektronikGigantenLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElektronikGiganten.Services
{
    public class ProductRepository : GenericRepository<Product, ElektronikGigantenContext>, IProductRepository
    {
        public ProductRepository(ElektronikGigantenContext dbcontext)
            : base(dbcontext)
        {

        }
    }
}
