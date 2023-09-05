using System.Net;

namespace WEB_API.Dtos
{
    public class ApiResponse
    {

        public HttpStatusCode statusCode { get; set; }
        public bool IsExitoso { get; set; } = true;

        public List<string> ErrorMessages { get; set; }

        public object Resultado { get; set; }

    }
}
