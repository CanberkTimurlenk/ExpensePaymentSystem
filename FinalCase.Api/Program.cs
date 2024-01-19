
using FinalCase.Api.Extensions;
using FinalCase.BackgroundJobs.QueueOperations;
using FinalCase.BackgroundJobs.QueueService;
using FinalCase.Business.Assembly;
using FinalCase.Data.Constants.Storage;
using FinalCase.Data.Contexts;
using FinalCase.Services.NotificationService;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FinalCaseDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString(DbKeys.SqlServer)));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));

builder.Services.AddJwtConfiguration(builder.Configuration); // extension method
builder.Services.AddSwagger(); // extension method

builder.Services.AddSingleton<INotificationService, QueueNotificationService>();
builder.Services.AddSingleton<IQueueService, QueueService>();

builder.Services.AddAutoMapper(typeof(AssemblyReference).Assembly);
builder.Services.AddHangfire(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseHangfireDashboard();

app.EnableReportingJobs();

app.MapControllers();

app.Run();
