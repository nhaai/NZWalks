using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartRepository cartRepository;
        private readonly IMapper mapper;
        private readonly ILogger<CartsController> logger;

        public CartsController(ICartRepository cartRepository, IMapper mapper, ILogger<CartsController> logger)
        {
            this.cartRepository = cartRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cartsDomainModel = await cartRepository.GetAllAsync();
            var cartsDto = mapper.Map<List<CartDto>>(cartsDomainModel);

            return Ok(cartsDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var cartDomainModel = await cartRepository.GetByIdAsync(id);

            if (cartDomainModel == null)
            {
                return NotFound();
            }

            var cartDto = mapper.Map<CartDto>(cartDomainModel);

            return Ok(cartDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddCartRequestDto addCartRequestDto)
        {
            var cartDomainModel = mapper.Map<Cart>(addCartRequestDto);
            cartDomainModel = await cartRepository.CreateAsync(cartDomainModel);

            var cartDto = mapper.Map<CartDto>(cartDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = cartDto.Id }, cartDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCartRequestDto updateCartRequestDto)
        {
            var cartDomainModel = mapper.Map<Cart>(updateCartRequestDto);
            cartDomainModel = await cartRepository.UpdateAsync(id, cartDomainModel);

            if (cartDomainModel == null)
            {
                return NotFound();
            }

            var cartDto = mapper.Map<CartDto>(cartDomainModel);

            return Ok(cartDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var cartDomainModel = await cartRepository.DeleteAsync(id);

            if (cartDomainModel == null)
            {
                return NotFound();
            }

            var cartDto = mapper.Map<CartDto>(cartDomainModel);

            return Ok(cartDto);
        }
    }
}
