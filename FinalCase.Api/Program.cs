using FinalCase.Api.Extensions;
using FinalCase.Business.Assembly;
using FluentValidation.AspNetCore;
using Hangfire;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Extensions
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddSwagger();
builder.Services.AddMediatR();
builder.Services.AddAutoMapper(typeof(AssemblyReference).Assembly);
builder.Services.AddHangfire(builder.Configuration);
builder.Services.AddFluentValidation();
builder.Services.RegisterServices();

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
