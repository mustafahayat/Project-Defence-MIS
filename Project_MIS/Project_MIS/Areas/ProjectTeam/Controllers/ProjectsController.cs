using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_MIS.Data;
using Project_MIS.Models;

namespace Project_MIS.Areas.ProjectTeam.Controllers
{
    [Area("ProjectTeam")]
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProjectTeam/Projects
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Projects.Include(p => p.Lecturer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProjectTeam/Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Lecturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Name", project.LecturerId);
            ViewData["Students"] =  await  _context.Students.Where(p => p.ProjectId == project.Id).ToListAsync();
            return View(project);
        }

        // GET: ProjectTeam/Projects/Create
        public IActionResult Create()
        {
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Name");
            return View();
        }

        // POST: ProjectTeam/Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,LecturerId,ProjectState,Id")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Name", project.LecturerId);
            return View(project);
        }

        // GET: ProjectTeam/Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Name", project.LecturerId);
            return View(project);
        }

        // POST: ProjectTeam/Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,LecturerId,ProjectState,Id")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Name", project.LecturerId);
            return View(project);
        }

        // GET: ProjectTeam/Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Lecturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Name", project.LecturerId);
            ViewData["Students"] = await _context.Students.Where(p => p.ProjectId == project.Id).ToListAsync();

            return View(project);
        }

        // POST: ProjectTeam/Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }

        public async Task<IActionResult> ReadyProject()
        {
            var readyProject = await _context.Projects
                .Include(l=>l.Lecturer)
                .Where(p => p.ProjectState == ProjectState.Ready)
                .ToListAsync();
            return View(readyProject);
        }

        public async Task<IActionResult> CompletedProject()
        {
            var readyProject = await _context.Projects
                .Include(l=>l.Lecturer)
                .Where(p => p.ProjectState == ProjectState.Completed)
                .ToListAsync();
            return View(readyProject);
        }

        public async Task<IActionResult> ReadyForEvaluate()
        {
            var readyProject = await _context.Projects
                .Include(l => l.Lecturer)
                .Where(p => p.ProjectState == ProjectState.Ready)
                .ToListAsync();
            return View(readyProject);
        }

        public async Task<IActionResult> ProjectDescription(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Lecturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Name", project.LecturerId);
            ViewData["Students"] = await _context.Students.Where(p => p.ProjectId == project.Id).ToListAsync();
            return View(project);
        }


        public IActionResult TeacherFeedback()
        {
            throw new NotImplementedException();
        }
    }
}
