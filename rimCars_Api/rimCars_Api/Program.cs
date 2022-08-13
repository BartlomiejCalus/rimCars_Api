using Microsoft.AspNetCore.Identity;
using rimCars_Api.Entities;
using rimCars_Api.Services;
using rimCars_Api.Authorization;
using rimCars_Api;
using FluentValidation;
using rimCars_Api.Models;
using rimCars_Api.Models.Validation;
using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var AuthenticationSettings = new AuthenticationSettings();

builder.Configuration.GetSection("Authentication").Bind(AuthenticationSettings);

builder.Services.AddSingleton(AuthenticationSettings);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(conf =>
{
    conf.RequireHttpsMetadata = false;
    conf.SaveToken = true;
    conf.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = AuthenticationSettings.JwtIssuer,
        ValidAudience = AuthenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthenticationSettings.JwtKey))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CompanyOwner", builder => builder.AddRequirements(new CompanyOwnerRequirement()));
});

builder.Services.AddScoped<IAuthorizationHandler, CompanyOwnerRequirementHandler>();
builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddDbContext<SalonsDbContext>();
builder.Services.AddScoped<DataSeeder>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ISalonService, SalonService>();
builder.Services.AddScoped<IRimService, RimService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidation>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
SeedData(app);

app.UseAuthentication();
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.UseAuthorization();
app.Run();


void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<DataSeeder>();
        service.seed();
    }
}