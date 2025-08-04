using EmploRecrutingProject.Application.Employees.Queries.Task2CQuery;
using EmploRecrutingProject.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmploRecrutingProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Zwraca listę zespołów, w których żaden z pracowników nie złożył żadnego wniosku urlopowego w 2019 roku.
    /// </summary>
    [HttpGet("without-vacation-in-2019")]
    public async Task<ActionResult<List<TeamVm>>> GetTeamsWithoutVacationIn2019(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new Task2CQuery(), cancellationToken);
        return Ok(result);
    }
}
