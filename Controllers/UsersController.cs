using System.Threading.Tasks;
using DatingApp.api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using DatingApp.api.DTOS;
using System.Collections.Generic;

namespace DatingApp.api.Controllers
{
   
    [Route("api/[Controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        public UsersController(IDatingRepository repo,IMapper mapper)
        {
            _repo= repo;
            _mapper= mapper;
        }

        [HttpGet]

        public async Task<IActionResult> GetUsers()
        {
            var users= await _repo.GetUsers();
            var userToReturn=_mapper.Map<IEnumerable<UserForListsDTO>>(users);
            return Ok(userToReturn);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetUser(int id)
        {
            var user= await _repo.GetUser(id);
            var userToReturn=_mapper.Map<UserForDetailedDTO>(user);
            return Ok(userToReturn);
        }
    }
}