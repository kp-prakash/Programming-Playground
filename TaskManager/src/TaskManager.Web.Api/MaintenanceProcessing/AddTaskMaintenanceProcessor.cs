namespace TaskManager.Web.Api.MaintenanceProcessing
{
    using System.Net.Http;
    using TaskManager.Common;
    using TaskManager.Common.TypeMapping;
    using TaskManager.Data.Entities;
    using TaskManager.Web.Api.Models;
    using Task = TaskManager.Web.Api.Models.Task;

    public class AddTaskMaintenanceProcessor : IAddTaskMaintenanceProcessor
    {
        private readonly IAutoMapper autoMapper;

        private readonly IAddTaskQueryProcessor queryProcessor;

        public AddTaskMaintenanceProcessor(IAddTaskQueryProcessor queryProcessor, IAutoMapper autoMapper)
        {
            this.queryProcessor = queryProcessor;
            this.autoMapper = autoMapper;
        }

        public Task AddTask(NewTask newTask)
        {
            var taskEntity = autoMapper.Map<Data.Entities.Task>(newTask);
            queryProcessor.AddTask(taskEntity);
            var task = autoMapper.Map<Task>(taskEntity);

            // TODO: Implement link service.
            task.AddLink(new Link
            {
                Method = HttpMethod.Get.Method,
                Href = "http://localhost/TaskManager.Web.Api/api/v1/tasks/" + task.TaskId,
                Rel = Constants.CommonLinkRelValues.Self
            });

            return task;
        }
    }
}