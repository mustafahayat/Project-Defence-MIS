using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_MIS.Data;
using Project_MIS.Models;
using Project_MIS.Utilities;

namespace Project_MIS.Areas.ProjectTeam.Controllers
{
    [Area("ProjectTeam")]
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public StudentsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _webHostEnvironment = environment;
        }

        // GET: ProjectTeam/Students
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Students
                .Include(s => s.Department)
                .Include(s => s.Faculty)
                .Include(p=>p.Project);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProjectTeam/Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Department)
                .Include(s => s.Faculty)
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", student.DepartmentId);
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name", student.FacultyId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", student.ProjectId);

            return View(student);
        }

        // GET: ProjectTeam/Students/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name");

            return View();
        }

        // POST: ProjectTeam/Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("RollNo,DepartmentId,FacultyId,Name,LastName,Image,Email,Phone,Id, Gender, ProjectId, Role")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                // Here we will work on image
                var webRootPath = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                var studentFromDb =await _context.Students.FindAsync(student.Id);

//======================== ===================== mean we choose a photo for student =============================================
                if (files.Any())
                {

                    // validate only the image is allowed
                    if (files[0].ContentType != "image/jpeg")
                    {
                        ModelState.AddModelError(string.Empty, "Failed! Only image type is allowed.");
                        ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", student.DepartmentId);
                        ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Description", student.FacultyId);
                        ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", student.ProjectId);

            return View(student);
                    }
                    var upload = Path.Combine(webRootPath, "images");
                    var fileName = files[0].FileName;

                    await using (var fileStream = 
                        new FileStream(Path.Combine(upload, studentFromDb.Id+"_"+fileName), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    studentFromDb.Image = @"\images\" + studentFromDb.Id + "_" + fileName;

                }
                // mean we didn't upload photo, so we use the default one
                else
                {
                    if (studentFromDb.Gender.ToString() == "Male")
                    {
                        var upload = Path.Combine(webRootPath,
                            @"images\" + StaticDetails.DefaultMale);
                        System.IO.File.Copy(upload, webRootPath+ @"\images\"+studentFromDb.Id + "_" + StaticDetails.DefaultMale);
                        studentFromDb.Image = @"\images\" + studentFromDb.Id + "_" + StaticDetails.DefaultMale;

                    }
                    else
                    {
                        var upload = Path.Combine(webRootPath,
                            @"images\" + StaticDetails.DefaultFemale);
                        System.IO.File.Copy(upload, webRootPath + @"\images\" + studentFromDb.Id + "_" + StaticDetails.DefaultFemale);
                        studentFromDb.Image = @"\images\" + studentFromDb.Id + "_" + StaticDetails.DefaultFemale;

                    }
                }
                await _context.SaveChangesAsync();
//=================================================== End Of Image ============================================================
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", student.DepartmentId);
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Description", student.FacultyId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", student.ProjectId);

            return View(student);
        }

        // GET: ProjectTeam/Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", student.DepartmentId);
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name", student.FacultyId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", student.ProjectId);

            return View(student);
        }

        // POST: ProjectTeam/Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("RollNo,DepartmentId,FacultyId,Name,LastName,Image,Email,Phone,Id,Gender, ProjectId, Role")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var webRootPath = _webHostEnvironment.WebRootPath;
                    var studentFromDb = await _context.Students.FindAsync(id);
                    var files = HttpContext.Request.Form.Files;
                    
                    //mean we choose another image, so we have to update this and remove the old one

                    if (files.Any())
                    {

                        // validate only the image is allowed
                        if (files[0].ContentType!= "image/jpeg")
                        {
                            ModelState.AddModelError(string.Empty, "Failed! Only image type is allowed.");
                            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", student.DepartmentId);
                            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Description", student.FacultyId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", student.ProjectId);

            return View(student);
                        }


                        var fileName = files[0].FileName;
                        var upload = Path.Combine(webRootPath, "images");

                        // Now let's delete the old one
                        // because we have added the (\), we have to remove this first
                        var oldImagePath = Path.Combine(webRootPath, studentFromDb.Image.Trim('\\'));
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
                        studentFromDb.Image = @"\images\" + id + "_" + fileName;
                    }

                    //here no image is choose we don't update the image field 
                    studentFromDb.RollNo = student.RollNo;
                    studentFromDb.Name = student.Name;
                    studentFromDb.LastName = student.LastName;
                    studentFromDb.Gender = student.Gender;
                    studentFromDb.FacultyId = student.FacultyId;
                    studentFromDb.DepartmentId = student.DepartmentId;
                    studentFromDb.Email = student.Email;
                    studentFromDb.Phone = student.Phone;
                    studentFromDb.ProjectId = student.ProjectId;
                    studentFromDb.Role = student.Role;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", student.DepartmentId);
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Description", student.FacultyId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", student.ProjectId);

            return View(student);
        }

        // GET: ProjectTeam/Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Department)
                .Include(s => s.Faculty)
                .Include(p=>p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", student.DepartmentId);
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name", student.FacultyId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", student.ProjectId);

            return View(student);
        }

        // POST: ProjectTeam/Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);


            // here we have to remove the image from server as well
            var webRootPath = _webHostEnvironment.WebRootPath;

            // Now let's delete the old one
            // because we have added the (\), we have to remove this first

            var oldImagePath = Path.Combine(webRootPath, student.Image.Trim('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

           
            
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Test()
        {
            return View();
        }
    }
}
