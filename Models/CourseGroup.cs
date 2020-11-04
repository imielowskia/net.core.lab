using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CW4.Models
{
    [Table("CourseGroups")]
    public class CourseGroup
    {
        public int CourseID { get; set; }
        public Course Course { get; set; }
        public int GroupID { get; set; }
        public Group Group { get; set; }
    }
}
