using AircraftRepair.Data;
using AircraftRepair.DTOs.Tasks;
using AircraftRepair.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AircraftRepair.Controllers;


[Route("api/[controller]")]
public class AssignementsController : ControllerBase{

    private readonly AircraftRepairDbContext _db;

    public AssignementsController(AircraftRepairDbContext db)
    {
        _db = db;
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> SetAssignees(int id, SetAssigneesRequest request)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task == null)
            return NotFound();

        var existingAssignments = _db.Assignments
            .Where(a => a.IdTask == id);

        _db.Assignments.RemoveRange(existingAssignments);

        var newAssignments = request.UserIds.Select(userId => new Assignment
        {
            IdTask = id,
            AppUserId = userId
        });

        await _db.Assignments.AddRangeAsync(newAssignments);
        await _db.SaveChangesAsync();

        return NoContent();
    }


}

