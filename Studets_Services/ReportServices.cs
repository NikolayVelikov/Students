using Microsoft.EntityFrameworkCore;
using Studets_Repository.Interfaces;
using Studets_Services.Dto;
using Studets_Services.Enumerators;
using Studets_Services.Interfaces;
using System.Text;

namespace Studets_Services
{
    public class ReportServices : IReportServices
    {
        private readonly IRepository _repository;

        public ReportServices(IRepository repository)
        {
            _repository = repository;
        }

        public async Task GenerateStudetReportAsync(ICollection<string> pins, int minCredit, DateTime startDate, DateTime endDate, ReportType reportType, string filePath)
        {
            var students = await _repository.GetStudets(startDate, endDate, pins)
                                           .Select(x => new StudetDto
                                           {
                                               FullName = $"{x.FirstName} {x.LastName}",
                                               Courses = x.StudentsCoursesXrefs.Select(y => new CourseDto
                                               {
                                                   CourseName = y.Course.Name,
                                                   Credit = y.Course.Credit,
                                                   Time = y.Course.TotalTime,
                                                   Instructor = $"{y.Course.Instructor.FirstName} {y.Course.Instructor.LastName}"
                                               }).ToArray()
                                           })
                                           .ToArrayAsync();

            switch (reportType)
            {
                case ReportType.csv: CreateCsv(students, filePath); break;
                case ReportType.html: CreateHTML(students, filePath); break;
            }
        }

        private void CreateCsv(StudetDto[] students, string filePath)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Student, Total Credit");
            sb.AppendLine(", Course Name, Time, Credit, Instructor");

            foreach (var student in students)
            {
                sb.AppendLine($"{student.FullName}, {student.TotalCredit}");
                foreach (var course in student.Courses)
                {
                    sb.AppendLine($", {course.CourseName}, {course.Time}, {course.Credit}, {course.Instructor}");
                }
            }

            File.WriteAllText(filePath, sb.ToString());
        }

        private void CreateHTML(StudetDto[] studets, string filePath)
        {
            var sb = new StringBuilder();
            sb.Append("<style>table {border-collapse: collapse;}td{border: 1px solid black;padding: 5px;}</style>");
            sb.Append("<table>");

            sb.Append(FillRow(column1: "Student", column2: "Total Credit"));
            sb.Append(FillRow(column2: "Course Name", column3: "Time", column4: "Credit", column5: "Instructor"));

            foreach (var student in studets)
            {
                string studetRow = FillRow(column1: student.FullName, column2: student.TotalCredit.ToString());
                sb.Append(studetRow);
                foreach (var course in student.Courses)
                {
                    string courseRow = FillRow(column2: course.CourseName,
                                               column3: course.Time.ToString(),
                                               column4: course.Credit.ToString(),
                                               column5: course.Instructor);
                    sb.Append(courseRow);
                }
            }

            sb.Append("</table>");

            File.WriteAllText(filePath, sb.ToString());
        }

        private string FillRow(string column1 = "",
                               string column2 = "",
                               string column3 = "",
                               string column4 = "",
                               string column5 = "")
        {
            var sb = new StringBuilder();
            sb.Append("<tr>");
            sb.Append($"<td>{column1}</td>");
            sb.Append($"<td>{column2}</td>");
            sb.Append($"<td>{column3}</td>");
            sb.Append($"<td>{column4}</td>");
            sb.Append($"<td>{column5}</td>");
            sb.Append("</tr>");

            return sb.ToString();
        }
    }
}