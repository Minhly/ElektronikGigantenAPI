using ElektronikGigantenLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElektronikGiganten.Services
{
    public class CustomerRepository : GenericRepository<Customer, ElektronikGigantenContext>, ICustomerRepository
    {
        public CustomerRepository(ElektronikGigantenContext dbcontext)
            : base(dbcontext)
        {
            
        }

        public async Task<Customer> verifyLogin(LoginInfo customerLogin)
        {
            var customer = await _dbcontext.Customers.Where(c => c.Email == customerLogin.Email && c.Password == customerLogin.Password).FirstOrDefaultAsync();
                return customer;
        }
    }
}
