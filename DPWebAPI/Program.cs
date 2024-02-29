using DPWebAPI.DBContexts;
using DPWebAPI.Models;
using DPWebAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//if(builder.Environment.IsDevelopment() || builder.Environment.EnvironmentName == "Local")// comment this line when published to web api on live server
//{// comment this line when published to web api on live server
    var RazorCompile = builder.Services.AddRazorPages();
    RazorCompile.AddRazorRuntimeCompilation();
//}// comment this line when published to web api on live server
builder.Services.AddDbContext<ApplicationDBContext>(optionsAction: options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUserDetails, UserService>();
builder.Services.AddScoped<ILoginDetails, LoginDetails>();
builder.Services.AddScoped<IReportDetails, ReportDetails>();
builder.Services.AddScoped<IReportDetails, ReportDetails>();
builder.Services.AddScoped<ICommonModule, CommonModel>();
builder.Services.AddDbContext<ApplicationDBContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(options => options.RespectBrowserAcceptHeader = true).AddXmlSerializerFormatters().AddXmlDataContractSerializerFormatters();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment() || builder.Environment.EnvironmentName == "Local")// comment this line when published to web api on live server
//{// comment this line when published to web api on live server
    app.UseSwagger();
    app.UseSwaggerUI();
//}// comment this line when published to web api on live server

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
