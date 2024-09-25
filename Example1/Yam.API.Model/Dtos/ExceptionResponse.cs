using System.Net;

namespace Yam.API.Model.Dtos
{
    public record ExceptionResponse(HttpStatusCode StatusCode, string Description);
}
