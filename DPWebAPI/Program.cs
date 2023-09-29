using DPWebAPI.DBContexts;
using DPWebAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
if(builder.Environment.IsDevelopment() || builder.Environment.EnvironmentName == "Local")
{
    var RazorCompile = builder.Services.AddRazorPages();
    RazorCompile.AddRazorRuntimeCompilation();
}
//builder.Services.AddDbContext<ApplicationDBContext>(optionsAction: options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUserDetails, UserService>();
builder.Services.AddScoped<ILoginDetails, LoginDetails>();
builder.Services.AddDbContext<ApplicationDBContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || builder.Environment.EnvironmentName == "Local")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
