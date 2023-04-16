using Microsoft.EntityFrameworkCore;
using Studets_Database;
using Studets_Repository.Interfaces;
using Studets_Repository;
using Studets_Services.Interfaces;
using Studets_Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CourseraContext>(opt =>
        opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddScoped<IReportServices, ReportServices>();
builder.Services.AddScoped<IRepository, Repository_MSSQL>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
