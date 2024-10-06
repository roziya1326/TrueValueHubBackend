using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueValueHub.Models;
using TrueValueHub.Data;
using TrueValueHub.Dto;

namespace TrueValueHub.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApiDbContext _context;

        public ProjectRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            return await _context.Projects.Include(p => p.Parts).ToListAsync();
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            return await _context.Projects.Include(p => p.Parts)
                                          .FirstOrDefaultAsync(p => p.ProjectId == id);
        }

        public async Task<Project> UpdateProjectAsync(Project project)
        {
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Project> AddProject(Project project)
        {
            var newproject = await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return newproject.Entity;
        }

        public async Task UpdateProjectPartsAsync(Project project)
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Part> AddPart(Part part)
        {
            
             var newPart = await _context.Parts.AddAsync(part);
             await _context.SaveChangesAsync();
             return newPart.Entity;
        }
    }
}
