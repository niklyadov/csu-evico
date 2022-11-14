using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json.Serialization;
using Evico.Api;
using Evico.Api.Extensions;
using Evico.Api.QueryBuilders;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Evico.Api.Services.Auth.Vk;
using Evico.Api.UseCases.Auth;
using Evico.Api.UseCases.Event;
using Evico.Api.UseCases.Event.Category;
using Evico.Api.UseCases.Event.Photo;
using Evico.Api.UseCases.Event.Review;
using Evico.Api.UseCases.Event.Review.Photo;
using Evico.Api.UseCases.Photo;
using Evico.Api.UseCases.Place;
using Evico.Api.UseCases.Place.Category;
using Evico.Api.UseCases.Place.Photo;
using Evico.Api.UseCases.Place.Review;
using Evico.Api.UseCases.Place.Review.Photo;
using FluentResults;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Minio.AspNetCore;
using Serilog;

const string allowAnyCorsOrigin = "Allow any";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("Default"),
        b => b.MigrationsAssembly("Evico.Api")));


builder.Services.AddScoped<EventQueryBuilder>();
builder.Services.AddScoped<PlaceQueryBuilder>();
builder.Services.AddScoped<ProfileQueryBuilder>();
builder.Services.AddScoped<EventReviewQueryBuilder>();
builder.Services.AddScoped<ExternalPhotoQueryBuilder>();
builder.Services.AddScoped<PlaceReviewQueryBuilder>();
builder.Services.AddScoped<EventCategoryQueryBuilder>();
builder.Services.AddScoped<PlaceCategoryQueryBuilder>();
builder.Services.AddScoped<PhotoQueryBuilder>();
builder.Services.AddScoped<ProfilePhotoQueryBuilder>();
builder.Services.AddScoped<EventPhotoQueryBuilder>();
builder.Services.AddScoped<PlacePhotoQueryBuilder>();
builder.Services.AddScoped<EventReviewPhotoQueryBuilder>();
builder.Services.AddScoped<PlaceReviewPhotoQueryBuilder>();

builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<PlaceService>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<EventReviewService>();
builder.Services.AddScoped<PlaceReviewService>();
builder.Services.AddScoped<EventCategoryService>();
builder.Services.AddScoped<PlaceCategoryService>();
builder.Services.AddScoped<PhotoService>();
builder.Services.AddScoped<ProfilePhotoService>();
builder.Services.AddScoped<EventPhotoService>();
builder.Services.AddScoped<PlacePhotoService>();
builder.Services.AddScoped<EventReviewPhotoService>();
builder.Services.AddScoped<PlaceReviewPhotoService>();
builder.Services.AddScoped<FileService>();

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

builder.Services.AddScoped<GetPhotoByIdUseCase>();

builder.Services.AddScoped<AddPlacePhotoUseCase>();
builder.Services.AddScoped<DeletePlacePhotoUseCase>();

builder.Services.AddScoped<AddPlaceReviewPhotoUseCase>();
builder.Services.AddScoped<DeletePlaceReviewPhotoUseCase>();

builder.Services.AddScoped<AddEventPhotoUseCase>();
builder.Services.AddScoped<DeleteEventPhotoUseCase>();

builder.Services.AddScoped<AddEventReviewPhotoUseCase>();
builder.Services.AddScoped<DeleteEventReviewPhotoUseCase>();

builder.Services.AddScoped<AddEventCategoryUseCase>();
builder.Services.AddScoped<GetEventCategoryByIdUseCase>();
builder.Services.AddScoped<GetEventCategoriesUseCase>();
builder.Services.AddScoped<UpdateEventCategoryUseCase>();
builder.Services.AddScoped<DeleteEventCategoryUseCase>();

builder.Services.AddScoped<AddPlaceCategoryUseCase>();
builder.Services.AddScoped<GetPlaceCategoryByIdUseCase>();
builder.Services.AddScoped<GetPlaceCategoriesUseCase>();
builder.Services.AddScoped<UpdatePlaceCategoryUseCase>();
builder.Services.AddScoped<DeletePlaceCategoryUseCase>();

#region Auth

builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<VkAuthServiceConfiguration>(builder.Configuration.GetSection("VkAuthService"));
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

#endregion

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
            policy.WithMethods("GET", "POST", "PUT", "DELETE");
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

#region Minio

builder.Services.Configure<MinioBucketsConfiguration>(builder.Configuration.GetSection("Minio"));
builder.Services.AddMinio(config =>
{
    config.Endpoint = builder.Configuration["Minio:Endpoint"];

    config.ConfigureClient(clientConfig =>
    {
        clientConfig.WithCredentials(
            builder.Configuration["Minio:AccessKey"],
            builder.Configuration["Minio:SecretKey"]);
    });
});

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