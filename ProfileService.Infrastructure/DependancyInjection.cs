using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProfileService.Domain.Interfaces;
using ProfileService.Infrastructure.Persistence;
using ProfileService.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using ProfileService.Infrastructure.Messaging;

namespace ProfileService.Infrastructure
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(opt =>
            opt.UseNpgsql(config.GetConnectionString("AppDb")));
            services.AddScoped<IProfileRepository,ProfileRepository>();
            services.AddScoped<IJwtRepository, JwtRepository>();
            services.AddHttpContextAccessor();
            services.AddSingleton<RabbitMqConnection>();
            services.AddHostedService<RabbitMqConsumerRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>(); 
            services.AddScoped<ISmsRepository, SmsRepository>();
            services.AddScoped<INotificationPublisher, RabbitMqNotificationPublisher>();

            return services;
        }
    }
}
