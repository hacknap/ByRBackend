using ByR.Entities;
using ByR.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByR.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUser
    {
        private readonly DataContext context;
        public UserRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<ActionResult<PageAndSortResponse<User>>> GetUsersPageAndSort([FromQuery] PageAndSortRequest param)
        {
            IEnumerable<User> listUser = null;
            if (param.Direction.ToLower() == "asc")
                listUser = await context.User.OrderBy(p => EF.Property<object>(p, param.Column)).Where(p => p.IsDelete.Equals(false)).ToListAsync();
            else if (param.Direction.ToLower() == "desc")
                listUser = await context.User.OrderByDescending(p => EF.Property<object>(p, param.Column)).Where(p => p.IsDelete.Equals(false)).ToListAsync();
            else
                listUser = await context.User.OrderBy(p => p.Id).Where(p => p.IsDelete.Equals(false)).ToListAsync();
            int total = 0;
            if (!string.IsNullOrEmpty(param.Filter))
            {
                listUser = listUser.Where(ele => ele.Name.Contains(param.Filter) || ele.Ci.Contains(param.Filter)).Where(p => p.IsDelete.Equals(false));
            }
            total = listUser.Count();
            listUser = listUser.Skip((param.Page - 1) * param.PageSize).Take(param.PageSize);

            var result = new PageAndSortResponse<User>
            {
                Data = listUser,
                TotalRows = total
            };

            return result;
        }
        public User GetUserLogin(string email, string password)
        {
            return context.User.FirstOrDefault(ele => ele.Email == email && ele.Password == password);
        }

        public string GetRoleUser(string id)
        {
            var role = context.RoleUser.Include(r => r.Role).FirstOrDefault(ru => ru.User.Id == id);

            return role.Role.Description; 
        }
        public Role GetRoleUserDescription(string description)
        {
            var role = context.Role.FirstOrDefault(r=>r.Description.Equals(description));
            return role;
        }

        public async void CreateRolUser(Role role, User user)
        {
            RoleUser roleUser = new RoleUser();
            roleUser.User =user;
            roleUser.Role = role;

            roleUser.IsDelete = false;
            roleUser.Register = DateTime.Now;
            
            await this.context.RoleUser.AddAsync(roleUser);
            try
            {

                await SaveAllAsync();
            }
            catch (Exception)
            {

              
            }


        }

        public User GerUserById(string id)
        {
            return context.User.Find(id);
        }
    }
}
