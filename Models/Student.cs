using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CW4.Models
{
    [Table("Students")]
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Imie { get; set;}
        public string Nazwisko { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataB { get; set; }
        public bool Aktywny { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public string ImieNazwisko
        {
            get
            {
                return Imie + " " + Nazwisko;
            }
        }
        
    }
}
