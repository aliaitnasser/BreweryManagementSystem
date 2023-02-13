using Application.Core;
using Application.Repositories;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

using Persistence;

using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
	options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBrewerRepository, BrewerRepository>();
builder.Services.AddScoped<IWholesalerRepository, WholesalerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddDbContext<DataContext>(opt =>
{
	opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
	var context = services.GetRequiredService<DataContext>();
	await context.Database.MigrateAsync();
	await Seed.SeedData(context);
}
catch (Exception ex)
{
	var logger = services.GetRequiredService<ILogger<Program>>();
	logger.LogError(ex, "An error occurred during migration");
}
app.Run();
