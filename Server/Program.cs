using GreenCoinHealth.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");

// Configuraci�n del secret key para JWT
var secretKey = builder.Configuration["Settings:Key"];
var keyBytes = Encoding.UTF8.GetBytes(secretKey);

// Agregar servicios al contenedor
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();

var conectionString = builder.Configuration.GetConnectionString("GreenCoinHealth");
builder.Services.AddDbContext<GreenCoinHealthContext>(
    options => options.UseSqlServer(conectionString)
);

// Configurar autenticaci�n JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Settings:Issuer"],
            ValidAudience = builder.Configuration["Settings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
        };
    });

// Configurar autenticaci�n con cookies para el inicio de sesi�n
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login"; // Ruta de inicio de sesi�n
    });

builder.Services.AddScoped<UserRepository>();

var app = builder.Build();

// Configuraci�n del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // A�adir esta l�nea antes de UseAuthorization
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
