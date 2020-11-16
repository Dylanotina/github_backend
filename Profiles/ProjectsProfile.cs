using AutoMapper;
using Github_backend.Dtos;
using Github_backend.Models;


namespace Github_backend.Profiles 
{
    public class ProjectsProfile : Profile
    {
        public ProjectsProfile()
        {          
            CreateMap<Project, ProjectsReadDto>();
            CreateMap<ProjectsCreateDto, Project>();
            CreateMap<Project, ProjectsCreateDto>();
        }
    }
}