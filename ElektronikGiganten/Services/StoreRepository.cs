using ElektronikGigantenLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElektronikGiganten.Services
{
    public class StoreRepository : GenericRepository<Store, ElektronikGigantenContext>, IStoreRepository
    {
        public StoreRepository(ElektronikGigantenContext dbcontext)
            : base(dbcontext)
        {

        }
    }
}

