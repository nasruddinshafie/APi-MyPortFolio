using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortFolio.Data;
using MyPortFolio.Entities;

namespace MyPortFolio.Controllers
{
    
    public class ProjectsController : BaseApiController
    {
        private readonly DataContext _context;
       public ProjectsController(DataContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public async Task< ActionResult<IEnumerable<Project>>> GetProjects() 
        {
            var projects = await _context.Projects.ToListAsync();
            
            return projects;
        }

        [HttpGet("{id}")]
        public async Task< ActionResult<Project>> GetProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            return project;
        }

       

    }
}
