using CapstoneTelevision.Data;
using CapstoneTelevision.Repositories;
using CapstoneTelevision.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var jwtval = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtval["Key"]);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(i => i.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Authentication
builder.Services.AddAuthentication(i =>
{
    i.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    i.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(i =>
{
    i.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtval["Issuer"],
        ValidAudience = jwtval["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});
//Authoristion
builder.Services.AddAuthorization(i =>
{
    i.AddPolicy("AdminOnly", j => j.RequireRole("Admin"));
    i.AddPolicy("EditorOnly", policy =>
       policy.RequireRole("Editor"));
    i.AddPolicy("ChanelManagerOnly", policy =>
      policy.RequireRole("Channel Manager"));
    i.AddPolicy("ContentProducerOnly", policy =>
      policy.RequireRole("Content Producer"));
    i.AddPolicy("DirectorOnly", policy =>
      policy.RequireRole("Director"));
    i.AddPolicy("AdvertiserOnly", policy =>
        policy.RequireRole("Advertiser"));
    i.AddPolicy("ReporterOnly", policy =>
        policy.RequireRole("Repoter"));
   
});
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var a = builder.Configuration.GetSection("AllowedHosts").Get<string[]>();
builder.Services.AddCors(ops =>
{
    ops.AddPolicy("localcors", p =>
    {
        p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("localcors");
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
