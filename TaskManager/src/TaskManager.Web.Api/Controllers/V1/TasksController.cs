namespace TaskManager.Web.Api.Controllers.V1
{
    using System.Net.Http;
    using System.Web.Http;
    using TaskManager.Web.Api.MaintenanceProcessing;
    using TaskManager.Web.Api.Models;
    using TaskManager.Web.Common;
    using TaskManager.Web.Common.Routing;

    [ApiVersion1RoutePrefix("tasks")]
    [UnitOfWorkActionFilter]
    public class TasksController : ApiController
    {
        private readonly IAddTaskMaintenanceProcessor addTaskMaintenanceProcessor;

        public TasksController(IAddTaskMaintenanceProcessor addTaskMaintenanceProcessor)
        {
            this.addTaskMaintenanceProcessor = addTaskMaintenanceProcessor;
        }

        [Route("", Name = "AddTaskRoute")]
        [HttpPost]
        public IHttpActionResult AddTask(HttpRequestMessage request, NewTask newTask)
        {
            var task = addTaskMaintenanceProcessor.AddTask(newTask);
            var result = new TaskCreatedActionResult(request, task);
            return result;
        }
    }
}