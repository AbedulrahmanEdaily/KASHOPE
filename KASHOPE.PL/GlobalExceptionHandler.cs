using KASHOPE.DAL.DTO.Response;
using Microsoft.AspNetCore.Diagnostics;

namespace KASHOPE.PL
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            var erroreDetails = new ErroreDetails
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = exception.InnerException.Message
            };
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(erroreDetails);
            return true;
        }
    }
}
