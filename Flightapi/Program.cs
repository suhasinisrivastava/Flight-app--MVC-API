using flightapi.Models;
using flightapi.Repository;
using flightapi.Service;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Ace52024Context>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add services to the container.
builder.Services.AddScoped<IFlight<Suhasiniflight>,FlightRepo>();
builder.Services.AddScoped<IFlightServ<Suhasiniflight>,FlightServ>();
builder.Services.AddScoped<IBooking<Suhasinibooking>,BookingRepo>();
builder.Services.AddScoped<IBookingServ<Suhasinibooking>,BookingServ>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
