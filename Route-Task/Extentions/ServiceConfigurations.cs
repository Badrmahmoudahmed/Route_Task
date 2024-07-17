using Microsoft.AspNetCore.Mvc;
using OrderManagement.Core.Services.Contract;
using OrderMangement.Infrastructure;
using OrderMangement.Services;
using OrederManagement.Core.Repository.Contract;
using OrederManagement.Core.Services.Contract;
using Route_Task.ErrorHandler;
using Route_Task.Helpers;
using System.Text.Json.Serialization;

namespace Route_Task.Extentions
{
    public static class ServiceConfigurations
    {
        public static IServiceCollection ApplyServices(this IServiceCollection Services)
        {
            Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            }); ;
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            Services.AddSwaggerGen();
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddScoped(typeof(IOrderServices), typeof(OrderService));
            Services.AddScoped(typeof(ITokenService), typeof(TokenService));
            Services.AddScoped(typeof(IInvoice), typeof(InvoiceService));
            Services.AddScoped(typeof(IEmailSender), typeof(EmailSender));
            Services.AddAutoMapper(typeof(MappingProfile));
            Services.Configure<ApiBehaviorOptions>(o =>
            {
                o.InvalidModelStateResponseFactory = actioncontext =>
                {
                    var errors = actioncontext.ModelState.Where(p => p.Value.Errors.Count > 0)
                    .SelectMany(P => P.Value.Errors)
                    .Select(e => e.ErrorMessage).ToList();

                    var validateError = new ValidateError()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(validateError);
                };
            });
            return Services;
        }
    }
}
