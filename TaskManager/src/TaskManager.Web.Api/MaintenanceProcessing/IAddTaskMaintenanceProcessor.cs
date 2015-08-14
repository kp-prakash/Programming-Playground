namespace TaskManager.Web.Api.MaintenanceProcessing
{
    using TaskManager.Web.Api.Models;

    public interface IAddTaskMaintenanceProcessor
    {
        Task AddTask(NewTask newTask);
    }
}