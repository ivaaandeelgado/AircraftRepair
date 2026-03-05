using AircraftRepair.DTOs.Users;

namespace AircraftRepair.DTOs.Tasks
{
    public class TaskDetailDto
    {
        public int IdTask { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int IdState { get; set; }
        public int StateCode { get; set; }
        public string StateValue { get; set; } = string.Empty;

        public String description {  get; set; }

        public DateTime? DateAssignment { get; set; }
        public DateTime? DateDelivery { get; set; }

        public List<UserListItemDto> Assignees { get; set; } = new();
    }
}
