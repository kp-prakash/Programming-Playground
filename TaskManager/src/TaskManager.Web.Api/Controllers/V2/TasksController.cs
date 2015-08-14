namespace TaskManager.Web.Api.Controllers.V2
{
    using System.Net.Http;
    using System.Web.Http;
    using TaskManager.Web.Api.Models;

    [RoutePrefix("api/{apiVersion:apiVersionConstraint(v2)}/tasks")]
    public class TasksController : ApiController
    {
        [Route("", Name = "AddTaskRouteV2")]
        [HttpPost]
        public Task AddTask(HttpRequestMessage request, Task newTask)
        {
            return new Task
            {
                Subject = "In V2, newTask.Subject = " + newTask.Subject
            };
        }
    }
}