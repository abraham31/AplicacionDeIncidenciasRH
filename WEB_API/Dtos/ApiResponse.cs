using System.Net;

namespace WEB_API.Dtos
{
    public class ApiResponse
    {

        public HttpStatusCode statusCode { get; set; }
        public bool IsExitoso { get; set; } = true;

        public List<string> ErrorMessages { get; set; } = new List<string>();

        public object Resultado { get; set; }

    }
}
