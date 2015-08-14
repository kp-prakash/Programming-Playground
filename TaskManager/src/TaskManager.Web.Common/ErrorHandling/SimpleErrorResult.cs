namespace TaskManager.Web.Common.ErrorHandling
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class SimpleErrorResult : IHttpActionResult
    {
        private readonly string errorMessage;
        private readonly HttpRequestMessage requestMessage;
        private readonly HttpStatusCode statusCode;

        public SimpleErrorResult(HttpRequestMessage requestMessage, HttpStatusCode statusCode, string errorMessage)
        {
            this.errorMessage = errorMessage;
            this.requestMessage = requestMessage;
            this.statusCode = statusCode;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(requestMessage.CreateErrorResponse(statusCode, errorMessage));
        }
    }
}