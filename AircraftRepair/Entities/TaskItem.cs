using System.ComponentModel.DataAnnotations;

namespace AircraftRepair.Entities;

public class TaskItem
{
    [Key]
    public int IdTask { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? DateAssignment { get; set; }
    public DateTime? DateDelivery { get; set; }

    public int IdState { get; set; }
    public TaskState TaskState { get; set; }

    public ICollection<Assignment> Assignments { get; set; }
}