using Microsoft.Extensions.Configuration;
using PucMinas.TCC.Domain.Models;
using PucMinas.TCC.Domain.Repositories;
using PucMinas.TCC.Persistence.SqlServer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PucMinas.TCC.Authentication.Persistences.SqlServer
{
    public class UserRepository : SqlServerRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration)
           : base(configuration.GetConnectionString("SqlConnection"))
        {
        }

        public Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> Find(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserModel>> FindAll()
        {
            throw new NotImplementedException();
        }

        public async Task<UserModel> FindByUserId(string userId)
        {
            //TODO: Dado fictício para POC
            IList<UserModel> lstUser = new List<UserModel>
            {
                new UserModel
                {
                    UserId = "Teste",
                    Password = "T3st3123456!!",
                    Role = "Admin"
                },
                new UserModel
                {
                    UserId = "Teste2",
                    Password = "T3st3987654!!",
                    Role = "Other"
                }
            };
            return lstUser.FirstOrDefault(f => f.UserId == userId);
        }

        public Task Save(UserModel model)
        {
            throw new NotImplementedException();
        }

        public Task Update(UserModel model)
        {
            throw new NotImplementedException();
        }
    }
}
