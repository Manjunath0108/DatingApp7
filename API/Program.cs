
using API.Data;
using API.Entities;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services .AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod()
.WithOrigins("https://localhost:4200"));

//used to run the controllers
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    {
        var context=services.GetRequiredService<DataContext>();
        await context.Database.MigrateAsync();
        await Seed.SeedUsers(context);
    }
}
catch (Exception ex)
{
    var logger=services.GetService<ILogger<Program>>();
    logger.LogError(ex,"An error occured while migration");
    
}

app.Run();




/*  
Create the web application builder using WebApplication.CreateBuilder(args).
Add services to the container, including controllers, application services, and identity services.
Use the ExceptionMiddleware middleware to handle exceptions.
Configure the HTTP request pipeline to allow cross-origin resource sharing (CORS) from https://localhost:4200.
Use authentication and authorization middlewares.
Map the controllers.
Create a scope and get the required services from the service provider.
Migrate the database using the DataContext and seed the users using the Seed class.
If an error occurs while migrating, log the error using the ILogger service.
Start the application using app.Run().
*/