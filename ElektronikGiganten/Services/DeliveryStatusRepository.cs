using ElektronikGigantenLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElektronikGiganten.Services
{
    public class DeliveryStatusRepository : GenericRepository<DeliveryStatus, ElektronikGigantenContext>, IDeliveryStatusRepository
    {
        public DeliveryStatusRepository(ElektronikGigantenContext dbcontext)
            : base(dbcontext)
        {

        }
    }
}