using AircraftRepair.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AircraftRepair.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskStateController : ControllerBase
{
    private readonly AircraftRepairDbContext _db;

    public TaskStateController(AircraftRepairDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var states = await _db.TaskStates
            .AsNoTracking()
            .OrderBy(x => x.Code)
            .ToListAsync();

        return Ok(states);
    }
}