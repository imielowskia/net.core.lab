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
    public class GroupsController : Controller
    {
        private readonly CW4Context _context;

        public GroupsController(CW4Context context)
        {
            _context = context;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            var cW4Context = _context.Groups.Include(s=>s.Students).Include(cg=>cg.CourseGroups).ThenInclude(c => c.Course);
            return View(await cW4Context.ToListAsync());
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            GetCourseList();
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa")] Group @group)
        {
            if (ModelState.IsValid)
            {
                var lista = HttpContext.Request.Form["selectedCourses"];
                _context.Add(@group);
                await _context.SaveChangesAsync();

                foreach (var l in lista)
                {
                    var cg = new CourseGroup();
                    cg.CourseID = int.Parse(l);
                    cg.GroupID = @group.Id;
                    _context.Add(cg);
                    await _context.SaveChangesAsync();

                }
                return RedirectToAction(nameof(Index));
                

            }
            return View(@group);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }
            GetSelectedCourseList(@group);
            return View(@group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa")] Group @group)
        {
            if (id != @group.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    foreach(var cgold in _context.CourseGroups.Where(cg => cg.GroupID == group.Id))
                    {
                        _context.Remove(cgold);
                    }
                    var lista = HttpContext.Request.Form["selectedCourses"];
                    foreach (var l in lista)
                    {
                        var cg = new CourseGroup();
                        cg.CourseID = int.Parse(l);
                        cg.GroupID = group.Id;
                        _context.Add(cg);
                        await _context.SaveChangesAsync();
                    }

                    _context.Update(@group);
                    

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(@group.Id))
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
            return View(@group);
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @group = await _context.Groups.FindAsync(id);
            _context.Groups.Remove(@group);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }

        private void GetCourseList()
        {
            var allcourses = _context.Courses;
            var Kursy = new List<C>();
            foreach (var c in allcourses)
            {
                Kursy.Add(new C
                {
                    CourseId = c.Id,
                    Nazwa = c.Nazwa,
                    Checked = ""
                });
            }
            ViewData["courses"] = Kursy;
        }

        private void GetSelectedCourseList(Group group)
        {
            var allcourses = _context.Courses;
            var selectcourses = _context.CourseGroups.Where(cg => cg.GroupID == group.Id).ToList();
            var Kursy = new List<C>();
            foreach (var c in allcourses)
            {
                Kursy.Add(new C
                {
                    CourseId = c.Id,
                    Nazwa = c.Nazwa,
                    Checked = ""
                });
            }
            foreach(var k in Kursy)
            {
                if (selectcourses.Exists(cg => cg.CourseID == k.CourseId))
                {
                    k.Checked = "checked";
                }

            }
            ViewData["courses"] = Kursy;
        }
    }
}
