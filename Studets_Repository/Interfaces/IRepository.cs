using Studets_Database.Models;

namespace Studets_Repository.Interfaces
{
    public interface IRepository
    {
        IQueryable<Student> GetStudets(DateTime startDate, DateTime endDate, ICollection<string> pins);
    }
}