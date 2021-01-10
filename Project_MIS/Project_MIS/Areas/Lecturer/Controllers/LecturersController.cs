using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_MIS.Data;
using Project_MIS.Models;
using Project_MIS.Utilities;

namespace Project_MIS.Areas.Lecturer.Controllers
{
    [Area("Lecturer")]
    public class LecturersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LecturersController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _webHostEnvironment = environment;
        }

        // GET: Lecturer/Lecturers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Lecturers
                .Include(l => l.Department);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Lecturer/Lecturers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturer = await _context.Lecturers
                .Include(l => l.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lecturer == null)
            {
                return NotFound();
            }

            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", lecturer.DepartmentId);
            return View(lecturer);
        }

        // GET: Lecturer/Lecturers/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        // POST: Lecturer/Lecturers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("DepartmentId,Name,LastName,Image,Gender,Email,Phone,Id")] Models.Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lecturer);
                await _context.SaveChangesAsync();
               
                // Here we will work on image
                var webRootPath = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                var lecturerFromDb = await _context.Lecturers.FindAsync(lecturer.Id);

                //======================== ===================== mean we choose a photo for lecturer =============================================
                if (files.Any())
                {

                    // validate only the image is allowed
                    if (files[0].ContentType != "image/jpeg")
                    {
                        ModelState.AddModelError(string.Empty, "Failed! Only image type is allowed.");
                        ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", lecturer.DepartmentId);
                       return View(lecturer);
                    }
                    var upload = Path.Combine(webRootPath, "images");
                    var fileName = files[0].FileName;

                    await using (var fileStream =
                        new FileStream(Path.Combine(upload, lecturerFromDb.Id + "_" + fileName), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    lecturerFromDb.Image = @"\images\" + lecturerFromDb.Id + "_" + fileName;

                }
                // mean we didn't upload photo, so we use the default one
                else
                {
                    if (lecturerFromDb.Gender.ToString() == "Male")
                    {
                        var upload = Path.Combine(webRootPath,
                            @"images\" + StaticDetails.ManTeacher);
                        System.IO.File.Copy(upload, webRootPath + @"\images\" + lecturerFromDb.Id + "_" + StaticDetails.ManTeacher);
                        lecturerFromDb.Image = @"\images\" + lecturerFromDb.Id + "_" + StaticDetails.ManTeacher;

                    }
                    else
                    {
                        var upload = Path.Combine(webRootPath,
                            @"images\" + StaticDetails.WomanTeacher);
                        System.IO.File.Copy(upload, webRootPath + @"\images\" + lecturerFromDb.Id + "_" + StaticDetails.WomanTeacher);
                        lecturerFromDb.Image = @"\images\" + lecturerFromDb.Id + "_" + StaticDetails.WomanTeacher;

                    }
                }
                await _context.SaveChangesAsync();
                //=================================================== End Of Image ============================================================

                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", lecturer.DepartmentId);
            return View(lecturer);
        }

        // GET: Lecturer/Lecturers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", lecturer.DepartmentId);
            return View(lecturer);
        }

        // POST: Lecturer/Lecturers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentId,Name,LastName,Image,Gender,Email,Phone,Id")] Models.Lecturer lecturer)
        {
            if (id != lecturer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var webRootPath = _webHostEnvironment.WebRootPath;
                    var lecturerFromDb = await _context.Lecturers.FindAsync(id);
                    var files = HttpContext.Request.Form.Files;

                    //mean we choose another image, so we have to update this and remove the old one

                    if (files.Any())
                    {

                        // validate only the image is allowed
                        if (files[0].ContentType != "image/jpeg")
                        {
                            ModelState.AddModelError(string.Empty, "Failed! Only image type is allowed.");
                            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", lecturer.DepartmentId);
                            return View(lecturer);
                        }


                        var fileName = files[0].FileName;
                        var upload = Path.Combine(webRootPath, "images");

                        // Now let's delete the old one
                        // because we have added the (\), we have to remove this first
                        var oldImagePath = Path.Combine(webRootPath, lecturerFromDb.Image.Trim('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                        // Now let's add teh new one
                        await using (var fileStream =
                            new FileStream(Path.Combine(upload, id + "_" + fileName), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        // If image is set we update the image
                        lecturerFromDb.Image = @"\images\" + id + "_" + fileName;
                    }

                    //here no image is choose we don't update the image field 
                   
                    lecturerFromDb.Name = lecturer.Name;
                    lecturerFromDb.LastName = lecturer.LastName;
                    lecturerFromDb.Gender = lecturer.Gender;
                   
                    lecturerFromDb.DepartmentId = lecturer.DepartmentId;
                    lecturerFromDb.Email = lecturer.Email;
                    lecturerFromDb.Phone = lecturer.Phone;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LecturerExists(lecturer.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", lecturer.DepartmentId);
            return View(lecturer);
        }

        // GET: Lecturer/Lecturers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturer = await _context.Lecturers
                .Include(l => l.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lecturer == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", lecturer.DepartmentId);

            return View(lecturer);
        }

        // POST: Lecturer/Lecturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lecturer = await _context.Lecturers.FindAsync(id);
            _context.Lecturers.Remove(lecturer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LecturerExists(int id)
        {
            return _context.Lecturers.Any(e => e.Id == id);
        }
    }
}
