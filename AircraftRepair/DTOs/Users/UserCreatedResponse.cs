namespace AircraftRepair.DTOs.Users
{
    public class UserCreatedResponse
    {
        public int IdUser { get; set; }
        public string UserName { get; set; } = string.Empty;

        public int IdPermission { get; set; }
        public int PermissionCode { get; set; }
        public string PermissionValue { get; set; } = string.Empty;
    }
}
