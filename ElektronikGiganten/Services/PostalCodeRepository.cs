using ElektronikGigantenLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElektronikGiganten.Services
{
    public class PostalCodeRepository : GenericRepository<PostalCode, ElektronikGigantenContext>, IPostalCodeRepository
    {
        public PostalCodeRepository(ElektronikGigantenContext dbcontext)
            : base(dbcontext)
        {

        }
    }
}
