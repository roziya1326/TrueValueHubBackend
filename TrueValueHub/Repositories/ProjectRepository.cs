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
            return await _context.Projects.AsNoTracking().Include(p => p.Parts).ThenInclude(m => m.Materials).Include(p => p.Parts) 
                     .ThenInclude(part => part.ChildParts).ThenInclude(ma => ma.Materials)
                                           .ToListAsync();

        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            
            return await _context.Projects
        .Include(p => p.Parts)
            .ThenInclude(part => part.Materials)
        .Include(p => p.Parts)
            .ThenInclude(part => part.ChildParts)
                .ThenInclude(child => child.Materials)
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

        public async  Task<List<Project>> GetProjectByName(string name)
        {
            
                try
                {
                    return await _context.Projects.Where(p => p.ProjectName.Contains(name)).Include(p => p.Parts).ThenInclude(m => m.Materials).Include(p => p.Parts)
                    .ThenInclude(part => part.ChildParts).ThenInclude(ma => ma.Materials).ToListAsync();
                }
                catch (DbUpdateException dbEx)
                {
                    throw new Exception("A database error occurred while retrieving parts.", dbEx);
                }
                catch (Exception ex)
                {
                    throw new Exception("An unexpected error occurred in the repository while fetching parts.", ex);
                }
            }
        
    }
}
