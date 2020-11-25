using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CW4.Models
{
    [Table("Grades")]
    public class Grade
    {
        public int StudentID {get;set;}
        public int GroupID { get; set; }
        public int CourseID { get; set; }
        public int Ocena { get; set; }
        [DataType(DataType.Date)]
        public DataType Data { get; set; }
    }
}
