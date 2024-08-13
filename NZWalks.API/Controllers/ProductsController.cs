using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly ILogger<ProductsController> logger;

        public ProductsController(IProductRepository productRepository, IMapper mapper, ILogger<ProductsController> logger)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        // [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAccending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var productsDomainModel = await productRepository.GetAllAsync(filterOn, filterQuery,
                 sortBy, isAccending ?? true, pageNumber, pageSize);
            var productsDto = mapper.Map<List<ProductDto>>(productsDomainModel);

            return Ok(productsDto);
        }

        [HttpGet("{id:int}")]
        // [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var productDomainModel = await productRepository.GetByIdAsync(id);

            if (productDomainModel == null)
            {
                return NotFound();
            }

            var productDto = mapper.Map<ProductDto>(productDomainModel);

            return Ok(productDto);
        }

        [HttpPost]
        [ValidateModel]
        // [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddProductRequestDto addProductRequestDto)
        {
            var productDomainModel = mapper.Map<Product>(addProductRequestDto);
            productDomainModel = await productRepository.CreateAsync(productDomainModel);

            var productDto = mapper.Map<ProductDto>(productDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = productDto.Id }, productDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        [ValidateModel]
        // [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductRequestDto updateProductRequestDto)
        {
            var productDomainModel = mapper.Map<Product>(updateProductRequestDto);
            productDomainModel = await productRepository.UpdateAsync(id, productDomainModel);

            if (productDomainModel == null)
            {
                return NotFound();
            }

            var productDto = mapper.Map<ProductDto>(productDomainModel);

            return Ok(productDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        // [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var productDomainModel = await productRepository.DeleteAsync(id);

            if (productDomainModel == null)
            {
                return NotFound();
            }

            var productDto = mapper.Map<ProductDto>(productDomainModel);

            return Ok(productDto);
        }
    }
}
