using Microsoft.AspNetCore.Mvc;
using MultitenancyPostgres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultitenancyPostgres.DataLayer
{
    public interface IUserRepository
    {
        Task<LoginResponse> LoginUser(LoginRequest loginRequest);

        public void  AddUser(User user);

        public Task<List<User>> GetUser();


    }
}
