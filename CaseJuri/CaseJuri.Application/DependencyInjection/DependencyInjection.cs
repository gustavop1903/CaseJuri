using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using CaseJuri.Application.UseCases.Tasks;
using CaseJuri.Application.Mappings;

namespace CaseJuri.Application.DependencyInjection
{   
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(TaskProfile).Assembly);

            services.AddScoped<UpdateToDoTaskUseCase>();
            services.AddScoped<CompleteToDoTaskUseCase>();
            services.AddScoped<GetByIdToDoTaskUseCase>();
            services.AddScoped<GetAllToDoTasksUseCase>();
            services.AddScoped<DeleteToDoTaskUseCase>();
            services.AddScoped<CreateToDoTaskUseCase>();
            services.AddScoped<StartToDoTaskUseCase>();

            return services;
        }
    }
}