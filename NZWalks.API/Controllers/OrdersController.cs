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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        private readonly ILogger<OrdersController> logger;

        public OrdersController(IOrderRepository orderRepository, IMapper mapper, ILogger<OrdersController> logger)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] SearchFilterDto searchFilterDto)
        {
            var searchDomainModel = mapper.Map<SearchFilter>(searchFilterDto);
            (var ordersDomainModel, int totalItemCount) = await orderRepository.GetAllAsync(searchDomainModel);

            var ordersDto = mapper.Map<List<OrderDto>>(ordersDomainModel);
            var ordersPagedList = new StaticPagedList<OrderDto>(ordersDto, searchFilterDto.PageNumber, searchFilterDto.PageSize, totalItemCount);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(ordersPagedList.GetMetaData()));

            return Ok(ordersPagedList);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var orderDomainModel = await orderRepository.GetByIdAsync(id);

            if (orderDomainModel == null)
            {
                return NotFound();
            }

            var orderDto = mapper.Map<OrderDto>(orderDomainModel);

            return Ok(orderDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddOrderRequestDto addOrderRequestDto)
        {
            var orderDomainModel = mapper.Map<Order>(addOrderRequestDto);
            orderDomainModel = await orderRepository.CreateAsync(orderDomainModel);

            var orderDto = mapper.Map<OrderDto>(orderDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = orderDto.Id }, orderDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateOrderRequestDto updateOrderRequestDto)
        {
            var orderDomainModel = mapper.Map<Order>(updateOrderRequestDto);
            orderDomainModel = await orderRepository.UpdateAsync(id, orderDomainModel);

            if (orderDomainModel == null)
            {
                return NotFound();
            }

            var orderDto = mapper.Map<OrderDto>(orderDomainModel);

            return Ok(orderDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var orderDomainModel = await orderRepository.DeleteAsync(id);

            if (orderDomainModel == null)
            {
                return NotFound();
            }

            var orderDto = mapper.Map<OrderDto>(orderDomainModel);

            return Ok(orderDto);
        }
    }
}
