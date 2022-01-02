using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElektronikGiganten.Dtos
{
    public class CreditCardDto
    {
        public int Id { get; set; }
        public int CardNumer { get; set; }
        public int CustomerId { get; set; }
    }
}
