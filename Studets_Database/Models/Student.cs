using System;
using System.Collections.Generic;

namespace Studets_Database.Models
{
    public partial class Student
    {
        public Student()
        {
            StudentsCoursesXrefs = new HashSet<StudentsCoursesXref>();
        }

        public string Pin { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime TimeCreated { get; set; }

        public virtual ICollection<StudentsCoursesXref> StudentsCoursesXrefs { get; set; }
    }
}
