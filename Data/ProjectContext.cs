using Github_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Github_backend.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> opt) : base(opt)
    
        {

        }

        public DbSet<Project> Projects { get; set; }
    }
}