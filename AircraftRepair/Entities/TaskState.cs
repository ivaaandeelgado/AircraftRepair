using System.ComponentModel.DataAnnotations;

namespace AircraftRepair.Entities;

public class TaskState
{

    [Key]
    public int IdState { get; set; }
    public int Code { get; set; }
    public string Value { get; set; }

    public ICollection<TaskItem> Tasks { get; set; }
}