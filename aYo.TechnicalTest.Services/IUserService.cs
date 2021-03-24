using aYo.TechnicalTest.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace aYo.TechnicalTest.Services
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUser(string email, string password);
        Task CreateUser(ApplicationUser applicationUser);
    }
}
