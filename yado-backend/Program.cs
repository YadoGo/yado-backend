using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using yado_backend.Data;
using yado_backend.Repositories;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

// Add services to the container.

builder.Services.AddControllers(option =>
{
    // Cache profile
    option.CacheProfiles.Add("CacheProfile60sec", new CacheProfile()
    {
        Duration = 60
    });

    option.CacheProfiles.Add("CacheProfile120sec", new CacheProfile()
    {
        Duration = 120
    });

    option.CacheProfiles.Add("CacheProfile1day", new CacheProfile()
    {
        Duration = 86400
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        Description =
        "JWT authorization using the Bearer schema. \r\n\r\n" +
        "Enter the prefix 'Bearer' followed by a space and then your token\r\n\r\n" +
        "Example \"Bearer XXXXXXXXXXX\""
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

// Configure cache
builder.Services.AddResponseCaching();

// Configure Authorize
var key = Environment.GetEnvironmentVariable("SecretKey");

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


// Configure db
builder.Services.AddDbContext<AppDbContext>();

// Configure Mapper
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IPopulationRepository, PopulationRepository>();
builder.Services.AddScoped<ISiteRepository, SiteRepository>();
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();


// Configure CORS
var allowedOriginsLocalhost = Environment.GetEnvironmentVariable("Localhost");
var allowedOriginsDevelop = Environment.GetEnvironmentVariable("Develop");
var allowedOriginsMain = Environment.GetEnvironmentVariable("Main");

var allowedOrigins = new string[]
{
    allowedOriginsLocalhost,
    allowedOriginsDevelop,
    allowedOriginsMain
};


builder.Services.AddCors(P => P.AddPolicy("PolicyCors", build =>
{
    build.WithOrigins(allowedOrigins).AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("PolicyCors");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

