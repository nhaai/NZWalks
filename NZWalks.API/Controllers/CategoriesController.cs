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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        private readonly ILogger<CategoriesController> logger;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper, ILogger<CategoriesController> logger)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        // [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAccending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var categoriesDomainModel = await categoryRepository.GetAllAsync(filterOn, filterQuery,
                 sortBy, isAccending ?? true, pageNumber, pageSize);
            var categoriesDto = mapper.Map<List<CategoryDto>>(categoriesDomainModel);

            return Ok(categoriesDto);
        }

        [HttpGet("{id:int}")]
        // [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var categoryDomainModel = await categoryRepository.GetByIdAsync(id);

            if (categoryDomainModel == null)
            {
                return NotFound();
            }

            var categoryDto = mapper.Map<CategoryDto>(categoryDomainModel);

            return Ok(categoryDto);
        }

        [HttpPost]
        [ValidateModel]
        // [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddCategoryRequestDto addCategoryRequestDto)
        {
            var categoryDomainModel = mapper.Map<Category>(addCategoryRequestDto);
            categoryDomainModel = await categoryRepository.CreateAsync(categoryDomainModel);

            var categoryDto = mapper.Map<CategoryDto>(categoryDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = categoryDto.Id }, categoryDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        [ValidateModel]
        // [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoryRequestDto updateCategoryRequestDto)
        {
            var categoryDomainModel = mapper.Map<Category>(updateCategoryRequestDto);
            categoryDomainModel = await categoryRepository.UpdateAsync(id, categoryDomainModel);

            if (categoryDomainModel == null)
            {
                return NotFound();
            }

            var categoryDto = mapper.Map<CategoryDto>(categoryDomainModel);

            return Ok(categoryDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        // [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var categoryDomainModel = await categoryRepository.DeleteAsync(id);

            if (categoryDomainModel == null)
            {
                return NotFound();
            }

            var categoryDto = mapper.Map<CategoryDto>(categoryDomainModel);

            return Ok(categoryDto);
        }
    }
}
