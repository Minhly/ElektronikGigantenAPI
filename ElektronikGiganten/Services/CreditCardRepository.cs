using ElektronikGigantenLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElektronikGiganten.Services
{
    public class CreditCardRepository : GenericRepository<CreditCard, ElektronikGigantenContext>, ICreditCardRepository
    {
        public CreditCardRepository(ElektronikGigantenContext dbcontext)
            : base(dbcontext)
        {

        }
    }
}