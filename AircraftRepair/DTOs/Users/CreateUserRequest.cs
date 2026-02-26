namespace AircraftRepair.DTOs.Users
{
    public class CreateUserRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public int PermissionCode { get; set; }
    }
}
