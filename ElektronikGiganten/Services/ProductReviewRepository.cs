using ElektronikGigantenLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElektronikGiganten.Services
{
    public class ProductReviewRepository : GenericRepository<ProductReview, ElektronikGigantenContext>, IProductReviewRepository
    {
        public ProductReviewRepository(ElektronikGigantenContext dbcontext)
            : base(dbcontext)
        {

        }
    }
}
