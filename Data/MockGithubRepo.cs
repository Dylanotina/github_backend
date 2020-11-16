using System.Collections.Generic;
using System.Threading.Tasks;
using Github_backend.Models;

namespace Github_backend.Data 
{
    public class MockGithubRepo : IGithubRepo<Project>
    {
        public void CreateProject(DataGithub project)
        {
            throw new System.NotImplementedException();
        }

        public void CreateProject(Project project)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteProject(DataGithub project)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteProject(Project project)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DataGithub> GetAllProjects()
        {
            var Projects = new List<DataGithub> {
                new DataGithub{id=1, name="Test",html_url="aaaa.com", description="ceci est un test", created_at="ecrire sur un qwerty est difficile"},
                new DataGithub{id=2, name="Test2",html_url="aaaa.com", description="ceci est un test numero 2", created_at="ecrire sur un qwerty est difficile"},
                new DataGithub{id=3, name="Test3",html_url="aaaa.com", description="ceci est un test numero 3", created_at="ecrire sur un qwerty est difficile"}

            };
            return Projects;
        }

        public DataGithub getProjectById(long id)
        {
            return new DataGithub{id=1, name="Test",html_url="aaaa.com", description="ceci est un test", created_at="ecrire sur un qwerty est difficile"};
        }

        public bool saveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateProject(DataGithub project)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateProject(Project project)
        {
            throw new System.NotImplementedException();
        }

        Task IGithubRepo<Project>.CreateProject(Project project)
        {
            throw new System.NotImplementedException();
        }

        Task IGithubRepo<Project>.DeleteProject(Project project)
        {
            throw new System.NotImplementedException();
        }


        Task<IEnumerable<Project>> IGithubRepo<Project>.GetAllProjects()
        {
            throw new System.NotImplementedException();
        }

     

        Task<Project> IGithubRepo<Project>.getProjectById(long id)
        {
            throw new System.NotImplementedException();
        }

        Task IGithubRepo<Project>.UpdateProject(Project project)
        {
            throw new System.NotImplementedException();
        }
    }
}