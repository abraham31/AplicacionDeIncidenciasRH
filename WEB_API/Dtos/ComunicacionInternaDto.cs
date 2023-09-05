namespace WEB_API.Dtos
{
    public class ComunicacionInternaDto
    {
        public int UserId { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaEnvio { get; set; }
        public bool Leeido { get; set; }
    }
}
