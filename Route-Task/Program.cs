
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrderManagement.Core.Services.Contract;
using OrderMangement.Infrastructure;
using OrderMangement.Infrastructure.Data;
using OrderMangement.Services;
using OrederManagement.Core.Repository.Contract;
using OrederManagement.Core.Services.Contract;
using Route_Task.ErrorHandler;
using Route_Task.Extentions;
using Route_Task.Helpers;
using Route_Task.Middllewares;
using System.Text;
using System.Text.Json.Serialization;

namespace Route_Task
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Service Configuration
            builder.Services.ApplyServices();
            builder.Services.AddAuthentication(o =>
               {
                   o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               }
                ).AddJwtBearer(o=>
                {
                    o.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    };
                  
                }
                );
            builder.Services.AddAuthorization(o =>
            {
                o.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                o.AddPolicy("CustomerOnly", policy => policy.RequireRole("Customer"));
            }
            );

            builder.Services.AddDbContext<OrderMangementDBContxt>(o =>
            {
                o.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
            });
            
            #endregion

            var app = builder.Build();

            #region Update DataBase
            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var dbcontext = service.GetRequiredService<OrderMangementDBContxt>();
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();

            try
            {
                await dbcontext.Database.MigrateAsync();
                await OrderMangementDataSeed.SeedAsync(dbcontext);
            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error Occuerd During Apply The Migration");
            }
            #endregion

            #region Kestrel Configuration
            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.MapControllers();

            app.Run();
            #endregion
        }
    }
}
