using AutoMapper;
using LMS.API.Data;
using LMS.API.Models.Domain;
using LMS.API.Models.DTOs;
using LMS.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly LMSDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IMemberRepository memberRepository;

        public MembersController(LMSDbContext dbContext, IMapper mapper, IMemberRepository memberRepository)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.memberRepository = memberRepository;
        }
        //Get all members
        // GET: api/members
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var memberEntities = await memberRepository.GetAllAsync();
            return Ok(mapper.Map<List<MemberDTO>>(memberEntities));
        }
        //Create member
        // POST: api/members
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AddMemberRequestDTO memberRequestDTO)
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (memberRequestDTO == null)
            {
                return BadRequest("Member cannot be null");
            }
            var memberEntity = mapper.Map<Member>(memberRequestDTO);
            memberEntity = await memberRepository.CreateAsync(memberEntity);

            // If the member is not created, return a 400 Bad Request response
            if (memberEntity == null)
            {
                return BadRequest("Member could not be created.");
            }
            // Return the created member with a 201 Created response
            var memberDTO = mapper.Map<MemberDTO>(memberEntity);
            return CreatedAtRoute("GetByIdAsync", new { id = memberDTO.MemberId }, memberDTO);
        }

        //Get member by id
        // GET: api/members/{id}
        [HttpGet("{id:Guid}", Name = "GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var memberEntity = await memberRepository.GetByIdAsync(id);
            if (memberEntity == null)
            {
                return NotFound();
            }
            // Data mapping section
            // Return the member with a 200 OK response
            return Ok(mapper.Map<MemberDTO>(memberEntity));
        }

        //Update member
        // PUT: api/members/{id}
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateMemberRequestDTO updateMemberRequestDTO)
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the member is null
            if (updateMemberRequestDTO == null)
            {
                return BadRequest("Member cannot be null");
            }

            //Map the updateMemberRequestDTO to the Member entity
            var memberEntity = mapper.Map<Member>(updateMemberRequestDTO);
            var updatedMember = await memberRepository.UpdateMemberAsync(id, memberEntity);
            // If the member is not found, return a 404 Not Found response
            if (updatedMember == null)
            {
                return NotFound();
            }
            // Data mapping section
            // Return the updated member with a 200 OK response
            return Ok(mapper.Map<MemberDTO>(updatedMember));
        }

        //Delete member
        // DELETE: api/members/{id}
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var memberEntity = await memberRepository.GetByIdAsync(id);
            if (memberEntity == null)
            {
                return NotFound();
            }
            // Data extraction section
            var isDeleted = await memberRepository.DeleteMemberAsync(id);
            if (!isDeleted)
            {
                return BadRequest("Member could not be deleted.");
            }
            // Return a 204 No Content response
            return NoContent();
        }
    }
}
