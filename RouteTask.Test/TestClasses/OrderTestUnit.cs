using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OrderManagement.Core.Services.Contract;
using OrederManagement.Core.Entities;
using OrederManagement.Core.Repository.Contract;
using OrederManagement.Core.Services.Contract;
using Route_Task.Controllers;
using Route_Task.ErrorHandler;
using Route_Task.Helpers;
using Xunit;
using Assert = Xunit.Assert;

namespace Route_Task.TestClasses
{
    [TestClass]
    public class OrderTestUnit
    {
        private Mock<IGenericRepository<Order>> _repositoryMock;
        private Mock<IOrderServices> _orderServicesMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IInvoice> _invoiceMock;
        private Mock<IEmailSender> _emailSenderMock;
        private Mock<IGenericRepository<Custmor>> _custmorRepoMock;
        private OrderController _orderController;

        [TestInitialize]
        public void Setup()
        {
            _repositoryMock = new Mock<IGenericRepository<Order>>();
            _orderServicesMock = new Mock<IOrderServices>();
            _mapperMock = new Mock<IMapper>();
            _invoiceMock = new Mock<IInvoice>();
            _emailSenderMock = new Mock<IEmailSender>();
            _custmorRepoMock = new Mock<IGenericRepository<Custmor>>();
            _orderController = new OrderController(_repositoryMock.Object, _orderServicesMock.Object, _mapperMock.Object, _invoiceMock.Object, _emailSenderMock.Object, _custmorRepoMock.Object);
        }

        [TestMethod]
        public async Task CreateOrder_ValidOrder()
        {
            var createOrderParams = new CreateOrderParmanter
            {
                PaymentMethod = "CreditCard",
                CustmorId = 1,
                OrderItems = new List<CreateOrderItemParams>
                {
                    new CreateOrderItemParams { ProductId = 1, Quantity = 2, Discount = 20 }
                }
            };

            var mappedItems = new List<OrderItem>
            {
                new OrderItem { ProductId = 1, Quantity = 2, UnitPrice = 10m }
            };

            var order = new Order { Id = 1, CustmorId = 1, TotalAmount = 20m };
            var invoice = new Invoice { Id = 1, OrderId = 1, TotalAmount = 20m };

            _mapperMock.Setup(m => m.Map<List<CreateOrderItemParams>, List<OrderItem>>(createOrderParams.OrderItems)).Returns(mappedItems);
            _orderServicesMock.Setup(s => s.CreateOrderAsync(createOrderParams.PaymentMethod, createOrderParams.CustmorId, mappedItems)).ReturnsAsync(order);
            _invoiceMock.Setup(i => i.CreateInvoice(order.Id, order.TotalAmount)).ReturnsAsync(invoice);

            var result = await _orderController.CreateOrder(createOrderParams);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedOrder = Assert.IsType<Order>(okResult.Value);
            Assert.Equal(order.Id, returnedOrder.Id);
        }

        [TestMethod]
        public async Task CreateOrder_NotEnoughStock()
        {
            // Arrange
            var createOrderParams = new CreateOrderParmanter
            {
                PaymentMethod = "CreditCard",
                CustmorId = 1,
                OrderItems = new List<CreateOrderItemParams>
                {
                    new CreateOrderItemParams { ProductId = 1, Quantity = 2, Discount = 20 }
                }
            };

            var mappedItems = new List<OrderItem>
            {
                new OrderItem { ProductId = 1, Quantity = 2, UnitPrice = 10m }
            };

            _mapperMock.Setup(m => m.Map<List<CreateOrderItemParams>, List<OrderItem>>(createOrderParams.OrderItems)).Returns(mappedItems);
            _orderServicesMock.Setup(s => s.CreateOrderAsync(createOrderParams.PaymentMethod, createOrderParams.CustmorId, mappedItems)).ReturnsAsync((Order)null);

            // Act
            var result = await _orderController.CreateOrder(createOrderParams);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var apiResponse = Assert.IsType<ApiResponse>(badRequestResult.Value);
            Assert.Equal(400, apiResponse.Status);
            Assert.Equal("Not Enough Item At Stock", apiResponse.Message);
        }

        [TestMethod]
        public async Task CreateOrder_ErrorCreatingInvoice()
        {
            // Arrange
            var createOrderParams = new CreateOrderParmanter
            {
                PaymentMethod = "CreditCard",
                CustmorId = 1,
                OrderItems = new List<CreateOrderItemParams>
                {
                    new CreateOrderItemParams { ProductId = 1, Quantity = 2, Discount = 20 }
                }
            };

            var mappedItems = new List<OrderItem>
            {
                new OrderItem { ProductId = 1, Quantity = 2, UnitPrice = 10m }
            };

            var order = new Order { Id = 1, CustmorId = 1, TotalAmount = 20m };

            _mapperMock.Setup(m => m.Map<List<CreateOrderItemParams>, List<OrderItem>>(createOrderParams.OrderItems)).Returns(mappedItems);
            _orderServicesMock.Setup(s => s.CreateOrderAsync(createOrderParams.PaymentMethod, createOrderParams.CustmorId, mappedItems)).ReturnsAsync(order);
            _invoiceMock.Setup(i => i.CreateInvoice(order.Id, order.TotalAmount)).ReturnsAsync((Invoice)null);

            // Act
            var result = await _orderController.CreateOrder(createOrderParams);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var apiResponse = Assert.IsType<ApiResponse>(badRequestResult.Value);
            Assert.Equal(400, apiResponse.Status);
            Assert.Equal("Error At Invoice Creation", apiResponse.Message);
        }
    }
}

