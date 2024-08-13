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
    public class CartItemsController : ControllerBase
    {
        private readonly ICartItemRepository cartItemRepository;
        private readonly IMapper mapper;
        private readonly ILogger<CartItemsController> logger;

        public CartItemsController(ICartItemRepository cartItemRepository, IMapper mapper, ILogger<CartItemsController> logger)
        {
            this.cartItemRepository = cartItemRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        // [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            var cartItemsDomainModel = await cartItemRepository.GetAllAsync();
            var cartItemsDto = mapper.Map<List<CartItemDto>>(cartItemsDomainModel);

            return Ok(cartItemsDto);
        }

        [HttpGet("{id:int}")]
        // [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var cartItemDomainModel = await cartItemRepository.GetByIdAsync(id);

            if (cartItemDomainModel == null)
            {
                return NotFound();
            }

            var cartItemDto = mapper.Map<CartItemDto>(cartItemDomainModel);

            return Ok(cartItemDto);
        }

        [HttpPost]
        [ValidateModel]
        // [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddCartItemRequestDto addCartItemRequestDto)
        {
            var cartItemDomainModel = mapper.Map<CartItem>(addCartItemRequestDto);
            cartItemDomainModel = await cartItemRepository.CreateAsync(cartItemDomainModel);

            var cartItemDto = mapper.Map<CartItemDto>(cartItemDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = cartItemDto.Id }, cartItemDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        [ValidateModel]
        // [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCartItemRequestDto updateCartItemRequestDto)
        {
            var cartItemDomainModel = mapper.Map<CartItem>(updateCartItemRequestDto);
            cartItemDomainModel = await cartItemRepository.UpdateAsync(id, cartItemDomainModel);

            if (cartItemDomainModel == null)
            {
                return NotFound();
            }

            var cartItemDto = mapper.Map<CartItemDto>(cartItemDomainModel);

            return Ok(cartItemDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        // [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var cartItemDomainModel = await cartItemRepository.DeleteAsync(id);

            if (cartItemDomainModel == null)
            {
                return NotFound();
            }

            var cartItemDto = mapper.Map<CartItemDto>(cartItemDomainModel);

            return Ok(cartItemDto);
        }
    }
}
