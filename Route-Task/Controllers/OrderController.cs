using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Core.Services.Contract;
using OrederManagement.Core.Entities;
using OrederManagement.Core.Repository.Contract;
using OrederManagement.Core.Services.Contract;
using OrederManagement.Core.Specification;
using Route_Task.ErrorHandler;
using Route_Task.Helpers;

namespace Route_Task.Controllers
{

    public class OrderController : ApiBaseController
    {
        private readonly IGenericRepository<Order> _repository;
        private readonly IOrderServices _orderServices;
        private readonly IMapper _mapper;
        private readonly IInvoice _invoice;
        private readonly IEmailSender _emailSender;
        private readonly IGenericRepository<Custmor> _custmorrepo;

        public OrderController(IGenericRepository<Order> repository, IOrderServices orderServices, IMapper mapper,
            IInvoice invoice, IEmailSender emailSender, IGenericRepository<Custmor> custmorrepo)
        {
            _repository = repository;
            _orderServices = orderServices;
            _mapper = mapper;
            _invoice = invoice;
            _emailSender = emailSender;
            _custmorrepo = custmorrepo;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(CreateOrderParmanter CreatedOrder)
        {
            var MappedItems = _mapper.Map<List<CreateOrderItemParams>, List<OrderItem>>(CreatedOrder.OrderItems);
            var order = await _orderServices.CreateOrderAsync(CreatedOrder.PaymentMethod, CreatedOrder.CustmorId, MappedItems);

            if (order is null) return BadRequest(new ApiResponse(400, "Not Enough Item At Stock"));
            var invoice = await _invoice.CreateInvoice(order.Id, order.TotalAmount);
            if (invoice is null) return BadRequest(new ApiResponse(400, "Error At Invoice Creation"));
            var mappedOrder = _mapper.Map<Order, OrderToReturnDto>(order);
            return Ok(mappedOrder);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrder(int id)
        {
            var spec = new OrderWithOrderItemSpec(id);
            var order = await _repository.GetByIdAsyncWithSpec(spec);
            if (order is null) return NotFound(new ApiResponse(404));
            var mappedOrder = _mapper.Map<Order, OrderToReturnDto>(order);

            return Ok(mappedOrder);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrders()
        {
            var spec = new OrderWithOrderItemSpec();

            var orders = await _repository.GetAllAsyncWithSpec(spec);

            if (orders is null) return BadRequest(new ApiResponse(400));
            var mappedOrder = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders);
            return Ok(mappedOrder);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{orderid}/{Status}")]
        public async Task<ActionResult<Order>> UpdateStatus(string status, int orderid)
        {
            var spec = new OrderWithOrderItemSpec(orderid);

            var order = await _repository.GetByIdAsyncWithSpec(spec);

            if (order is null) return NotFound(new ApiResponse(404));

            var custmor = await _custmorrepo.GetByIdAsync(order.CustmorId);

            order.Status = (Statues)Enum.Parse(typeof(Statues), status);


            var result = await _repository.UpdateAsync(order);

            await _emailSender.SendMailAsync(custmor.Email, "Order Status Updated", $"Your Order Status Updated to {status}");

            if (!(result > 0)) return BadRequest(new ApiResponse(400));
            var Mappedorder = _mapper.Map<Order,OrderToReturnDto>(await _repository.GetByIdAsyncWithSpec(spec));
            return Ok(Mappedorder);

        }
    }
}
