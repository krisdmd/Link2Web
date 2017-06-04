using Link2Web.Models;
using System;
using System.Collections.Generic;

namespace Link2Web.DAL.Repositories
{
    public interface IProjectRepository: IDisposable
    {
        List<Project> GetProjects();
        Project GetProjectById(int id, string userId);
        void InsertProject(Project project);
        void DeleteProject(int id);
        void UpdateProject(Project project);
        void Save();
    }
}
