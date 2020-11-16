using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Github_backend.Models;

namespace Github_backend.Data
{
    public class MySqlGithubRepo : IGithubRepo<Project>
    {
        private readonly ProjectContext _context;

        public MySqlGithubRepo(ProjectContext context)
        {
            _context = context;
        }

        public async Task CreateProject(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }
          await  _context.Projects.AddAsync(project);
          await _context.SaveChangesAsync();
        }

      

        public async Task DeleteProject(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

             _context.Projects.Remove(project);
             await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<Project> getProjectById(long id)
        {
            return await _context.Projects.FindAsync(id);
        }


        public async Task UpdateProject(Project project)
        {
            _context.Entry(project).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
             await _context.SaveChangesAsync();
        }

    }
}