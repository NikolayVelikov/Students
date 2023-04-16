using Microsoft.AspNetCore.Mvc;
using Studets.Api.Dto;
using Studets_Services.Enumerators;
using Studets_Services.Interfaces;

namespace Studets_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportServices _reportServices;

        public ReportController(IReportServices reportServices, IWebHostEnvironment webHost)
        {
            _reportServices = reportServices;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateReport([FromBody] ReportInputModel input)
        {
            var fileName = $"Studets_Report_{DateTime.Today.ToString("dd-MM-yyyy h:mm")}.{input.DocumentType.ToString()}";

            var host = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(host, "wwwroot", "reports", fileName);

            await _reportServices.GenerateStudetReportAsync(pins: input.Pins,
                                                            minCredit: input.MinCredit.Value,
                                                            startDate: input.StartDate,
                                                            endDate: input.EndDate,
                                                            reportType: input.DocumentType.Value,
                                                            filePath: filePath);

            return Ok($"File is generated to: {filePath}");
        }
    }
}
