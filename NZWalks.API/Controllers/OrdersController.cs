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
        // [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAccending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var ordersDomainModel = await orderRepository.GetAllAsync(filterOn, filterQuery,
                 sortBy, isAccending ?? true, pageNumber, pageSize);
            var ordersDto = mapper.Map<List<OrderDto>>(ordersDomainModel);

            return Ok(ordersDto);
        }

        [HttpGet("{id:int}")]
        // [Authorize(Roles = "Reader")]
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
        // [Authorize(Roles = "Writer")]
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
        // [Authorize(Roles = "Writer")]
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
        // [Authorize(Roles = "Writer,Reader")]
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
