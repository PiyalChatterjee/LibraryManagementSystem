using LMS.API.Data;
using LMS.API.Models.Domain;
using LMS.API.Models.Enums;
using LMS.API.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly LMSDbContext dbContext;

        public UsersController(LMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //Get all users
        // GET: api/users
        [HttpGet]
        public IActionResult GetAll()
        {
            // Data extraction section
            var userEntities = dbContext.Users.ToList();

            // Data mapping section
            var userDTOList = new List<UserDTO>();
            foreach (var user in userEntities)
            {
                var userDTO = new UserDTO
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = user.Role.ToString(),
                    Status = user.Status.ToString(),
                    DateCreated = user.DateCreated,
                    LastLogin = user.LastLogin
                };
                userDTOList.Add(userDTO);
            }
            // Return the list of users with a 200 OK response
            return Ok(userDTOList);
        }

        //Get user by id
        // GET: api/users/{id}
        [HttpGet("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            // Data extraction section
            // Using Find method because it is optimized for primary key lookups and 
            // it can return the entity from the context cache if it was previously loaded.
            var userEntity = dbContext.Users.Find(id);

            // If the user is not found, return a 404 Not Found response
            if (userEntity == null)
            {
                return NotFound();
            }

            // Data mapping section
            var userDTO = new UserDTO
            {
                UserId = userEntity.UserId,
                Username = userEntity.Username,
                Email = userEntity.Email,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Role = userEntity.Role.ToString(),
                Status = userEntity.Status.ToString(),
                DateCreated = userEntity.DateCreated,
                LastLogin = userEntity.LastLogin
            };
            // Return the user with a 200 OK response
            return Ok(userDTO);
        }

        //Create user
        // POST: api/users
        [HttpPost]
        public IActionResult Create([FromBody] AddUserRequestDTO addUserRequest)
        {
            // Data mapping section
            var userEntity = new User
            {
                UserId = Guid.NewGuid(),
                Username = addUserRequest.Username,
                Password = addUserRequest.Password, // In a real application, you should hash the password
                Email = addUserRequest.Email,
                FirstName = addUserRequest.FirstName,
                LastName = addUserRequest.LastName,
                Role = Enum.Parse<UserRoles>(addUserRequest.Role),
                Status = Enum.Parse<UserStatus>(addUserRequest.Status),
                DateCreated = DateTime.UtcNow,
                LastLogin = null
            };
            // Data insertion section
            dbContext.Users.Add(userEntity);
            dbContext.SaveChanges();

            // Data mapping section for the response
            var userDTO = new UserDTO
            {
                UserId = userEntity.UserId,
                Username = userEntity.Username,
                Email = userEntity.Email,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Role = userEntity.Role.ToString(),
                Status = userEntity.Status.ToString(),
                DateCreated = userEntity.DateCreated,
                LastLogin = userEntity.LastLogin
            };
            // Return the created user with a 201 Created response
            return CreatedAtAction(nameof(GetById), new { id = userDTO.UserId }, userDTO);
        }
        //Update user
        // PUT: api/users/{id}
        [HttpPut("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateUserRequestDTO updateUserRequest)
        {
            // Data extraction section
            var userEntity = dbContext.Users.Find(id);
            // If the user is not found, return a 404 Not Found response
            if (userEntity == null)
            {
                return NotFound();
            }
            // Data mapping section
            userEntity.Username = updateUserRequest.Username;
            userEntity.Email = updateUserRequest.Email;
            userEntity.Password = updateUserRequest.Password ?? userEntity.Password; // In a real application, you should hash the password
            userEntity.FirstName = updateUserRequest.FirstName;
            userEntity.LastName = updateUserRequest.LastName;
            userEntity.Role = Enum.Parse<UserRoles>(updateUserRequest.Role);
            userEntity.Status = Enum.Parse<UserStatus>(updateUserRequest.Status);
            userEntity.LastLogin = DateTime.UtcNow; // Update last login time
            // Data update section
            dbContext.SaveChanges();
            // Data mapping section for the response
            var userDTO = new UserDTO
            {
                UserId = userEntity.UserId,
                Username = userEntity.Username,
                Email = userEntity.Email,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Role = userEntity.Role.ToString(),
                Status = userEntity.Status.ToString(),
                DateCreated = userEntity.DateCreated,
                LastLogin = userEntity.LastLogin
            };
            // Return the updated user with a 200 OK response
            return Ok(userDTO);
        }

        //Delete user
        // DELETE: api/users/{id}
        [HttpDelete("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            // Data extraction section
            var userEntity = dbContext.Users.Find(id);
            // If the user is not found, return a 404 Not Found response
            if (userEntity == null)
            {
                return NotFound();
            }
            // Data deletion section
            dbContext.Users.Remove(userEntity);
            dbContext.SaveChanges();
            // Return a 204 No Content response
            return NoContent();
        }
    }
}
