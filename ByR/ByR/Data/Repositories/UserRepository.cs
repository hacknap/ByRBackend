using ByR.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
