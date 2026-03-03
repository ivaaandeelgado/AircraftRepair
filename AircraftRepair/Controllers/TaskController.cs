using AircraftRepair.Data;
using AircraftRepair.DTOs.Tasks;
using AircraftRepair.DTOs.Users;
using AircraftRepair.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;

namespace AircraftRepair.Controllers;



[ApiController]
[Route("api/[controller]")]

public class TaskController : ControllerBase{


    private readonly AircraftRepairDbContext _db;

    public TaskController(AircraftRepairDbContext db)
    {
        _db = db;
    }


    // POST: api/Task POST create new task
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateTask(CreateTaskRequest request)
    {
        var defaultState = await _db.TaskStates
            .FirstOrDefaultAsync(s => s.Code == 1); // 1 = Pending

        if (defaultState == null)
            return BadRequest("Default state not found");

        DateTime? parsedDateDelivery = null;

        if (!string.IsNullOrWhiteSpace(request.DateDelivery))
        {
            if (DateTime.TryParseExact(
                    request.DateDelivery,
                    "yyyy-MM-ddTHH:mm:ss",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeUniversal,
                    out DateTime result))
            {
                parsedDateDelivery = result;
            }
            else
            {
                return BadRequest("DateDelivery must be in format yyyy-MM-ddTHH:mm:ss");
            }
        }

        var task = new TaskItem
        {
            Title = request.Title,
            Description = request.Description,
            DateAssignment = DateTime.UtcNow,
            DateDelivery = parsedDateDelivery,
            IdState = defaultState.IdState
        };

        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTaskById),
            new { id = task.IdTask },
            task.IdTask);
    }


    // GET: api/Task/5 GET task by id
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskListItemDto>> GetTaskById(int id)
    {
        var task = await _db.Tasks
            .Include(t => t.TaskState)
            .Include(t => t.Assignments)
                .ThenInclude(a => a.AppUser)
            .FirstOrDefaultAsync(t => t.IdTask == id);

        if (task == null)
            return NotFound();

        var result = new TaskListItemDto
        {
            IdTask = task.IdTask,
            Title = task.Title,
            IdState = task.IdState,
            StateCode = task.TaskState.Code,
            StateValue = task.TaskState.Value,
            DateAssignment = task.DateAssignment,
            DateDelivery = task.DateDelivery,
            Assignees = task.Assignments.Select(a => new UserListItemDto
            {
                IdUser = a.AppUser.IdUser,
                UserName = a.AppUser.UserName
            }).ToList(),
            IsUnassigned = !task.Assignments.Any(),
            IsOverdue = task.DateDelivery.HasValue
                        && task.DateDelivery.Value < DateTime.UtcNow
                        && task.TaskState.Code != 3
        };

        return Ok(result);
    }
    // PUT: api/Task/5 PUT update task
    [HttpPut]
    public async Task<IActionResult> UpdateTask(int id, UpdateTaskRequest request)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task == null)
            return NotFound();

        task.Title = request.Title;
        task.Description = request.Description;
        task.DateDelivery = request.DateDelivery;

        await _db.SaveChangesAsync();

        return NoContent();
    }

    


    // GET: api/Task GET all tasks
    [HttpGet]
    public async Task<ActionResult<List<TaskListItemDto>>> GetTasks()
    {
        var tasks = await _db.Tasks
            .Include(t => t.TaskState)
            .Include(t => t.Assignments)
                .ThenInclude(a => a.AppUser)
            .ToListAsync();

        var result = tasks.Select(t => new TaskListItemDto
        {
            IdTask = t.IdTask,
            Title = t.Title,
            IdState = t.IdState,
            StateCode = t.TaskState.Code,
            StateValue = t.TaskState.Value,
            DateAssignment = t.DateAssignment,
            DateDelivery = t.DateDelivery,
            Assignees = t.Assignments.Select(a => new UserListItemDto
            {
                IdUser = a.AppUser.IdUser,
                UserName = a.AppUser.UserName
            }).ToList(),
            IsUnassigned = !t.Assignments.Any(),
            IsOverdue = t.DateDelivery.HasValue
                        && t.DateDelivery.Value < DateTime.UtcNow
                        && t.TaskState.Code != 3 
        }).ToList();

        return Ok(result);
    }

    [Authorize]
    [HttpGet("my-tasks")]
    public async Task<ActionResult<List<TaskListItemDto>>> GetMyTasks()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim == null)
            return Unauthorized();

        int userId = int.Parse(userIdClaim);

        var tasks = await _db.Tasks
            .Include(t => t.TaskState)
            .Include(t => t.Assignments)
                .ThenInclude(a => a.AppUser)
            .Where(t => t.Assignments.Any(a => a.AppUserId == userId))
            .Select(t => new TaskListItemDto
            {
                IdTask = t.IdTask,
                Title = t.Title,
                IdState = t.IdState,
                StateValue = t.TaskState.Value,
                DateAssignment = t.DateAssignment,
                DateDelivery = t.DateDelivery,
                IsUnassigned = !t.Assignments.Any(),
                IsOverdue = t.DateDelivery.HasValue
                            && t.DateDelivery < DateTime.UtcNow
                            && t.IdState != 3
            })
            .ToListAsync();

        return Ok(tasks);
    }

}
    

