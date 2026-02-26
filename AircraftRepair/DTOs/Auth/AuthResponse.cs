namespace AircraftRepair.DTOs.Auth
{
    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty; //El token JWT generado para el usuario autenticado NO se si se va a implementar en esta version, pero se deja preparado para futuras implementaciones
        public int IdUser { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
