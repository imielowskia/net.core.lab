using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CW4.Models;

namespace CW4.Controllers
{
    public class CoursesController : Controller
    {
        private readonly CW4Context _context;

        public CoursesController(CW4Context context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var cW4Context = _context.Courses.Include(cg => cg.CourseGroups).ThenInclude(g => g.Group);
            return View(await cW4Context.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
            return View(course);
        }

        //metoda Grade do wystawiania ocen
        public async Task<IActionResult> Grade(int? cid, int? gid)
        {
            var Slist =  _context.Students.Where(s => s.GroupId == gid);
            var xcourse = await _context.Courses.FindAsync(cid);
            var xgroup = await _context.Groups.FindAsync(gid);
            var GSList = new List<GS>();
            foreach(Student s in Slist)
            {
                GSList.Add(new GS
                {
                    StudentID = s.Id,
                    ImieNazwisko = s.ImieNazwisko,
                    Ocena = 0
                });
            }
            ViewData["grades"] = GSList;
            ViewData["course"] = xcourse;
            ViewData["group"] = xgroup;
            return View(xcourse);
        }

        //POST Grade

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Grade()
        {
            int cid = int.Parse(HttpContext.Request.Form["cid"]);
            int gid = int.Parse(HttpContext.Request.Form["gid"]);
            var Slist = _context.Students.Where(s => s.GroupId == gid);

            foreach(Student s in Slist)
            {
                int xgr = int.Parse(HttpContext.Request.Form[s.Id.ToString()]);
                var g = new Grade();
                g.StudentID = s.Id;
                g.CourseID = cid;
                g.GroupID = gid;
                g.Ocena = xgr;
                await _context.SaveChangesAsync();
                
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
