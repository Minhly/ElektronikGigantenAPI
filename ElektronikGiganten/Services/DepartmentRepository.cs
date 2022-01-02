using ElektronikGigantenLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElektronikGiganten.Services
{
    public class DepartmentRepository : GenericRepository<Department, ElektronikGigantenContext>, IDepartmentRepository
    {
        public DepartmentRepository(ElektronikGigantenContext dbcontext)
            : base(dbcontext)
        {

        }
    }
}