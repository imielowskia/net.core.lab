using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CW4.Models
{
    public class GS
    {
        public int StudentID { get; set; }
        public string ImieNazwisko { get; set; }
        public int Ocena { get; set; }
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
    }
}
