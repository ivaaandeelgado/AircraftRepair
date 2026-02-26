using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AircraftRepair.Entities;

public class AppUser
{
    [Key]
    public int IdUser { get; set; }

    public int IdPermission { get; set; }

    [ForeignKey(nameof(IdPermission))]
    public Permission Permission { get; set; } = null!;

    public string UserName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
}