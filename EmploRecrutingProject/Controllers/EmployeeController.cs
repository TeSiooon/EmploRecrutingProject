using EmploRecrutingProject.Application.Employees.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmploRecrutingProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
    {
        var employeeId = await mediator.Send(command);
        return Ok(employeeId);
    }
}
