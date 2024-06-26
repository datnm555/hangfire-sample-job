using Hangfire;
using Microsoft.EntityFrameworkCore;
using ScgcJob.Context;
using ScgcJob.Extensions;
using ScgcJob.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// builder.Services.AddScoped<IJobService, JobService>();

// Add services to the container.
builder.Services.ConfigureErp(builder.Configuration);

builder.Services.ConfigureHangfire(builder.Configuration);

builder.Services.AddDbContext<DefaultDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHangfireDashboard("/dashboard");

app.MapControllers();

app.Run();