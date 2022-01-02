using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElektronikGiganten.Dtos
{
    public class ProductReviewDto
    {
        public int Id { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
        public int ProductId { get; set; }
    }
}
