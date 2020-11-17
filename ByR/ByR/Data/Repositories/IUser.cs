using ByR.Entities;
using ByR.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByR.Data.Repositories
{
    public interface IUser : IGeneric<User>
    {
       User GetUserLogin(string email, string password);
       
        
       User GerUserById(string id);


       string GetRoleUser(string id);

       Role GetRoleUserDescription(string description);

       public void CreateRolUser(Role rolId, User userId);
       Task<ActionResult<PageAndSortResponse<User>>> GetUsersPageAndSort([FromQuery] PageAndSortRequest param);
    }
}
