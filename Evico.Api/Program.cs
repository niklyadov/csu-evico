using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Evico.Api;
using Evico.Api.Extensions;
using Evico.Api.QueryBuilders;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Evico.Api.Services.Auth.Vk;
using Evico.Api.UseCases.Auth;
using Evico.Api.UseCases.Event;
using Evico.Api.UseCases.Event.Category;
using Evico.Api.UseCases.Event.Review;
using Evico.Api.UseCases.Place;
using Evico.Api.UseCases.Place.Category;
using Evico.Api.UseCases.Place.Review;
using FluentResults;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

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
builder.Services.AddScoped<PlaceReviewQueryBuilder>();

builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<PlaceService>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<ExternalPhotoService>();
builder.Services.AddScoped<EventReviewService>();
builder.Services.AddScoped<PlaceReviewService>();

builder.Services.AddScoped<AuthViaVkUseCase>();
builder.Services.AddScoped<CreateNewTokensUseCase>();

builder.Services.AddScoped<AddEventUseCase>();
builder.Services.AddScoped<GetEventsUseCase>();
builder.Services.AddScoped<GetEventByIdUseCase>();
builder.Services.AddScoped<UpdateEventUseCase>();
builder.Services.AddScoped<DeleteEventByIdUseCase>();

builder.Services.AddScoped<AddEventReviewUseCase>();
builder.Services.AddScoped<GetEventReviewByIdUseCase>();
builder.Services.AddScoped<GetEventReviewsUseCase>();
builder.Services.AddScoped<UpdateEventReviewUseCase>();
builder.Services.AddScoped<DeleteEventReviewUseCase>();

builder.Services.AddScoped<AddPlaceUseCase>();
builder.Services.AddScoped<GetPlacesUseCase>();
builder.Services.AddScoped<GetPlaceByIdUseCase>();
builder.Services.AddScoped<UpdatePlaceUseCase>();
builder.Services.AddScoped<DeletePlaceUseCase>();

builder.Services.AddScoped<AddPlaceReviewUseCase>();
builder.Services.AddScoped<GetPlaceReviewByIdUseCase>();
builder.Services.AddScoped<GetPlaceReviewsUseCase>();
builder.Services.AddScoped<UpdatePlaceReviewUseCase>();
builder.Services.AddScoped<DeletePlaceReviewUseCase>();

builder.Services.AddScoped<AddEventCategoryUseCase>();
builder.Services.AddScoped<GetEventCategoryByIdUseCase>();
builder.Services.AddScoped<GetEventCategoriesUseCase>();
builder.Services.AddScoped<UpdateEventCategoryUseCase>();
builder.Services.AddScoped<DeleteEventCategoryUseCase>();

builder.Services.AddScoped<AddPlaceCategoryUseCase>();
builder.Services.AddScoped<GetPlaceCategoryByIdUseCase>();
builder.Services.AddScoped<GetPlaceCategoryByIdUseCase>();
builder.Services.AddScoped<GetPlaceCategoryByIdUseCase>();
builder.Services.AddScoped<GetPlaceCategoryByIdUseCase>();

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
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CSU-EVICO API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
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

builder.Services.UseCustomModelValidationErrorHandler();

#region Use Serilog

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

Log.Logger = logger;

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

Result.Setup(config => 
    config.Logger = new FluentResultsLogger(app.Logger));

app.UseCustomExceptionHandler();

app.UseCors(allowAnyCorsOrigin);

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();