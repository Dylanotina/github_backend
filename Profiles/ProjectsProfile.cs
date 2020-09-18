using AutoMapper;
using Github_backend.Dtos;
using Github_backend.Models;

namespace Github_backend.Profiles 
{
    public class ProjectsProfile : Profile
    {
        public ProjectsProfile()
        {
            CreateMap<DataGithub, ProjectsReadDto>();
            CreateMap<ProjectsCreateDto, DataGithub>();
            CreateMap<DataGithub, ProjectsCreateDto>();
        }
    }
}