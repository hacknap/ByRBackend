using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ByR.Data.Repositories;
using ByR.Entities;
using ByR.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ByR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUser _users;
        private readonly AppSettings _appSettings;
        public UsersController(IUser users, IOptions<AppSettings> appSettings)
        {
            this._users = users;
            _appSettings = appSettings.Value;
        }


        //obtener usuarios que no sean borrados
        // GET: api/Users

        [HttpGet]
        public IQueryable<User> GetUsers()
        {
            return this._users.GetAll().Where(c => c.IsDelete.Equals(false));
        }

        //Obtener usuario 
        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            return await _users.GetByIdAsync(id);
        }

        //Obtener si el usuario y password es correcto y darle un token 
        // GET: api/Users/Nombre/Clave
        [HttpGet("{nameUser}/{password}")]
        public ActionResult<User> GetUser(string nameUser, string password)
        {
            var user = _users.GetUserLogin(nameUser,password);
            var token = generateJwtToken(user);
            user.Token = token;
            return user;
        }

        //agregar un usuario
        // POST: api/User 
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {

           
            if (ModelState.IsValid)
            {
                user.IsDelete = false;
                user.Register = DateTime.Now;
                await _users.CreateAsync(user);
            }
            else {
                return BadRequest();
            }
               
            return user;
        }



        // PUT: api/User/5
        [HttpPut]
        public async Task<ActionResult<User>> PutProperty(User user)
        {
            if (ModelState.IsValid)
            {
                await _users.UpdateAsync(user);
            }
            else
            {
                return BadRequest();
            }

            return user;
        }



        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUsuario(string id)
        {
            var user = await _users.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _users.DeleteAsync(user);
          
            return user;
        }

      





















        /// metodos adiconales para el token
        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public User GetUserById(string id)
        {
            return _users.GerUserById(id);
        }



    }
}
