using AircraftRepair.Data;
using AircraftRepair.DTOs.Auth;
using AircraftRepair.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AircraftRepair.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AircraftRepairDbContext _db;
    private readonly JwtTokenService _jwt;

    public AuthController(AircraftRepairDbContext db, JwtTokenService jwt)
    {
        _db = db;
        _jwt = jwt;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
    {
        var userName = request.UserName.Trim();

        var user = await _db.AppUsers
            .Include(x => x.Permission)
            .FirstOrDefaultAsync(x => x.UserName == userName);


        if (user is null)
            return Unauthorized("Invalid credentials");

        var hasher = new PasswordHasher<object>();
        var verifyResult = hasher.VerifyHashedPassword(null, user.PasswordHash, request.Password);

        if (verifyResult == PasswordVerificationResult.Failed)
            return Unauthorized("Invalid credentials");

        if (verifyResult == PasswordVerificationResult.SuccessRehashNeeded)
        {
            user.PasswordHash = hasher.HashPassword(null, request.Password);
            await _db.SaveChangesAsync();
        }

        var role = user.Permission.Value; // "Admin" o "User"
        var token = _jwt.CreateToken(user, role);

        return Ok(new AuthResponse
        {
            Token = token,
            IdUser = user.IdUser,
            UserName = user.UserName,
            Role = role
        });
    }

    [HttpGet("me")]
    [Authorize]
    public ActionResult GetMe()
    {
        var idUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userName = User.FindFirstValue(ClaimTypes.Name);
        var role = User.FindFirstValue(ClaimTypes.Role);

        return Ok(new
        {
            idUser = int.Parse(idUser!),
            userName,
            role
        });
    }

    // ---- Define role mapping here ----
    private static string GetUserRole(Entities.AppUser user)
    {
        // ✅ RECOMMENDED: if you have user.Role stored in DB
        // return user.Role;

        // TEMP fallback (until you store roles properly):
        // Example: if user is an admin when IdAdmin is null OR not null (depends on your meaning)
        // This is only a placeholder. You should store role explicitly.
        return "User";
    }
}