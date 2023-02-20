using Persistence;
using Smart.Configuration;
using Smart.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure()
    .AddApplication()
    .AddDatabase(builder.Configuration)
    .AddPresentation();

builder.Services.AddTransient<GlobalExceptionHandlingException>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.UseMiddleware<GlobalExceptionHandlingException>();
app.MapControllers();

app.MigrateDatabase();
app.UseSeedData();

app.Run();
