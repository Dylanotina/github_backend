using System.Collections.Generic;
using System.Threading.Tasks;
using Github_backend.Models;

namespace Github_backend.Data 
{
    public interface IGithubRepo<T> where T : Project
    {
        

         Task<IEnumerable<T>> GetAllProjects();
        Task<T> getProjectById(int id);
        Task CreateProject(T project);
        Task UpdateProject(T project);
        Task DeleteProject(T project);
    }
}