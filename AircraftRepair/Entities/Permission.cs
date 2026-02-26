using System.ComponentModel.DataAnnotations;

namespace AircraftRepair.Entities;

public class Permission
{
    [Key]
    public int IdPermission { get; set; }
    public int Code { get; set; }
    public string Value { get; set; }
    public ICollection<AppUser> AppUsers { get; set; } = new List<AppUser>();
}