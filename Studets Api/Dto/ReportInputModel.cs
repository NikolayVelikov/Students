using Studets_Services.Enumerators;

namespace Studets.Api.Dto
{
    public class ReportInputModel
    {
        public ReportInputModel()
        {
            Pins = new List<string>();
            DocumentType = ReportType.csv;
            MinCredit = 0;
        }

        public ICollection<string> Pins { get; set; }

        public int? MinCredit { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ReportType? DocumentType { get; set; }
    }
}