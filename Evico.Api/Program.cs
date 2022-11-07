using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Evico.Api;
using Evico.Api.QueryBuilder;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Evico.Api.Services.Auth.Vk;
using Evico.Api.UseCases.Auth;
using Evico.Api.UseCases.Event;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

const string allowAnyCorsOrigin = "Allow any";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("Default"),
        b => b.MigrationsAssembly("Evico.Api")));

builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<VkAuthServiceConfiguration>(builder.Configuration.GetSection("VkAuthService"));

builder.Services.AddScoped<EventQueryBuilder>();
builder.Services.AddScoped<PlaceQueryBuilder>();
builder.Services.AddScoped<ProfileQueryBuilder>();
builder.Services.AddScoped<EventReviewQueryBuilder>();
builder.Services.AddScoped<ExternalPhotoQueryBuilder>();

builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<PlaceService>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<ExternalPhotoService>();
builder.Services.AddScoped<EventReviewService>();

builder.Services.AddScoped<AuthViaVkUseCase>();
builder.Services.AddScoped<CreateNewTokensUseCase>();

builder.Services.AddScoped<AddEventUseCase>();
builder.Services.AddScoped<AddEventUseCase>();
builder.Services.AddScoped<GetEventsUseCase>();
builder.Services.AddScoped<GetEventByIdUseCase>();
builder.Services.AddScoped<UpdateEventUseCase>();
builder.Services.AddScoped<DeleteEventByIdUseCase>();

builder.Services.AddScoped<AddEventReviewUseCase>();
builder.Services.AddScoped<GetEventReviewByIdUseCase>();
builder.Services.AddScoped<GetEventReviewsUseCase>();
builder.Services.AddScoped<UpdateEventReviewUseCase>();
builder.Services.AddScoped<DeleteEventReviewByIdUseCase>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
    
    options.TokenValidationParameters.NameClaimType = JwtRegisteredClaimNames.Name;
});

builder.Services.AddScoped<JwtTokensService>();
builder.Services.AddScoped<JwtAuthService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<VkAuthService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "CSU-EVICO API", 
        Version = "v1" 
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        In = ParameterLocation.Header, 
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey 
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        { 
            new OpenApiSecurityScheme 
            { 
                Reference = new OpenApiReference 
                { 
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer" 
                } 
            },
            Array.Empty<string>()
        } 
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(allowAnyCorsOrigin,
        policy =>
        {
            policy.WithOrigins("*");
            policy.WithHeaders("Origin", "X-Requested-With", "Content-Type", "Accept", "Authorization");
        });
});
builder.Services.Configure<RouteOptions>(options =>
    options.LowercaseUrls = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowAnyCorsOrigin);

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();