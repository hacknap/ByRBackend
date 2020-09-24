using ByR.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByR.Data.Repositories
{
    public interface IUser : IGeneric<User>
    {
       User GetUserLogin(string nameUser, string password);
       User GerUserById(string id);
    }
}
