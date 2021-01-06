using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_MIS.Data;
using Project_MIS.Models;

namespace Project_MIS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Departments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Departments
                .Include(u=>u.Faculty)
                
                .ToListAsync());
        }

        // GET: Admin/Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .Include(f=>f.Faculty)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Admin/Departments/Create
        public IActionResult Create()
        {
            ViewData["University"] = new SelectList(_context.Universities, "Id", "Name");

            return View();
        }

        // POST: Admin/Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id, FacultyId")] Department department)
        {
            if (ModelState.IsValid)
            {
                var departmentInDb =  _context.Departments
                    .Where(f =>f.FacultyId == department.FacultyId && f.Name.ToLower() == department.Name.ToLower());
                if (departmentInDb.Any())
                {
                   ModelState.AddModelError(string.Empty, "The '"+department.Name + "' named Department is already available in database. try to add different one." );
                   ViewData["University"] = new SelectList(_context.Universities, "Id", "Name");
                   return View(department);
                }
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Admin/Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .Include(f=>f.Faculty).FirstOrDefaultAsync(i => i.Id == id);
                 
            if (department == null)
            {
                return NotFound();
            }

            ViewData["Faculties"] = new SelectList(_context.Faculties, "Id", "Name", department.FacultyId);
            return View(department);
        }

        // POST: Admin/Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id, FacultyId")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var departmentInDb = _context.Departments
                        .Where(f => f.FacultyId == department.FacultyId && f.Name.ToLower() == department.Name.ToLower());
                    if (departmentInDb.Any())
                    {
                        ModelState.AddModelError(string.Empty, "The '" + department.Name + "' named Department is already available in database. try to add different one.");
                        ViewData["Faculties"] = new SelectList(_context.Faculties, "Id", "Name", department.FacultyId);
                        return View(department);
                    }

                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
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
            return View(department);
        }

        // GET: Admin/Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .Include(f=>f.Faculty)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Admin/Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<JsonResult> GetFaculties(int id)
        {
            List<Faculty> faculties =await _context.Faculties
                .Where(u => u.UniversityId == id).ToListAsync();
            return Json(new SelectList(faculties, "Id", "Name"));
        }
    }
}
