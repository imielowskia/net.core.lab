using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CW4.Models;

namespace CW4.ViewComponents
{
    public class GradeListViewComponent:ViewComponent
    {
        private readonly CW4Context _context;

        public GradeListViewComponent(CW4Context context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int cr, int gr, int e)
        {
            string xview = "Default";
            ViewData["cid"] = cr;
            ViewData["gid"] = gr;
            var items = await _context.Grades.Include(g => g.Student).Where(g => g.CourseID == cr).Where(g => g.GroupID == gr).ToListAsync(); 
            if (e == 1)
            {
                xview = "Edit";
                var GSList = new List<GS>();
                foreach (Grade grd in items)
                {
                    GSList.Add(new GS
                    {
                        StudentID = grd.Student.Id,
                        ImieNazwisko = grd.Student.ImieNazwisko,
                        Ocena = grd.Ocena,
                        Data = grd.Data
                    });
                }
                ViewData["grades"] = GSList;
            }  
            if (e == 2)
            {
                int cid = int.Parse(HttpContext.Request.Form["cid"]);
                int gid = int.Parse(HttpContext.Request.Form["gid"]);
                var GList = items;
                foreach (var s in GList)
                {
                    int xgr = int.Parse(HttpContext.Request.Form[s.StudentID.ToString()]);
                    s.Ocena = xgr;
                    s.Data = DateTime.Today;
                    _context.Update(s);
                }
                _context.SaveChanges();
            }
            return View(xview ,items);
        }
    }
}
