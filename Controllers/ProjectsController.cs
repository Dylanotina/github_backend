using System;
using System.Collections.Generic;
using AutoMapper;
using Github_backend.Data;
using Github_backend.Dtos;
using Github_backend.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Hangfire;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using System.Linq;

namespace Github_backend.Controllers 
{
    
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase 
    {
        private readonly IGithubRepo<Project> _repository;
        private readonly IMapper _mapper;
       

        public ProjectsController(IGithubRepo<Project> repository, IMapper mapper)
        {
            Console.WriteLine("Created Scheduler!");
            _repository = repository;
            _mapper = mapper;
            
            RecurringJob.AddOrUpdate(() => this.logFunct(), Cron.Hourly);
        }
       

        [HttpGet]
        public async  Task<ActionResult<IEnumerable<ProjectsReadDto>>> GetAllProjects()
        {
            return Ok(_mapper.Map<IEnumerable<ProjectsReadDto>>( await _repository.GetAllProjects()));
        }

        [HttpGet("{id}", Name="GetProjectById")]
        public async Task<ActionResult<ProjectsReadDto>> GetProjectById(long id) 
        {
            if (_repository.getProjectById(id) != null)
            {
            return Ok(_mapper.Map<ProjectsReadDto>( await _repository.getProjectById(id)));   
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<ProjectsReadDto> CreateProject(ProjectsCreateDto project)
        {
            if (project != null)
            {
            var ProjectModel  = _mapper.Map<Project>(project);
            _repository.CreateProject(ProjectModel); 
            

            var projectReadDto = _mapper.Map<ProjectsReadDto>(ProjectModel);  
            return CreatedAtRoute(nameof(GetProjectById),new {Id = projectReadDto.id}, projectReadDto);
            }
            
            return BadRequest();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProject(int id, ProjectsCreateDto projectUpdate)
        {
            var ProjectFromDatabase = _repository.getProjectById(id);
            if (ProjectFromDatabase == null)
            {
                return NotFound();
            }
            _mapper.Map(projectUpdate,ProjectFromDatabase);
            var ProjectModel  = _mapper.Map<Project>(ProjectFromDatabase);

            _repository.UpdateProject(ProjectModel);


            return NoContent();
        }   

        [HttpPatch("{id}")]
        public ActionResult  PartialProjectUpdate(int id, JsonPatchDocument<ProjectsCreateDto> patchDocument)
        {
            var ProjectFromDatabase = _repository.getProjectById(id);
            if (ProjectFromDatabase == null)
            {
                return NotFound();
            }
            var ProjectToPatch = _mapper.Map<ProjectsCreateDto>(ProjectFromDatabase);
            patchDocument.ApplyTo(ProjectToPatch, ModelState);
            if(!TryValidateModel(ProjectToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(ProjectToPatch,ProjectFromDatabase);
             var ProjectModel  = _mapper.Map<Project>(ProjectFromDatabase);            

            _repository.UpdateProject(ProjectModel);

        

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProjectAsync(long id)
        {
            var ProjectFromDatabase = _repository.getProjectById(id);
            if (ProjectFromDatabase == null)
            {
                return NotFound();
            }

             var ProjectModel  = _mapper.Map<Project>(ProjectFromDatabase);
              _repository.DeleteProject(ProjectModel);
            return NoContent();
        }


        
        public async Task logFunct()
        {
            Console.WriteLine("Ceci est un test");
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://api.github.com/users/dylanotina/repos");
                //Console.WriteLine(response);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var listProjects = JsonSerializer.Deserialize<List<Project>>(responseBody);

                    listProjects.Sort();

                    Console.WriteLine("\n Projets tries!");

                    foreach( Project project in listProjects)
                    {
                        Console.WriteLine(project);
                        await this.AddRows(project);
                        
                    }

                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\n Exception Caught!");
                Console.WriteLine("Message : {0}", e.Message);
            }
        }

        public async Task AddRows(Project remote)
        {
            var local = await _repository.GetAllProjects();
            List<Project> list = local.ToList();
            list.Sort();
            if(list.BinarySearch(remote) < 0)
            {
                await _repository.CreateProject(remote);
            }else
            {
            var key = list.BinarySearch(remote); 
            Console.WriteLine(key);
            var project = list.ElementAt(key);
            await  _repository.DeleteProject(project);
            await _repository.CreateProject(remote);
            }
        }         
    }
}