﻿using PilotTask.Data.Entities;
using PilotTask.Models;

namespace PilotTask.Data.Infrastructure.Persistence.Data.Interfaces.IServices.ITasksService
{
    public interface ITasksService
    {
        public Task<Tasks?> CreateTask(TaskModel task);
        
        public Task<bool?> UpdateTask(int taskId, TaskModel task);

        public Task<bool?> DeleteTask(int taskId);

        public Task<List<Tasks>?> RetriveTasks();
        
        public Task<Tasks?> RetriveTasks(int taskId);
        
        public Task<List<Tasks>?> RetriveTasksByProfileId(int profileId);
    }
}
