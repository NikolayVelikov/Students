namespace Studets_Services.Dto
{
    public class StudetDto
    {
        public StudetDto()
        {
            Courses = new HashSet<CourseDto>();
        }

        public string FullName { get; set; }

        public int TotalCredit => SumCreadits();

        public ICollection<CourseDto> Courses { get; set; }

        private int SumCreadits()
        {
            if (Courses.Count == 0)
            {
                return 0;
            }

            return Courses.Sum(x => x.Credit);
        }
    }
}