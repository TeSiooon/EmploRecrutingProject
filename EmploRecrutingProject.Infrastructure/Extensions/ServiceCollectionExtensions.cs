using EmploRecrutingProject.Application.Abstractions;
using EmploRecrutingProject.Application.Abstractions.Repositories;
using EmploRecrutingProject.Application.Abstractions.Services;
using EmploRecrutingProject.Application.Common;
using EmploRecrutingProject.Infrastructure.Persistance;
using EmploRecrutingProject.Infrastructure.Repositories;
using EmploRecrutingProject.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmploRecrutingProject.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("EmploDb");

        services.AddDbContext<EmploRecrutingProjectDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<EmploRecrutingProjectDbContext>());


        //Register other infrastructure services here
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IEmployeeHierarchyRepository, EmployeeHierarchyRepository>();
        services.AddScoped<IEmployeeHierarchyService, EmployeeHierarchyService>();
        services.AddScoped<IVacationPolicyService, VacationPolicyService>();
    }
}
