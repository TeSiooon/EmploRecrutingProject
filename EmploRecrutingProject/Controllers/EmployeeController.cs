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
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
    {
        var employeeId = await mediator.Send(command);
        return Ok(employeeId);
    }
    /// <summary>
    /// Zwraca listę pracowników z zespołu o nazwie “.NET”, którzy mają co najmniej jeden wniosek urlopowy w 2019 roku.
    /// </summary>
    [HttpGet("with-vacations-in-net-2019")]
    public async Task<ActionResult<List<EmployeeVm>>> GetEmployeesWithVacationsIn2019() 
    {
        var result = await mediator.Send(new GetTask2AQuery());
        return Ok(result);
    }

    /// <summary>
    /// Zwraca listę pracowników wraz z liczbą dni urlopowych zużytych w bieżącym roku.
    /// Dniem zużytym jest dzień, który znajduje się w całości w przeszłości.
    /// </summary>
    [HttpGet("used-vacation-this-year")]
    public async Task<ActionResult<List<UsedVacationVm>>> GetUsedVacationThisYearAsync()
    {
        var result = await mediator.Send(new Task2BQuery());
        return Ok(result);
    }

    /// <summary>
    /// Wylicza ile dni urlopowych ma do wykorzystania pracownik w bieżącym roku
    /// </summary>
    [HttpGet("avaible-vacation-days")]
    public async Task<ActionResult<List<UsedVacationVm>>> GetAvaibleVacationDaysAsync([FromQuery] FreeDaysQuery query)
    {
        var result = await mediator.Send(query);
        return Ok(result);
    }
}
