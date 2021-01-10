using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_MIS.Data;
using Project_MIS.Models;

namespace Project_MIS.Areas.Lecturer.Controllers
{
    [Area("Lecturer")]
    public class EvaluationCommitteesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EvaluationCommitteesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lecturer/EvaluationCommittees
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EvaluationCommittees
                .Include(c=>c.Committee)
                .Include(e => e.Lecturer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Lecturer/EvaluationCommittees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluationCommittee = await _context.EvaluationCommittees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evaluationCommittee == null)
            {
                return NotFound();
            }

            ViewData["CommitteeId"] =
                new SelectList(_context.Committees, "Id", "Name", evaluationCommittee.CommitteeId);
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Name", evaluationCommittee.LecturerId);
            return View(evaluationCommittee);
        }

        // GET: Lecturer/EvaluationCommittees/Create
        public IActionResult Create()
        {
            ViewData["Lecturers"] = new List<Models.Lecturer>( _context.Lecturers.ToList());
            ViewData["CommitteeId"] = new SelectList(_context.Committees, "Id", "Name");
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Name");
            ViewData["Committees"] = new List<Committee>(_context.Committees.Distinct().ToList());

            return View();
        }

        // POST: Lecturer/EvaluationCommittees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( EvaluationCommittee evaluationCommittee)
        {
            if (ModelState.IsValid)
            {

                var ec = _context.EvaluationCommittees.Where(e => e.LecturerId == evaluationCommittee.LecturerId);
                if (ec.Any())
                {
                    ModelState.AddModelError(string.Empty, "Error, This member is already available.");
                    ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Name", evaluationCommittee.LecturerId);
                    ViewData["CommitteeId"] = new SelectList(_context.Committees, "Id", "Name", evaluationCommittee.CommitteeId);
                    ViewData["Lecturers"] = new List<Models.Lecturer>(_context.Lecturers).ToList();

                    return View(evaluationCommittee);
                }

                _context.Add(evaluationCommittee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Name", evaluationCommittee.LecturerId);
            ViewData["CommitteeId"] = new SelectList(_context.Committees, "Id", "Name", evaluationCommittee.CommitteeId);
            ViewData["Lecturers"] = new List<Models.Lecturer>(_context.Lecturers).ToList();

            return View(evaluationCommittee);
        }

        // GET: Lecturer/EvaluationCommittees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluationCommittee = await _context.EvaluationCommittees.FindAsync(id);
            if (evaluationCommittee == null)
            {
                return NotFound();
            }
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Name", evaluationCommittee.LecturerId);
            ViewData["CommitteeId"] = new SelectList(_context.Committees, "Id", "Name", evaluationCommittee.CommitteeId);

            ViewData["Lecturers"] = new List<Models.Lecturer>(_context.Lecturers).ToList();

                    return View(evaluationCommittee);
        }

        // POST: Lecturer/EvaluationCommittees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  EvaluationCommittee evaluationCommittee)
        {
            if (id != evaluationCommittee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var ec = _context.EvaluationCommittees.Where(e => e.LecturerId == evaluationCommittee.LecturerId);
                    if (ec.Any())
                    {
                        ModelState.AddModelError(string.Empty, "Error, This member is already available.");
                        ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Name", evaluationCommittee.LecturerId);
                        ViewData["CommitteeId"] = new SelectList(_context.Committees, "Id", "Name", evaluationCommittee.CommitteeId);
                        ViewData["Lecturers"] = new List<Models.Lecturer>(_context.Lecturers).ToList();

                        return View(evaluationCommittee);
                    }
                    _context.Update(evaluationCommittee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvaluationCommitteeExists(evaluationCommittee.Id))
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
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Name", evaluationCommittee.LecturerId);
            ViewData["CommitteeId"] = new SelectList(_context.Committees, "Id", "Name", evaluationCommittee.CommitteeId);
            ViewData["Lecturers"] = new List<Models.Lecturer>(_context.Lecturers).ToList();

                        return View(evaluationCommittee);
        }

        // GET: Lecturer/EvaluationCommittees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluationCommittee = await _context.EvaluationCommittees.FirstOrDefaultAsync(m => m.Id == id);
            if (evaluationCommittee == null)
            {
                return NotFound();
            }
            ViewData["CommitteeId"] =
                new SelectList(_context.Committees, "Id", "Name", evaluationCommittee.CommitteeId);
            ViewData["LecturerId"] = new SelectList(_context.Lecturers, "Id", "Name", evaluationCommittee.LecturerId);

            return View(evaluationCommittee);
        }

        // POST: Lecturer/EvaluationCommittees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evaluationCommittee = await _context.EvaluationCommittees.FindAsync(id);
            _context.EvaluationCommittees.Remove(evaluationCommittee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvaluationCommitteeExists(int id)
        {
            return _context.EvaluationCommittees.Any(e => e.Id == id);
        }
    }
}
