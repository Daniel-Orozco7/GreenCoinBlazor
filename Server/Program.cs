using GreenCoinHealth.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.ResponseCompression;


using System.Text;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");
var secretKey = builder.Configuration.GetSection("Settings").GetSection("Key").ToString();
var keyBytes = Encoding.UTF8.GetBytes(secretKey);



// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();


var conectionString = builder.Configuration.GetConnectionString("GreenCoinHealth");

builder.Services.AddDbContext<GreenCoinHealthContext>(
    options => options.UseSqlServer(conectionString)
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
