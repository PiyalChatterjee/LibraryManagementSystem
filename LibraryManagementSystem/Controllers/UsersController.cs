using LMS.API.Data;
using LMS.API.Models.Domain;
using LMS.API.Models.Enums;
using LMS.API.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMS.API.Repositories;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly LMSDbContext dbContext;
        private readonly IUserRepository userRepository;

        public UsersController(LMSDbContext dbContext, IUserRepository userRepository)
        {
            this.dbContext = dbContext;
            this.userRepository = userRepository;
        }
        //Get all users
        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            // Data extraction section
            var userEntities = await userRepository.GetAllAsync();

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
        [HttpGet("{id:Guid}", Name = "GetUserById")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var userEntity = await userRepository.GetByIdAsync(id);
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
        public async Task<IActionResult> Create([FromBody] AddUserRequestDTO addUserRequest)
        {
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

            userEntity = await userRepository.CreateUserAsync(userEntity);


            // If the user is not created, return a 400 Bad Request response
            if (userEntity == null)
            {
                return BadRequest("User could not be created.");
            }
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
            return CreatedAtRoute(
                routeName: "GetUserById",
                routeValues: new { id = userDTO.UserId },
                value: userDTO
            );
        }

        //Update user
        // PUT: api/users/{id}
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateUserRequestDTO updateUserRequest)
        {
            //Map DTO to domain model
            var userEntity = new User
            {
                UserId = id,
                Username = updateUserRequest.Username,
                Password = updateUserRequest.Password ?? "", // In a real application, you should hash the password
                Email = updateUserRequest.Email,
                FirstName = updateUserRequest.FirstName,
                LastName = updateUserRequest.LastName,
                Role = Enum.Parse<UserRoles>(updateUserRequest.Role),
                Status = Enum.Parse<UserStatus>(updateUserRequest.Status),
            };
            // Data extraction section
            userEntity = await userRepository.UpdateUserAsync(id, userEntity);
            // If the user is not found, return a 404 Not Found response
            if (userEntity == null)
            {
                return NotFound();
            }
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
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var isUserDeleted = await userRepository.DeleteUserAsync(id);
            // If the user is not found, return a 404 Not Found response
            if (!isUserDeleted)
            {
                return NotFound();
            }
            // Return a 204 No Content response
            return NoContent();
        }
    }
}
