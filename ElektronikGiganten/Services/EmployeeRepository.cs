using ElektronikGigantenLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElektronikGiganten.Services
{
    public class EmployeeRepository : GenericRepository<Employee, ElektronikGigantenContext>, IEmployeeRepository
    {
        public EmployeeRepository(ElektronikGigantenContext dbcontext)
            : base(dbcontext)
        {
        }
    }
}
