using NUIX.InvestmentPerformance.API.Services;

var builder = WebApplication.CreateBuilder(args);
// TODO - explain what is happening in the program.cs
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register investment service for dependency injection
builder.Services.AddScoped<IInvestmentService, InvestmentService>();

var app = builder.Build();

// Enable middleware to serve Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
