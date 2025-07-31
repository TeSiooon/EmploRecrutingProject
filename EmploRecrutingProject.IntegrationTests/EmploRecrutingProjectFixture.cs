using EmploRecrutingProject.Application.Abstractions.Repositories;
using EmploRecrutingProject.Application.Abstractions.Services;
using EmploRecrutingProject.Application.Extensions;
using EmploRecrutingProject.Common.Common;
using EmploRecrutingProject.Infrastructure.Persistance;
using EmploRecrutingProject.Infrastructure.Repositories;
using EmploRecrutingProject.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EmploRecrutingProject.IntegrationTests;

public class EmploRecrutingProjectFixture : IAsyncLifetime, IDisposable
{
    private readonly ServiceProvider serviceProvider;
    private readonly IServiceScope scope;

    public IMediator Mediator { get; }
    public IEmployeeHierarchyRepository EmployeeHierarchyRepository { get; }
    public IEmployeeRepository EmployeeRepository { get; }
    public IEmployeeHierarchyService EmployeeHierarchyService { get; }

    public EmploRecrutingProjectFixture()
    {
        var services = new ServiceCollection();
    

        services.AddDbContext<EmploRecrutingProjectDbContext>(options =>
        {
            options.UseInMemoryDatabase($"EmploRecrutingProjectTestDb_{Guid.NewGuid()}");
        });

        services.AddScoped<IEmployeeHierarchyRepository, EmployeeHierarchyRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IEmployeeHierarchyService, EmployeeHierarchyService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddApplication();

        serviceProvider = services.BuildServiceProvider();

        scope = serviceProvider.CreateScope();

        Mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        EmployeeHierarchyRepository = scope.ServiceProvider.GetRequiredService<IEmployeeHierarchyRepository>();
        EmployeeRepository = scope.ServiceProvider.GetRequiredService<IEmployeeRepository>();
        EmployeeHierarchyService = scope.ServiceProvider.GetRequiredService<IEmployeeHierarchyService>();
    }

    public void Dispose()
    {
        scope.Dispose();
    }

    public async Task DisposeAsync()
    {
        var db = scope.ServiceProvider.GetRequiredService<EmploRecrutingProjectDbContext>();
        await db.Database.EnsureDeletedAsync();
    }

    public async Task InitializeAsync()
    {
        var db = scope.ServiceProvider.GetRequiredService<EmploRecrutingProjectDbContext>();
        await db.Database.EnsureCreatedAsync();
    }

    public Task<TResult> ExecuteCommandAsync<TResult>(IRequest<TResult> command)
    {
        var result = Mediator.Send(command);
        var db = scope.ServiceProvider.GetRequiredService<EmploRecrutingProjectDbContext>();
        return result;
    }

    public Task<TResponse> ExecuteQueryAsync<TResponse>(IRequest<TResponse> query) => Mediator.Send(query);
}
