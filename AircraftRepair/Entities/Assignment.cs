using System.ComponentModel.DataAnnotations;

namespace AircraftRepair.Entities;

public class Assignment
{
    [Key]
    public int IdAssignment { get; set; }

    public AppUser AppUser { get; set; }

    public int AppUserId { get; set; }

    public int IdTask { get; set; }
    public TaskItem TaskItem { get; set; }
}