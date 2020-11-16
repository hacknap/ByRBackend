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
        private readonly IRolUser _roluser;
        private readonly AppSettings _appSettings;
        public UsersController(IUser users, IRolUser rolUser, IOptions<AppSettings> appSettings)
        {
            this._users = users;
            _appSettings = appSettings.Value;
            this._roluser = rolUser;
        }


        //obtener usuarios que no sean borrados
        // GET: api/Users

        [Authorize]
        [HttpGet]
        public IQueryable<User> GetUsers()
        {
            return this._users.GetAll().Where(c => c.IsDelete.Equals(false));
        }

        //Obtener usuario 
        // GET: api/Users/5
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(string id)
        {

            var user= _users.GerUserById(id);
            return user;
        }


        //Obtener si el usuario y password es correcto y darle un token 
        // GET: api/Users/Nombre/Clave
        [HttpGet("{nameUser}/{password}")]
        public ActionResult<User> GetUser(string nameUser, string password)
        {
            User user = new User();

            if (nameUser == null && password == null)
            {
                return BadRequest();

            }
            user = _users.GetUserLogin(nameUser, password);
            if (user == null)
            {
                return NotFound();
            }
            var token = generateJwtToken(user);
            user.Token = token;
            var roleDescription = _users.GetRoleUser(user.Id);
            user.Role = roleDescription;


            return user;
        }

        //agregar un usuario
        // POST: api/User 
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {


            if (ModelState.IsValid && user.Role != null)
            {

                var rol = _users.GetRoleUserDescription(user.Role);

                if (rol == null)
                {
                    return NotFound();
                }

                user.IsDelete = false;
                user.Register = DateTime.Now;

                await _users.CreateAsync(user);

                var roluser = new RoleUser()
                {
                    IsDelete = false,
                    Register = DateTime.Now,
                    Role = rol,
                    User = user
                };

                await this._roluser.CreateAsync(roluser);



            }
            else
            {
                return BadRequest();
            }

            return user;
        }



        // PUT: api/User/5
        [HttpPut]
        public async Task<ActionResult<User>> PutUser(User user)
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


        [Authorize]
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





    }
}
