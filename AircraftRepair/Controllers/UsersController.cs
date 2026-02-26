using AircraftRepair.Data;
using AircraftRepair.DTOs.Users;
using AircraftRepair.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AircraftRepair.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UsersController : ControllerBase
{
    private readonly AircraftRepairDbContext _db;

    public UsersController(AircraftRepairDbContext db)
    {
        _db = db;
    }

    // GET /api/users  -> para que el admin pueda asignar usuarios a tareas
    [HttpGet]
    public async Task<ActionResult<List<UserListItemDto>>> GetAll()
    {
        var users = await _db.AppUsers
            .AsNoTracking()
            .Include(x => x.Permission)
            .OrderBy(x => x.UserName)
            .Select(x => new UserListItemDto
            {
                IdUser = x.IdUser,
                UserName = x.UserName,
                Role = x.Permission.Value
            })
            .ToListAsync();

        return Ok(users);
    }

    // POST /api/users -> crear usuario (solo Admin)
    [HttpPost]
    public async Task<ActionResult<UserCreatedResponse>> Create([FromBody] CreateUserRequest request)
    {
        var userName = request.UserName.Trim();

        if (string.IsNullOrWhiteSpace(userName))
            return BadRequest("UserName is required");

        if (request.Password is null || request.Password.Length < 6)
            return BadRequest("Password must be at least 6 characters");

        if (request.PermissionCode != 1 && request.PermissionCode != 2)
            return BadRequest("PermissionCode must be 1 (Admin) or 2 (User)");

        var userNameExists = await _db.AppUsers.AnyAsync(x => x.UserName == userName);
        if (userNameExists)
            return Conflict("UserName already exists");

        var permission = await _db.Permissions.FirstOrDefaultAsync(p => p.Code == request.PermissionCode);
        if (permission is null)
            return BadRequest("Permission not found. Seed permissions first.");

        var hasher = new PasswordHasher<object>();
        var passwordHash = hasher.HashPassword(null, request.Password);

        var user = new AppUser
        {
            UserName = userName,
            PasswordHash = passwordHash,
            IdPermission = permission.IdPermission
        };

        _db.AppUsers.Add(user);
        await _db.SaveChangesAsync();

        // Recargar Permission para devolver respuesta completa (opcional)
        await _db.Entry(user).Reference(x => x.Permission).LoadAsync();

        var response = new UserCreatedResponse
        {
            IdUser = user.IdUser,
            UserName = user.UserName,
            IdPermission = user.IdPermission,
            PermissionCode = user.Permission.Code,
            PermissionValue = user.Permission.Value
        };

        return CreatedAtAction(nameof(GetAll), new { idUser = user.IdUser }, response);
    }
}