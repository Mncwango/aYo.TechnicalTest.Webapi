using aYo.TechnicalTest.Data.UnitofWork;
using aYo.TechnicalTest.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace aYo.TechnicalTest.Services
{
    public class UserService : IUserService
    {
        protected IUnitOfWork _unitOfWork;
        
        public UserService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task CreateUser(ApplicationUser applicationUser)
        {
            
           await _unitOfWork.GetRepository<ApplicationUser>().Insert(applicationUser);
           await _unitOfWork.SaveAsync();
        }

        public async Task<ApplicationUser> GetUser(string email, string password)
        {
            return await _unitOfWork.GetRepository<ApplicationUser>().GetOne(u => u.Email == email && u.Password == password);
        }
    }
}