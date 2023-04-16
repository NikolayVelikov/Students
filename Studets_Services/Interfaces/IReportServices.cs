using Studets_Services.Enumerators;

namespace Studets_Services.Interfaces
{
    public interface IReportServices
    {
        Task GenerateStudetReportAsync(ICollection<string> pins, int minCredit, DateTime startDate, DateTime endDate, ReportType reportType, string filePath);
    }
}