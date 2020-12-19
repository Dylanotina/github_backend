using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Github_backend.Data;

namespace Github_backend.Models
{
    public static class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<ProjectContext>());
            }
        }

        public static void SeedData(ProjectContext context)
        {
            context.Database.Migrate();
        }
    }
}