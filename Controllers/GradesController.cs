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
    public class GradesController : Controller
    {
        private readonly CW4Context _context;

        public GradesController(CW4Context context)
        {
            _context = context;
        }

        // GET: Grades
        public ActionResult Index()
        {
            ViewData["courses"] = _context.CourseGroups.Include(cg => cg.Course).Include(cg => cg.Group).OrderBy(cg=>cg.CourseID).ThenBy(cg=>cg.GroupID);
            return View();
        }

        // POST: Save - zapis ocen z viewcomponent GradeList
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save()
        {
            int cid = int.Parse(HttpContext.Request.Form["cid"]);
            int gid = int.Parse(HttpContext.Request.Form["gid"]);
            var GList = await _context.Grades.Where(g => g.GroupID == gid).Where(c => c.CourseID == cid).ToListAsync();
            foreach(var s in GList)
            {
                int xgr = int.Parse(HttpContext.Request.Form[s.StudentID.ToString()]);
                s.Ocena = xgr;
                s.Data = DateTime.Today;
                _context.Update(s);
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult GradeList(int cr, int gr, int e)
        {
            return ViewComponent("GradeList", new { cr, gr, e });
        }
    }
}
