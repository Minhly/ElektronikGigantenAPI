using ElektronikGigantenLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElektronikGiganten.Services
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer> verifyLogin(LoginInfo customerLogin);
    }
}
