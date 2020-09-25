using ByR.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ByR.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUser
    {
        private readonly DataContext context;
        public UserRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public User GerUserById(string id)
        {
            return context.User.FirstOrDefault(x => x.Id == id);
        }

        public User GetUserLogin(string nameUser, string password)
        {
            return context.User.FirstOrDefault(ele => ele.Name == nameUser && ele.Password == password);

        }

    }
}
