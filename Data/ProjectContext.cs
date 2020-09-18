using Github_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Github_backend.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> opt) : base(opt)
    
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=projects;user=dylan;password=Didibasketnba17+");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Project> Projects { get; set; }
    }
}