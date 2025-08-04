using EmploRecrutingProject.Application.Employees.Commands.Create;
using EmploRecrutingProject.Application.Employees.Queries.FreeDaysGuery;
using EmploRecrutingProject.Application.Employees.Queries.Task2AQuery;
using EmploRecrutingProject.Application.Employees.Queries.Task2BQuery;
using EmploRecrutingProject.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmploRecrutingProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command, CancellationToken cancellationToken)
    {
        var employeeId = await mediator.Send(command, cancellationToken);
        return Ok(employeeId);
    }

    /// <summary>
    /// Zwraca listę pracowników z zespołu o nazwie “.NET”, którzy mają co najmniej jeden wniosek urlopowy w 2019 roku.
    /// </summary>
    [HttpGet("with-vacations-in-net-2019")]
    public async Task<ActionResult<List<EmployeeVm>>> GetEmployeesWithVacationsIn2019(CancellationToken cancellationToken) 
    {
        var result = await mediator.Send(new GetTask2AQuery(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Zwraca listę pracowników wraz z liczbą dni urlopowych zużytych w bieżącym roku.
    /// Dniem zużytym jest dzień, który znajduje się w całości w przeszłości.
    /// </summary>
    [HttpGet("used-vacation-this-year")]
    public async Task<ActionResult<List<UsedVacationVm>>> GetUsedVacationThisYearAsync(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new Task2BQuery(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Wylicza ile dni urlopowych ma do wykorzystania pracownik w bieżącym roku
    /// </summary>
    [HttpGet("avaible-vacation-days")]
    public async Task<ActionResult<List<UsedVacationVm>>> GetAvaibleVacationDaysAsync([FromQuery] FreeDaysQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }
}
