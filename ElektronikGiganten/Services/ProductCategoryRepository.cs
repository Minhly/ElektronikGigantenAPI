using ElektronikGigantenLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElektronikGiganten.Services
{
    public class ProductCategoryRepository : GenericRepository<ProductCategory, ElektronikGigantenContext>, IProductCategoryRepository
    {
        public ProductCategoryRepository(ElektronikGigantenContext dbcontext)
            : base(dbcontext)
        {

        }
    }
}
