using LMS.API.Data;
using LMS.API.Models.Domain;
using LMS.API.Models.Enums;
using LMS.API.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMS.API.Repositories;
using AutoMapper;
using LMS.API.CustomActionFilters;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly LMSDbContext dbContext;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UsersController(LMSDbContext dbContext, IUserRepository userRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        //Get all users
        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            // Data extraction section
            var userEntities = await userRepository.GetAllAsync();

            // Data mapping section
            // Return the list of users with a 200 OK response
            return Ok(mapper.Map<List<UserDTO>>(userEntities));
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
            // Return the user with a 200 OK response
            return Ok(mapper.Map<UserDTO>(userEntity));
        }


        //Create user
        // POST: api/users
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddUserRequestDTO addUserRequest)
        {
            var userEntity = mapper.Map<User>(addUserRequest);
            userEntity.DateCreated = DateTime.UtcNow; // Assign DateCreated as DateTime.Now

            userEntity = await userRepository.CreateUserAsync(userEntity);

            // If the user is not created, return a 400 Bad Request response
            if (userEntity == null)
            {
                return BadRequest("User could not be created.");
            }
            // Data mapping section for the response
            var userDTO = mapper.Map<UserDTO>(userEntity);

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
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateUserRequestDTO updateUserRequest)
        {
            //Map DTO to domain model
            var userEntity = mapper.Map<User>(updateUserRequest);
            // Data extraction section
            userEntity = await userRepository.UpdateUserAsync(id, userEntity);
            // If the user is not found, return a 404 Not Found response
            if (userEntity == null)
            {
                return NotFound();
            }
            userEntity.LastLogin = DateTime.UtcNow; // Update last login time
            // Data mapping section for the response
            var userDTO = mapper.Map<UserDTO>(userEntity);
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
