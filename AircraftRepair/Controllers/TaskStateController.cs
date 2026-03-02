using AircraftRepair.Data;
using AircraftRepair.DTOs.Tasks;
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


    // PATCH: api/Task/5/state PATCH update task state
    [HttpPatch("{id}/state")]
    public async Task<IActionResult> UpdateTaskState(int id, UpdateTaskStateRequest request)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task == null)
            return NotFound();

        var state = await _db.TaskStates
            .FirstOrDefaultAsync(s => s.Code == request.NewStateCode);

        if (state == null)
            return BadRequest("Invalid state code");

        task.IdState = state.IdState;

        await _db.SaveChangesAsync();

        return NoContent();
    }
}