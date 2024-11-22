using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SendIt.Auth;
using SendIt.Data;
using SendIt.Dto;
using SendIt.Mapping;
using SendIt.Repo;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
   options =>
   {
       options.SwaggerDoc("v1", new OpenApiInfo { Title = "SendIt", Version = "v1" });
       options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
       {
           Name = "Authorization",
           In = ParameterLocation.Header,
           Type = SecuritySchemeType.ApiKey,
           Scheme = JwtBearerDefaults.AuthenticationScheme
       });

       options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference=new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    },
                    Scheme = "Oauth2",
                    Name = JwtBearerDefaults.AuthenticationScheme,
                    In = ParameterLocation.Header
                },

                new List<string>()
            }

        });
   });


builder.Services.AddDbContext<SendItDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("SendConnectionString")));


builder.Services.AddDbContext<SendAuthDbContext>(options =>
 options.UseNpgsql(builder.Configuration.GetConnectionString("SendAuthConnectionString")));





builder.Services.AddAutoMapper(typeof(Profiles));

builder.Services.AddScoped<IBookRepo,BookRepo>();
builder.Services.AddScoped<ITokenRepo, TokenRepo>();
builder.Services.AddTransient<IEmailRepo,EmailRepo>();




builder.Services.AddIdentityCore<User>()
  .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<User>>("SendIt")
    .AddEntityFrameworkStores<SendAuthDbContext>()
    .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
});




builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
