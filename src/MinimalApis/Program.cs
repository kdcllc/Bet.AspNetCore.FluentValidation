using Bet.AspNetCore.FluentValidation;
using FluentValidation;
using static Microsoft.AspNetCore.Http.Results;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/", () => "Minimal Api FluentValidation Example");


app.MapPost("/person", (Validated<Person> req) =>
{
    // deconstruct to bool & Person
    var (isValid, value) = req;

    return isValid
        ? Ok(value)
        : ValidationProblem(req.Errors);
})
.WithName("PostPerson");


app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public record Person(string? Name, int? Age);

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(m => m.Name).NotEmpty();
        RuleFor(m => m.Age).NotEmpty().GreaterThan(0);
    }
}