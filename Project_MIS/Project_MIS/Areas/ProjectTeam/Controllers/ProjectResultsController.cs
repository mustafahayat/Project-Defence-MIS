using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_MIS.Data;
using Project_MIS.Models;

namespace Project_MIS.Areas.ProjectTeam.Controllers
{
    [Area("ProjectTeam")]
    public class ProjectResultsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectResultsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProjectTeam/ProjectResults
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProjectResults.Include(p => p.Committee).Include(p => p.Lecturer).Include(p => p.Project);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProjectTeam/ProjectResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectResult = await _context.ProjectResults
                .Include(p => p.Committee)
                .Include(p => p.Lecturer)
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectResult == null)
            {
                return NotFound();
            }

            return View(projectResult);
        }

        // GET: ProjectTeam/ProjectResults/Create
        
        // POST: ProjectTeam/ProjectResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project_MIS.ViewModel.ProjectProjectResultViewModel projectResult)
        {
            if (ModelState.IsValid)
            {
                var claim = (ClaimsIdentity) this.User.Identities;

                // this will return the user id, that is current login user
                var claimIdentity = claim.FindFirst(ClaimTypes.NameIdentifier);

                _context.Add(projectResult.ProjectResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(CommitteeFeedback), projectResult);
        }

        // GET: ProjectTeam/ProjectResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectResult = await _context.ProjectResults.FindAsync(id);
            var projectFromDb =await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectResult.ProjectId);
            if (projectResult == null )
            {
                return NotFound();
            }

            var pr = new Project_MIS.ViewModel.ProjectProjectResultViewModel()
            {
                Project = projectFromDb,
                ProjectResult = projectResult

            };
            ViewData["CommitteeId"] = new SelectList(_context.Committees, "Id", "Name", projectResult.CommitteeId);
            return View(pr);
        }

        // POST: ProjectTeam/ProjectResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Project_MIS.ViewModel.ProjectProjectResultViewModel projectResult)
        {
            if (id != projectResult.ProjectResult.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                 
                    _context.Update(projectResult.ProjectResult);
                    await _context.SaveChangesAsync();
               
                return RedirectToAction(nameof(Index));
            }
            ViewData["CommitteeId"] = new SelectList(_context.Committees, "Id", "Name", projectResult.ProjectResult.CommitteeId);
            return View(projectResult);
        }

        // GET: ProjectTeam/ProjectResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectResult = await _context.ProjectResults
                .Include(p => p.Committee)
                .Include(p => p.Lecturer)
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectResult == null)
            {
                return NotFound();
            }

            return View(projectResult);
        }

        // POST: ProjectTeam/ProjectResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectResult = await _context.ProjectResults.FindAsync(id);
            _context.ProjectResults.Remove(projectResult);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectResultExists(int id)
        {
            return _context.ProjectResults.Any(e => e.Id == id);
        }

        public async Task<IActionResult> CommitteeFeedback(int? id)
        {
            if (id ==  null)
            {
                return NotFound();
            }
            var project =await _context.Projects.FindAsync(id);
            Project_MIS.ViewModel.ProjectProjectResultViewModel model =
                new Project_MIS.ViewModel.ProjectProjectResultViewModel()
                {
                    Project = project,
                    ProjectResult = new ProjectResult()
                };
            ViewData["CommitteeId"] = new SelectList(_context.Committees, "Id", "Name");

            return View(model);
             
        }
    }
}
