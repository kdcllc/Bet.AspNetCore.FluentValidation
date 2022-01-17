using Bet.AspNetCore.FluentValidation;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

using System.Diagnostics;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.EnsureLocalDbCreated(args);

// add application dependecies
builder.Services.AddApplication();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var problemDetails = new ValidationProblemDetails(context.ModelState);
        throw new InputValidationException("Validation Failed", problemDetails.Errors);
    };
});

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

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
        var exception = errorFeature.Error;

        // https://tools.ietf.org/html/rfc7807#section-3.1
        var problemDetails = new ProblemDetails
        {
            Type = $"https://example.com/problem-types/{exception.GetType().Name}",
            Title = "An unexpected error occurred!",
            Detail = "Something went wrong",
            Instance = errorFeature switch
            {
                ExceptionHandlerFeature e => e.Path,
                _ => "unknown"
            },
            Status = StatusCodes.Status400BadRequest,
            Extensions =
                {
                    ["trace"] = Activity.Current?.Id ?? context?.TraceIdentifier
                }
        };

        switch (exception)
        {
            case InputValidationException validationException:
                problemDetails.Status = StatusCodes.Status403Forbidden;
                problemDetails.Title = "One or more validation errors occurred";
                problemDetails.Detail = "The request contains invalid parameters. More information can be found in the errors.";
                problemDetails.Extensions["errors"] = validationException.Errors;
                break;
        }

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = problemDetails.Status.Value;
        context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue()
        {
            NoCache = true,
        };
        await JsonSerializer.SerializeAsync(context.Response.Body, problemDetails);
    });
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
