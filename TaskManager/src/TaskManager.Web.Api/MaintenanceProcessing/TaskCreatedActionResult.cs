namespace TaskManager.Web.Api.MaintenanceProcessing
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Task = TaskManager.Web.Api.Models.Task;

    public class TaskCreatedActionResult : IHttpActionResult
    {
        private readonly Task createdTask;
        private readonly HttpRequestMessage httpRequestMessage;

        public TaskCreatedActionResult(HttpRequestMessage httpRequestMessage, Task createdTask)
        {
            this.httpRequestMessage = httpRequestMessage;
            this.createdTask = createdTask;
        }

        public HttpResponseMessage Execute()
        {
            var responseMessage = httpRequestMessage.CreateResponse(HttpStatusCode.Created, createdTask);
            responseMessage.Headers.Location = LocationLinkCalculator.GetLocationLink(createdTask);
            return responseMessage;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.FromResult(Execute());
        }
    }
}