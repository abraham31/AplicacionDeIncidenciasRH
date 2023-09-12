namespace WEB_API.Dtos
{
    public class SolicitudDto<T>
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public T Solicitud { get; set; }
    }
}
