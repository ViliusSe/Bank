using Domain.Exceptions;
using System.Net;

namespace Bank.WebApi.Middlewares
{
    /// <summary>
    /// Throws catched exceptions.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the RequestDelegate.
        /// </summary>
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes HttpContext.
        /// </summary>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (ex)
                {
                    case UserExistsException:
                    case AccountExistsException:
                    case WrongAccountTypeException:
                    case UserNotFoundException: // ????????????????
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
            }
        }
    }
}
