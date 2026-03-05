using AircraftRepair.DTOs.Users;

namespace AircraftRepair.DTOs.Tasks
{
    public class TaskListItemDto
    {
        public int IdTask { get; set; }
        public string Title { get; set; } = string.Empty;

        public int IdState { get; set; }
        public int StateCode { get; set; }
        public string StateValue { get; set; } = string.Empty;

        public DateTime? DateAssignment { get; set; }
        public DateTime? DateDelivery { get; set; }
        
        public String description { get; set; }

        public List<UserListItemDto> Assignees { get; set; } = new();

        public bool IsUnassigned { get; set; }
        public bool IsOverdue { get; set; }
    }       
}
