using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;
using X.PagedList;

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
        public async Task<IActionResult> GetAll([FromQuery] SearchFilterDto searchFilterDto)
        {
            var searchDomainModel = mapper.Map<SearchFilter>(searchFilterDto);
            (var categoriesDomainModel, int totalItemCount) = await categoryRepository.GetAllAsync(searchDomainModel);

            var categoriesDto = mapper.Map<List<CategoryDto>>(categoriesDomainModel);
            var categoriesPagedList = new StaticPagedList<CategoryDto>(categoriesDto, searchFilterDto.PageNumber, searchFilterDto.PageSize, totalItemCount);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(categoriesPagedList.GetMetaData()));

            return Ok(categoriesPagedList);
        }

        [HttpGet("{id:int}")]
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
