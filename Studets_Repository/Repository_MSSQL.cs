using Studets_Database;
using Studets_Database.Models;
using Studets_Repository.Interfaces;

namespace Studets_Repository
{
    public class Repository_MSSQL : IRepository
    {
        private readonly CourseraContext _context;

        public Repository_MSSQL(CourseraContext context)
        {
            _context = context;
        }

        public IQueryable<Student> GetStudets(DateTime startDate, DateTime endDate, ICollection<string> pins)
        {
            var query = _context.Students.AsQueryable();
            if (pins is not null && pins.Count > 0)
            {
                query = query.Where(x => pins.Contains(x.Pin));
            }

            query = query.Where(x => x.StudentsCoursesXrefs.Any(x => x.CompletionDate >= startDate && x.CompletionDate <= endDate));

            return query;
        }
    }
}