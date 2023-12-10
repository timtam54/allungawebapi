using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using System.Configuration;
using AllungaWebAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));//added thams 7/10/2023

builder.Services.AddControllers();
builder.Services.AddDbContext<dbcontext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AllungDB")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

var app = builder.Build();

//app.MapGet("/", () => "Swagger Disabled - go to /API/Params!");
//app.Map("/something", (context) => context.Response.WriteAsync("Hello World!"));
//https://stackoverflow.com/questions/57846127/what-are-the-differences-between-app-userouting-and-app-useendpoints
//https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-7.0
//https://stackoverflow.com/questions/57846127/what-are-the-differences-between-app-userouting-and-app-useendpoints
//https://learn.microsoft.com/en-us/answers/questions/1350132/how-does-the-userouting-method-work-in-asp-net-cor
//builder.Services.AddSwaggerGen();
app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
 //  app.UseSwagger();
 //  app.UseSwaggerUI();
//}
///
app.UseHttpsRedirection();
app.UseAuthentication();//added thams 7/10/2023
app.UseAuthorization();

app.MapControllers();

app.Run();
