using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using X.PagedList;
using X.PagedList.Extensions;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly ILogger<UsersController> logger;

        public UsersController(IUserRepository userRepository, IMapper mapper, ILogger<UsersController> logger)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        // [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll([FromQuery] SearchFilterDto searchFilterDto)
        {
            var searchDomainModel = mapper.Map<SearchFilter>(searchFilterDto);
            (var usersDomainModel, int total) = await userRepository.GetAllAsync(searchDomainModel);
            var usersDto = mapper.Map<List<UserDto>>(usersDomainModel);

            if (searchFilterDto.PageNumber != null)
            {
                return Ok(new StaticPagedList<UserDto>(usersDto, (int)searchFilterDto.PageNumber, searchFilterDto.PageSize, total));
            }

            return Ok(usersDto);
        }

        [HttpGet("{id:Guid}")]
        // [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var userDomainModel = await userRepository.GetByIdAsync(id.ToString());

            if (userDomainModel == null)
            {
                return NotFound();
            }

            var userDto = mapper.Map<UserDto>(userDomainModel);

            return Ok(userDto);
        }

        [HttpPost]
        [ValidateModel]
        // [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddUserRequestDto addUserRequestDto)
        {
            var userDomainModel = mapper.Map<User>(addUserRequestDto);
            userDomainModel = await userRepository.CreateAsync(userDomainModel);

            var userDto = mapper.Map<UserDto>(userDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = userDto.Id }, userDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        // [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateUserRequestDto updateUserRequestDto)
        {
            var userDomainModel = mapper.Map<User>(updateUserRequestDto);
            userDomainModel = await userRepository.UpdateAsync(id.ToString(), userDomainModel);

            if (userDomainModel == null)
            {
                return NotFound();
            }

            var userDto = mapper.Map<UserDto>(userDomainModel);

            return Ok(userDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        // [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var userDomainModel = await userRepository.DeleteAsync(id.ToString());

            if (userDomainModel == null)
            {
                return NotFound();
            }

            var userDto = mapper.Map<UserDto>(userDomainModel);

            return Ok(userDto);
        }
    }
}
