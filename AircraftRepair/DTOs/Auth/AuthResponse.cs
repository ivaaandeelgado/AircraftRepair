namespace AircraftRepair.DTOs.Auth
{
    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        public int IdUser { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
