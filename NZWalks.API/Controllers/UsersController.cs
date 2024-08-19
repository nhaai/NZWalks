using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;
using X.PagedList;
using static Azure.Core.HttpHeader;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly ILogger<UsersController> logger;

        public UsersController(UserManager<User> userManager, IUserRepository userRepository, IMapper mapper, ILogger<UsersController> logger)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] SearchFilterDto searchFilterDto)
        {
            var searchDomainModel = mapper.Map<SearchFilter>(searchFilterDto);
            (var usersDomainModel, int totalItemCount) = await userRepository.GetAllAsync(searchDomainModel);

            var usersDto = mapper.Map<List<UserDto>>(usersDomainModel);
            var usersPagedList = new StaticPagedList<UserDto>(usersDto, searchFilterDto.PageNumber, searchFilterDto.PageSize, totalItemCount);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(usersPagedList.GetMetaData()));

            return Ok(usersPagedList);
        }

        [HttpGet("{id:Guid}")]
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
        public async Task<IActionResult> Create([FromBody] AddUserRequestDto addUserRequestDto)
        {
            var userDomainModel = mapper.Map<User>(addUserRequestDto);
            var identityResult = await userManager.CreateAsync(userDomainModel, addUserRequestDto.Password);

            if (identityResult.Succeeded)
            {
                if (!string.IsNullOrEmpty(addUserRequestDto.Role))
                {
                    await userManager.AddToRoleAsync(userDomainModel, addUserRequestDto.Role);

                    if (identityResult.Succeeded)
                    {
                        var userDto = mapper.Map<UserDto>(userDomainModel);

                        return CreatedAtAction(nameof(GetById), new { id = userDto.Id }, userDto);
                    }
                }
            }

            return Ok(addUserRequestDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateUserRequestDto updateUserRequestDto)
        {
            var userDomainModel = await userManager.FindByIdAsync(id.ToString());
            userDomainModel.FullName = updateUserRequestDto.FullName;
            userDomainModel.AddressLine1 = updateUserRequestDto.AddressLine1;
            userDomainModel.AddressLine2 = updateUserRequestDto.AddressLine2;
            userDomainModel.City = updateUserRequestDto.City;
            userDomainModel.PostalCode = updateUserRequestDto.PostalCode;
            userDomainModel.Country = updateUserRequestDto.Country;
            userDomainModel.AvatarUrl = updateUserRequestDto.AvatarUrl;
            userDomainModel.IsActive = updateUserRequestDto.IsActive;
            userDomainModel.Notes = updateUserRequestDto.Notes;
            userDomainModel.PhoneNumber = updateUserRequestDto.PhoneNumber;

            var identityResult = await userManager.UpdateAsync(userDomainModel);

            if (identityResult.Succeeded)
            {
                if (!string.IsNullOrEmpty(updateUserRequestDto.Role))
                {
                    var roles = await userManager.GetRolesAsync(userDomainModel);
                    await userManager.RemoveFromRolesAsync(userDomainModel, roles);
                    await userManager.AddToRoleAsync(userDomainModel, updateUserRequestDto.Role);

                    if (identityResult.Succeeded)
                    {
                        var userDto = mapper.Map<UserDto>(userDomainModel);

                        return Ok(userDto);
                    }
                }
            }

            return Ok(updateUserRequestDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
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
