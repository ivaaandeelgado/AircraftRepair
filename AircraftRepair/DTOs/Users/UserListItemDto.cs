namespace AircraftRepair.DTOs.Users
{
    public class UserListItemDto
    {
        public int IdUser { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
