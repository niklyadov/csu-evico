using Evico.Api;
using Evico.Api.QueryBuilder;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Evico.Api.UseCases.Event;
using Microsoft.EntityFrameworkCore;

const string allowAnyCorsOrigin = "Allow any";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("Default"),
        b => b.MigrationsAssembly("Evico.Api")));

builder.Services.Configure<JwtTokensServiceConfiguration>(builder.Configuration.GetSection("JwtTokensService"));
builder.Services.Configure<VkAuthServiceConfiguration>(builder.Configuration.GetSection("VkAuthService"));

builder.Services.AddScoped<EventQueryBuilder>();
builder.Services.AddScoped<PlaceQueryBuilder>();
builder.Services.AddScoped<ProfileQueryBuilder>();

builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<PlaceService>();
builder.Services.AddScoped<ProfileService>();

builder.Services.AddScoped<AddEventUseCase>();
builder.Services.AddScoped<AddEventUseCase>();
builder.Services.AddScoped<GetEventsUseCase>();
builder.Services.AddScoped<GetEventByIdUseCase>();
builder.Services.AddScoped<UpdateEventByIdUseCase>();
builder.Services.AddScoped<DeleteEventByIdUseCase>();

builder.Services.AddScoped<JwtTokensService>();
builder.Services.AddScoped<JwtAuthService>();
builder.Services.AddScoped<VkAuthService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(allowAnyCorsOrigin,
        policy =>
        {
            policy.WithOrigins("*");
            policy.WithHeaders("Origin", "X-Requested-With", "Content-Type", "Accept");
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();