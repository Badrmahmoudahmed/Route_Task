using AutoMapper;
using OrederManagement.Core.Entities;

namespace Route_Task.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReurnDto>();
            CreateMap<CustmorToReturnDto, Custmor>();
            CreateMap<CreateOrderItemParams, OrderItem>();
            CreateMap<Invoice, InvoiceDto>();
            CreateMap<Order, OrderToReturnDto>();
            CreateMap<OrderItem, OrderItemToReturnDto>();
            
        }
    }
}
